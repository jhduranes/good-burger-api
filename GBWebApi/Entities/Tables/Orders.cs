namespace Entities.Tables
{
    public class Orders
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }        
    }
}
