namespace SW4DAAssignment3.Models;

public class OrderBakingGood
{
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int BakingGoodId { get; set; }
    public BakingGood? BakingGood { get; set; }

    public int Quantity { get; set; }
}
