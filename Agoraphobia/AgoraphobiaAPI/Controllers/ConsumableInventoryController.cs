using AgoraphobiaAPI.Dtos.ConsumableInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using Microsoft.AspNetCore.Mvc;

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
        if (consumableInventories.Exists(x => x.ConsumableId == consumable.Id))
        {
            var updated = await _consumableInventoryRepository.AddOneAsync(consumableInventoryRequest);
            if (updated is null)
                return BadRequest("Something unexpected happened");
            return Ok(updated.ToConsumableInventoryDto());
        }
        var consumableInventory = new ConsumableInventory()
        {
            ConsumableId = consumable.Id,
            PlayerId = player.Id,
            Player = player,
            Consumable = consumable,
            Quantity = 1
        };
        await _consumableInventoryRepository.CreateAsync(consumableInventory);
        return Created("agoraphobia/consumableInventories", consumableInventory.ToUpdateConsumableInventoryRequestDto());
    }

}