using Entities.Enums;
using Entities.Tables;

namespace Domain.Interfaces.Repository
{
    public interface IMenuRepository
    {
        List<Products> ListAllProducts();
        List<Products> ListSandwichsByTypes(TypeProductsEnum type);
    }
}
