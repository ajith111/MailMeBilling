using System;
using System.Collections.Generic;
using System.Configuration;
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
        const string name ="";
        const string branch = "";
        const string roll = "";
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Login(string Invailde)
        {
            if (Invailde != null)
            {
                ViewBag.errorlog = "Username & password";
            }
           
            return View();
        }


       
        public IActionResult GetAll()
        {
            var sale = _context.salesinvoicesummery.ToList();
            return Json(sale);
            
        }
        public IActionResult Dashbord()
        {
          
            DshbordVM dashbordVM = new DshbordVM();
            var customer = _context.customerdetails.ToList();
           

            foreach (var item in customer)
            {

                dashbordVM.customerDetails.Add(item);
            }


            var expence = _context.expens.ToList();

            foreach (var item in expence)
            {
                dashbordVM.Expens.Add(item);
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
                        
                var name = login.Email;
                var branch = login.Branch;
                var roll = login.Roll;
                DateTime todaydate = DateTime.UtcNow;
                DateTime dateStart = DateTime.UtcNow.AddDays(-15);
                var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

                var cpen = pendingcustomer.Count();

                var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

                var vpen = pendingvendor.Count();


                if (HttpContext.Session.GetString(SD.Sessionname)!= null)
                {

                    
                    HttpContext.Session.SetObject(SD.Sessionname, name);
                    HttpContext.Session.SetObject(SD.Statusroll, roll);
                    HttpContext.Session.SetObject(SD.Statusbranch, branch);
                    HttpContext.Session.SetObject(SD.vcount ,vpen);
                    HttpContext.Session.SetObject(SD.ccount ,cpen);


                }
               

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
                var msg = "Invailde";
                ViewBag.errorlog = "Please Enter correct Username/password";
                    return RedirectToAction("Login","Admin", new { Invailde = msg });
                }
          
           
        }
        public ActionResult Logout()
        {

            HttpContext.Session.Clear();
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
           
            if (ViewBag.data == null)
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }


     
    }
}
