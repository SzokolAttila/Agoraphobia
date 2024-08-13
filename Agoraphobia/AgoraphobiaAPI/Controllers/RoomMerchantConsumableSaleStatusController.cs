using AgoraphobiaAPI.Dtos.RoomMerchantConsumableSaleStatus;
using AgoraphobiaAPI.Dtos.RoomMerchantConsumableSaleStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/roomMerchantConsumableSaleStatus")]
    public class RoomMerchantConsumableSaleStatusController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IConsumableRepository _consumableRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IRoomMerchantConsumableSaleStatusRepository _consumableSaleStatusRepository;

        public RoomMerchantConsumableSaleStatusController(
            IRoomRepository roomRepository,
            IPlayerRepository playerRepository,
            IConsumableRepository consumableRepository,
            IMerchantRepository merchantRepository,
            IRoomMerchantConsumableSaleStatusRepository consumableSaleStatusRepository
        )
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
            _consumableRepository = consumableRepository;
            _merchantRepository = merchantRepository;
            _consumableSaleStatusRepository = consumableSaleStatusRepository;
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetConsumableSaleStatuses([FromRoute] int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player is null)
                return NotFound();
            var saleStatuses = await _consumableSaleStatusRepository.GetConsumableSalesAsync(player.Id);
            return Ok(saleStatuses.Select(x => x.ToRoomMerchantConsumableSaleStatusDto()));
        }
        [HttpPost]
        public async Task<IActionResult> AddToConsumableSales([FromBody] ConsumableSaleStatusRequestDto statusDto)
        {
            var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            var consumable = await _consumableRepository.GetByIdAsync(statusDto.ConsumableId);
            var merchant = await _merchantRepository.GetByIdAsync(statusDto.MerchantId);
            if (player is null)
                return BadRequest("Player not found");
            if (room is null)
                return BadRequest("Room not found");
            if (consumable is null)
                return BadRequest("Consumable not found");
            if (merchant is null)
                return BadRequest("Merchant not found");

            var consumableSaleStatuses = await _consumableSaleStatusRepository.GetConsumableSalesAsync(statusDto.PlayerId);
            if (consumableSaleStatuses.Exists(x => x.RoomId == room.Id && x.ConsumableId == consumable.Id && x.MerchantId == merchant.Id))
            {
                var updated = await _consumableSaleStatusRepository.AddOneAsync(statusDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomMerchantConsumableSaleStatusDto());
            }
            var status = new RoomMerchantConsumableSaleStatus
            {
                Consumable = consumable,
                Quantity = 1,
                ConsumableId = consumable.Id,
                Player = player,
                PlayerId = player.Id,
                Room = room,
                RoomId = room.Id,
                Merchant = merchant,
                MerchantId = merchant.Id
            };
            await _consumableSaleStatusRepository.CreateAsync(status);
            return Created("agoraphobia/roomMerchantConsumableSaleStatus", status.ToRoomMerchantConsumableSaleStatusDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromConsumableSales([FromRoute] int id)
        {
            var saleStatus = await _consumableSaleStatusRepository.GetByIdAsync(id);
            if (saleStatus is null)
                return NotFound();

            if (saleStatus.Quantity > 0)
            {
                var updated = await 
                    _consumableSaleStatusRepository.RemoveOneAsync(new()
                {
                    ConsumableId = saleStatus.ConsumableId,
                    MerchantId = saleStatus.MerchantId,
                    PlayerId = saleStatus.PlayerId,
                    RoomId = saleStatus.RoomId
                });
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomMerchantConsumableSaleStatusDto());
            }

            return BadRequest("Consumable not available for sale");
        }
    }
}
