using Microsoft.AspNetCore.Mvc;

namespace NottiesRebuiltV3.Controllers
{
    public class RecyclingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
