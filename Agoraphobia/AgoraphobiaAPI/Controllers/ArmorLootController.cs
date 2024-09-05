using AgoraphobiaAPI.Dtos.ArmorLoot;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Armors;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[ApiController]
[Route("agoraphobia/armorLoots")]
public class ArmorLootController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly IArmorRepository _armorRepository;
    private readonly IArmorLootRepository _armorLootRepository;

    public ArmorLootController(
        IRoomRepository roomRepository, 
        IArmorRepository armorRepository, 
        IArmorLootRepository armorLootRepository
        )
    {
        _roomRepository = roomRepository;
        _armorRepository = armorRepository;
        _armorLootRepository = armorLootRepository;
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetArmorLoot([FromRoute] int roomId)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room is null)
            return NotFound();
        var armorLoots = await _armorLootRepository.GetArmorLootsAsync(roomId);
        return Ok(armorLoots.Select(x => x.ToArmorLootDto()));
    }

    [HttpPost]
    public async Task<IActionResult> AddToArmorLoot(ArmorLootRequestDto armorLootRequestDto)
    {
        var room = await _roomRepository.GetByIdAsync(armorLootRequestDto.RoomId);
        var armor = await _armorRepository.GetByIdAsync(armorLootRequestDto.ArmorId);
        if (room is null)
            return BadRequest("Room not found");
        if (armor is null)
            return BadRequest("Armor not found");
        
        var armorLoots = await _armorLootRepository.GetArmorLootsAsync(room.Id);
        var createdLoot = armorLoots.Find(x => x.ArmorId == armor.Id);
        if (createdLoot != null)
        {
            var updated = await _armorLootRepository.AddOneAsync(createdLoot.Id);
            if (updated is null)
                return BadRequest("Something unexpected happened");
            return Ok(updated.ToArmorLootDto());
        }
        var armorLoot = new ArmorLoot()
        {
            RoomId = room.Id,
            ArmorId = armor.Id,
            Quantity = 1,
            Room = room,
        };
        await _armorLootRepository.CreateAsync(armorLoot);
        return Created("agoraphobia/armorLoots", armorLoot.ToArmorLootDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromArmorLoot([FromRoute] int id)
    {
        var armorLoot = await _armorLootRepository.GetByIdAsync(id);
        if (armorLoot is null)
            return NotFound();
        
        if (armorLoot.Quantity > 1)
        {
            var updated = await _armorLootRepository.RemoveOneAsync(id);
            if (updated is null)
                return BadRequest("Something unexpected happened"); 
            return Ok(updated.ToArmorLootDto());
        }

        await _armorLootRepository.DeleteAsync(id);
        return NoContent();
    }
}