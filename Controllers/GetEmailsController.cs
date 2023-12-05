using Microsoft.AspNetCore.Mvc;
using NottiesRebuiltV3.Data;
using NottiesRebuiltV3.Models.Repositories.Abstract;
using System.Threading.Tasks;

namespace NottiesRebuiltV3.Controllers
{
    public class GetEmailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GetEmailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            string emails = "";
            var posts = _context.NewsLetterEmails;
            foreach (var post in posts)
            {
                emails += post.Email + ",";
            }
            ViewBag.Emails = emails;
            return View();
        }
    }
}
