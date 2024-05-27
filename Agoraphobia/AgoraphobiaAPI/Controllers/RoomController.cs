using AgoraphobiaAPI.Dtos.Room;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[ApiController]
[Route("agoraphobia/rooms")]
public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;

    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return Ok(rooms.Select(x => x.ToRoomDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room is null)
            return NotFound();

        return Ok(room.ToRoomDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoomRequestDto roomDto)
    {
        var room = roomDto.ToRoomFromCreateDto();
        var created = await _roomRepository.CreateAsync(room);
        if (created is null)
            return BadRequest("Room not found");
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room.ToRoomDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateRoomRequestDto roomDto)
    {
        var room = await _roomRepository.UpdateAsync(id, roomDto);
        if (room is null)
            return NotFound();
        return Ok(room.ToRoomDto());
    }

[HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var room = await _roomRepository.DeleteAsync(id);
        if (room is null)
            return NotFound();
        return NoContent();
    }
}