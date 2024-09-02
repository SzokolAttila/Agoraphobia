using AgoraphobiaAPI.Dtos.ArmorDroprate;
using AgoraphobiaLibrary.JoinTables.Armors;

namespace AgoraphobiaAPI.Mappers;

public static class ArmorDroprateMapper
{
    public static ArmorDroprateDto ToArmorDroprateDto(this ArmorDroprate armorDroprate)
    {
        return new ArmorDroprateDto
        {
            Armor = armorDroprate.Armor!.ToArmorDto(),
            Droprate = armorDroprate.Droprate,
            ArmorId = armorDroprate.ArmorId,
            EnemyId = armorDroprate.EnemyId,
            Id = armorDroprate.Id,
        };
    }
}