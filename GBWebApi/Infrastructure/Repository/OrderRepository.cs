using Domain.Interfaces.Repository;
using Entities.Tables;
using Entities.ViewModels;
using Infrastructure.Configuration;

namespace Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _databaseContext;
        MessageViewModel message = new MessageViewModel();

        public OrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public MessageViewModel SubmitOrder(Orders order, List<ItensOrder> itens)
        {
            try
            {
                var data_Order = _databaseContext.Orders.Update(order);
                _databaseContext.SaveChanges();

                var idOrder = data_Order.Entity.Id;
                foreach (var i in itens)
                {
                    i.IdOrder = idOrder;
                    _databaseContext.ItensOrder.Add(i);
                }
                _databaseContext.SaveChanges();

                message.SuccessMessage = idOrder.ToString();
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

        public MessageViewModel UpdateOrder(Orders order, List<ItensOrder> itens)
        {
            try
            {
                var data_Order = _databaseContext.Orders.Update(order);                 
                foreach (var i in itens)
                {
                    i.IdOrder = order.Id;
                    _databaseContext.ItensOrder.Add(i);
                }
                _databaseContext.SaveChanges();

                message.SuccessMessage = order.Id.ToString();
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

        public List<Orders> ListAllOrders()
        {
            return _databaseContext.Orders.ToList();    
        }

        public Orders GetOrderById(int id)
        {
            return _databaseContext.Orders.FirstOrDefault(x => x.Id == id);
        }

        public List<ItensOrder> ListItensOrdersById(int id)
        {
            return _databaseContext.ItensOrder.Where(x => x.IdOrder == id).ToList();
        }

        public MessageViewModel RemoveOrder(Orders order, List<ItensOrder> itens)
        {
            try
            {
                _databaseContext.Orders.Remove(order);
                foreach(var i in itens)
                {
                    _databaseContext.ItensOrder.Remove(i);
                }
                _databaseContext.SaveChanges();

                message.SuccessMessage = "Order deleted successfully.";
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

        public MessageViewModel RemoveItensOrder(List<ItensOrder> itens)
        {
            try
            {
                foreach (var i in itens)
                {
                    _databaseContext.ItensOrder.Remove(i);
                }
                _databaseContext.SaveChanges();

                message.SuccessMessage = "Itens deleted successfully.";
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = true;
            }
            catch (Exception ex)
            {
                message.FailedMessage = ex.Message;
                message.DateTimeReturn = DateTime.Now;
                message.PerformedService = false;
                return message;
            }
            return message;
        }
    }
}
