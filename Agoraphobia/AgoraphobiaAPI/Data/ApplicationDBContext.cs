using AgoraphobiaLibrary;
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ArmorInventory>(x => x.HasKey(y => new { y.PlayerId, y.ArmorId }));
            builder.Entity<ArmorInventory>()
                .HasOne(x => x.Player)
                .WithMany(x => x.ArmorInventories)
                .HasForeignKey(x => x.PlayerId);       
            
            builder.Entity<ArmorInventory>()
                .HasOne(x => x.Armor)
                .WithMany(x => x.ArmorInventories)
                .HasForeignKey(x => x.ArmorId);

            
            builder.Entity<WeaponInventory>(x => x.HasKey(y => new { y.PlayerId, y.WeaponId }));
            builder.Entity<WeaponInventory>()
                .HasOne(x => x.Player)
                .WithMany(x => x.WeaponInventories)
                .HasForeignKey(x => x.PlayerId);       
            
            builder.Entity<WeaponInventory>()
                .HasOne(x => x.Weapon)
                .WithMany(x => x.WeaponInventories)
                .HasForeignKey(x => x.WeaponId);
            
            builder.Entity<ConsumableInventory>(x => x.HasKey(y => new { y.PlayerId, y.ConsumableId }));
            builder.Entity<ConsumableInventory>()
                .HasOne(x => x.Player)
                .WithMany(x => x.ConsumableInventories)
                .HasForeignKey(x => x.PlayerId);       
            
            builder.Entity<ConsumableInventory>()
                .HasOne(x => x.Consumable)
                .WithMany(x => x.ConsumableInventories)
                .HasForeignKey(x => x.ConsumableId);

            builder.Entity<ArmorLoot>(x => x.HasKey(y => new { y.RoomId, y.ArmorId }));
            builder.Entity<ArmorLoot>()
                .HasOne(x => x.Room)
                .WithMany(x => x.Armors)
                .HasForeignKey(x => x.RoomId);

            builder.Entity<ArmorLoot>()
                .HasOne(x => x.Armor)
                .WithMany(x => x.ArmorLoots)
                .HasForeignKey(x => x.ArmorId);


            builder.Entity<WeaponLoot>(x => x.HasKey(y => new { y.RoomId, y.WeaponId }));
            builder.Entity<WeaponLoot>()
                .HasOne(x => x.Room)
                .WithMany(x => x.Weapons)
                .HasForeignKey(x => x.RoomId);

            builder.Entity<WeaponLoot>()
                .HasOne(x => x.Weapon)
                .WithMany(x => x.WeaponLoots)
                .HasForeignKey(x => x.WeaponId);

            builder.Entity<ConsumableLoot>(x => x.HasKey(y => new { y.RoomId, y.ConsumableId }));
            builder.Entity<ConsumableLoot>()
                .HasOne(x => x.Room)
                .WithMany(x => x.Consumables)
                .HasForeignKey(x => x.RoomId);

            builder.Entity<ConsumableLoot>()
                .HasOne(x => x.Consumable)
                .WithMany(x => x.ConsumableLoots)
                .HasForeignKey(x => x.ConsumableId);

            builder.Entity<ArmorDroprate>(x => x.HasKey(p => new { p.EnemyId, p.ArmorId }));
            builder.Entity<ArmorDroprate>()
                .HasOne(p => p.Enemy)
                .WithMany(x => x.ArmorDroprates)
                .HasForeignKey(x => x.EnemyId);

            builder.Entity<ArmorDroprate>()
                .HasOne(p => p.Armor)
                .WithMany(x => x.ArmorDroprates)
                .HasForeignKey(x => x.ArmorId);

            builder.Entity<WeaponDroprate>(x => x.HasKey(p => new { p.EnemyId, p.WeaponId }));
            builder.Entity<WeaponDroprate>()
                .HasOne(p => p.Enemy)
                .WithMany(x => x.WeaponDroprates)
                .HasForeignKey(x => x.EnemyId);

            builder.Entity<WeaponDroprate>()
                .HasOne(p => p.Weapon)
                .WithMany(x => x.WeaponDroprates)
                .HasForeignKey(x => x.WeaponId);

            builder.Entity<ConsumableDroprate>(x => x.HasKey(p => new { p.EnemyId, p.ConsumableId }));
            builder.Entity<ConsumableDroprate>()
                .HasOne(p => p.Enemy)
                .WithMany(x => x.ConsumableDroprates)
                .HasForeignKey(x => x.EnemyId);

            builder.Entity<ConsumableDroprate>()
                .HasOne(p => p.Consumable)
                .WithMany(x => x.ConsumableDroprates)
                .HasForeignKey(x => x.ConsumableId);

            builder.Entity<Effect>(x => x.HasKey(y => new { y.PlayerId, y.ConsumableId }));
            builder.Entity<Effect>()
                .HasOne(x => x.Player)
                .WithMany(x => x.Effects)
                .HasForeignKey(x => x.PlayerId);

            builder.Entity<Effect>()
                .HasOne(x => x.Consumable)
                .WithMany(x => x.Effects)
                .HasForeignKey(x => x.ConsumableId);
        }
    }
}