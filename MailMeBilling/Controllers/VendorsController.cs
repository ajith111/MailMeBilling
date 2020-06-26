using System;
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
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vendors
        public async Task<IActionResult> Index()
        {
            return View(await _context.vendor.ToListAsync());
        }

        // GET: Vendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Vendor = await _context.vendor
                .FirstOrDefaultAsync(m => m.vendorId == id);
            if (Vendor == null)
            {
                return NotFound();
            }

            return View(Vendor);
        }

        // GET: Vendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorId,Name,Mobilenumber,Bankname,Accountnumber,Ifsccode,branch")] Vendor Vendor)
        {
            if (ModelState.IsValid)
            {
                ViewBag.branch = HttpContext.Session.GetString("branch");
                var Branch = ViewBag.branch;
                Vendor.Branch = Branch;
                _context.Add(Vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Vendor);
        }

        // GET: Vendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Vendor = await _context.vendor.FindAsync(id);
            if (Vendor == null)
            {
                return NotFound();
            }
            return View(Vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorId,Name,Mobilenumber,Bankname,Accountnumber,Ifsccode,branch")] Vendor Vendor)
        {
            if (id != Vendor.vendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.branch = HttpContext.Session.GetString("branch");
                    var Branch = ViewBag.branch;
                    Vendor.Branch = Branch;
                    _context.Update(Vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(Vendor.vendorId))
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
            return View(Vendor);
        }

        // GET: Vendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Vendor = await _context.vendor
                .FirstOrDefaultAsync(m => m.vendorId == id);
            if (Vendor == null)
            {
                return NotFound();
            }

            return View(Vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Vendor = await _context.vendor.FindAsync(id);
            _context.vendor.Remove(Vendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            return _context.vendor.Any(e => e.vendorId == id);
        }
    }
}
