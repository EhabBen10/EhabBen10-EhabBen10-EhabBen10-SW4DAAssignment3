namespace SW4DAAssignment3.Models
{
    public class Supermarket
    {
        public int SupermarketId { get; set; }
        public string? Name { get; set; }
        public string? offload_location { get; set; }
        public int track_id { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}