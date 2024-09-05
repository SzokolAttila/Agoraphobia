using AgoraphobiaLibrary;
using AgoraphobiaLibrary.JoinTables.Armors;
using AgoraphobiaLibrary.JoinTables.Consumables;
using AgoraphobiaLibrary.JoinTables.Rooms;
using AgoraphobiaLibrary.JoinTables.Weapons;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            :base (dbContextOptions)
        {
            
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Effect> Effects { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ArmorInventory> ArmorInventories { get; set; }
        public DbSet<WeaponInventory> WeaponInventories { get; set; }
        public DbSet<ConsumableInventory> ConsumableInventories { get; set; }
        public DbSet<ArmorLoot> ArmorLoots { get; set; }
        public DbSet<WeaponLoot> WeaponLoots { get; set; }
        public DbSet<ConsumableLoot> ConsumableLoots { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Consumable> Consumables { get; set; }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<ArmorDroprate> ArmorDroprates { get; set; }
        public DbSet<WeaponDroprate> WeaponDroprates { get; set; }
        public DbSet<ConsumableDroprate> ConsumableDroprates { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<ConsumableSale> ConsumableSales { get; set; }
        public DbSet<WeaponSale> WeaponSales { get; set; }
        public DbSet<ArmorSale> ArmorSales { get; set; }
        public DbSet<RoomEnemyStatus> RoomEnemyStatus { get; set; }
        public DbSet<RoomArmorLootStatus> RoomArmorLootStatus { get; set; }
        public DbSet<RoomWeaponLootStatus> RoomWeaponLootStatus { get; set; }
        public DbSet<RoomConsumableLootStatus> RoomConsumableLootStatus { get; set; }
        public DbSet<RoomMerchantArmorSaleStatus> RoomMerchantArmorSaleStatus { get; set; }
        public DbSet<RoomMerchantWeaponSaleStatus> RoomMerchantWeaponSaleStatus { get; set; }
        public DbSet<RoomMerchantConsumableSaleStatus> RoomMerchantConsumableSaleStatus { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ArmorInventory>(x => x.HasKey(y => y.Id));
            builder.Entity<ArmorInventory>()
                .HasOne(x => x.Player)
                .WithMany(x => x.ArmorInventories)
                .HasForeignKey(x => x.PlayerId);       
            
            builder.Entity<ArmorInventory>()
                .HasOne(x => x.Armor)
                .WithMany(x => x.ArmorInventories)
                .HasForeignKey(x => x.ArmorId);

            
            builder.Entity<WeaponInventory>(x => x.HasKey(y => y.Id));
            builder.Entity<WeaponInventory>()
                .HasOne(x => x.Player)
                .WithMany(x => x.WeaponInventories)
                .HasForeignKey(x => x.PlayerId);       
            
            builder.Entity<WeaponInventory>()
                .HasOne(x => x.Weapon)
                .WithMany(x => x.WeaponInventories)
                .HasForeignKey(x => x.WeaponId);
            
            builder.Entity<ConsumableInventory>(x => 
                x.HasKey(y => y.Id));
            builder.Entity<ConsumableInventory>()
                .HasOne(x => x.Player)
                .WithMany(x => x.ConsumableInventories)
                .HasForeignKey(x => x.PlayerId);       
            
            builder.Entity<ConsumableInventory>()
                .HasOne(x => x.Consumable)
                .WithMany(x => x.ConsumableInventories)
                .HasForeignKey(x => x.ConsumableId);

            builder.Entity<ArmorLoot>(x => x.HasKey(y => y.Id));
            builder.Entity<ArmorLoot>()
                .HasOne(x => x.Room)
                .WithMany(x => x.Armors)
                .HasForeignKey(x => x.RoomId);

            builder.Entity<ArmorLoot>()
                .HasOne(x => x.Armor)
                .WithMany(x => x.ArmorLoots)
                .HasForeignKey(x => x.ArmorId);


            builder.Entity<WeaponLoot>(x => x.HasKey(y => y.Id));
            builder.Entity<WeaponLoot>()
                .HasOne(x => x.Room)
                .WithMany(x => x.Weapons)
                .HasForeignKey(x => x.RoomId);

            builder.Entity<WeaponLoot>()
                .HasOne(x => x.Weapon)
                .WithMany(x => x.WeaponLoots)
                .HasForeignKey(x => x.WeaponId);

            builder.Entity<ConsumableLoot>(x => x.HasKey(y => y.Id));
            builder.Entity<ConsumableLoot>()
                .HasOne(x => x.Room)
                .WithMany(x => x.Consumables)
                .HasForeignKey(x => x.RoomId);

            builder.Entity<ConsumableLoot>()
                .HasOne(x => x.Consumable)
                .WithMany(x => x.ConsumableLoots)
                .HasForeignKey(x => x.ConsumableId);

            builder.Entity<ArmorDroprate>(x => x.HasKey(p => p.Id));
            builder.Entity<ArmorDroprate>()
                .HasOne(p => p.Enemy)
                .WithMany(x => x.ArmorDroprates)
                .HasForeignKey(x => x.EnemyId);

            builder.Entity<ArmorDroprate>()
                .HasOne(p => p.Armor)
                .WithMany(x => x.ArmorDroprates)
                .HasForeignKey(x => x.ArmorId);

            builder.Entity<WeaponDroprate>(x => x.HasKey(p => p.Id));
            builder.Entity<WeaponDroprate>()
                .HasOne(p => p.Enemy)
                .WithMany(x => x.WeaponDroprates)
                .HasForeignKey(x => x.EnemyId);

            builder.Entity<WeaponDroprate>()
                .HasOne(p => p.Weapon)
                .WithMany(x => x.WeaponDroprates)
                .HasForeignKey(x => x.WeaponId);

            builder.Entity<ConsumableDroprate>(x => x.HasKey(p => p.Id));
            builder.Entity<ConsumableDroprate>()
                .HasOne(p => p.Enemy)
                .WithMany(x => x.ConsumableDroprates)
                .HasForeignKey(x => x.EnemyId);

            builder.Entity<ConsumableDroprate>()
                .HasOne(p => p.Consumable)
                .WithMany(x => x.ConsumableDroprates)
                .HasForeignKey(x => x.ConsumableId);

            builder.Entity<Effect>(x => x.HasKey(y => y.Id));
            builder.Entity<Effect>()
                .HasOne(x => x.Player)
                .WithMany(x => x.Effects)
                .HasForeignKey(x => x.PlayerId);

            builder.Entity<Effect>()
                .HasOne(x => x.Consumable)
                .WithMany(x => x.Effects)
                .HasForeignKey(x => x.ConsumableId);

            builder.Entity<ArmorSale>(x => x.HasKey(y => y.Id));
            builder.Entity<ArmorSale>()
                .HasOne(x => x.Merchant)
                .WithMany(x => x.ArmorSales)
                .HasForeignKey(x => x.MerchantId);

            builder.Entity<ArmorSale>()
                .HasOne(x => x.Armor)
                .WithMany(x => x.ArmorSales)
                .HasForeignKey(x => x.ArmorId);

            builder.Entity<WeaponSale>(x => x.HasKey(y => y.Id));
            builder.Entity<WeaponSale>()
                .HasOne(x => x.Merchant)
                .WithMany(x => x.WeaponSales)
                .HasForeignKey(x => x.MerchantId);

            builder.Entity<WeaponSale>()
                .HasOne(x => x.Weapon)
                .WithMany(x => x.WeaponSales)
                .HasForeignKey(x => x.WeaponId);

            builder.Entity<ConsumableSale>(x => x.HasKey(y => y.Id));
            builder.Entity<ConsumableSale>()
                .HasOne(x => x.Merchant)
                .WithMany(x => x.ConsumableSales)
                .HasForeignKey(x => x.MerchantId);

            builder.Entity<ConsumableSale>()
                .HasOne(x => x.Consumable)
                .WithMany(x => x.ConsumableSales)
                .HasForeignKey(x => x.ConsumableId);

            builder.Entity<RoomEnemyStatus>(x => x.HasKey(y => new { y.PlayerId, y.RoomId }));
            builder.Entity<RoomEnemyStatus>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomEnemyStatusList)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomEnemyStatus>()
                .HasOne(x => x.Player)
                .WithMany(x => x.RoomEnemyStatusList)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Player>()
                .HasOne(x => x.Room)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Room>()
                .HasOne(x => x.Enemy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Room>()
                .HasOne(x => x.Merchant)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RoomArmorLootStatus>(x => x.HasKey(y => y.Id));
            builder.Entity<RoomArmorLootStatus>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomArmorLootStatus)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomArmorLootStatus>()
                .HasOne(x => x.Player)
                .WithMany(x => x.RoomArmorLootStatus)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomArmorLootStatus>()
                .HasOne(x => x.Armor)
                .WithMany(x => x.RoomArmorLootStatus)
                .HasForeignKey(x => x.ArmorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RoomWeaponLootStatus>(x => 
                x.HasKey(y => y.Id));
            builder.Entity<RoomWeaponLootStatus>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomWeaponLootStatus)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomWeaponLootStatus>()
                .HasOne(x => x.Player)
                .WithMany(x => x.RoomWeaponLootStatus)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomWeaponLootStatus>()
                .HasOne(x => x.Weapon)
                .WithMany(x => x.RoomWeaponLootStatus)
                .HasForeignKey(x => x.WeaponId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RoomConsumableLootStatus>(x => 
                x.HasKey(y => y.Id));
            builder.Entity<RoomConsumableLootStatus>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomConsumableLootStatus)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomConsumableLootStatus>()
                .HasOne(x => x.Player)
                .WithMany(x => x.RoomConsumableLootStatus)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomConsumableLootStatus>()
                .HasOne(x => x.Consumable)
                .WithMany(x => x.RoomConsumableLootStatus)
                .HasForeignKey(x => x.ConsumableId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RoomMerchantArmorSaleStatus>(x => x.HasKey(y => y.Id));
            builder.Entity<RoomMerchantArmorSaleStatus>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomMerchantArmorSaleStatus)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomMerchantArmorSaleStatus>()
                .HasOne(x => x.Player)
                .WithMany(x => x.RoomMerchantArmorSaleStatus)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomMerchantArmorSaleStatus>()
                .HasOne(x => x.Merchant)
                .WithMany(x => x.RoomMerchantArmorSaleStatus)
                .HasForeignKey(x => x.MerchantId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomMerchantArmorSaleStatus>()
                .HasOne(x => x.Armor)
                .WithMany(x => x.RoomMerchantArmorSaleStatus)
                .HasForeignKey(x => x.ArmorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RoomMerchantWeaponSaleStatus>(x => 
                x.HasKey(y => y.Id));
            builder.Entity<RoomMerchantWeaponSaleStatus>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomMerchantWeaponSaleStatus)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomMerchantWeaponSaleStatus>()
                .HasOne(x => x.Player)
                .WithMany(x => x.RoomMerchantWeaponSaleStatus)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomMerchantWeaponSaleStatus>()
                .HasOne(x => x.Merchant)
                .WithMany(x => x.RoomMerchantWeaponSaleStatus)
                .HasForeignKey(x => x.MerchantId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomMerchantWeaponSaleStatus>()
                .HasOne(x => x.Weapon)
                .WithMany(x => x.RoomMerchantWeaponSaleStatus)
                .HasForeignKey(x => x.WeaponId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RoomMerchantConsumableSaleStatus>(x => 
                x.HasKey(y => y.Id));
            builder.Entity<RoomMerchantConsumableSaleStatus>()
                .HasOne(x => x.Room)
                .WithMany(x => x.RoomMerchantConsumableSaleStatus)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomMerchantConsumableSaleStatus>()
                .HasOne(x => x.Player)
                .WithMany(x => x.RoomMerchantConsumableSaleStatus)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomMerchantConsumableSaleStatus>()
                .HasOne(x => x.Merchant)
                .WithMany(x => x.RoomMerchantConsumableSaleStatus)
                .HasForeignKey(x => x.MerchantId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RoomMerchantConsumableSaleStatus>()
                .HasOne(x => x.Consumable)
                .WithMany(x => x.RoomMerchantConsumableSaleStatus)
                .HasForeignKey(x => x.ConsumableId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}