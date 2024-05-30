using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/roomArmorLootStatus")]
    public class RoomArmorLootStatusController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IArmorRepository _armorRepository;
        private readonly IRoomArmorLootStatusRepository _armorStatusRepository;

        public RoomArmorLootStatusController(
            IRoomRepository roomRepository, 
            IPlayerRepository playerRepository, 
            IArmorRepository armorRepository, 
            IRoomArmorLootStatusRepository armorStatusRepository
            )
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
            _armorRepository = armorRepository;
            _armorStatusRepository = armorStatusRepository;
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetRoomArmorLootStatus([FromRoute] int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player is null)
                return NotFound();
            var lootStatuses = await _armorStatusRepository.GetRoomArmorLootStatusesAsync(player.Id);
            return Ok(lootStatuses.Select(x => x.ToRoomArmorLootStatusDto()));
        }
    }
}
