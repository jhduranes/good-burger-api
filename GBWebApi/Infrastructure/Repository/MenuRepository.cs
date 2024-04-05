using Domain.Interfaces.Repository;
using Entities.Enums;
using Entities.Tables;
using Infrastructure.Configuration;
using System.Linq;

namespace Infrastructure.Repository
{
    public class MenuRepository: IMenuRepository
    {
        private readonly DatabaseContext _databaseContext;

        public MenuRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
             
        public List<Products> ListAllProducts()
        {
            return _databaseContext.Products.ToList();
        }

        public List<Products> ListSandwichsByTypes(TypeProductsEnum type)
        {
            return _databaseContext.Products.Where(p => p.Type == type).ToList();
        }
    }
}
