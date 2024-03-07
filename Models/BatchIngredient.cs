namespace SW4DAAssignment3.Models;

public class BatchIngredient
{
    public int BatchId { get; set; }
    public Batch? Batch { get; set; }
    public int IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    public int Quantity { get; set; }
}
