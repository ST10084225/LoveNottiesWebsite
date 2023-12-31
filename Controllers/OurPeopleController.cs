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
using Azure.Storage.Blobs.Models;
using System.IO;

namespace NottiesRebuiltV3.Controllers
{
    public class OurPeopleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobService _IBlobService;
        private readonly IOurPeopleRepository _IOurPeopleRepository;

        public OurPeopleController(ApplicationDbContext context, IBlobService IBlobService, IOurPeopleRepository IOurPeopleRepository)
        {
            _context = context;
            _IBlobService = IBlobService;
            _IOurPeopleRepository = IOurPeopleRepository;
        }

        // GET: OurPeople
        public async Task<IActionResult> Index()
        {
            return View(_IOurPeopleRepository.GetAllOurPeople());
        }

        // GET: OurPeople/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ourPeople = await _context.OurPeoples
                .FirstOrDefaultAsync(m => m.OurPersonID == id);
            if (ourPeople == null)
            {
                return NotFound();
            }

            return View(ourPeople);
        }

        // GET: OurPeople/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OurPeople/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OurPersonID,OurPersonFirstName,OurPersonLastName,OurPersonTitle,OurPersonImageID,OurPersonDescription," +
            "OurPersonImageFile")] OurPeople ourPeople)
        {
            if (ModelState.IsValid)
            {
                //Get the most recent OurPerson ID, and add 1
                var ourPeopleList = await _context.OurPeoples.ToListAsync();
                var lastPerson = ourPeopleList.LastOrDefault();
                if (lastPerson != null)
                {
                    ourPeople.OurPersonID = (Convert.ToInt32(lastPerson.OurPersonID) + 1).ToString();
                    ourPeople.OurPersonImageID = (Convert.ToInt32(lastPerson.OurPersonID) + 1).ToString();
                }
                else
                {
                    ourPeople.OurPersonID = "1";
                    ourPeople.OurPersonImageID = "1";
                }

                byte[] fileByteArray;    //1st change here
                if (ourPeople.OurPersonImageFile != null)
                {
                    using (var item = new MemoryStream())
                    {
                        ourPeople.OurPersonImageFile.CopyTo(item);
                        fileByteArray = item.ToArray(); //2nd change here

                        if (_IBlobService.DoesBlobExists(ourPeople.OurPersonImageID, BlobContainer.ourpeopleimages).Result == false)
                        {
                            await _IBlobService.UploadFileBlobAsync(fileByteArray, ourPeople.OurPersonImageID, BlobContainer.ourpeopleimages);
                        }
                    }
                }
                _context.Add(ourPeople);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ourPeople);
        }

        // GET: OurPeople/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ourPeople = await _context.OurPeoples.FindAsync(id);
            if (ourPeople == null)
            {
                return NotFound();
            }
            return View(ourPeople);
        }

        // POST: OurPeople/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OurPersonID,OurPersonFirstName,OurPersonLastName,OurPersonTitle,OurPersonDescription")] OurPeople ourPeople)
        {
            if (id != ourPeople.OurPersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ourPeople);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OurPeopleExists(ourPeople.OurPersonID))
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
            return View(ourPeople);
        }

        // GET: OurPeople/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ourPeople = await _context.OurPeoples
                .FirstOrDefaultAsync(m => m.OurPersonID == id);
            if (ourPeople == null)
            {
                return NotFound();
            }

            return View(ourPeople);
        }

        // POST: OurPeople/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ourPeople = await _context.OurPeoples.FindAsync(id);

            //find and delete the blog image
            if (_IBlobService.DoesBlobExists(ourPeople.OurPersonImageID, BlobContainer.blogimages).Result == true)
            {
                await _IBlobService.DeleteFromBlob(ourPeople.OurPersonImageID, BlobContainer.blogimages);
            }

            _context.OurPeoples.Remove(ourPeople);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OurPeopleExists(string id)
        {
            return _context.OurPeoples.Any(e => e.OurPersonID == id);
        }
    }
}
