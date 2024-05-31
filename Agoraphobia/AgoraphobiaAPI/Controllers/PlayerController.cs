using AgoraphobiaAPI.Dtos.Player;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[ApiController]
[Route("agoraphobia/players")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRoomRepository _roomRepository;
    public PlayerController(
        IPlayerRepository playerRepository, 
        IAccountRepository accountRepository, 
        IRoomRepository roomRepository
        )
    {
        _playerRepository = playerRepository;
        _accountRepository = accountRepository;
        _roomRepository = roomRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var players = await _playerRepository.GetAllAsync();
        return Ok(players.Select(x => x.ToPlayerDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var player = await _playerRepository.GetByIdAsync(id);
        if (player is null)
            return NotFound();

        return Ok(player.ToPlayerDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerRequestDto playerDto)
    {
        var player = playerDto.ToAccountFromCreateDto();
        var account = await _accountRepository.GetByIdAsync(player.AccountId);
        if (account is null)
            return BadRequest("Account not found");
        var room = await _roomRepository.GetByIdAsync(player.RoomId);
        if (room is null)
            return BadRequest("Room not found");
        if (account.Players.Exists(x => x.SlotId == playerDto.SlotId))
        {
            await _playerRepository.DeleteAsync(
                account.Players.Find(x => x.SlotId == playerDto.SlotId)!.Id);
        }
        await _playerRepository.CreateAsync(player);
        return CreatedAtAction(nameof(GetById), new { id = player.Id }, player.ToPlayerDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePlayerRequestDto playerDto)
    {
        var room = await _roomRepository.GetByIdAsync(playerDto.RoomId);
        if (room is null)
            return BadRequest("Room not found");
        var player = await _playerRepository.UpdateAsync(id, playerDto);
        if (player is null)
            return NotFound();
        return Ok(player.ToPlayerDto());
    }

[HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var player = await _playerRepository.DeleteAsync(id);
        if (player is null)
            return NotFound();
        return NoContent();
    }
}