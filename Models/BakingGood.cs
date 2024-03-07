namespace SW4DAAssignment3.Models;

public class BakingGood
{
    public int BakingGoodId { get; set; }
    public string? Name { get; set; }

    public ICollection<OrderBakingGood>? OrderBakingGoods { get; set; }
}

