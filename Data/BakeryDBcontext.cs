using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SW4DAAssignment3.Models;

namespace SW4DAAssignment3.Data
{
    public class BakeryDBcontext : IdentityDbContext<BakeryUser>
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

            modelBuilder.Entity<IngredientAllergen>()
                .HasKey(ia => new { ia.IngredientId, ia.AllergenId });

            modelBuilder.Entity<IngredientAllergen>()
                .HasOne(ia => ia.Ingredient)
                .WithMany(i => i.IngredientAllergens)
                .HasForeignKey(ia => ia.IngredientId);

            modelBuilder.Entity<IngredientAllergen>()
                .HasOne(ia => ia.Allergen)
                .WithMany(a => a.IngredientAllergens)
                .HasForeignKey(ia => ia.AllergenId);

            modelBuilder.Entity<BakeryUser>().ToTable("BakeryUser");
            modelBuilder.Entity<IdentityRole>()
            .ToTable("ApiRoles")
            .HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("BakeryRoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("BakeryUserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("BakeryUserLogins")
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("BakeryUserRoles")
                .HasKey(r => new { r.UserId, r.RoleId });

            modelBuilder.Entity<IdentityUserToken<string>>() // Add these lines
                .ToTable("BakeryUserTokens")
                .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        }

        public DbSet<BakingGood> BakingGoods { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBakingGood> OrderBakingGoods { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BatchIngredient> BatchIngredients { get; set; }
        public DbSet<BatchBackingGood> BatchBackingGoods { get; set; }

        public DbSet<Supermarket> Supermarkets { get; set; }

        public DbSet<OrderSupermarket> OrderSupermarkets { get; set; }

        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<IngredientAllergen> IngredientAllergens { get; set; }
    }
}