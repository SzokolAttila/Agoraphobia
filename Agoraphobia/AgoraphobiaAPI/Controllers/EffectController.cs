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
    public async Task<IActionResult> AddEffect(EffectRequestDto effectRequestDto)
    {
        var player = await _playerRepository.GetByIdAsync(effectRequestDto.PlayerId);
        var consumable = await _consumableRepository.GetByIdAsync(effectRequestDto.ConsumableId);
        if (player is null)
            return BadRequest("Player not found");
        if (consumable is null)
            return BadRequest("Consumable not found");
        
        var newEffect = new Effect()
        {
            PlayerId = player.Id,
            ConsumableId = consumable.Id,
            CurrentDuration = consumable.Duration,
            Player = player,
            Consumable = consumable
        };
        await _effectRepository.CreateAsync(newEffect);
        return Created("agoraphobia/effects", newEffect.ToUpdateEffectRequestDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromEffect([FromRoute] int id)
    {
        var effect = await _effectRepository.GetByIdAsync(id);
        if (effect is null)
            return NotFound();
        
        if (effect.CurrentDuration > 1)
        {
            var updated = await _effectRepository.RemoveOneAsync(id);
            if (updated is null)
                return BadRequest("Something unexpected happened"); 
            return Ok(updated.ToUpdateEffectRequestDto());
        }

        await _effectRepository.DeleteAsync(effect);
        return NoContent();
    }
}