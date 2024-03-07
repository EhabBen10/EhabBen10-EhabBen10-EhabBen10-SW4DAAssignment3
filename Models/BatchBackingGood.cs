namespace SW4DAAssignment3.Models
{
    public class BatchBackingGood
    {
        public int BatchId { get; set; }
        public Batch? Batch { get; set; }
        public int BakingGoodId { get; set; }
        public BakingGood? BakingGood { get; set; }

    }
}