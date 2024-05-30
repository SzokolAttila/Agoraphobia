using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/roomConsumableLootStatus")]
    public class RoomConsumableLootStatusController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IConsumableRepository _consumableRepository;
        private readonly IRoomConsumableLootStatusRepository _consumableStatusRepository;

        public RoomConsumableLootStatusController(
            IRoomRepository roomRepository,
            IPlayerRepository playerRepository,
            IConsumableRepository consumableRepository,
            IRoomConsumableLootStatusRepository consumableStatusRepository
        )
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
            _consumableRepository = consumableRepository;
            _consumableStatusRepository = consumableStatusRepository;
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetRoomConsumableLootStatus([FromRoute] int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player is null)
                return NotFound();
            var lootStatuses = await _consumableStatusRepository.GetRoomConsumableLootStatusesAsync(player.Id);
            return Ok(lootStatuses.Select(x => x.ToRoomConsumableLootStatusDto()));
        }
    }
}
