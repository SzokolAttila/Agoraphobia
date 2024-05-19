using AgoraphobiaAPI.Dtos.Consumable;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [Route("agoraphobia/consumables")]
    [ApiController]
    public class ConsumableController : ControllerBase
    {
        private readonly IConsumableRepository _consumableRepository;
        public ConsumableController(IConsumableRepository consumableRepository)
        {
            _consumableRepository = consumableRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var consumables = await _consumableRepository.GetAllAsync();
            return Ok(consumables.Select(x => x.ToConsumableDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var consumable = await _consumableRepository.GetByIdAsync(id);
            return consumable is null ? NotFound() : Ok(consumable.ToConsumableDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConsumableRequestDto consumable)
        {
            var consumableModel = consumable.ToConsumableFromCreateDto();
            await _consumableRepository.CreateAsync(consumableModel);
            return CreatedAtAction(nameof(GetById), new { id = consumableModel.Id }, consumableModel.ToConsumableDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateConsumableRequestDto consumable)
        {
            var consumableModel = await _consumableRepository.UpdateAsync(id, consumable);
            if (consumableModel is null)
                return NotFound();
            return Ok(consumableModel.ToConsumableDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var consumableModel = await _consumableRepository.DeleteAsync(id);
            if (consumableModel is null)
                return NotFound();
            return NoContent();
        }
    }
}
