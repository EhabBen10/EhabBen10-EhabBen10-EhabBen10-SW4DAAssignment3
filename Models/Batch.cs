namespace SW4DAAssignment3.Models
{
    public class Batch
    {
        public int BatchId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public DateTime Target_Finish_Time { get; set; }

        public ICollection<BatchBackingGood>? BatchBackingGoods { get; set; }
        public ICollection<BatchIngredient>? BatchIngredients { get; set; }
    }
}