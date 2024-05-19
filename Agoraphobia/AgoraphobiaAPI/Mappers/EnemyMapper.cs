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
            Attack = enemy.Attack,
            Defense = enemy.Defense,
            Sanity = enemy.Sanity,
            Hp = enemy.Hp,
            DreamCoins = enemy.DreamCoins,
            Armors = enemy.ArmorDroprates.Select(x => x.ToArmorDroprateDto()).ToList(),
            Weapons = enemy.Weapons,
            Consumables = enemy.Consumables
        };
    }
}