using AgoraphobiaAPI.Dtos.RoomEnemyStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/roomEnemyStatus")]
    public class RoomEnemyStatusController : ControllerBase
    {
        private readonly IRoomEnemyStatusRepository _roomEnemyStatusRepository;
        private readonly IPlayerRepository _playerRepository;

        public RoomEnemyStatusController(
            IRoomEnemyStatusRepository roomEnemyStatusRepository, 
            IPlayerRepository playerRepository
            )
        {
            _roomEnemyStatusRepository = roomEnemyStatusRepository;
            _playerRepository = playerRepository;
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
    }
}
