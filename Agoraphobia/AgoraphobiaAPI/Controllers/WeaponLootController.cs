using AgoraphobiaAPI.Dtos.WeaponLoot;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[ApiController]
[Route("agoraphobia/weaponLoots")]
public class WeaponLootController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly IWeaponRepository _weaponRepository;
    private readonly IWeaponLootRepository _weaponLootRepository;

    public WeaponLootController(
        IRoomRepository roomRepository, 
        IWeaponRepository weaponRepository, 
        IWeaponLootRepository weaponLootRepository
        )
    {
        _roomRepository = roomRepository;
        _weaponRepository = weaponRepository;
        _weaponLootRepository = weaponLootRepository;
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetWeaponLoot([FromRoute] int roomId)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room is null)
            return NotFound();
        var weaponLoots = await _weaponLootRepository.GetWeaponLootsAsync(roomId);
        return Ok(weaponLoots.Select(x => x.ToWeaponLootDto()));
    }

    [HttpPost]
    public async Task<IActionResult> AddToWeaponLoot(WeaponLootRequestDto weaponLootRequestDto)
    {
        var room = await _roomRepository.GetByIdAsync(weaponLootRequestDto.RoomId);
        var weapon = await _weaponRepository.GetByIdAsync(weaponLootRequestDto.WeaponId);
        if (room is null)
            return BadRequest("Room not found");
        if (weapon is null)
            return BadRequest("Weapon not found");
        
        var weaponLoots = await _weaponLootRepository.GetWeaponLootsAsync(room.Id);
        if (weaponLoots.Exists(x => x.WeaponId == weapon.Id))
        {
            var updated = await _weaponLootRepository.AddOneAsync(weaponLootRequestDto);
            if (updated is null)
                return BadRequest("Something unexpected happened");
            return Ok(updated.ToUpdateWeaponLootRequestDto());
        }
        var weaponLoot = new WeaponLoot()
        {
            RoomId = room.Id,
            WeaponId = weapon.Id,
            Quantity = 1,
            Room = room,
            Weapon = weapon
        };
        await _weaponLootRepository.CreateAsync(weaponLoot);
        return Created("agoraphobia/weaponLoots", weaponLoot.ToUpdateWeaponLootRequestDto());
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveFromWeaponLoot([FromBody] WeaponLootRequestDto weaponLootRequestDto)
    {
        var room = await _roomRepository.GetByIdAsync(weaponLootRequestDto.RoomId);
        var weapon = await _weaponRepository.GetByIdAsync(weaponLootRequestDto.WeaponId);
        if (room is null)
            return BadRequest("Room not found");
        if (weapon is null)
            return BadRequest("Weapon not found");

        var weaponLoots = await _weaponLootRepository.GetWeaponLootsAsync(room.Id);
        var weaponLoot = weaponLoots.FirstOrDefault(x => x.WeaponId == weapon.Id);
        if (weaponLoot is null)
            return NotFound();
        
        if (weaponLoot.Quantity > 1)
        {
            var updated = await _weaponLootRepository.RemoveOneAsync(weaponLootRequestDto);
            if (updated is null)
                return BadRequest("Something unexpected happened"); 
            return Ok(updated.ToUpdateWeaponLootRequestDto());
        }

        await _weaponLootRepository.DeleteAsync(weaponLoot);
        return NoContent();
    }
}