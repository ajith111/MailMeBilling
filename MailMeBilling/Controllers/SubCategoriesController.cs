﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MailMeBilling.Data;
using MailMeBilling.Models;
using Microsoft.AspNetCore.Http;

namespace MailMeBilling.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubCategories
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.subcategory.ToListAsync());
        }

        // GET: SubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.subcategory
                .FirstOrDefaultAsync(m => m.Subcategoryid == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // GET: SubCategories/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subcategoryid,Subcatagory,Subcatagorydiscription")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                  ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
                var Branch = ViewBag.branch;
                subCategory.Branch = Branch;
                _context.Add(subCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subCategory);
        }

        // GET: SubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.subcategory.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Subcategoryid,Subcatagory,Subcatagorydiscription")] SubCategory subCategory)
        {
            if (id != subCategory.Subcategoryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                      ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
                    var Branch = ViewBag.branch;
                    subCategory.Branch = Branch;
                    _context.Update(subCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategoryExists(subCategory.Subcategoryid))
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
            return View(subCategory);
        }

        // GET: SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.subcategory
                .FirstOrDefaultAsync(m => m.Subcategoryid == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _context.subcategory.FindAsync(id);
            _context.subcategory.Remove(subCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoryExists(int id)
        {
            return _context.subcategory.Any(e => e.Subcategoryid == id);
        }
        public JsonResult fillsub(string mob)
        {

            var deatils = _context.subcategory.Where(c => c.Subcatagory == mob).SingleOrDefault();
            return new JsonResult(deatils);

        }
    }
}
