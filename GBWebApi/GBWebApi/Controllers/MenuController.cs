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

        /// <summary>
        /// Creating a product registration
        /// </summary> 
        /// <param name= "cliente">Product View model</param>
        /// <returns>Retorna status ok, menu options data created if successful.</returns>        
        [HttpPost]        
        [Route("AddItemMenu")]
        public ActionResult AddMenuItem([FromBody] ProductInsertViewModel productVM)
        {
            var msg = _service.AddMenuItem(productVM);            
            return Json(msg);
        }

        /// <summary>
        /// Consult all products on the Menu.
        /// </summary>         
        /// <returns>Returns status ok, menu options data if successful.</returns>
        [HttpGet]
        [Route("GetAllMenuOptions")]
        public ActionResult GetAllMenuOptions()
        {
            var item = _service.ListAllMenuItens();
            if (item == null) return NotFound("Register not found.");
            return Ok(item);            
        }

        /// <summary>
        /// Consult Sandwichs on the Menu.
        /// </summary>         
        /// <returns>Returns status ok, Sandwichs options data if successful.</returns>
        [HttpGet]
        [Route("GetAllSandwichs")]
        public ActionResult GetAllSandwichs()
        {
            var item = _service.ListSandwichs();
            if (item == null) return NotFound("Register not found.");
            return Ok(item);

        }

        /// <summary>
        /// Consult Extras products on the Menu.
        /// </summary>         
        /// <returns>Returns status ok, Extras options data if successful.</returns>
        [HttpGet]
        [Route("GetAllExtras")]
        public ActionResult GetAllExtras()
        {
            var item = _service.ListExtras();
            if (item == null) return NotFound("Register not found.");
            return Ok(item);
        }



    }
}
