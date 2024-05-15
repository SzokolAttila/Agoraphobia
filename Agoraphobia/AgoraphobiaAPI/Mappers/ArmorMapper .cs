using AgoraphobiaAPI.Dtos.Armor;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class ArmorMapper
{
    public static Armor ToArmorFromCreateDto(this CreateArmorRequestDto armor)
    {
        return new Armor(armor.Name, armor.Description, armor.RarityIdx, armor.Price, armor.Defense, armor.Hp, armor.ArmorTypeIdx);
    }
}