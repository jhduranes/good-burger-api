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

        public List<Products> ListAllMenuItens()
        {
            return _databaseContext.Products.ToList();
        }

        public List<Products> ListMenuItensByTypes(string type)
        {
            return _databaseContext.Products.Where(p => p.Type == type).ToList();
        }

        public Products GetProductById(int id)
        {
            return _databaseContext.Products.FirstOrDefault(x => x.Id == id);            
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

        public MessageViewModel RemoveItemMenu(Products products)
        {
            try
            {
                _databaseContext.Products.Remove(products);
                _databaseContext.SaveChanges();

                message.SuccessMessage = "Item Menu deleted successfully.";
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = true;
            }
            catch (Exception ex)
            {
                message.FailedMessage = $"There was an error in the deletion process: {ex.Message}";
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = false;
                return message;
            }
            return message;
        }

        public MessageViewModel UpdateItemMenu(Products products)
        {
            try
            {
                _databaseContext.Products.Update(products);
                _databaseContext.SaveChanges();

                message.SuccessMessage = "Item Menu updated successfully.";
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = true;
            }
            catch (Exception ex)
            {
                message.FailedMessage = $"There was an error in the updated process: {ex.Message}";
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = false;
                return message;
            }
            return message;
        }
    }
}
