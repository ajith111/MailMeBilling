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
    public class CustomerDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerDetails
        public async Task<IActionResult> Index()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            return View(await _context.customerdetails.ToListAsync());
        }

        // GET: CustomerDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }

            var customerDetails = await _context.customerdetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerDetails == null)
            {
                return NotFound();
            }

            return View(customerDetails);
        }

        // GET: CustomerDetails/Create
        public IActionResult Create()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            return View();
        }

        // POST: CustomerDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Customerid,Customername,Mobilenumber,Emailid,DOB,Address")] CustomerDetails customerDetails)
        {
            if (ModelState.IsValid)
            {
                ViewBag.data = HttpContext.Session.GetString("name");
                var Name = ViewBag.data;
                customerDetails.Entryby = Name;

                ViewBag.branch = HttpContext.Session.GetString("branch");
                var Branch = ViewBag.branch;
                customerDetails.Branch = Branch;
             
                customerDetails.Entrydate =  DateTime.UtcNow;
               
              
                _context.Add(customerDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerDetails);
        }

        // GET: CustomerDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }

            var customerDetails = await _context.customerdetails.FindAsync(id);
            if (customerDetails == null)
            {
                return NotFound();
            }
            return View(customerDetails);
        }

        // POST: CustomerDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Customerid,Customername,Mobilenumber,Emailid,DOB,Address")] CustomerDetails customerDetails)
        {
            if (id != customerDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.data = HttpContext.Session.GetString("name");
                    var Name = ViewBag.data;
                    ViewBag.branch = HttpContext.Session.GetString("branch");
                    var Branch = ViewBag.branch;
                    customerDetails.Branch = Branch;
                    customerDetails.Entrydate =  DateTime.UtcNow;
                    customerDetails.Entryby = Name;
                    _context.Update(customerDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerDetailsExists(customerDetails.Id))
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
            return View(customerDetails);
        }

        // GET: CustomerDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }

            var customerDetails = await _context.customerdetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerDetails == null)
            {
                return NotFound();
            }

            return View(customerDetails);
        }

        // POST: CustomerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerDetails = await _context.customerdetails.FindAsync(id);
            _context.customerdetails.Remove(customerDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerDetailsExists(int id)
        {
            return _context.customerdetails.Any(e => e.Id == id);
        }
    }
}
