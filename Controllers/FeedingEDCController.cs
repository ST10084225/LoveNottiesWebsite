using Microsoft.AspNetCore.Mvc;

namespace NottiesRebuiltV3.Controllers
{
    public class FeedingEDCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
