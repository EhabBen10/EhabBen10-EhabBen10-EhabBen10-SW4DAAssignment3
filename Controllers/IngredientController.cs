using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SW4DAAssignment3.Data;
using SW4DAAssignment3.Models;

namespace SW4DAAssignment3.Controllers;

[Route("[controller]")]
[ApiController]
public class IngredientController : ControllerBase
{
    private readonly BakeryDBcontext _context;
    //TODO
    //der skal måske ikke stå querycontroller men IngredientController her
    private readonly ILogger<QueryController> _logger;

    public IngredientController(BakeryDBcontext context, ILogger<QueryController> logger)
    {
        _context = context;
        _logger = logger;
    }


    [HttpPost]
    [Route("AddIngredient")]
    [QuantityValidator]
    [Authorize(Roles = "Admin, Manager")]
    public IActionResult AddIngredient(string name, int quantity)
    {
        // Create a new ingredient object
        var ingredient = new Ingredient
        {
            Name = name,
            StockQuantity = quantity
        };

        // Validate the ingredient
        if (!TryValidateModel(ingredient))
        {
            return BadRequest(ModelState);
        }

        // Add the ingredient to the database
        _context.Ingredients.Add(ingredient);
        _context.SaveChanges();

        var timestamp = new DateTimeOffset(DateTime.UtcNow);
        var loginfo = new Loginfo
        {
            specificUser = User.Identity?.Name,
            Operation = "post Ingredient",
            Timestamp = timestamp.DateTime
        };
        _logger.LogInformation("Get called {@LogInfo} ", loginfo);

        return Ok($"Ingredient added successfully with id {ingredient.IngredientId}");
    }

    [HttpDelete(Name = "DeleteIngredient")]
    [ResponseCache(NoStore = true)]
    [Authorize(Roles = "Admin, Manager")]
    public IActionResult DeleteIngredient(int id)
    {
        // Find the ingredient by id
        var ingredient = _context.Ingredients.Find(id);

        if (ingredient == null)
        {
            return NotFound($"Ingredient with id {id} not found");
        }

        // Remove the ingredient from the database
        _context.Ingredients.Remove(ingredient);
        _context.SaveChanges();

        var timestamp = new DateTimeOffset(DateTime.UtcNow);
        var loginfo = new Loginfo
        {
            specificUser = User.Identity?.Name,
            Operation = "delete Ingredient",
            Timestamp = timestamp.DateTime
        };
        _logger.LogInformation("Get called {@LogInfo} ", loginfo);

        return Ok($"Ingredient with id {id} deleted successfully");
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public IActionResult UpdateIngredientQuantity(int id, int quantity)
    {
        // Find the ingredient by id
        var ingredient = _context.Ingredients.Find(id);

        if (ingredient == null)
        {
            return NotFound($"Ingredient with id {id} not found");
        }
        // Update the stock quantity
        ingredient.StockQuantity = quantity;

        // Validate the ingredient
        if (!TryValidateModel(ingredient))
        {
            return BadRequest(ModelState);
        }

        // Save the changes
        _context.SaveChanges();

        var timestamp = new DateTimeOffset(DateTime.UtcNow);
        var loginfo = new Loginfo
        {
            specificUser = User.Identity?.Name,
            Operation = "update Ingredient",
            Timestamp = timestamp.DateTime
        };
        _logger.LogInformation("Get called {@LogInfo} ", loginfo);

        return Ok($"Ingredient quantity updated successfully, the product name is {ingredient.Name} with quantitys {ingredient.StockQuantity}");
    }



}