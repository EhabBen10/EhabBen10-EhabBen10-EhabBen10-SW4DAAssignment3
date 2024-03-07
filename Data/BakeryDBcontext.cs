using Microsoft.EntityFrameworkCore;
using SW4DAAssignment3.Models;

namespace SW4DAAssignment3.Data
{
    public class BakeryDBcontext : DbContext
    {
        public BakeryDBcontext(DbContextOptions<BakeryDBcontext> options) : base(options)
        {
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