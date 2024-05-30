using AgoraphobiaAPI.Dtos.RoomConsumableLootStatus;
using AgoraphobiaAPI.Dtos.RoomConsumableLootStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
using AgoraphobiaLibrary.JoinTables.Rooms;
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
        [HttpPost]
        public async Task<IActionResult> AddToWeaponSales([FromBody] ConsumableLootStatusRequestDto statusDto)
        {
            var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            var consumable = await _consumableRepository.GetByIdAsync(statusDto.ConsumableId);
            if (player is null)
                return BadRequest("Player not found");
            if (room is null)
                return BadRequest("Room not found");
            if (consumable is null)
                return BadRequest("Consumable not found");

            var lootStatuses = await _consumableStatusRepository.GetRoomConsumableLootStatusesAsync(statusDto.PlayerId);
            if (lootStatuses.Exists(x => x.RoomId == room.Id && x.ConsumableId == consumable.Id))
            {
                var updated = await _consumableStatusRepository.AddOneAsync(statusDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomConsumableLootStatusDto());
            }
            var status = new RoomConsumableLootStatus
            {
                Consumable = consumable,
                Quantity = 1,
                ConsumableId = consumable.Id,
                Player = player,
                PlayerId = player.Id,
                Room = room,
                RoomId = room.Id
            };
            await _consumableStatusRepository.CreateAsync(status);
            return Created("agoraphobia/roomConsumableLootStatus", status.ToRoomConsumableLootStatusDto());
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveFromWeaponSales([FromBody] ConsumableLootStatusRequestDto statusDto)
        {
            var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            var consumable = await _consumableRepository.GetByIdAsync(statusDto.ConsumableId);
            if (player is null)
                return BadRequest("Player not found");
            if (room is null)
                return BadRequest("Room not found");
            if (consumable is null)
                return BadRequest("Consumable not found");

            var lootStatuses = await _consumableStatusRepository.GetRoomConsumableLootStatusesAsync(player.Id);
            var lootStatus = lootStatuses.FirstOrDefault(x => x.RoomId == room.Id && x.ConsumableId == consumable.Id);
            if (lootStatus is null)
                return NotFound();

            if (lootStatus.Quantity > 1)
            {
                var updated = await _consumableStatusRepository.RemoveOneAsync(statusDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomConsumableLootStatusDto());
            }

            await _consumableStatusRepository.DeleteAsync(lootStatus);
            return NoContent();
        }
    }
}
