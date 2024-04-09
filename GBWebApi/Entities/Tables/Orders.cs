namespace Entities.Tables
{
    public class Orders
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}
