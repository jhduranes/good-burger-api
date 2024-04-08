using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Entities.Tables;
using Entities.ViewModels;

namespace Service.Services
{
    public class MenuService : IMenuService
    {
        private IMenuRepository _repository;
        MessageViewModel message = new MessageViewModel();

        public MenuService(IMenuRepository repository)
        {
            _repository = repository;
        }

        public MessageViewModel AddMenuItem(ProductInsertViewModel productVM)
        {

            if (string.IsNullOrEmpty(productVM.Description))
            {
                message.FailedMessage = "The item description field is mandatory.";
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = false;
                return message;
            }

            if ((productVM.Price < 1))
            {
                message.FailedMessage = "Value in the price field is invalid.";
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = false;
                return message;
            }

            Products products = new Products();
            products.Price = productVM.Price;
            products.Description = productVM.Description;
            if (productVM.Type == Entities.Enums.TypeProductsEnum.Sandwich)
            {
                products.Type = "Sandwich";
            }
            else
            {
                products.Type = "Extra";
            }
            message = _repository.AddMenuItem(products);
            return message;            
        }

        public List<Products> ListAllMenuItens()
        {
            return _repository.ListAllMenuItens();            
        }        

        public List<Products> ListExtras()
        {
            return _repository.ListMenuItensByTypes("Extra");
        }

        public List<Products> ListSandwichs()
        {
            return _repository.ListMenuItensByTypes("Sandwich");
        }
    }
}
