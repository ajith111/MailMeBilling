using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailMeBilling.Data;
using MailMeBilling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeZoneConverter;

namespace MailMeBilling.Controllers
{
  
    public class SalesinvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
       
        public SalesinvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           
            ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            ViewBag.roll = HttpContext.Session.GetObject(SD.Statusroll);

            
            string Branch = ViewBag.branch;
            loadtemp load = new loadtemp();
           

            var billno = _context.salesinvoicesummery.OrderByDescending(i => i.Billid).FirstOrDefault(); //bill No
            if (billno != null)
            {
                ViewBag.Billno = billno.Billid + 1;
            }
            else
            {
                ViewBag.Billno = 1;
            }
            int bill = ViewBag.Billno;
           
            //for print
            if (bill != 0)
            {
                int bi = bill - 1;
                var tmp = _context.tempseccions.Where(i => i.Branch == Branch).ToList();
                var tmpsale = _context.salesinvoices.Where(i => i.Branch == Branch && i.Billno == bi).ToList();
                if (tmp.Count != 0)
                {
                    foreach (var item in tmp)
                    {
                        load.tempseccions.Add(item);
                    }

                }
                else
                {
                    foreach (var item in tmpsale)
                    {
                        load.salesinvoices.Add(item);
                    }
                    var tmpsummery = _context.salesinvoicesummery.Where(i => i.Billid == bi).ToList();
                    if (tmpsummery != null)
                    {

                        foreach (var item in tmpsummery)
                        {
                            // var istdate = TimeZoneInfo.ConvertTimeFromUtc(item.Billdate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                            TimeZoneInfo timeZone = TZConvert.GetTimeZoneInfo("India Standard Time");                           
                            var isdate = TimeZoneInfo.ConvertTime(item.Billdate, timeZone);
                            ViewBag.billdate = isdate;
                            load.salesinvoicesummeries.Add(item);
                        }
                    }

                }
               


               

                
            }
            return View(load);
        }


        [HttpPost]
        public IActionResult addtmp(tempseccion tempseccion)
        {
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
            tempseccion.Billdate =  DateTime.UtcNow;
            tempseccion.Billby = Name;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            var Branch = ViewBag.branch;
            tempseccion.Branch = Branch;
            _context.tempseccions.Add(tempseccion);
            _context.SaveChanges();

            return Json(new { success = true, message = "Save successful." });
          
        }
        [HttpPost]
        public IActionResult addtmpsummery(Salesinvoicesummery tempseccion)
        {
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
            tempseccion.Billdate =  DateTime.UtcNow;
            tempseccion.Billby = Name;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            var Branch = ViewBag.branch;
            tempseccion.Branch = Branch;

            var checkcustomer = _context.customerdetails.Where(i => i.Mobilenumber == tempseccion.Mobilenumber).FirstOrDefault();
            if (checkcustomer == null)
            {
                CustomerDetails cd = new CustomerDetails();
                cd.Mobilenumber = tempseccion.Mobilenumber;
                cd.Customername = tempseccion.Customername;
                cd.Address = tempseccion.Address;
                cd.Branch = Branch;
                cd.Entrydate =  DateTime.UtcNow;
                cd.Entryby = Name;
                _context.customerdetails.Add(cd);
                _context.SaveChanges();
            }
           
            if (tempseccion.Balance != 0)
            {
                tempseccion.status = "Pending";
            }
            else
            {
                tempseccion.status = "Close";
            }
            List<Salesinvoice> salesinvoice = new List<Salesinvoice>();
           
            var tmp = _context.tempseccions.Where(i => i.Billno == tempseccion.Billid).ToList();
            foreach (var item in tmp)
            {
                var ss = new Salesinvoice();

                ss.Productname = item.Productname;
                ss.Category = item.Category;
                ss.Subcategory = item.Subcategory;
                ss.Color = item.Color;
                ss.Brand = item.Brand;
                ss.Rate = item.Rate;
                ss.Quantity = item.Quantity;
                ss.Hsncode = item.Hsncode;
                ss.Amount = item.Amount;
                ss.Billno = item.Billno;
                ss.Billdate = item.Billdate;
                ss.Billby = item.Billby;
                ss.Branch = Branch;

                salesinvoice.Add(ss);
            }
            foreach (var item in tmp)
            {
                var qty = item.Productname;
                var prgb = _context.product.Where(p => p.productname == qty).SingleOrDefault();
                var pquantity = prgb.stock - item.Quantity;
                prgb.stock = pquantity;
                _context.product.Update(prgb);


            }
            _context.salesinvoices.AddRange(salesinvoice);
            _context.salesinvoicesummery.Add(tempseccion);
            _context.SaveChanges();
            var billno = tempseccion.Billid;
            Customerpaymenthistry cph = new Customerpaymenthistry();
            cph.Mobile = tempseccion.Mobilenumber;
            cph.Customername = tempseccion.Customername;
            cph.Address = tempseccion.Address;
            cph.paymenttype = tempseccion.Paymenttype;
            cph.Payment = tempseccion.Paid;
            cph.Recivedby = Name;
            cph.Paiddate =  DateTime.UtcNow;
            cph.Balance = tempseccion.Balance;
            cph.refno = tempseccion.Refcode;
            cph.Branch = Branch;
            cph.total = tempseccion.Totalamount;
            cph.billid = billno;
            _context.customerpaymenthistry.Add(cph);

         
            var cleartmp = _context.tempseccions.Where(i => i.Billno == tempseccion.Billid).ToList();
                _context.tempseccions.RemoveRange(cleartmp);
            _context.SaveChanges();
            

            return Json(new { success = true, message = "Save successful." });
           
        }
        public IActionResult deletetmp(int id)
        {
           var app = _context.tempseccions.Where( i => i.Id == id).FirstOrDefault();
            _context.tempseccions.Remove(app);
            _context.SaveChanges();

            return Json(new { success = true, message = "Delete successful." });
        }

        [HttpPost]
        public JsonResult Getproductname(string Prefix)
        {
            var result = (from N in _context.product
                          where N.productname.Contains(Prefix)
                          select new { N.productname });
            return Json(result);
        }
        [HttpGet]
        public JsonResult fillcol(string name)
        {
           
            var deatils = _context.product.Where(c => c.productname == name).SingleOrDefault();
            return new JsonResult(deatils);

        }
        [HttpGet]
        public JsonResult fillcusdetails(string mob)
        {

            var deatils = _context.customerdetails.Where(c => c.Mobilenumber == mob).SingleOrDefault();
            return new JsonResult(deatils);

        }


    }
}
