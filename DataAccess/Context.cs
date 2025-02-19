using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; } 
        public DbSet<Item> Items { get; set; }
        public DbSet<CardItem> CardItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CardItem>()
                .HasOne(ci => ci.Item)  // CardItem'da bir Item var
                .WithMany(i => i.CardItems)  // Item birden fazla CardItem'a sahip olabilir
                .HasForeignKey(ci => ci.ItemId)  // Foreign key'in ItemId olduğunu belirt
                .OnDelete(DeleteBehavior.Cascade);  // Silme işlemi sırasında ilişkili verinin silinmesini sağlar

            modelBuilder.Entity<CardItem>()
               .HasOne(ci => ci.Card)  // CardItem'da bir Card var
               .WithMany(c => c.CardItems)  // Card birden fazla CardItem'a sahip olabilir
               .HasForeignKey(ci => ci.CardId)  // Foreign key'in CardId olduğunu belirt
               .OnDelete(DeleteBehavior.Cascade);  // Silme işlemi sırasında ilişkili verinin silinmesini sağlar
        }

    }
}
