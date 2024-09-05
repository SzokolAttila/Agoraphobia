using AgoraphobiaAPI.Dtos.ConsumableLoot;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Consumables;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[ApiController]
[Route("agoraphobia/consumableLoots")]
public class ConsumableLootController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly IConsumableRepository _consumableRepository;
    private readonly IConsumableLootRepository _consumableLootRepository;

    public ConsumableLootController(
        IRoomRepository roomRepository, 
        IConsumableRepository consumableRepository, 
        IConsumableLootRepository consumableLootRepository
        )
    {
        _roomRepository = roomRepository;
        _consumableRepository = consumableRepository;
        _consumableLootRepository = consumableLootRepository;
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetConsumableLoot([FromRoute] int roomId)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room is null)
            return NotFound();
        var consumableLoots = await _consumableLootRepository.GetConsumableLootsAsync(roomId);
        return Ok(consumableLoots.Select(x => x.ToConsumableLootDto()));
    }

    [HttpPost]
    public async Task<IActionResult> AddToConsumableLoot(ConsumableLootRequestDto consumableLootRequestDto)
    {
        var room = await _roomRepository.GetByIdAsync(consumableLootRequestDto.RoomId);
        var consumable = await _consumableRepository.GetByIdAsync(consumableLootRequestDto.ConsumableId);
        if (room is null)
            return BadRequest("Room not found");
        if (consumable is null)
            return BadRequest("Consumable not found");
        
        var consumableLoots = await _consumableLootRepository.GetConsumableLootsAsync(room.Id);
        var createdLoot = consumableLoots.Find(x => x.ConsumableId == consumable.Id);
        if (createdLoot != null)
        {
            var updated = await _consumableLootRepository.AddOneAsync(createdLoot.Id);
            if (updated is null)
                return BadRequest("Something unexpected happened");
            return Ok(updated.ToConsumableLootDto());
        }
        var consumableLoot = new ConsumableLoot()
        {
            RoomId = room.Id,
            ConsumableId = consumable.Id,
            Quantity = 1,
            Consumable = consumable
        };
        await _consumableLootRepository.CreateAsync(consumableLoot);
        return Created("agoraphobia/consumableLoots", consumableLoot.ToConsumableLootDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromConsumableLoot([FromRoute] int id)
    {
        var consumableLoot = await _consumableLootRepository.GetByIdAsync(id);
        if (consumableLoot is null)
            return NotFound();
        
        if (consumableLoot.Quantity > 1)
        {
            var updated = await _consumableLootRepository.RemoveOneAsync(id);
            if (updated is null)
                return BadRequest("Something unexpected happened"); 
            return Ok(updated.ToConsumableLootDto());
        }

        await _consumableLootRepository.DeleteAsync(id);
        return NoContent();
    }
}