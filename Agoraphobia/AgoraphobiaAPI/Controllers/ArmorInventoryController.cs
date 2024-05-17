using AgoraphobiaAPI.Dtos.ArmorInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[Route("agoraphobia/armorInventories")]
[ApiController]
public class ArmorInventoryController : ControllerBase
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IArmorRepository _armorRepository;
    private readonly IArmorInventoryRepository _armorInventoryRepository;
    public ArmorInventoryController(
        IPlayerRepository playerRepository,
        IArmorRepository armorRepository,
        IArmorInventoryRepository armorInventoryRepository
        )
    {
        _playerRepository = playerRepository;
        _armorRepository = armorRepository;
        _armorInventoryRepository = armorInventoryRepository;
    }

    [HttpGet("{playerId}")]
    public async Task<IActionResult> GetArmorInventory([FromRoute] int playerId)
    {
        var player = await _playerRepository.GetByIdAsync(playerId);
        if (player is null)
            return NotFound();
        var armorInventories = await _armorInventoryRepository.GetArmorInventoriesAsync(playerId);
        return Ok(armorInventories.Select(x => x.ToArmorInventoryDto()));
    }

    [HttpPost]
    public async Task<IActionResult> AddToArmorInventory([FromBody] ArmorInventoryRequestDto armorInventoryRequestDto)
    {
        var player = await _playerRepository.GetByIdAsync(armorInventoryRequestDto.PlayerId);
        var armor = await _armorRepository.GetByIdAsync(armorInventoryRequestDto.ArmorId);
        if (player is null)
            return BadRequest("Player not found");
        if (armor is null)
            return BadRequest("Armor not found");
        
        var armorInventories = await _armorInventoryRepository.GetArmorInventoriesAsync(armorInventoryRequestDto.PlayerId);
        if (armorInventories.Exists(x => x.ArmorId == armorInventoryRequestDto.ArmorId))
        {
            var updated = await _armorInventoryRepository.AddOneAsync(armorInventoryRequestDto);
            if (updated is null)
                return BadRequest("Something unexpected happened");
            return Ok(updated.ToUpdateArmorInventoryRequestDto());
        }
        var armorInventory = new ArmorInventory
        {
            PlayerId = armorInventoryRequestDto.PlayerId,
            ArmorId = armorInventoryRequestDto.ArmorId,
            Quantity = 1,
            Player = player,
            Armor = armor
        };
        await _armorInventoryRepository.CreateAsync(armorInventory);
        return Created("agoraphobia/armorInventories", armorInventory.ToUpdateArmorInventoryRequestDto());
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFromArmorInventory([FromBody] ArmorInventoryRequestDto armorInventoryRequestDto)
    {
        var player = await _playerRepository.GetByIdAsync(armorInventoryRequestDto.PlayerId);
        var armor = await _armorRepository.GetByIdAsync(armorInventoryRequestDto.ArmorId);
        if (player is null)
            return BadRequest("Player not found");
        if (armor is null)
            return BadRequest("Armor not found");

        var armorInventories = await _armorInventoryRepository.GetArmorInventoriesAsync(player.Id);
        var armorInventory = armorInventories.FirstOrDefault(x => x.ArmorId == armor.Id);
        if (armorInventory is null)
            return NotFound();
        
        if (armorInventory.Quantity > 1)
        {
            var updated = await _armorInventoryRepository.RemoveOneAsync(armorInventoryRequestDto);
            if (updated is null)
                return BadRequest("Something unexpected happened"); 
            return Ok(updated.ToUpdateArmorInventoryRequestDto());
        }

        await _armorInventoryRepository.DeleteAsync(armorInventory);
        return NoContent();
    }
}