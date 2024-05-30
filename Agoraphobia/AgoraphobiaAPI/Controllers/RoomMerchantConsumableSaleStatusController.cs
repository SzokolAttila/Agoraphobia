using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
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
    }
}
