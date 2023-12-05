using Microsoft.AspNetCore.Mvc;

namespace NottiesRebuiltV3.Controllers
{
    public class HumanityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
