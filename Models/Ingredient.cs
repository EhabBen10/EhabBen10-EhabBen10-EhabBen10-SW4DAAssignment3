namespace SW4DAAssignment3.Models;

public class Ingredient
{
    public int IngredientId { get; set; }
    public string? Name { get; set; }
    public int StockQuantity { get; set; }
    public ICollection<BatchIngredient>? BatchIngredients { get; set; }
}
