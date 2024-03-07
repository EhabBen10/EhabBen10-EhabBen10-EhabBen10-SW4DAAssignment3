using Microsoft.AspNetCore.Mvc;
using SW4DAAssignment3.Data;
using SW4DAAssignment3.Models;

namespace SW4DAAssignment3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly BakeryDBcontext _context;

        public SeedController(BakeryDBcontext context)
        {
            _context = context;
        }
        [HttpPut(Name = "Seed")]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> Put()
        {

            // Check if the ingredients already exist
            if (!_context.Ingredients.Any())
            {
                _context.Ingredients.AddRange(
                    new Ingredient { Name = "Sugar", StockQuantity = 100000 },
                    new Ingredient { Name = "Flour", StockQuantity = 242000 },
                    new Ingredient { Name = "Salt", StockQuantity = 10000 }
                );
                // Check if the order already exists
                if (!_context.Orders.Any())
                {
                    var order = new Order
                    {
                        DeliveryPlace = "Finlandsgade 17, 8200 Aarhus N",
                        DeliveryDate = DateTime.Parse("2024-05-03"),
                        ValidityPeriod = "Your Validity Period"
                    };

                    _context.Orders.Add(order);

                    var bakingGoods = new List<BakingGood>
                    {
                        new BakingGood { Name = "Alexandertorte" },
                        new BakingGood { Name = "Butter cookies" },
                        new BakingGood { Name = "Studenterbrød" },
                        new BakingGood { Name = "Romkugler" }
                    };

                    _context.BakingGoods.AddRange(bakingGoods);

                    await _context.SaveChangesAsync();

                    var orderBakingGoods = new List<OrderBakingGood>
                    {
                        new OrderBakingGood { OrderId = order.OrderId, BakingGoodId = bakingGoods[0].BakingGoodId, Quantity = 300 },
                        new OrderBakingGood { OrderId = order.OrderId, BakingGoodId = bakingGoods[1].BakingGoodId, Quantity = 100 },
                        new OrderBakingGood { OrderId = order.OrderId, BakingGoodId = bakingGoods[2].BakingGoodId, Quantity = 100 },
                        new OrderBakingGood { OrderId = order.OrderId, BakingGoodId = bakingGoods[3].BakingGoodId, Quantity = 200 }
                    };

                    _context.OrderBakingGoods.AddRange(orderBakingGoods);
                }

                await _context.SaveChangesAsync();
            }

            return Ok();

        }
    }
}