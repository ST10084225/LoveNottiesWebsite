using Microsoft.AspNetCore.Mvc;

namespace NottiesRebuiltV3.Controllers
{
    public class DonateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
