using Microsoft.EntityFrameworkCore;
using SW4DAAssignment3.Models;

namespace SW4DAAssignment3.Data
{
    public class BakeryDBcontext : DbContext
    {
        public BakeryDBcontext(DbContextOptions<BakeryDBcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderBakingGood>()
                .HasKey(obg => new { obg.OrderId, obg.BakingGoodId });

            modelBuilder.Entity<OrderBakingGood>()
                .HasOne(obg => obg.Order)
                .WithMany(o => o.OrderBakingGoods)
                .HasForeignKey(obg => obg.OrderId);

            modelBuilder.Entity<OrderBakingGood>()
                .HasOne(obg => obg.BakingGood)
                .WithMany(bg => bg.OrderBakingGoods)
                .HasForeignKey(obg => obg.BakingGoodId);

            modelBuilder.Entity<BatchIngredient>()
                .HasKey(bi => new { bi.BatchId, bi.IngredientId });

            modelBuilder.Entity<BatchIngredient>()
                .HasOne(bi => bi.Batch)
                .WithMany(b => b.BatchIngredients)
                .HasForeignKey(bi => bi.BatchId);

            modelBuilder.Entity<BatchIngredient>()
                .HasOne(bi => bi.Ingredient)
                .WithMany(i => i.BatchIngredients)
                .HasForeignKey(bi => bi.IngredientId);

            modelBuilder.Entity<BatchBackingGood>()
                .HasKey(bbg => new { bbg.BatchId, bbg.BakingGoodId });

            modelBuilder.Entity<BatchBackingGood>()
                .HasOne(bbg => bbg.Batch)
                .WithMany(b => b.BatchBackingGoods)
                .HasForeignKey(bbg => bbg.BatchId);

            modelBuilder.Entity<BatchBackingGood>()
                .HasOne(bbg => bbg.BakingGood)
                .WithMany(bg => bg.BatchBackingGoods)
                .HasForeignKey(bbg => bbg.BakingGoodId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Batch)
                .WithOne(b => b.Order)
                .HasForeignKey<Batch>(b => b.OrderId);

            modelBuilder.Entity<OrderSupermarket>()
                .HasKey(os => new { os.OrderId, os.SupermarketId });

            modelBuilder.Entity<OrderSupermarket>()
                .HasOne(os => os.Order)
                .WithMany(o => o.OrderSupermarkets)
                .HasForeignKey(os => os.OrderId);

            modelBuilder.Entity<OrderSupermarket>()
                .HasOne(os => os.Supermarket)
                .WithMany(s => s.OrderSupermarkets)
                .HasForeignKey(os => os.SupermarketId);

        }
        public DbSet<BakingGood> BakingGoods { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBakingGood> OrderBakingGoods { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BatchIngredient> BatchIngredients { get; set; }
        public DbSet<BatchBackingGood> BatchBackingGoods { get; set; }
    }
}