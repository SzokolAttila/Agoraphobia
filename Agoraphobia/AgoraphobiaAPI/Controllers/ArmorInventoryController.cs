using AgoraphobiaAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        return Ok(await _armorInventoryRepository.GetArmorsAsync(playerId));
    }
}