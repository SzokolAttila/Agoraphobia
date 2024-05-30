using AgoraphobiaAPI.Dtos.RoomWeaponLootStatus;
using AgoraphobiaAPI.Dtos.RoomWeaponLootStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
using AgoraphobiaLibrary.JoinTables.Rooms;
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
        [HttpPost]
        public async Task<IActionResult> AddToWeaponSales([FromBody] WeaponLootStatusRequestDto statusDto)
        {
            var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            var weapon = await _weaponRepository.GetByIdAsync(statusDto.WeaponId);
            if (player is null)
                return BadRequest("Player not found");
            if (room is null)
                return BadRequest("Room not found");
            if (weapon is null)
                return BadRequest("Weapon not found");

            var lootStatuses = await _weaponStatusRepository.GetRoomWeaponLootStatusesAsync(statusDto.PlayerId);
            if (lootStatuses.Exists(x => x.RoomId == room.Id && x.WeaponId == weapon.Id))
            {
                var updated = await _weaponStatusRepository.AddOneAsync(statusDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomWeaponLootStatusDto());
            }
            var status = new RoomWeaponLootStatus
            {
                Weapon = weapon,
                Quantity = 1,
                WeaponId = weapon.Id,
                Player = player,
                PlayerId = player.Id,
                Room = room,
                RoomId = room.Id
            };
            await _weaponStatusRepository.CreateAsync(status);
            return Created("agoraphobia/roomWeaponLootStatus", status.ToRoomWeaponLootStatusDto());
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveFromWeaponSales([FromBody] WeaponLootStatusRequestDto statusDto)
        {
            var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            var weapon = await _weaponRepository.GetByIdAsync(statusDto.WeaponId);
            if (player is null)
                return BadRequest("Player not found");
            if (room is null)
                return BadRequest("Room not found");
            if (weapon is null)
                return BadRequest("Weapon not found");

            var lootStatuses = await _weaponStatusRepository.GetRoomWeaponLootStatusesAsync(player.Id);
            var lootStatus = lootStatuses.FirstOrDefault(x => x.RoomId == room.Id && x.WeaponId == weapon.Id);
            if (lootStatus is null)
                return NotFound();

            if (lootStatus.Quantity > 1)
            {
                var updated = await _weaponStatusRepository.RemoveOneAsync(statusDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomWeaponLootStatusDto());
            }

            await _weaponStatusRepository.DeleteAsync(lootStatus);
            return NoContent();
        }
    }
}
