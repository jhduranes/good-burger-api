using Entities.Tables;
using Entities.ViewModels;

namespace Domain.Interfaces.Repository
{
    public interface IMenuRepository
    {
        List<Products> ListAllMenuItens();
        List<Products> ListMenuItensByTypes(string type);
        MessageViewModel AddMenuItem(Products products);
        Products GetProductById(int id);
        MessageViewModel RemoveItemMenu(Products products);
        MessageViewModel UpdateItemMenu(Products products);
    }
}
