using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
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
}