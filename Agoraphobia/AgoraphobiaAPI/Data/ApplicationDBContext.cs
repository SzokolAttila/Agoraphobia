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
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Consumable> Consumables { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ArmorInventory>(x => x.HasKey(p => new { p.PlayerId, p.ArmorId }));
            builder.Entity<ArmorInventory>()
                .HasOne(p => p.Player)
                .WithMany(x => x.ArmorInventories)
                .HasForeignKey(x => x.PlayerId);       
            
            builder.Entity<ArmorInventory>()
                .HasOne(p => p.Armor)
                .WithMany(x => x.ArmorInventories)
                .HasForeignKey(x => x.ArmorId);
        }
    }
}