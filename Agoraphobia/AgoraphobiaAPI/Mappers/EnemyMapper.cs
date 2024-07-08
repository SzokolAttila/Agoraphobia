using AgoraphobiaAPI.Dtos.Enemy;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class EnemyMapper
{
    public static Enemy ToEnemyFromCreateDto(this CreateEnemyRequestDto enemyDto)
    {
        return new Enemy(enemyDto.Name, enemyDto.Description, enemyDto.Hp, enemyDto.Defense, enemyDto.Attack,
            enemyDto.Sanity, enemyDto.DreamCoins);
    }

    public static EnemyDto ToEnemyDto(this Enemy enemy)
    {
        return new EnemyDto()
        {
            Id = enemy.Id,
            Name = enemy.Name,
            Description = enemy.Description,
            Attack = enemy.Attack,
            Defense = enemy.Defense,
            Sanity = enemy.Sanity,
            Hp = enemy.Hp,
            DreamCoins = enemy.DreamCoins,
            ArmorDroprates = enemy.ArmorDroprates.Select(x => x.ToArmorDroprateDto()).ToList(),
            WeaponDroprates = enemy.WeaponDroprates.Select(x => x.ToWeaponDroprateDto()).ToList(),
            ConsumableDroprates = enemy.ConsumableDroprates.Select(x => x.ToConsumableDroprateDto()).ToList()
        };
    }
}