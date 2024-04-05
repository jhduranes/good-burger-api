using Microsoft.AspNetCore.Mvc;

namespace GBWebApi.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
