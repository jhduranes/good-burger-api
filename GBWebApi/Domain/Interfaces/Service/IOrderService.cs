using Entities.Tables;
using Entities.ViewModels;

namespace Domain.Interfaces.Service
{
    public interface IOrderService
    {
        OrderResponseViewModel SubmitOrder(SubmitOrderViewModel order);
        List<OrderResponseViewModel> ListAllOrders();
        OrderResponseViewModel UpdateOrder(SubmitOrderViewModel order, int idOrder);
        MessageViewModel RemoveOrder(int idOrder);
    }
}
