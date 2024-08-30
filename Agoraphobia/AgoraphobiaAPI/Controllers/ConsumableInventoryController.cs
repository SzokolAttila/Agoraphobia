using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Microsoft.AspNetCore.Mvc;
using ConsumableInventory = AgoraphobiaLibrary.JoinTables.Consumables.ConsumableInventory;

namespace AgoraphobiaAPI.Controllers;

[ApiController]
[Route("agoraphobia/consumableInventories")]
public class ConsumableInventoryController : ControllerBase
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IConsumableRepository _consumableRepository;
    private readonly IConsumableInventoryRepository _consumableInventoryRepository;

    public ConsumableInventoryController(
        IPlayerRepository playerRepository, 
        IConsumableRepository consumableRepository, 
        IConsumableInventoryRepository consumableInventoryRepository
        )
    {
        _playerRepository = playerRepository;
        _consumableRepository = consumableRepository;
        _consumableInventoryRepository = consumableInventoryRepository;
    }
    
    [HttpGet("{playerId}")]
    public async Task<IActionResult> GetConsumableInventory([FromRoute] int playerId)
    {
        var player = await _playerRepository.GetByIdAsync(playerId);
        if (player is null)
            return NotFound();
        var consumableInventories = await _consumableInventoryRepository.GetConsumableInventoriesAsync(playerId);
        return Ok(consumableInventories.Select(x => x.ToConsumableInventoryDto()));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToConsumableInventory(ConsumableInventoryRequestDto consumableInventoryRequest)
    {
        var player = await _playerRepository.GetByIdAsync(consumableInventoryRequest.PlayerId);
        var consumable = await _consumableRepository.GetByIdAsync(consumableInventoryRequest.ConsumableId);
        if (player is null)
            return BadRequest("Player not found");
        if (consumable is null)
            return BadRequest("Consumable not found");
        
        var consumableInventories = await _consumableInventoryRepository.GetConsumableInventoriesAsync(player.Id);
        var createdInventory = consumableInventories.Find(x => x.ConsumableId == consumable.Id);
        if (createdInventory != null)
        {
            var updated = await _consumableInventoryRepository.AddOneAsync(createdInventory.Id);
            if (updated is null)
                return BadRequest("Something unexpected happened");
            return Ok(updated.ToConsumableInventoryDto());
        }
        var consumableInventory = new ConsumableInventory()
        {
            ConsumableId = consumable.Id,
            PlayerId = player.Id,
            Consumable = consumable,
            Quantity = 1
        };
        await _consumableInventoryRepository.CreateAsync(consumableInventory);
        return Created("agoraphobia/consumableInventories", consumableInventory.ToConsumableInventoryDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromConsumableInventory([FromRoute] int id)
    {
        var consumableInventory = await _consumableInventoryRepository.GetByIdAsync(id);
        if (consumableInventory is null)
            return NotFound();
        
        if (consumableInventory.Quantity > 1)
        {
            var updated = await _consumableInventoryRepository.RemoveOneAsync(id);
            if (updated is null)
                return BadRequest("Something unexpected happened"); 
            return Ok(updated.ToConsumableInventoryDto());
        }

        await _consumableInventoryRepository.DeleteAsync(id);
        return NoContent();
    }
}