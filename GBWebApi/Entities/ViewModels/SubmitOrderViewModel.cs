namespace Entities.ViewModels
{
    public class SubmitOrderViewModel
    {
        public string CustomerName { get; set; }        
        public List<ItensOrderSubmit> Itens { get; set; }
    }

    public class ItensOrderSubmit
    {
        public int IdProduct { get; set; }
    }
}
