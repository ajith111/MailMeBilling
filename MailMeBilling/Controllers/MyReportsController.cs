using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailMeBilling.Data;
using MailMeBilling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailMeBilling.Controllers
{
    public class MyReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MyReportsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Salesreport()
        {
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            var sr = _context.salesinvoicesummery.ToList();
            return View(sr);
        }
        public IActionResult Purchasereport()
        {
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            var sr = _context.purchaseinvoicesummeries.ToList();
            return View(sr);
        }

        public IActionResult Customerpayment(string id)
        {
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            var list = _context.customerdetails.Where(i => i.Mobilenumber == id).FirstOrDefault();
            var bill = _context.salesinvoicesummery.Where(i => i.Mobilenumber == id).ToList();
            ViewBag.billsummery = bill;
            var histry = _context.customerpaymenthistry.Where(i => i.Mobile == id).ToList();
            ViewBag.histry = histry;


            return View(list);
        }
        public IActionResult Updatepayments(Customerpaymenthistry cph)
        {
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            cph.Paiddate =  DateTime.Now;
            cph.Recivedby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            cph.Branch = Branch;

            var billhis = _context.customerpaymenthistry.Add(cph);
            var billamount = _context.salesinvoicesummery.Where(i => i.Billid == cph.billid).FirstOrDefault();
           decimal paid = cph.Payment;
            decimal cal = billamount.Balance - paid;
            billamount.Balance = cal;
            if (cal == 0)
            {
                billamount.status = "Close";
            }
            _context.salesinvoicesummery.Update(billamount);
            _context.SaveChanges();



            return Json(new { success = true, message = "Save successfull." });
        }

        public IActionResult Vendorpayment(string id)
        {
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            var list = _context.vendor.Where(i => i.Mobilenumber == id).FirstOrDefault();
            var bill = _context.purchaseinvoicesummeries.Where(i => i.Mobilenumber == id).ToList();
            ViewBag.billsummery = bill;
            var histry = _context.vendorpayments.Where(i => i.Mobile == id).ToList();
            ViewBag.histry = histry;


            return View(list);
        }
        public IActionResult Updatevendorpayments(Vendorpayment cph)
        {
            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            cph.Paiddate =  DateTime.Now;
            cph.Recivedby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            cph.Branch = Branch;

            var billhis = _context.vendorpayments.Add(cph);
            var billamount = _context.purchaseinvoicesummeries.Where(i => i.Billid == cph.billid).FirstOrDefault();
            decimal paid = cph.Payment;
            decimal cal = billamount.Balance - paid;
            billamount.Balance = cal;
            if (cal == 0)
            {
                billamount.status = "Close";
            }
            _context.purchaseinvoicesummeries.Update(billamount);
            _context.SaveChanges();



            return Json(new { success = true, message = "Save successfull." });
        }

        [HttpGet]
        public FileResult Viewbill(int id)
        {

            var file = _context.purchaseinvoicesummeries.Where(i => i.Billid == id).FirstOrDefault();



            return File(file.upload, "application/pdf");

        }



        public IActionResult print(int id)
        {

            DateTime todaydate =  DateTime.Now;
            DateTime dateStart = DateTime.Now.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();
            ViewBag.CustomerPending = pendingcustomer.Count();
            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();
            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            loadtemp load = new loadtemp();

            var billno = _context.salesinvoicesummery.Where(i => i.Billid == id).FirstOrDefault(); //bill No         

            
            if (billno != null)
            {               
               
                var tmpsale = _context.salesinvoices.Where(i => i.Branch == Branch && i.Billno == id).ToList();
               
                    foreach (var item in tmpsale)
                    {
                        load.salesinvoices.Add(item);
                    }
                    var tmpsummery = _context.salesinvoicesummery.Where(i => i.Billid == id).ToList();
                   

                        foreach (var item in tmpsummery)
                        {
                            load.salesinvoicesummeries.Add(item);
                        }
                  






            }
            return View(load);
        }

    }
}
