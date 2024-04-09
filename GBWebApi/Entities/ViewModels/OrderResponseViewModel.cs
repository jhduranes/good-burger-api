namespace Entities.ViewModels
{
    public class OrderResponseViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<ItensOrderResponse> Itens { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }        
        public decimal Total { get; set; }
        public string Message { get; set; }
    }

    public class ItensOrderResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }        
        public decimal Price { get; set; }
    }
}
