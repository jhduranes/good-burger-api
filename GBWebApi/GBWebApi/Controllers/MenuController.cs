using Domain.Interfaces.Service;
using Entities.Enums;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GBWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]    
    public class MenuController : Controller
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }
                            
        [HttpGet]
        [Route("GetAllMenuOptions")]
        public ActionResult GetAllMenuOptions()
        {
            var item = _service.ListAllMenuItens();
            if (item == null) return NotFound("Register not found.");
            return Ok(item);            
        }
                
        [HttpGet]
        [Route("GetAllSandwichs")]
        public ActionResult GetAllSandwichs()
        {
            var item = _service.ListSandwichs();
            if (item == null) return NotFound("Register not found.");
            return Ok(item);

        }
                
        [HttpGet]
        [Route("GetAllExtras")]
        public ActionResult GetAllExtras()
        {
            var item = _service.ListExtras();
            if (item == null) return NotFound("Register not found.");
            return Ok(item);
        }

        [HttpPost]
        [Route("AddItemMenu")]
        public ActionResult AddMenuItem([FromBody] ProductInsertViewModel productVM)
        {
            var msg = _service.AddMenuItem(productVM);
            return Json(msg);
        }

        [HttpPut("UpdateItemMenu/{id}")]
        public ActionResult UpdateItemMenu([FromBody] ProductInsertViewModel product, int id)
        {
            var msg = _service.UpdateItemMenu(product, id);
            return Json(msg);
        }

        [HttpDelete]
        [Route("RemoveItemMenu/{id}")]
        public ActionResult RemoveItemMenu(int id)
        {
            var item = _service.RemoveItemMenu(id);
            return Ok(item);
        }
    }
}
