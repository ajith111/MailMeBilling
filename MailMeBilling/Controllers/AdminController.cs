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
        public IActionResult Dashbord()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            return View();
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
                return RedirectToAction("Dashbord","Admin");
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
