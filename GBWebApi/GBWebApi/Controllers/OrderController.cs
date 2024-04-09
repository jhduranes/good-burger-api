using Domain.Interfaces.Service;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GBWebApi.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }
             
        [HttpPost]
        [Route("SubmitOrder")]
        public ActionResult SubmitOrder([FromBody] SubmitOrderViewModel order)
        {
            var ret = _service.SubmitOrder(order);
            return Json(ret);
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public ActionResult GetAllOrders()
        {
            var item = _service.ListAllOrders();
            if (item == null) return NotFound("Register not found.");
            return Ok(item);
        }

        [HttpPut("UpdateOrder/{idOrder}")]        
        public ActionResult UpdateOrder([FromBody] SubmitOrderViewModel order, int idOrder)
        {
            var msg = _service.UpdateOrder(order, idOrder);            
            return Json(msg);
        }

        [HttpDelete]
        [Route("RemoveOrder/{idOrder}")]
        public ActionResult RemoveOrder(int idOrder)
        {
            var item = _service.RemoveOrder(idOrder);           
            return Ok(item);
        }

    }
}
