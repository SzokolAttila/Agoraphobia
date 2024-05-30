using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
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
    }
}
