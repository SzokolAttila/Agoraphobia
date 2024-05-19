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
        public DbSet<ArmorInventory> ArmorInventories { get; set; }
        public DbSet<WeaponInventory> WeaponInventories { get; set; }
        public DbSet<ConsumableInventory> ConsumableInventories { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Consumable> Consumables { get; set; }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<ArmorDroprate> ArmorDroprates { get; set; }
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
                
            builder.Entity<ArmorDroprate>(x => x.HasKey(p => new { p.EnemyId, p.ArmorId }));
            builder.Entity<ArmorDroprate>()
                .HasOne(p => p.Enemy)
                .WithMany(x => x.ArmorDroprates)
                .HasForeignKey(x => x.EnemyId);

            builder.Entity<ArmorDroprate>()
                .HasOne(p => p.Armor)
                .WithMany(x => x.ArmorDroprates)
                .HasForeignKey(x => x.ArmorId);
        }
    }
}