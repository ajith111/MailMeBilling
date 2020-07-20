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
    public class creditnotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public creditnotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: creditnotes
        public async Task<IActionResult> Index()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            return View(await _context.creditnote.ToListAsync());
        }
        public async Task<IActionResult> CustomerIndex()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            return View(await _context.creditnote.Where(i => i.person == "customer").ToListAsync());
        }

        public async Task<IActionResult> VendorIndex()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            return View(await _context.creditnote.Where(i => i.person == "vendor").ToListAsync());
        }

        // GET: creditnotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }

            var creditnote = await _context.creditnote
                .FirstOrDefaultAsync(m => m.cid == id);
            if (creditnote == null)
            {
                return NotFound();
            }

            return View(creditnote);
        }

        // GET: creditnotes/Create
        public IActionResult Create()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            return View();
        }

        // POST: creditnotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cid,person,particular,totalamount,name,mobilenumber,address,paymenttype,refno")] creditnote creditnote)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (ModelState.IsValid)
            {
                ViewBag.data = HttpContext.Session.GetString("name");
                var Name = ViewBag.data;              
                ViewBag.branch = HttpContext.Session.GetString("branch");
                creditnote.addby = Name;
                creditnote.branch = Branch;
                creditnote.cdate = DateTime.UtcNow;
                _context.Add(creditnote);
                await _context.SaveChangesAsync();
                var cno = creditnote.cid;
                if (creditnote.person =="customer")
                {
                    var checkcustomer = _context.customerdetails.Where(i => i.Mobilenumber == creditnote.mobilenumber).FirstOrDefault();
                    if (checkcustomer == null)
                    {
                        CustomerDetails cd = new CustomerDetails();
                        cd.Mobilenumber = creditnote.mobilenumber;
                        cd.Customername = creditnote.name;
                        cd.Address = creditnote.address;
                        cd.Branch = Branch;
                        cd.Entrydate = DateTime.UtcNow;
                        cd.Entryby = Name;
                        _context.customerdetails.Add(cd);

                        


                        Creditpaymenthistry cph1 = new Creditpaymenthistry();
                        cph1.Mobile = creditnote.mobilenumber;
                        cph1.Customername = creditnote.name;
                        cph1.Address = creditnote.address;
                        cph1.paymenttype = creditnote.paymenttype;
                        cph1.Payment = creditnote.totalamount;
                        cph1.Recivedby = Name;
                        cph1.Paiddate = DateTime.UtcNow;
                        cph1.Balance = 0;
                        cph1.refno = creditnote.refno;
                        cph1.Branch = Branch;
                        cph1.total = creditnote.totalamount;
                        cph1.billid = cno;
                        _context.creditpaymenthistries.Add(cph1);

                     
                        _context.SaveChanges();
                    }

                }

                if (creditnote.person == "vendor")
                {
                    var checkvendor = _context.vendor.Where(i => i.Mobilenumber == creditnote.mobilenumber).FirstOrDefault();
                    if (checkvendor == null)
                    {
                        Vendor cd = new Vendor();
                        cd.Mobilenumber = creditnote.mobilenumber;
                        cd.Name = creditnote.name;
                        cd.Address = creditnote.address;
                        cd.Branch = Branch;
                        cd.Entrydate = DateTime.UtcNow;
                        cd.Entryby = Name;
                        _context.vendor.Add(cd);
                        Creditpaymenthistry cph1 = new Creditpaymenthistry();
                        cph1.Mobile = creditnote.mobilenumber;
                        cph1.Customername = creditnote.name;
                        cph1.Address = creditnote.address;
                        cph1.paymenttype = creditnote.paymenttype;
                        cph1.Payment = creditnote.totalamount;
                        cph1.Recivedby = Name;
                        cph1.Paiddate = DateTime.UtcNow;
                        cph1.Balance = 0;
                        cph1.refno = creditnote.refno;
                        cph1.Branch = Branch;
                        cph1.total = creditnote.totalamount;
                        cph1.billid = cno;
                        _context.creditpaymenthistries.Add(cph1);
                        _context.SaveChanges();
                    }
                }
                
                return View();
            }
            return View(creditnote);
        }

        // GET: creditnotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }

            var creditnote = await _context.creditnote.FindAsync(id);
            if (creditnote == null)
            {
                return NotFound();
            }
            return View(creditnote);
        }

        // POST: creditnotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("cid,cdate,person,particular,totalamount,name,mobilenumber,address")] creditnote creditnote)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id != creditnote.cid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    creditnote.cdate = DateTime.UtcNow;
                    _context.Update(creditnote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!creditnoteExists(creditnote.cid))
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
            return View(creditnote);
        }

        // GET: creditnotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }

            var creditnote = await _context.creditnote
                .FirstOrDefaultAsync(m => m.cid == id);
            if (creditnote == null)
            {
                return NotFound();
            }

            return View(creditnote);
        }

        // POST: creditnotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditnote = await _context.creditnote.FindAsync(id);
            _context.creditnote.Remove(creditnote);
            await _context.SaveChangesAsync();
            return View();
        }

        private bool creditnoteExists(int id)
        {
            return _context.creditnote.Any(e => e.cid == id);
        }
        [HttpGet]
        public JsonResult fillcusdetails(string mob)
        {

            var deatils = _context.customerdetails.Where(c => c.Mobilenumber == mob).SingleOrDefault();
            return new JsonResult(deatils);

        }
    }
}