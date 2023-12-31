﻿using System;
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
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobService _IBlobService;
        private readonly IBlogRepository _IBlogRepository;

        public BlogController(ApplicationDbContext context, IBlobService IBlobService, IBlogRepository iBlogRepository)
        {
            _context = context;
            _IBlobService = IBlobService;
            _IBlogRepository = iBlogRepository;
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            return View( _IBlogRepository.GetAllBlogPosts());
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogItem = await _context.BlogItems
                .FirstOrDefaultAsync(m => m.BlogID == id);
            if (blogItem == null)
            {
                return NotFound();
            }

            blogItem.BlogViews = (Convert.ToInt32(blogItem.BlogViews) + 1).ToString();
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
        public async Task<IActionResult> Create([Bind("BlogID,BlogImageID,BlogDescription,BlogViews,BlogDate,BlogTitle,BlogImageFile")] BlogItem blogItem)
        {
            if (ModelState.IsValid)
            {
                //Set the Blog View Counter to 0
                blogItem.BlogViews = "0";
                blogItem.BlogDate = DateTime.Now.ToShortDateString().ToString();

                //Get the most recent blog's ID, and add 1
                var blogList = await _context.BlogItems.ToListAsync();
                var lastBlog = blogList.LastOrDefault();
                if (lastBlog != null)
                {
                    blogItem.BlogID = (Convert.ToInt32(lastBlog.BlogID) + 1).ToString();
                    blogItem.BlogImageID = (Convert.ToInt32(lastBlog.BlogID) + 1).ToString();
                }
                else
                {
                    blogItem.BlogID = "1";
                    blogItem.BlogImageID = "1";
                }

                byte[] fileByteArray;    //1st change here
                if (blogItem.BlogImageFile != null)
                {
                    using (var item = new MemoryStream())
                    {
                        blogItem.BlogImageFile.CopyTo(item);
                        fileByteArray = item.ToArray(); //2nd change here

                        //find and delete the blog image if it already exists
                        if (_IBlobService.DoesBlobExists(blogItem.BlogImageID, BlobContainer.blogimages).Result == true)
                        {
                            await _IBlobService.DeleteFromBlob(blogItem.BlogImageID, BlobContainer.blogimages);
                        }

                        //Create new blob image
                        if (_IBlobService.DoesBlobExists(blogItem.BlogImageID, BlobContainer.blogimages).Result == false)
                        {
                            await _IBlobService.UploadFileBlobAsync(fileByteArray, blogItem.BlogImageID, BlobContainer.blogimages);
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

            var blogItem = await _context.BlogItems.FindAsync(id);
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
        public async Task<IActionResult> Edit(string id, [Bind("BlogID,BlogTitle,BlogImage,BlogDescription,BlogViews")] BlogItem blogItem)
        {
            if (id != blogItem.BlogID)
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
                    if (!BlogItemExists(blogItem.BlogID))
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

            var blogItem = await _context.BlogItems
                .FirstOrDefaultAsync(m => m.BlogID == id);
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
            var blogItem = await _context.BlogItems.FindAsync(id);

            //find and delete the blog image
            if (_IBlobService.DoesBlobExists(blogItem.BlogImageID, BlobContainer.blogimages).Result == true)
            {
                await _IBlobService.DeleteFromBlob(blogItem.BlogImageID, BlobContainer.blogimages);
            }

            //finalize delete
            _context.BlogItems.Remove(blogItem);
            await _context.SaveChangesAsync();

            //return to index
            return RedirectToAction(nameof(Index));
        }

        private bool BlogItemExists(string id)
        {
            return _context.BlogItems.Any(e => e.BlogID == id);
        }
    }
}
