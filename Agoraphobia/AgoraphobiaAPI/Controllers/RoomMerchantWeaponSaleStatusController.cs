using AgoraphobiaAPI.Dtos.RoomMerchantWeaponSaleStatus;
using AgoraphobiaAPI.Dtos.RoomMerchantWeaponSaleStatus;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaAPI.Repositories;
using AgoraphobiaLibrary.JoinTables.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [ApiController]
    [Route("agoraphobia/roomMerchantWeaponSaleStatus")]
    public class RoomMerchantWeaponSaleStatusController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IWeaponRepository _weaponRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IRoomMerchantWeaponSaleStatusRepository _weaponSaleStatusRepository;

        public RoomMerchantWeaponSaleStatusController(
            IRoomRepository roomRepository,
            IPlayerRepository playerRepository,
            IWeaponRepository weaponRepository,
            IMerchantRepository merchantRepository,
            IRoomMerchantWeaponSaleStatusRepository weaponSaleStatusRepository
        )
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
            _weaponRepository = weaponRepository;
            _merchantRepository = merchantRepository;
            _weaponSaleStatusRepository = weaponSaleStatusRepository;
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetWeaponSaleStatuses([FromRoute] int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player is null)
                return NotFound();
            var saleStatuses = await _weaponSaleStatusRepository.GetWeaponSalesAsync(player.Id);
            return Ok(saleStatuses.Select(x => x.ToRoomMerchantWeaponSaleStatusDto()));
        }
        [HttpPost]
        public async Task<IActionResult> AddToWeaponSales([FromBody] WeaponSaleStatusRequestDto statusDto)
        {
            var player = await _playerRepository.GetByIdAsync(statusDto.PlayerId);
            var room = await _roomRepository.GetByIdAsync(statusDto.RoomId);
            var weapon = await _weaponRepository.GetByIdAsync(statusDto.WeaponId);
            var merchant = await _merchantRepository.GetByIdAsync(statusDto.MerchantId);
            if (player is null)
                return BadRequest("Player not found");
            if (room is null)
                return BadRequest("Room not found");
            if (weapon is null)
                return BadRequest("Weapon not found");
            if (merchant is null)
                return BadRequest("Merchant not found");

            var weaponSaleStatuses = await _weaponSaleStatusRepository.GetWeaponSalesAsync(statusDto.PlayerId);
            if (weaponSaleStatuses.Exists(x => x.RoomId == room.Id && x.WeaponId == weapon.Id && x.MerchantId == merchant.Id))
            {
                var updated = await _weaponSaleStatusRepository.AddOneAsync(statusDto);
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomMerchantWeaponSaleStatusDto());
            }
            var status = new RoomMerchantWeaponSaleStatus
            {
                Weapon = weapon,
                Quantity = 1,
                WeaponId = weapon.Id,
                Player = player,
                PlayerId = player.Id,
                Room = room,
                RoomId = room.Id,
                Merchant = merchant,
                MerchantId = merchant.Id
            };
            await _weaponSaleStatusRepository.CreateAsync(status);
            return Created("agoraphobia/roomMerchantWeaponSaleStatus", status.ToRoomMerchantWeaponSaleStatusDto());
        }
        [HttpDelete("{weaponSaleStatusId}")]
        public async Task<IActionResult> RemoveFromWeaponSales([FromRoute] int weaponSaleStatusId)
        {
            var saleStatus = await _weaponSaleStatusRepository.GetByIdAsync(weaponSaleStatusId);
            if (saleStatus is null)
                return NotFound();

            if (saleStatus.Quantity > 0)
            {
                var updated = await _weaponSaleStatusRepository
                    .RemoveOneAsync(new WeaponSaleStatusRequestDto()
                {
                    RoomId = saleStatus.RoomId,
                    MerchantId = saleStatus.MerchantId,
                    PlayerId = saleStatus.PlayerId,
                    WeaponId = saleStatus.WeaponId,
                });
                if (updated is null)
                    return BadRequest("Something unexpected happened");
                return Ok(updated.ToRoomMerchantWeaponSaleStatusDto());
            }

            return BadRequest("Weapon not available for sale");
        }
    }
}
