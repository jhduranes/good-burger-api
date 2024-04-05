using Entities.Enums;

namespace Entities.Tables
{
    public class Products
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TypeProductsEnum Type { get; set; }
        public decimal Price { get; set; }
    }
}
