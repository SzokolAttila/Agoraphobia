using AgoraphobiaAPI.Dtos.RoomArmorLootStatus;
using AgoraphobiaAPI.Dtos.RoomMerchantArmorSaleStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/roomMerchantArmorSaleStatus")]
    public class RoomMerchantArmorSaleStatusController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IArmorRepository _armorRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IRoomMerchantArmorSaleStatusRepository _armorSaleStatusRepository;

        public RoomMerchantArmorSaleStatusController(
            IRoomRepository roomRepository,
            IPlayerRepository playerRepository,
            IArmorRepository armorRepository, 
            IMerchantRepository merchantRepository, 
            IRoomMerchantArmorSaleStatusRepository armorSaleStatusRepository
            )
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
            _armorRepository = armorRepository;
            _merchantRepository = merchantRepository;
            _armorSaleStatusRepository = armorSaleStatusRepository;
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetArmorSaleStatuses([FromRoute] int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player is null)
                return NotFound();
            var saleStatuses = await _armorSaleStatusRepository.GetArmorSalesAsync(player.Id);
            return Ok(saleStatuses.Select(x => x.ToRoomMerchantArmorSaleStatusDto()));
        }
        [HttpPost]
        public async Task<IActionResult> AddToArmorSales([FromBody] ArmorSaleStatusRequestDto statusDto)
        {
            var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            var armor = await _armorRepository.GetByIdAsync(statusDto.ArmorId);
            var merchant = await _merchantRepository.GetByIdAsync(statusDto.MerchantId);
            if (player is null)
                return BadRequest("Player not found");
            if (room is null)
                return BadRequest("Room not found");
            if (armor is null)
                return BadRequest("Armor not found");
            if (merchant is null)
                return BadRequest("Merchant not found");

            var armorSaleStatuses = await _armorSaleStatusRepository.GetArmorSalesAsync(statusDto.PlayerId);
            if (armorSaleStatuses.Exists(x => x.RoomId == room.Id && x.ArmorId == armor.Id && x.MerchantId == merchant.Id))
            {
                var updated = await _armorSaleStatusRepository.AddOneAsync(statusDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomMerchantArmorSaleStatusDto());
            }
            var status = new RoomMerchantArmorSaleStatus
            {
                Armor = armor,
                Quantity = 1,
                ArmorId = armor.Id,
                Player = player,
                PlayerId = player.Id,
                Room = room,
                RoomId = room.Id,
                Merchant = merchant,
                MerchantId = merchant.Id
            };
            await _armorSaleStatusRepository.CreateAsync(status);
            return Created("agoraphobia/roomMerchantArmorSaleStatus", status.ToRoomMerchantArmorSaleStatusDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromArmorSales([FromRoute] int id)
        {
            var saleStatus = await _armorSaleStatusRepository.GetByIdAsync(id);
            if (saleStatus is null)
                return NotFound();

            if (saleStatus.Quantity > 0)
            {
                var updated = await _armorSaleStatusRepository.RemoveOneAsync(
                    new ArmorSaleStatusRequestDto()
                {
                        RoomId = saleStatus.RoomId,
                        ArmorId = saleStatus.ArmorId,
                        MerchantId = saleStatus.MerchantId,
                        PlayerId = saleStatus.PlayerId
                });
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomMerchantArmorSaleStatusDto());
            }

            return BadRequest("Armor is not for sale");
        }
    }
}
