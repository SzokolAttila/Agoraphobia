using AgoraphobiaAPI.Dtos.Effect;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Mappers;
using AgoraphobiaLibrary;
using Microsoft.AspNetCore.Mvc;

namespace AgoraphobiaAPI.Controllers;

[ApiController]
[Route("agoraphobia/effects")]
public class EffectController : ControllerBase
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IConsumableRepository _consumableRepository;
    private readonly IEffectRepository _effectRepository;

    public EffectController(
        IPlayerRepository playerRepository, 
        IConsumableRepository consumableRepository, 
        IEffectRepository effectRepository
        )
    {
        _playerRepository = playerRepository;
        _consumableRepository = consumableRepository;
        _effectRepository = effectRepository;
    }

    [HttpGet("{playerId}")]
    public async Task<IActionResult> GetEffect([FromRoute] int playerId)
    {
        var player = await _playerRepository.GetByIdAsync(playerId);
        if (player is null)
            return NotFound();
        var effects = await _effectRepository.GetEffectsAsync(playerId);
        return Ok(effects.Select(x => x.ToEffectDto()));
    }

    [HttpPost]
    public async Task<IActionResult> AddToEffect(EffectRequestDto effectRequestDto)
    {
        var player = await _playerRepository.GetByIdAsync(effectRequestDto.PlayerId);
        var consumable = await _consumableRepository.GetByIdAsync(effectRequestDto.ConsumableId);
        if (player is null)
            return BadRequest("Player not found");
        if (consumable is null)
            return BadRequest("Consumable not found");
        
        var effects = await _effectRepository.GetEffectsAsync(player.Id);
        if (effects.Exists(x => x.ConsumableId == consumable.Id))
        {
            var updated = await _effectRepository.AddOneAsync(effectRequestDto);
            if (updated is null)
                return BadRequest("Something unexpected happened");
            return Ok(updated.ToUpdateEffectRequestDto());
        }
        var effect = new Effect()
        {
            PlayerId = player.Id,
            ConsumableId = consumable.Id,
            CurrentDuration = consumable.Duration,
            Player = player,
            Consumable = consumable
        };
        await _effectRepository.CreateAsync(effect);
        return Created("agoraphobia/effects", effect.ToUpdateEffectRequestDto());
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveFromEffect([FromBody] EffectRequestDto effectRequestDto)
    {
        var player = await _playerRepository.GetByIdAsync(effectRequestDto.PlayerId);
        var consumable = await _consumableRepository.GetByIdAsync(effectRequestDto.ConsumableId);
        if (player is null)
            return BadRequest("Player not found");
        if (consumable is null)
            return BadRequest("Consumable not found");

        var effects = await _effectRepository.GetEffectsAsync(player.Id);
        var effect = effects.FirstOrDefault(x => x.ConsumableId == consumable.Id);
        if (effect is null)
            return NotFound();
        
        if (effect.CurrentDuration > 1)
        {
            var updated = await _effectRepository.RemoveOneAsync(effectRequestDto);
            if (updated is null)
                return BadRequest("Something unexpected happened"); 
            return Ok(updated.ToUpdateEffectRequestDto());
        }

        await _effectRepository.DeleteAsync(effect);
        return NoContent();
    }
}