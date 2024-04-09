using Entities.Tables;
using Entities.ViewModels;

namespace Domain.Interfaces.Repository
{
    public interface IOrderRepository
    {
        MessageViewModel SubmitOrder(Orders order, List<ItensOrder> itens);
        MessageViewModel UpdateOrder(Orders order, List<ItensOrder> itens);
        List<Orders> ListAllOrders();
        Orders GetOrderById(int id);
        List<ItensOrder> ListItensOrdersById(int id);        
        MessageViewModel RemoveOrder(Orders order, List<ItensOrder> itens);
        MessageViewModel RemoveItensOrder(List<ItensOrder> itens);
    }
}
