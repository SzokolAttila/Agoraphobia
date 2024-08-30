using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[ApiController]
[Route("agoraphobia/weaponInventories")]
public class WeaponInventoryController : ControllerBase
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IWeaponRepository _weaponRepository;
    private readonly IWeaponInventoryRepository _weaponInventoryRepository;

    public WeaponInventoryController(
        IPlayerRepository playerRepository, 
        IWeaponRepository weaponRepository, 
        IWeaponInventoryRepository weaponInventoryRepository
        )
    {
        _playerRepository = playerRepository;
        _weaponRepository = weaponRepository;
        _weaponInventoryRepository = weaponInventoryRepository;
    }

    [HttpGet("{playerId}")]
    public async Task<IActionResult> GetWeaponInventory([FromRoute] int playerId)
    {
        var player = await _playerRepository.GetByIdAsync(playerId);
        if (player is null)
            return NotFound();
        var weaponInventories = await _weaponInventoryRepository.GetWeaponInventoriesAsync(playerId);
        return Ok(weaponInventories.Select(x => x.ToWeaponInventoryDto()));
    }

    [HttpPost]
    public async Task<IActionResult> AddToWeaponInventory(WeaponInventoryRequestDto weaponInventoryRequestDto)
    {
        var player = await _playerRepository.GetByIdAsync(weaponInventoryRequestDto.PlayerId);
        var weapon = await _weaponRepository.GetByIdAsync(weaponInventoryRequestDto.WeaponId);
        if (player is null)
            return BadRequest("Player not found");
        if (weapon is null)
            return BadRequest("Weapon not found");
        
        var weaponInventories = await _weaponInventoryRepository.GetWeaponInventoriesAsync(player.Id);
        var createdInventory = weaponInventories.Find(x => x.WeaponId == weapon.Id);
        if (createdInventory != null)
        {
            var updated = await _weaponInventoryRepository.AddOneAsync(createdInventory.Id);
            if (updated is null)
                return BadRequest("Something unexpected happened");
            return Ok(updated.ToWeaponInventoryDto());
        }
        var weaponInventory = new WeaponInventory()
        {
            PlayerId = player.Id,
            WeaponId = weapon.Id,
            Quantity = 1,
            Player = player,
            Weapon = weapon
        };
        await _weaponInventoryRepository.CreateAsync(weaponInventory);
        return Created("agoraphobia/weaponInventories", weaponInventory.ToWeaponInventoryDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromWeaponInventory([FromRoute] int id)
    {
        var weaponInventory = await _weaponInventoryRepository.GetByIdAsync(id);
        if (weaponInventory is null)
            return NotFound();
        
        if (weaponInventory.Quantity > 1)
        {
            var updated = await _weaponInventoryRepository.RemoveOneAsync(id);
            if (updated is null)
                return BadRequest("Something unexpected happened"); 
            return Ok(updated.ToWeaponInventoryDto());
        }

        await _weaponInventoryRepository.DeleteAsync(id);
        return NoContent();
    }
}