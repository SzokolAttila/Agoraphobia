using AgoraphobiaAPI.Dtos.RoomEnemyStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/roomEnemyStatus")]
    public class RoomEnemyStatusController : ControllerBase
    {
        private readonly IRoomEnemyStatusRepository _roomEnemyStatusRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRoomRepository _roomRepository;

        public RoomEnemyStatusController(
            IRoomEnemyStatusRepository roomEnemyStatusRepository, 
            IPlayerRepository playerRepository, 
            IRoomRepository roomRepository
            )
        {
            _roomEnemyStatusRepository = roomEnemyStatusRepository;
            _playerRepository = playerRepository;
            _roomRepository = roomRepository;
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetRoomStatuses([FromRoute] int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player is null)
                return BadRequest("Player not found");

            var statuses = await _roomEnemyStatusRepository.GetRoomStatusesAsync(playerId);
            return Ok(statuses.Select(x => x.ToRoomEnemyStatusDto()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoomStatus([FromBody] CreateRoomEnemyStatusDto statusDto)
        {
            var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            if (player is null)
                return BadRequest("Player not found");
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            if (room is null)
                return BadRequest("Room not found");
            var status = await _roomEnemyStatusRepository.CreateRoomStatusAsync(statusDto.ToRoomStatusFromCreateDto());
            return Ok(status.ToRoomEnemyStatusDto());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoomStatus([FromBody] CreateRoomEnemyStatusDto statusDto)
        {
            var roomStatus = await _roomEnemyStatusRepository.UpdateRoomStatusAsync(statusDto);
            if (roomStatus is null)
                return NotFound();
            return Ok(statusDto);
        }
    }
}
