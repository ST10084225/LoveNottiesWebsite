using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NottiesRebuiltV3.Data;
using NottiesRebuiltV3.Models;
using NottiesRebuiltV3.Models.Repositories.Abstract;
using System.IO;

namespace NottiesRebuiltV3.Controllers
{
    public class WhatsOnController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobService _IBlobService;
  /*      private readonly IWhatsOnRepository _IBlogRepository;*/

        public WhatsOnController(ApplicationDbContext context, IBlobService IBlobService)
        {
            _context = context;
            _IBlobService = IBlobService;

        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            IEnumerable<WhatsOnModel> list = _context.WhatsOn;
            return View(list);
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogItem = await _context.WhatsOn
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (blogItem == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();

            return View(blogItem);
        }

        // GET: Blog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,PostImageID,PostDescription,PostDate,PostTitle, PostImageFile")] WhatsOnModel blogItem)
        {
            if (ModelState.IsValid)
            {
                //Set the Blog View Counter to 0
                blogItem.PostDate = DateTime.Now.ToShortDateString().ToString();

                //Get the most recent blog's ID, and add 1
                var blogList = await _context.WhatsOn.ToListAsync();
                var lastBlog = blogList.LastOrDefault();
                if (lastBlog != null)
                {
                    blogItem.PostID = (Convert.ToInt32(lastBlog.PostID) + 1).ToString();
                    blogItem.PostImageID = (Convert.ToInt32(lastBlog.PostID) + 1).ToString();
                }
                else
                {
                    blogItem.PostID = "1";
                    blogItem.PostImageID = "1";
                }

                byte[] fileByteArray;    //1st change here
                if (blogItem.PostImageFile != null)
                {
                    using (var item = new MemoryStream())
                    {
                        blogItem.PostImageFile.CopyTo(item);
                        fileByteArray = item.ToArray(); //2nd change here

                        //find and delete the blog image if it already exists
                        if (_IBlobService.DoesBlobExists(blogItem.PostImageID, BlobContainer.whatsonimages).Result == true)
                        {
                            await _IBlobService.DeleteFromBlob(blogItem.PostImageID, BlobContainer.whatsonimages);
                        }

                        //Create new blob image
                        if (_IBlobService.DoesBlobExists(blogItem.PostImageID, BlobContainer.whatsonimages).Result == false)
                        {
                            await _IBlobService.UploadFileBlobAsync(fileByteArray, blogItem.PostImageID, BlobContainer.whatsonimages);
                        }
                    }
                }
                _context.Add(blogItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogItem);
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogItem = await _context.WhatsOn.FindAsync(id);
            if (blogItem == null)
            {
                return NotFound();
            }
            return View(blogItem);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PostID,PostTitle,PostDescription")] WhatsOnModel blogItem)
        {
            if (id != blogItem.PostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogItemExists(blogItem.PostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogItem);
        }

        // GET: Blog/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogItem = await _context.WhatsOn
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (blogItem == null)
            {
                return NotFound();
            }

            return View(blogItem);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //find the blog in db
            var blogItem = await _context.WhatsOn.FindAsync(id);

            //find and delete the blog image
            if (_IBlobService.DoesBlobExists(blogItem.PostImageID, BlobContainer.whatsonimages).Result == true)
            {
                await _IBlobService.DeleteFromBlob(blogItem.PostImageID, BlobContainer.whatsonimages);
            }

            //finalize delete
            _context.WhatsOn.Remove(blogItem);
            await _context.SaveChangesAsync();

            //return to index
            return RedirectToAction(nameof(Index));
        }

        private bool BlogItemExists(string id)
        {
            return _context.WhatsOn.Any(e => e.PostID == id);
        }
    }
}
