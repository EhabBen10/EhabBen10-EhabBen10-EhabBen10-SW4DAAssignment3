
namespace SW4DAAssignment3.Models;

public class Order
{
    public int OrderId { get; set; }
    public string? DeliveryPlace { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string? ValidityPeriod { get; set; }
    public ICollection<OrderBakingGood>? OrderBakingGoods { get; set; }
    public Batch? Batch { get; set; }
}
