﻿using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers
{
    [Route("agoraphobia/armors")]
    [ApiController]
    public class ArmorController : ControllerBase
    {
        private readonly IArmorRepository _armorRepository;
        public ArmorController(IArmorRepository armorRepository)
        {
            _armorRepository = armorRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var armors = await _armorRepository.GetAllAsync();
            return Ok(armors.Select(x => x.ToArmorDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var armor = await _armorRepository.GetByIdAsync(id);
            return armor is null ? NotFound() : Ok(armor.ToArmorDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArmorRequestDto armor)
        {
            var armorModel = armor.ToArmorFromCreateDto();
            await _armorRepository.CreateAsync(armorModel);
            return CreatedAtAction(nameof(GetById), new { id = armorModel.Id }, armorModel.ToArmorDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateArmorRequestDto armor)
        {
            var armorModel = await _armorRepository.UpdateAsync(id, armor);
            if (armorModel is null)
                return NotFound();
            return Ok(armorModel.ToArmorDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var armorModel = await _armorRepository.DeleteAsync(id);
            if (armorModel is null)
                return NotFound();
            return NoContent();
        }
    }
}
