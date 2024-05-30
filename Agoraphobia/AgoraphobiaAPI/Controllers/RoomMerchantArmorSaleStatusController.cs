using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
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
    }
}
