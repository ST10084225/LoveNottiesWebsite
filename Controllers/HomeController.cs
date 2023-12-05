using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NottiesRebuiltV3.Data;
using NottiesRebuiltV3.Models;
using NottiesRebuiltV3.Models.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NottiesRebuiltV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmailID,Email")] NewsLetterModel emails)
        {
            if (ModelState.IsValid)
            {
                //Get the most recent OurPerson ID, and add 1
                var ourPeopleList = await _context.NewsLetterEmails.ToListAsync();
                var lastPerson = ourPeopleList.LastOrDefault();
                if (lastPerson != null)
                {
                    emails.EmailID = (Convert.ToInt32(lastPerson.EmailID) + 1).ToString();
                }
                else
                {
                    emails.EmailID = "1";
                }
                _context.Add(emails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
