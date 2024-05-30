using AgoraphobiaAPI.Dtos.ArmorLoot;
using AgoraphobiaAPI.Dtos.RoomArmorLootStatus;
using AgoraphobiaAPI.Dtos.WeaponSale;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.JoinTables.Weapons;
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
        [HttpPost]
        public async Task<IActionResult> AddToArmorLoot([FromBody] ArmorLootStatusRequestDto statusDto)
        {
            var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            var armor = await _armorRepository.GetByIdAsync(statusDto.ArmorId);
            if (player is null)
                return BadRequest("Player not found");
            if (room is null)
                return BadRequest("Room not found");
            if (armor is null)
                return BadRequest("Armor not found");

            var lootStatuses = await _armorStatusRepository.GetRoomArmorLootStatusesAsync(statusDto.PlayerId);
            if (lootStatuses.Exists(x => x.RoomId == room.Id && x.ArmorId == armor.Id))
            {
                var updated = await _armorStatusRepository.AddOneAsync(statusDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomArmorLootStatusDto());
            }
            var status = new RoomArmorLootStatus
            {
                Armor = armor,
                Quantity = 1,
                ArmorId = armor.Id,
                Player = player,
                PlayerId = player.Id,
                Room = room,
                RoomId = room.Id
            };
            await _armorStatusRepository.CreateAsync(status);
            return Created("agoraphobia/roomArmorLootStatus", status.ToRoomArmorLootStatusDto());
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveFromArmorLoot([FromBody] ArmorLootStatusRequestDto statusDto)
        {
                  var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            var armor = await _armorRepository.GetByIdAsync(statusDto.ArmorId);
            if (player is null)
                return BadRequest("Player not found");
            if (room is null)
                return BadRequest("Room not found");
            if (armor is null)
                return BadRequest("Armor not found");

            var lootStatuses = await _armorStatusRepository.GetRoomArmorLootStatusesAsync(player.Id);
            var lootStatus = lootStatuses.FirstOrDefault(x => x.RoomId == room.Id && x.ArmorId == armor.Id);
            if (lootStatus is null)
                return NotFound();

            if (lootStatus.Quantity > 1)
            {
                var updated = await _armorStatusRepository.RemoveOneAsync(statusDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomArmorLootStatusDto());
            }

            await _armorStatusRepository.DeleteAsync(lootStatus);
            return NoContent();
        }
    }
}
