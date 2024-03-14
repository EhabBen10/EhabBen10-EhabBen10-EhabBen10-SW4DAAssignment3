namespace SW4DAAssignment3.Models;

public class Ingredient
{
    public int IngredientId { get; set; }
    public string? Name { get; set; }
    [QuantityValidator(ErrorMessage = "Quantity must be a non-negative value.")]
    public int StockQuantity { get; set; }
    public ICollection<BatchIngredient>? BatchIngredients { get; set; }
    public ICollection<IngredientAllergen>? IngredientAllergens { get; set; }
}