using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailMeBilling.Data;
using MailMeBilling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailMeBilling.Controllers
{
    public class PurchaseinvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PurchaseinvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            DateTime todaydate = DateTime.UtcNow;
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


            var billno = _context.purchaseinvoicesummeries.OrderByDescending(i => i.Billid).FirstOrDefault();
            if (billno != null)
            {
                ViewBag.Billno = billno.Billid + 1;
            }
            else
            {
                ViewBag.Billno = 1;
            }
            int bill = ViewBag.Billno;
            var tmp = _context.tmppurchases.Where(i => i.Branch == Branch).ToList();
            foreach (var item in tmp)
            {
                load.tmppurchases.Add(item);
            }
           
            
            return View(load);
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
        [HttpPost]
        public IActionResult addtmp(Tmppurchase tempseccion)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            tempseccion.Billdate = DateTime.UtcNow;
            tempseccion.Billby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            tempseccion.Branch = Branch;
            _context.tmppurchases.Add(tempseccion);
            _context.SaveChanges();

            return Json(new { success = true, message = "Save successful." });

        }
        [HttpPost]
        public async Task<IActionResult> addtmpsummery(Purchaseinvoicesummery tempseccion , List<IFormFile> upload)
        {
            foreach (var item in upload)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        tempseccion.upload = stream.ToArray();
                    }
                }
            }
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            tempseccion.Billdate = DateTime.UtcNow;
            tempseccion.Billby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            tempseccion.Branch = Branch;
            var checkcustomer = _context.vendor.Where(i => i.Mobilenumber == tempseccion.Mobilenumber).FirstOrDefault();
            if (checkcustomer == null)
            {
                Vendor cd = new Vendor();
                cd.Mobilenumber = tempseccion.Mobilenumber;
                cd.Name = tempseccion.Vendorrname;
                cd.Address = tempseccion.Address;
                cd.Branch = Branch;
                cd.Entrydate = DateTime.UtcNow;
                cd.Entryby = Name;
                _context.vendor.Add(cd);
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
            List<Purchaseinvoice> salesinvoice = new List<Purchaseinvoice>();

            var tmp = _context.tmppurchases.Where(i => i.Billno == tempseccion.Billid).ToList();
            foreach (var item in tmp)
            {
                var ss = new Purchaseinvoice();

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
                var pquantity = prgb.stock + item.Quantity;
                prgb.stock = pquantity;
                _context.product.Update(prgb);


            }
            _context.purchaseinvoices.AddRange(salesinvoice);
            _context.purchaseinvoicesummeries.Add(tempseccion);
            _context.SaveChanges();

            var billno = tempseccion.Billid;
            Vendorpayment cph = new Vendorpayment();
            cph.Mobile = tempseccion.Mobilenumber;
            cph.name = tempseccion.Vendorrname;
            cph.Address = tempseccion.Address;
            cph.paymenttype = tempseccion.Paymenttype;
            cph.Payment = tempseccion.paid;
            cph.Recivedby = Name;
            cph.Paiddate = DateTime.UtcNow;
            cph.Balance = tempseccion.Balance;
            cph.refno = tempseccion.Refcode;
            cph.Branch = Branch;
            cph.total = tempseccion.Totalamount;
            cph.billid = billno;
            _context.vendorpayments.Add(cph);

          
            var cleartmp = _context.tmppurchases.Where(i => i.Billno == tempseccion.Billid).ToList();
            _context.tmppurchases.RemoveRange(cleartmp);
            _context.SaveChanges();


            // return Json(new { success = true, message = "Save successful." });
            return RedirectToAction("Index");
        }
        public IActionResult deletetmp(int id)
        {
            var app = _context.tmppurchases.Where(i => i.Id == id).FirstOrDefault();
            _context.tmppurchases.Remove(app);
            _context.SaveChanges();

            return Json(new { success = true, message = "Delete successful." });
        }

        [HttpGet]
        public JsonResult fillvendordetails(string mob)
        {

            var deatils = _context.vendor.Where(c => c.Mobilenumber == mob).SingleOrDefault();
            return new JsonResult(deatils);

        }
    }
}
