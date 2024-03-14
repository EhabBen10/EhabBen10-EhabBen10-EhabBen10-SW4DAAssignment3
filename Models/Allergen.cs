namespace SW4DAAssignment3.Models;

public class Allergen
{
    public int AllergenId { get; set; }
    public string Name { get; set; }

    public ICollection<IngredientAllergen> IngredientAllergens { get; set; }
}