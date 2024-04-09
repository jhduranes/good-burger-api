using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Resources;
using Entities.Tables;
using Entities.ViewModels;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repository;
        private IMenuRepository _menuRepository;
        DefaultFunctions functions = new DefaultFunctions();

        MessageViewModel message = new MessageViewModel();

        public OrderService(IOrderRepository repository, IMenuRepository menuRepository)
        {
            _repository = repository;
            _menuRepository = menuRepository;
        }

        public OrderResponseViewModel SubmitOrder(SubmitOrderViewModel order)
        {
            OrderResponseViewModel responseViewModel = new OrderResponseViewModel();
            responseViewModel.Itens = new List<ItensOrderResponse>();                      
                    
            if (string.IsNullOrEmpty(order.CustomerName))
            {
                responseViewModel.Message = "Please provide the customer's name.";
                return responseViewModel;
            }

            if (order.Itens.Count == 0)
            {
                responseViewModel.Message = "Please provide at least one item in the order.";
                return responseViewModel;
            }

            Orders orders = new Orders();
            List<ItensOrder> listProducts = new List<ItensOrder>();
            decimal amountOrder = 0;
            int countSandwichAux = 0;
            int countDrinkAux = 0;
            int countFriesAux = 0;

            foreach (var i in order.Itens)
            {
                #region Carregando os itens do pedido.
                ItensOrder itens = new ItensOrder();
                itens.IdProduct = i.IdProduct;
                listProducts.Add(itens);
                #endregion

                #region Pegando a descrição dos produtos pra mostrar na saída e valor para fazer o calulo.
                var prod = _menuRepository.GetProductById(i.IdProduct);
                ItensOrderResponse itensOrderResponse = new ItensOrderResponse();  
                itensOrderResponse.Id = prod.Id;
                itensOrderResponse.Description = prod.Description;
                itensOrderResponse.Price = prod.Price;
                responseViewModel.Itens.Add(itensOrderResponse);

                amountOrder += prod.Price;
                #endregion

                #region Verificação das quantidades
                if (prod.Type == "Sandwich") countSandwichAux++;
                if (prod.Description.ToUpper().Contains("DRINK")) countDrinkAux++;
                if (prod.Description.ToUpper().Contains("FRIES")) countFriesAux++;
                #endregion
            }

            if (countSandwichAux > 1)
            {
                responseViewModel.Message = "Sorry, but you are only allowed to choose one sandwich per order.";
                return responseViewModel;
            }
            if (countDrinkAux > 1)
            {
                responseViewModel.Message = "Sorry, but you are only allowed to choose one drink per order.";
                return responseViewModel;
            }
            if (countFriesAux > 1)
            {
                responseViewModel.Message = "Sorry, but you are only allowed to choose one fries per order.";
                return responseViewModel;
            }

            orders.CustomerName = order.CustomerName;
            orders.SubTotal = amountOrder;

            if (countSandwichAux == 1 && countDrinkAux == 1 && countFriesAux == 1)
            {
                orders.Discount = functions.CalculateDiscount(amountOrder, 20);
            }
            if (countSandwichAux == 1 && countDrinkAux == 1 && countFriesAux == 0)
            {
                orders.Discount = functions.CalculateDiscount(amountOrder, 15);
            }
            if (countSandwichAux == 1 && countDrinkAux == 0 && countFriesAux == 1)
            {
                orders.Discount = functions.CalculateDiscount(amountOrder, 10);
            }

            orders.Total = orders.SubTotal - orders.Discount;
            var ret = _repository.SubmitOrder(orders, listProducts);
            if (ret.PerformedService)
            {
                responseViewModel.Id = int.Parse(ret.SuccessMessage);
                responseViewModel.CustomerName = order.CustomerName;
                responseViewModel.SubTotal = orders.SubTotal;
                responseViewModel.Discount = orders.Discount;
                responseViewModel.Total = orders.Total;
                responseViewModel.Message = "Order placed successfully!";
            }
            else
            {
                responseViewModel.Message = ret.FailedMessage;
            }
            return responseViewModel;
        }

        public List<OrderResponseViewModel> ListAllOrders()
        {
            List<OrderResponseViewModel> orders = new List<OrderResponseViewModel>(); 
            var ret = _repository.ListAllOrders();

            foreach (var r in ret)
            {
                OrderResponseViewModel order = new OrderResponseViewModel();
                order.Id = r.Id;
                order.CustomerName = r.CustomerName;
                order.SubTotal = r.SubTotal;
                order.Discount = r.Discount;
                order.Total = r.Total;
                order.Itens = new List<ItensOrderResponse>();

                var itens = _repository.ListItensOrdersById(r.Id);
                foreach (var i in itens)
                {       
                    var prod = _menuRepository.GetProductById(i.IdProduct);
                    ItensOrderResponse itensOrderResponse = new ItensOrderResponse();
                    itensOrderResponse.Id = prod.Id;
                    itensOrderResponse.Description = prod.Description;
                    itensOrderResponse.Price = prod.Price;
                    order.Itens.Add(itensOrderResponse);                    
                }
                orders.Add(order);
            }
            return orders;
        }

        public MessageViewModel RemoveOrder(int idOrder)
        {
            var order = _repository.GetOrderById(idOrder);
            var itens = _repository.ListItensOrdersById(idOrder);
            var ret = _repository.RemoveOrder(order, itens);

            return ret;
        }

        public OrderResponseViewModel UpdateOrder(SubmitOrderViewModel order, int idOrder)
        {
            OrderResponseViewModel responseViewModel = new OrderResponseViewModel();
            responseViewModel.Itens = new List<ItensOrderResponse>();
            Orders orderDB = _repository.GetOrderById(idOrder);
            if (orderDB == null)
            {
                responseViewModel.Message = "Order not found";
                return responseViewModel;
            }
            else
            {
                //Removendo os itens do pedido para recalcular desconto e valor total
                var itens = _repository.ListItensOrdersById(idOrder);
                _repository.RemoveItensOrder(itens);
            }

            if (string.IsNullOrEmpty(order.CustomerName))
            {
                responseViewModel.Message = "Please provide the customer's name.";
                return responseViewModel;
            }

            if (order.Itens.Count == 0)
            {
                responseViewModel.Message = "Please provide at least one item in the order.";
                return responseViewModel;
            }

            List<ItensOrder> listProducts = new List<ItensOrder>();
            decimal amountOrder = 0;
            int countSandwichAux = 0;
            int countDrinkAux = 0;
            int countFriesAux = 0;

            foreach (var i in order.Itens)
            {
                #region Carregando os itens do pedido.
                ItensOrder itens = new ItensOrder();
                itens.IdProduct = i.IdProduct;
                listProducts.Add(itens);
                #endregion

                #region Pegando a descrição dos produtos pra mostrar na saída e valor para fazer o calulo.
                var prod = _menuRepository.GetProductById(i.IdProduct);
                ItensOrderResponse itensOrderResponse = new ItensOrderResponse();
                itensOrderResponse.Id = prod.Id;
                itensOrderResponse.Description = prod.Description;
                itensOrderResponse.Price = prod.Price;
                responseViewModel.Itens.Add(itensOrderResponse);

                amountOrder += prod.Price;
                #endregion

                #region Verificação das quantidades
                if (prod.Type == "Sandwich") countSandwichAux++;
                if (prod.Description.ToUpper().Contains("DRINK")) countDrinkAux++;
                if (prod.Description.ToUpper().Contains("FRIES")) countFriesAux++;
                #endregion
            }

            if (countSandwichAux > 1)
            {
                responseViewModel.Message = "Sorry, but you are only allowed to choose one sandwich per order.";
                return responseViewModel;
            }
            if (countDrinkAux > 1)
            {
                responseViewModel.Message = "Sorry, but you are only allowed to choose one drink per order.";
                return responseViewModel;
            }
            if (countFriesAux > 1)
            {
                responseViewModel.Message = "Sorry, but you are only allowed to choose one fries per order.";
                return responseViewModel;
            }

            orderDB.Id = idOrder;
            orderDB.CustomerName = order.CustomerName;
            orderDB.SubTotal = amountOrder;

            if (countSandwichAux == 1 && countDrinkAux == 1 && countFriesAux == 1)
            {
                orderDB.Discount = functions.CalculateDiscount(amountOrder, 20);
            }
            if (countSandwichAux == 1 && countDrinkAux == 1 && countFriesAux == 0)
            {
                orderDB.Discount = functions.CalculateDiscount(amountOrder, 15);
            }
            if (countSandwichAux == 1 && countDrinkAux == 0 && countFriesAux == 1)
            {
                orderDB.Discount = functions.CalculateDiscount(amountOrder, 10);
            }

            orderDB.Total = orderDB.SubTotal - orderDB.Discount;
            var ret = _repository.UpdateOrder(orderDB, listProducts);
            if (ret.PerformedService)
            {
                responseViewModel.Id = int.Parse(ret.SuccessMessage);
                responseViewModel.CustomerName = order.CustomerName;
                responseViewModel.SubTotal = orderDB.SubTotal;
                responseViewModel.Discount = orderDB.Discount;
                responseViewModel.Total = orderDB.Total;
                responseViewModel.Message = "Order altered successfully!";
            }
            else
            {
                responseViewModel.Message = ret.FailedMessage;
            }
            return responseViewModel;

        }
    }
}
