namespace SW4DAAssignment3.Models;

public class OrderSupermarket
{
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int SupermarketId { get; set; }
    public Supermarket? Supermarket { get; set; }
}