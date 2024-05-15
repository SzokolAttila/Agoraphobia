using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AgoraphobiaLibrary
{
    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Fabled
    }
    public enum ArmorPiece
    {
        Helmet,
        Chestplate,
        Gauntlet,
        Boots
    }

    public abstract record ItemRecords(int Id, string Name, string Description);
    public abstract record Item(int Id, string Name, string Description, ItemRarity Rarity, int Price)
        :ItemRecords(Id, Name, Description);
    public record Weapon(int Id, string Name, string Description, ItemRarity Rarity, int Price,
            double MinMultiplier, double MaxMultiplier, int Energy) : Item(Id, Name, Description, Rarity, Price);
    public record Armor(int Id, string Name, string Description, ItemRarity Rarity, int Price,
            int Defense, int HP, ArmorPiece ArmorType) : Item(Id, Name, Description, Rarity, Price);
    public record Consumable(int Id, string Name, string Description, ItemRarity Rarity, int Price,
            int Energy, int HP, int Defense, int Attack, int Duration) : Item(Id, Name, Description, Rarity, Price);
}
