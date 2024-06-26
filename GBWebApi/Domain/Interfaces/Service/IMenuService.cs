﻿using Entities.Tables;
using Entities.ViewModels;

namespace Domain.Interfaces.Service
{
    public interface IMenuService
    {
        List<Products> ListAllMenuItens();
        List<Products> ListSandwichs();
        List<Products> ListExtras();
        MessageViewModel AddMenuItem(ProductInsertViewModel productVM);
        MessageViewModel UpdateItemMenu(ProductInsertViewModel product, int id);
        MessageViewModel RemoveItemMenu(int id);
    }
}
