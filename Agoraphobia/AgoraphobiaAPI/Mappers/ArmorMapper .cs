using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class ArmorMapper
{
    public static Armor ToArmorFromCreateDto(this CreateArmorRequestDto armor)
    {
        return new Armor(armor.Name, armor.Description, armor.RarityIdx, armor.Price, armor.Defense, armor.Hp, armor.ArmorTypeIdx);
    }

    public static ArmorDto ToArmorDto(this Armor armor)
    {
        return new ArmorDto
        {
            Id = armor.Id,
            Description = armor.Description,
            Defense = armor.Defense,
            Hp = armor.Hp,
            Name = armor.Name,
            RarityIdx = armor.RarityIdx,
            ArmorTypeIdx = armor.ArmorTypeIdx,
            Price = armor.Price
        };
    }
}