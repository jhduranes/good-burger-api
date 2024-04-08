using Domain.Interfaces.Repository;
using Entities.Tables;
using Entities.ViewModels;
using Infrastructure.Configuration;

namespace Infrastructure.Repository
{
    public class MenuRepository: IMenuRepository
    {
        private readonly DatabaseContext _databaseContext;
        MessageViewModel message = new MessageViewModel();

        public MenuRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public MessageViewModel AddMenuItem(Products products)
        {
            try
            {
                _databaseContext.Products.Add(products);
                _databaseContext.SaveChanges();

                message.SuccessMessage = "Registration completed successfully";
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = true;                
            }
            catch (Exception ex)
            {
                message.FailedMessage = $"There was a registration error {ex.Message}";
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = false;
                return message;
            }
            return message;
        }

        public List<Products> ListAllMenuItens()
        {
            return _databaseContext.Products.ToList();
        }

        public List<Products> ListMenuItensByTypes(string type)
        {
            return _databaseContext.Products.Where(p => p.Type == type).ToList();
        }


    }
}
