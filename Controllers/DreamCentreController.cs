using Microsoft.AspNetCore.Mvc;

namespace NottiesRebuiltV3.Controllers
{
    public class DreamCentreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
