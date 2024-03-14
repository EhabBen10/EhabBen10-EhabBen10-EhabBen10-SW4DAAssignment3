namespace SW4DAAssignment3.Models;

public class IngredientAllergen
{
    public int IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    public int AllergenId { get; set; }
    public Allergen? Allergen { get; set; }
}
