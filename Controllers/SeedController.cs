using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            // // Check if the ingredients already exist
            if (!_context.Ingredients.Any())
            {
                _context.Ingredients.AddRange(
                    new Ingredient { Name = "Sugar", StockQuantity = 100000 },
                    new Ingredient { Name = "Flour", StockQuantity = 242000 },
                    new Ingredient { Name = "Salt", StockQuantity = 10000 }
                );
            }
            // Check if the order already exists
            if (!_context.Orders.Any())
            {
                var order = new Order
                {
                    DeliveryPlace = "Finlandsgade 17, 8200 Aarhus N",
                    DeliveryDate = Convert.ToString(DateTime.Parse("2024-05-03")),
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
                await _context.SaveChangesAsync();
            }
            if (!_context.Batches.Any())
            {
                var startTime = DateTime.Now;
                var Target_Finish_Time = startTime.AddMinutes(20);
                var actualFinishTime = startTime.AddMinutes(40);

                var batch = new Batch
                {
                    OrderId = 1,
                    StartTime = startTime,
                    FinishTime = actualFinishTime,
                    Target_Finish_Time = Target_Finish_Time
                };
                _context.Batches.Add(batch);
                _context.SaveChanges();
                var ingredients = new List<Ingredient>
                            {
                                new Ingredient {  Name = "Leftover cake", StockQuantity = 5000 },
                                new Ingredient {  Name = "Raspberry jam", StockQuantity = 30 },
                                new Ingredient { Name = "Cocoa", StockQuantity = 20 },
                                new Ingredient { Name = "Rum", StockQuantity = 30 },
                                new Ingredient {  Name = "Coconut flakes", StockQuantity = 0 }
                            };
                _context.Ingredients.AddRange(ingredients);
                await _context.SaveChangesAsync();
                var batchIngredients = new List<BatchIngredient>
                        {
                            new BatchIngredient { BatchId = batch.BatchId,IngredientId = ingredients[0].IngredientId, Quantity = 5000 },
                            new BatchIngredient { BatchId = batch.BatchId, IngredientId = ingredients[1].IngredientId, Quantity = 30 },
                            new BatchIngredient {  BatchId = batch.BatchId,IngredientId = ingredients[2].IngredientId, Quantity = 20 },
                            new BatchIngredient { BatchId = batch.BatchId, IngredientId = ingredients[3].IngredientId, Quantity = 30 },
                            new BatchIngredient { BatchId = batch.BatchId, IngredientId = ingredients[4].IngredientId, Quantity = 0 }
                        };
                _context.BatchIngredients.AddRange(batchIngredients);
                await _context.SaveChangesAsync();
            }
            if (!_context.Supermarkets.Any())
            {
                var supermarkets = new List<Supermarket>
                {
                    new Supermarket { track_id = 12022, Name = "Fakta", offload_location = "Randersvej 61, 8200 Aarhus N"},
                    new Supermarket { track_id = 11031, Name = "Netto", offload_location = "Randersvej 61, 8200 Aarhus N" }
                };
                _context.Supermarkets.AddRange(supermarkets);
                await _context.SaveChangesAsync();

                var faktaId = supermarkets.First(s => s.Name == "Fakta").SupermarketId;
                var nettoId = supermarkets.First(s => s.Name == "Netto").SupermarketId;
                var orderSupermarkets = new List<OrderSupermarket>
                {
                    new OrderSupermarket { OrderId = 1, SupermarketId = faktaId },
                    new OrderSupermarket { OrderId = 1, SupermarketId = nettoId }
                };
                _context.OrderSupermarkets.AddRange(orderSupermarkets);
                _context.SaveChanges();
            }

            // Insert into Order
            var order1 = new Order
            {
                DeliveryPlace = "Finlandsgade 17, 8200 Aarhus N",
                DeliveryDate = Convert.ToString(DateTime.Parse("2024-05-04")),
                ValidityPeriod = "Your Validity Period"
            };
            _context.Orders.Add(order1);
            _context.SaveChanges();

            // Get baking good ids
            var alexandertorteId = _context.BakingGoods.FirstOrDefault(bg => bg.Name == "Alexandertorte")!.BakingGoodId;
            var butterCookiesId = _context.BakingGoods.FirstOrDefault(bg => bg.Name == "Butter cookies")!.BakingGoodId;
            var studenterbrødId = _context.BakingGoods.FirstOrDefault(bg => bg.Name == "Studenterbrød")!.BakingGoodId;
            var romkuglerId = _context.BakingGoods.FirstOrDefault(bg => bg.Name == "Romkugler")!.BakingGoodId;

            // Insert into Order_BakingGood
            var orderBakingGoods1 = new List<OrderBakingGood>
                {
                    new OrderBakingGood { OrderId = order1.OrderId, BakingGoodId = alexandertorteId, Quantity = 2900 },
                    new OrderBakingGood { OrderId = order1.OrderId, BakingGoodId = butterCookiesId, Quantity = 1400 },
                    new OrderBakingGood { OrderId = order1.OrderId, BakingGoodId = studenterbrødId, Quantity = 1900 },
                    new OrderBakingGood { OrderId = order1.OrderId, BakingGoodId = romkuglerId, Quantity = 700 }
                };

            _context.OrderBakingGoods.AddRange(orderBakingGoods1);
            _context.SaveChanges();

            var order2 = new Order
            {
                DeliveryPlace = "order3 17, 8200 Aarhus N",
                DeliveryDate = Convert.ToString(DateTime.Parse("2024-05-03")),
                ValidityPeriod = "Your Validity Period"
            };
            _context.Orders.Add(order2);
            _context.SaveChanges();


            await _context.SaveChangesAsync();
            var startTime1 = DateTime.Now;
            var Target_Finish_Time1 = startTime1.AddMinutes(20);
            var actualFinishTime1 = Target_Finish_Time1.AddMinutes(10);

            var batch1 = new Batch
            {
                OrderId = 2,
                StartTime = startTime1,
                FinishTime = actualFinishTime1,
                Target_Finish_Time = Target_Finish_Time1
            };
            _context.Batches.Add(batch1);
            _context.SaveChanges();

            actualFinishTime1 = Target_Finish_Time1.AddMinutes(30);

            var batch2 = new Batch
            {
                OrderId = 3,
                StartTime = startTime1,
                FinishTime = actualFinishTime1,
                Target_Finish_Time = Target_Finish_Time1
            };
            _context.Batches.Add(batch2);
            _context.SaveChanges();

            AddAllenrgensToIngredients();
            seedGPScoordinates();
            return Ok();

        }
        private void AddAllenrgensToIngredients()
        {
            var allergens = new List<Allergen>
            {
                new Allergen { Name = "Gluten" },
                new Allergen { Name = "Lactose" },
                new Allergen { Name = "Fructose" },
            };
            _context.Allergens.AddRange(allergens);
            _context.SaveChanges();

            var glutenId = allergens.First(a => a.Name == "Gluten").AllergenId;
            var lactoseId = allergens.First(a => a.Name == "Lactose").AllergenId;
            var fructoseId = allergens.First(a => a.Name == "Fructose").AllergenId;


            var cake = _context.Ingredients.FirstOrDefault(i => i.Name == "Leftover cake");
            var jam = _context.Ingredients.FirstOrDefault(i => i.Name == "Raspberry jam");
            var cocoa = _context.Ingredients.FirstOrDefault(i => i.Name == "Cocoa");
            var rum = _context.Ingredients.FirstOrDefault(i => i.Name == "Rum");
            var coconut = _context.Ingredients.FirstOrDefault(i => i.Name == "Coconut flakes");

            var cakeAllergens = new List<IngredientAllergen>
            {
                new IngredientAllergen { IngredientId = cake!.IngredientId, AllergenId = glutenId },
                new IngredientAllergen { IngredientId = cake.IngredientId, AllergenId = lactoseId },
                new IngredientAllergen { IngredientId = jam!.IngredientId, AllergenId = fructoseId },
                new IngredientAllergen { IngredientId = cocoa!.IngredientId, AllergenId = lactoseId },
                new IngredientAllergen { IngredientId = rum!.IngredientId, AllergenId = fructoseId },
                new IngredientAllergen { IngredientId = coconut!.IngredientId, AllergenId = fructoseId }
            };
            _context.IngredientAllergens.AddRange(cakeAllergens);
            _context.SaveChanges();
        }

        private void seedGPScoordinates()
        {
            var supermarkets = _context.Supermarkets.ToList();

            var fakta = supermarkets.First(s => s.Name == "Fakta");
            var netto = supermarkets.First(s => s.Name == "Netto");

            fakta.GPScoordinates = "56.162939 10.203921";
            netto.GPScoordinates = "26.142929 11.203921";
            _context.SaveChanges();
        }
    }
}