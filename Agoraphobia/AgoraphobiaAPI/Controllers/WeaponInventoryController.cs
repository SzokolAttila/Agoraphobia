using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
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
}