using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MailMeBilling.Data;
using MailMeBilling.Models;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace MailMeBilling.Controllers
{
    public class RolleDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolleDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RolleDetails
        public async Task<IActionResult> Index()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            return View(await _context.employeedetails.ToListAsync());
        }

        // GET: RolleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }

            var rolleDetails = await _context.employeedetails
                .FirstOrDefaultAsync(m => m.Employeeid == id);
            if (rolleDetails == null)
            {
                return NotFound();
            }

            return View(rolleDetails);
        }

        // GET: RolleDetails/Create
        public IActionResult Create()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.branch = _context.branch.ToList();
            return View();
        }

        // POST: RolleDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Employeeid,Name,DOB,Mobilenumber,Address,Branch,Gender,Roll,Email,Password,Comfirmpassword")] RolleDetails rolleDetails)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            rolleDetails.Entrydate =  DateTime.Now;
            rolleDetails.Entryby = Name;
            Login log = new Login();
            log.Userid = rolleDetails.Email;
            log.Password = rolleDetails.Password;
            _context.login.Add(log);
                _context.Add(rolleDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           
        }

        // GET: RolleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.branch = _context.branch.ToList();
            var rolleDetails = await _context.employeedetails.FindAsync(id);
            if (rolleDetails == null)
            {
                return NotFound();
            }
            return View(rolleDetails);
        }

        // POST: RolleDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Employeeid,Name,DOB,Mobilenumber,Address,Branch,Gender,Roll,Email,Password,Comfirmpassword")] RolleDetails rolleDetails)
        {
            if (id != rolleDetails.Employeeid)
            {
                return NotFound();
            }

           
                try
                {

                ViewBag.data = HttpContext.Session.GetString("name");
                var Name = ViewBag.data;
                rolleDetails.Entrydate =  DateTime.Now;
                rolleDetails.Entryby = Name;
                Login log = new Login();
                  var login = _context.login.Where(i => i.Userid == rolleDetails.Email).FirstOrDefault();
                login.Password = rolleDetails.Password;
                _context.login.Update(login);
                _context.Update(rolleDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolleDetailsExists(rolleDetails.Employeeid))
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

        // GET: RolleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }

            var rolleDetails = await _context.employeedetails
                .FirstOrDefaultAsync(m => m.Employeeid == id);
            if (rolleDetails == null)
            {
                return NotFound();
            }

            return View(rolleDetails);
        }

        // POST: RolleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rolleDetails = await _context.employeedetails.FindAsync(id);
            _context.employeedetails.Remove(rolleDetails);
            var uname = rolleDetails.Email;
            var pass = rolleDetails.Password;
            var login = _context.login.Where(i => i.Userid == uname && i.Password == pass).FirstOrDefault();
            var logact = _context.login.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolleDetailsExists(int id)
        {
            return _context.employeedetails.Any(e => e.Employeeid == id);
        }
    }
}
