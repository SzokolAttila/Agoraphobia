using AgoraphobiaAPI.Dtos.WeaponInventory;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
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
        if (weaponInventories.Exists(x => x.WeaponId == weapon.Id))
        {
            var updated = await _weaponInventoryRepository.AddOneAsync(weaponInventoryRequestDto);
            if (updated is null)
                return BadRequest("Something unexpected happened");
            return Ok(updated.ToUpdateWeaponInventoryRequestDto());
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
        return Created("agoraphobia/weaponInventories", weaponInventory.ToUpdateWeaponInventoryRequestDto());
    }
}