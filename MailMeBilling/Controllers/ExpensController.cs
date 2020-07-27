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
using System.IO;

namespace MailMeBilling.Controllers
{
    public class ExpensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expens
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.expens.ToListAsync());
        }

        // GET: Expens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var expens = await _context.expens
                .FirstOrDefaultAsync(m => m.eid == id);
            if (expens == null)
            {
                return NotFound();
            }

            return View(expens);
        }

        // GET: Expens/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Expens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Expens expens, List<IFormFile> idpro)
        {
            foreach (var item in idpro)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        expens.idpro = stream.ToArray();
                    }
                }
            }
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            string Name = ViewBag.data;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);         
            string Branch = ViewBag.branch;
          
            if (ModelState.IsValid)
            {
                expens.branch = Branch;
                expens.entrydate = DateTime.UtcNow;
                expens.entryby = Name;
                _context.Add(expens);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Expens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var expens = await _context.expens.FindAsync(id);
            if (expens == null)
            {
                return NotFound();
            }
            return View(expens);
        }

        // POST: Expens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("eid,reason,amount")] Expens expens)
        {
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
           
            string Branch = ViewBag.branch;
           
            if (id != expens.eid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    expens.branch = Branch;
                    expens.entrydate = DateTime.UtcNow;
                    expens.entryby = Name;
                    _context.Update(expens);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpensExists(expens.eid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View();
            }
            return View(expens);
        }

        // GET: Expens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           

            if (id == null)
            {
                return NotFound();
            }

            var expens = await _context.expens
                .FirstOrDefaultAsync(m => m.eid == id);
            if (expens == null)
            {
                return NotFound();
            }

            return View(expens);
        }

        // POST: Expens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var expens = await _context.expens.FindAsync(id);
            _context.expens.Remove(expens);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpensExists(int id)
        {
            return _context.expens.Any(e => e.eid == id);
        }

        public IActionResult Addtransport()
        {
          
            return View();
        }

       
        public IActionResult Addsalary()
        {
           
            return View();
        }

       
        public IActionResult Addfule()
        {
           
            return View();
        }

       


        public IActionResult Addcompanyex()
        {
           
            return View();
        }

       
        public IActionResult Addstationary()
        {
           
            return View();
        }

       

    }
}
