using Entities.Enums;

namespace Entities.ViewModels
{
    public class ProductInsertViewModel
    {        
        public string Description { get; set; }
        public TypeProductsEnum Type { get; set; }
        public decimal Price { get; set; }
    }
}
