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
    public class ColorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Colors
        public async Task<IActionResult> Index()
        {
          
            return View(await _context.color.ToListAsync());
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.color
                .FirstOrDefaultAsync(m => m.Colorid == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Colorid,Colors,Colorsdscription")] Color color)
        {

            if (ModelState.IsValid)
            {
                ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
                var Branch = ViewBag.branch;
                color.Branch = Branch;
                _context.Add(color);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.color.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Colorid,Colors,Colorsdscription")] Color color)
        {
            if (id != color.Colorid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                      ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
                    var Branch = ViewBag.branch;
                    color.Branch = Branch;
                    _context.Update(color);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.Colorid))
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
            return View(color);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.color
                .FirstOrDefaultAsync(m => m.Colorid == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var color = await _context.color.FindAsync(id);
            _context.color.Remove(color);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorExists(int id)
        {
            return _context.color.Any(e => e.Colorid == id);
        }
        public JsonResult fillcol(string mob)
        {

            var deatils = _context.color.Where(c => c.Colors == mob).SingleOrDefault();
            return new JsonResult(deatils);

        }
    }
}
