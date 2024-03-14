using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SW4DAAssignment3.Data;
using SW4DAAssignment3.Models;

namespace SW4DAAssignment3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly BakeryDBcontext _context; // replace YourDbContext with the actual name of your DbContext

        public QueryController(BakeryDBcontext context) // replace YourDbContext with the actual name of your DbContext
        {
            _context = context;
        }

        [HttpGet("getIngredients")]
        public ActionResult<List<Ingredient>> GetIngredients()
        {
            var ingredientNames = new List<string> { "sugar", "flour", "salt" };

            var existingIngredients = _context.Ingredients
                .Where(i => ingredientNames.Contains(i.Name!))
                .ToList();

            if (!existingIngredients.Any())
            {
                return NotFound("Ingredients not found in the database.");
            }

            return Ok(existingIngredients);
        }

        [HttpGet("getOrder/{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Orders
                .Where(o => o.OrderId == id)
                .Select(o => new { o.DeliveryPlace, o.DeliveryDate, o.ValidityPeriod })
                .FirstOrDefault();

            if (order == null)
            {
                return NotFound("Order not found in the database.");
            }

            return Ok(order);
        }

        [HttpGet("getOrderBakingGoods/{orderId}")]
        public ActionResult<List<OrderBakingGood>> GetOrderBakingGoods(int orderId)
        {
            var orderBakingGoods = _context.OrderBakingGoods
                .Include(obg => obg.BakingGood)
                .Where(obg => obg.OrderId == orderId)
                .Select(obg => new { obg.BakingGood!.Name, obg.Quantity })
                .ToList();

            if (!orderBakingGoods.Any())
            {
                return NotFound("No baking goods found for the given order ID.");
            }

            return Ok(orderBakingGoods);
        }


        [HttpGet("getBatchIngredients/{batchId}")]
        public ActionResult<List<BatchIngredient>> GetBatchIngredients(int batchId)
        {
            var batchIngredients = _context.BatchIngredients
                .Include(bi => bi.Ingredient)
                .Where(bi => bi.BatchId == batchId)
                .Select(bi => new { bi.Ingredient!.Name, bi.Quantity })
                .ToList();

            if (!batchIngredients.Any())
            {
                return NotFound("No ingredients found for the given batch ID.");
            }

            return Ok(batchIngredients);
        }


        [HttpGet("getBatchIngredientsWithAllergen/{batchId}")]
        public ActionResult<List<BatchIngredient>> GetBatchIngredientsWithAllergen(int batchId)
        {
            var batchIngredients = _context.BatchIngredients?
                .Include(bi => bi.Ingredient)!
                .ThenInclude(i => i!.IngredientAllergens)!
                .ThenInclude(ia => ia.Allergen)
                .Where(bi => bi.BatchId == batchId)
                .Select(bi => new
                {
                    IngredientName = bi.Ingredient!.Name,
                    Quantity = bi.Quantity,
                    Allergens = bi.Ingredient.IngredientAllergens!.Select(ia => ia.Allergen!.Name).ToList()
                })
                .ToList();

            if (!batchIngredients!.Any())
            {
                return NotFound("No ingredients found for the given batch ID.");
            }

            return Ok(batchIngredients);
        }

        [HttpGet("getSupermarketTrackId/{orderId}")]
        public ActionResult GetSupermarketTrackId(int orderId)
        {
            var supermarketTrackId = _context.Orders
                .Where(o => o.OrderId == orderId)
                .SelectMany(o => o.OrderSupermarkets!)
                .Select(s => s.Supermarket!.track_id)
                .ToList();

            if (supermarketTrackId == null)
            {
                return NotFound("No supermarket found for the given order ID.");
            }

            return Ok(supermarketTrackId);
        }

        [HttpGet("getSupermarketTrackIdWithLocation/{orderId}")]
        public ActionResult GetSupermarketTrackIdWithLocation(int orderId)
        {
            var supermarketTrackId = _context.Orders
                .Where(o => o.OrderId == orderId)
                .SelectMany(o => o.OrderSupermarkets!)
                .Select(s => new
                {
                    s.Supermarket!.track_id,
                    s.Supermarket.offload_location,
                    s.Supermarket.GPScoordinates
                })
                .ToList();

            if (supermarketTrackId == null)
            {
                return NotFound("No supermarket found for the given order ID.");
            }

            return Ok(supermarketTrackId);
        }

        [HttpGet("getTotalQuantity")]
        public ActionResult<List<object>> GetTotalQuantity([FromQuery] List<int> orderIds)
        {
            var totalQuantities = _context.OrderBakingGoods
                .Include(obg => obg.BakingGood)
                .Where(obg => orderIds.Contains(obg.OrderId))
                .GroupBy(obg => obg.BakingGood!.Name)
                .Select(g => new { BakingGoodName = g.Key, TotalQuantity = g.Sum(obg => obg.Quantity) })
                .ToList();

            if (!totalQuantities.Any())
            {
                return NotFound("No baking goods found for the given order IDs.");
            }

            return Ok(totalQuantities);
        }

        [HttpGet("getAverageFinishTimeDifference")]
        public ActionResult GetAverageFinishTimeDifference()
        {
            var timeDifferences = _context.Batches
                .AsEnumerable()
                .Select(b => (b.FinishTime - b.Target_Finish_Time).TotalSeconds)
                .ToList();

            if (!timeDifferences.Any())
            {
                return NotFound("No batches found.");
            }

            var averageFinishTimeDifference = timeDifferences.Average();
            var averageTimeSpan = TimeSpan.FromSeconds(averageFinishTimeDifference);

            return Ok(averageTimeSpan);
        }

    }
}