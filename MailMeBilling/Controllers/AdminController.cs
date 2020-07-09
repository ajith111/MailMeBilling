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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        const string name = "_Name";
        const string branch = "_Branch";
        const string roll = "_Roll";
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Login()
        {
            ViewBag.error= "";
            return View();
        }
        public IActionResult GetAll()
        {
            var sale = _context.salesinvoicesummery.ToList();
            return Json(sale);
            
        }
        public IActionResult Dashbord()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            DshbordVM dashbordVM = new DshbordVM();
            var customer = _context.customerdetails.ToList();
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();

            foreach (var item in customer)
            {

                dashbordVM.customerDetails.Add(item);
            }
            var vendor = _context.vendor.ToList();
            foreach (var item in vendor)
            {

                dashbordVM.vendors.Add(item);
            }
            var sale = _context.salesinvoicesummery.ToList();
            foreach (var item in sale)
            {

                dashbordVM.salesinvoicesummeries.Add(item);
            }
            var purchase = _context.purchaseinvoicesummeries.ToList();
            foreach (var item in purchase)
            {

                dashbordVM.purchaseinvoicesummery.Add(item);
            }


            return View(dashbordVM);
        }
        [HttpPost]
        public IActionResult Admin_login(Login ad)
        {
          
            var login = _context.employeedetails.Where(i => i.Email == ad.Userid && i.Password == ad.Password).FirstOrDefault();
           
                if (login != null)
                {
                    ViewBag.msg = "login";
                    HttpContext.Session.SetString("name", login.Email);
                    HttpContext.Session.SetString("branch", login.Branch);
                    HttpContext.Session.SetString("roll", login.Roll);
                    ViewBag.data = HttpContext.Session.GetString("name");
                    ViewBag.branch = HttpContext.Session.GetString("branch");
                    ViewBag.roll = HttpContext.Session.GetString("roll");

                if (login.Roll == "SuperAdmin")
                {
                    return RedirectToAction("Dashbord", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Salesinvoice");

                }
               
                }
                else
                {
                    ViewBag.errorlog = "Please enter correct Username/password";
                    return RedirectToAction("Login");
                }
          
           
        }
        public ActionResult Logout()
        {

            HttpContext.Session.Clear();
            ViewBag.data = HttpContext.Session.GetString("name");
           
            if (ViewBag.data == null)
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }


     
    }
}
