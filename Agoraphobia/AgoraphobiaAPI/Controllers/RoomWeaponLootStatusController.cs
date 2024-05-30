using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/roomWeaponLootStatus")]
    public class RoomWeaponLootStatusController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IWeaponRepository _weaponRepository;
        private readonly IRoomWeaponLootStatusRepository _weaponStatusRepository;

        public RoomWeaponLootStatusController(
            IRoomRepository roomRepository,
            IPlayerRepository playerRepository,
            IWeaponRepository weaponRepository,
            IRoomWeaponLootStatusRepository weaponStatusRepository
        )
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
            _weaponRepository = weaponRepository;
            _weaponStatusRepository = weaponStatusRepository;
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetRoomWeaponLootStatus([FromRoute] int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player is null)
                return NotFound();
            var lootStatuses = await _weaponStatusRepository.GetRoomWeaponLootStatusesAsync(player.Id);
            return Ok(lootStatuses.Select(x => x.ToRoomWeaponLootStatusDto()));
        }
    }
}
