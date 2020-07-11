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
    public class MyReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
      
        public MyReportsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Salesreport()
        {
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            var sr = _context.salesinvoicesummery.Where(i => i.Branch == Branch && i.status !="Return" ).ToList();
            return View(sr);
        }
        public IActionResult Purchasereport()
        {
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            var sr = _context.purchaseinvoicesummeries.Where(i => i.status !="Return").ToList();
            return View(sr);
        }

        public IActionResult Customerpayment(string id)
        {
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
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
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            cph.Paiddate =  DateTime.UtcNow;
            cph.Recivedby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            cph.Branch = Branch;

            var billhis = _context.customerpaymenthistry.Add(cph);
            var billamount = _context.salesinvoicesummery.Where(i => i.Billid == cph.billid).FirstOrDefault();
           decimal paid = cph.Payment;
            decimal cal = billamount.Balance - paid;
            decimal calamt = billamount.Totalamount + paid;
            billamount.Balance = cal;
            billamount.Totalamount = calamt;


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
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
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
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            cph.Paiddate =  DateTime.UtcNow;
            cph.Recivedby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            cph.Branch = Branch;

            var billhis = _context.vendorpayments.Add(cph);
            var billamount = _context.purchaseinvoicesummeries.Where(i => i.Billid == cph.billid).FirstOrDefault();
            decimal paid = cph.Payment;
            decimal cal = billamount.Balance - paid;
            billamount.Balance = cal;

            decimal calamt = billamount.Totalamount + paid;
           
            billamount.Totalamount = calamt;
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

            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
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
                    //var istdate = TimeZoneInfo.ConvertTimeFromUtc(item.Billdate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    //ViewBag.billdate = istdate;
                    TimeZoneInfo timeZone = TZConvert.GetTimeZoneInfo("India Standard Time");
                    var isdate = TimeZoneInfo.ConvertTime(item.Billdate, timeZone);
                    ViewBag.billdate = isdate;
                    load.salesinvoicesummeries.Add(item);
                        }
                  






            }
            return View(load);
        }


        public IActionResult Salesreturnreport()
        {
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            var sr = _context.salesreturnsummeries.ToList();
            return View(sr);
        }
     
        public IActionResult Salesreturnbill(int id)
        {

            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            loadtemp load = new loadtemp();


            //for print
            if (id != 0)
            {
              
                var tmp = _context.tmpsalesreturns.Where(i => i.Branch == Branch && i.Billno == id).ToList();
                var tmpsale = _context.salesinvoices.Where(i => i.Branch == Branch && i.Billno == id).ToList();
                if (tmpsale.Count != 0)               
                {

                    if (tmp != null)
                    {
                        foreach (var item in tmp)
                        {
                            load.tmpsalesreturns.Add(item);
                        }
                    }
                    foreach (var item in tmpsale)
                    {
                        load.salesinvoices.Add(item);
                    }
                    var tmpsummery = _context.salesinvoicesummery.Where(i => i.Billid == id && i.Branch == Branch).ToList();
                    if (tmpsummery != null)
                    {

                        foreach (var item in tmpsummery)
                        {
                            load.salesinvoicesummeries.Add(item);
                        }
                    }

                }






            }
            return View(load);
        }

        public IActionResult addtmpreturnsummery(Salesreturnsummery tempseccion)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            tempseccion.Billdate = DateTime.UtcNow;
            tempseccion.Billby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            tempseccion.Branch = Branch;
            int totalqtys = 0;
            decimal toamt = 0;
            //Sales Return Table Values Save Method
            if (tempseccion.Balance != 0)
            {
                tempseccion.status = "Pending";
            }
            else
            {
                tempseccion.status = "Close";
            }
            List<SalesReturn> salesinvoice = new List<SalesReturn>();

            var tmp = _context.tmpsalesreturns.Where(i => i.Billno == tempseccion.Billid && i.Branch == tempseccion.Branch).ToList();
            foreach (var item in tmp)
            {
                var ss = new SalesReturn();

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
                ss.Resion = item.Reasion;

                salesinvoice.Add(ss);
            }// Quanrity Added
            foreach (var item in tmp)
            {
                var qty = item.Productname;
                var prgb = _context.product.Where(p => p.productname == qty).SingleOrDefault();
                var pquantity = prgb.stock + item.Quantity;
                prgb.stock = pquantity;
                _context.product.Update(prgb);


            }
                    

            var salesbill = _context.salesinvoices.Where(i => i.Billno == tempseccion.Billid && i.Branch == tempseccion.Branch).ToList();
            var salessummery = _context.salesinvoicesummery.Where(i => i.Billid == tempseccion.Billid && i.Branch == tempseccion.Branch).FirstOrDefault();

           
            var billnum = _context.salesinvoicesummery.OrderByDescending(i => i.Billid).Where(i =>  i.Branch == tempseccion.Branch).FirstOrDefault();
            var nobill = billnum.Billid + 1;
            List<Salesinvoice> salesinvoicebill = new List<Salesinvoice>();
            foreach (var item in salesbill)
            {
              
                var sin = new Salesinvoice();
                var tmpqty = _context.salesinvoices.Where(i => i.Billno == tempseccion.Billid && i.Branch == tempseccion.Branch && i.Productname == item.Productname).FirstOrDefault();
                var returnqty = _context.tmpsalesreturns.Where(i => i.Billno == tempseccion.Billid && i.Branch == tempseccion.Branch && i.Productname == item.Productname).FirstOrDefault();
                           
                sin.Productname = item.Productname;
                sin.Category = item.Category;
                sin.Subcategory = item.Subcategory;
                sin.Color = item.Color;
                sin.Brand = item.Brand;
                sin.Rate = item.Rate;
                sin.Hsncode = item.Hsncode;
                sin.Amount = item.Amount;
                var qty = 0;
                if (returnqty != null)
                {
                    qty = tmpqty.Quantity - returnqty.Quantity;
                }
                else
                {
                    qty = tmpqty.Quantity;
                }

                if (qty !=0)
                {
                    sin.Quantity = qty;
                    totalqtys = totalqtys + qty;
                    var tm = item.Rate * qty ;
                    toamt =+ tm;
                    ViewBag.totalqty = totalqtys;
                    ViewBag.totalamt = toamt;
                    sin.Amount = item.Rate * qty;
                    sin.Billno = nobill;

                    sin.Billdate = DateTime.UtcNow;
                    sin.Billby = Name;
                    sin.Branch = Branch;
                    salesinvoicebill.Add(sin);

                }
               
               
            }
            if (salesinvoicebill != null)
            {
                _context.salesinvoices.AddRange(salesinvoicebill);
            }
           

            Salesinvoicesummery sis = new Salesinvoicesummery();
            
            sis.Billid = nobill;
            sis.Totalqty = salessummery.Totalqty - tempseccion.Totalqty;
            sis.Totalamount = salessummery.Totalamount - tempseccion.Totalamount - salessummery.Cgst - salessummery.Sgst;
            sis.Gst = tempseccion.Gst;
            var gstcal = (salessummery.Totalamount * sis.Gst) / 100;
            if (sis.Gst != 0)            {
              
                sis.Cgst = gstcal / 2;
                sis.Sgst = gstcal / 2;
                sis.Igst = 0;
            }
            else
            {
                sis.Cgst =0;
                sis.Sgst = 0;
                sis.Igst = 0;

            }
           
            sis.Paymenttype = tempseccion.Paymenttype;
            sis.Refcode = tempseccion.Refcode;
            sis.discount = 0;
            sis.Balance = 0;
            sis.nettotal = sis.Totalamount + gstcal;
            sis.Billdate = DateTime.UtcNow;
            sis.Billby = Name;
            sis.status = "Close";
            sis.Customername = tempseccion.Customername;
            sis.Mobilenumber = tempseccion.Mobilenumber;
            sis.Address = tempseccion.Address;
            sis.Paid = sis.nettotal;
            sis.Branch = Branch;


            if (sis != null)
            {
                _context.salesinvoicesummery.Add(sis);
               
            }
            //Store Histry
            var billno = tempseccion.Billid;
            Salesreturnpaymenthistry cph = new Salesreturnpaymenthistry();
            cph.Mobile = tempseccion.Mobilenumber;
            cph.Customername = tempseccion.Customername;
            cph.Address = tempseccion.Address;
            cph.paymenttype = tempseccion.Paymenttype;
            cph.Payment = tempseccion.Paid;
            cph.Recivedby = Name;
            cph.Paiddate = DateTime.UtcNow;
            cph.Balance = tempseccion.Balance;
            cph.refno = tempseccion.Refcode;
            cph.Branch = Branch;
            cph.billid = billno;
            _context.salesreturnpaymenthistries.Add(cph);

            var cleartmp = _context.tmpsalesreturns.Where(i => i.Billno == tempseccion.Billid && i.Branch == cph.Branch).ToList();
            _context.tmpsalesreturns.RemoveRange(cleartmp);
           

            var statusclose = _context.salesinvoicesummery.Where(i => i.Billid == tempseccion.Billid && i.Branch == cph.Branch).FirstOrDefault();
            statusclose.status = "Return";
            _context.salesinvoicesummery.Update(statusclose);
            _context.salesreturns.AddRange(salesinvoice);
            _context.salesreturnsummeries.Add(tempseccion);
          
            _context.SaveChanges();
            return Json(new { success = true, message = "Save successful." });

        }
        //[HttpDelete]
        public IActionResult deletetmpreturn(int id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            var app = _context.tmpsalesreturns.Where(i => i.Id == id && i.Branch == Branch).FirstOrDefault();
            _context.tmpsalesreturns.Remove(app);
            _context.SaveChanges();

            return Json(new { success = true, message = "Delete successful." });
        }

        [HttpPost]
        public IActionResult SbillAddTmp(Tmpsalesreturn tmpsalesreturn)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            tmpsalesreturn.Billdate = DateTime.UtcNow;
            tmpsalesreturn.Billby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            tmpsalesreturn.Branch = Branch;
            var amount = tmpsalesreturn.Rate * tmpsalesreturn.Quantity;
            tmpsalesreturn.Amount = amount;
            _context.tmpsalesreturns.Add(tmpsalesreturn);
            _context.SaveChanges();
            return Json(new { success = true, message = "Save successfull." });
        }




        public IActionResult Purchasereturnreport()
        {
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            var sr = _context.purchasereturnsummeries.ToList();
            return View(sr);
        }
        public IActionResult Purchasereturnbill(int id)
        {

            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            loadtemp load = new loadtemp();


            //for print
            if (id != 0)
            {

                var tmp = _context.tmppurchasereturns.Where(i => i.Branch == Branch && i.Billno == id).ToList();
                var tmpsale = _context.purchaseinvoices.Where(i => i.Branch == Branch && i.Billno == id).ToList();
                if (tmpsale.Count != 0)
                {

                    if (tmp != null)
                    {
                        foreach (var item in tmp)
                        {
                            load.tmppurchasereturns.Add(item);
                        }
                    }
                    foreach (var item in tmpsale)
                    {
                        load.purchaseinvoices.Add(item);
                    }
                    var tmpsummery = _context.purchaseinvoicesummeries.Where(i => i.Billid == id && i.Branch == Branch).ToList();
                    if (tmpsummery != null)
                    {

                        foreach (var item in tmpsummery)
                        {
                            load.purchaseinvoicesummerys.Add(item);
                        }
                    }

                }

            }
            return View(load);
        }

        public IActionResult addtmpPurchasereturnsummery(Purchasereturnsummery tempseccion)
        
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            tempseccion.Billdate = DateTime.UtcNow;
            tempseccion.Billby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            tempseccion.Branch = Branch;
            int totalqtys = 0;
            decimal toamt = 0;
            //Sales Return Table Values Save Method
            if (tempseccion.Balance != 0)
            {
                tempseccion.status = "Pending";
            }
            else
            {
                tempseccion.status = "Close";
            }
            List<Purchasereturn> salesinvoice = new List<Purchasereturn>();

            var tmp = _context.tmppurchasereturns.Where(i => i.Billno == tempseccion.Billid && i.Branch == tempseccion.Branch).ToList();
            foreach (var item in tmp)
            {
                var ss = new Purchasereturn();

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
                ss.Resion = item.Reasion;

                salesinvoice.Add(ss);
            }// Quanrity Added
            foreach (var item in tmp)
            {
                var qty = item.Productname;
                var prgb = _context.product.Where(p => p.productname == qty).SingleOrDefault();
                var pquantity = prgb.stock - item.Quantity;
                prgb.stock = pquantity;
                _context.product.Update(prgb);


            }


            var salesbill = _context.purchaseinvoices.Where(i => i.Billno == tempseccion.Billid && i.Branch == tempseccion.Branch).ToList();
            var salessummery = _context.purchaseinvoicesummeries.Where(i => i.Billid == tempseccion.Billid && i.Branch == tempseccion.Branch).FirstOrDefault();


            var billnum = _context.purchaseinvoicesummeries.OrderByDescending(i => i.Billid).Where(i => i.Branch == tempseccion.Branch).FirstOrDefault();
            var nobill = billnum.Billid + 1;
            List<Purchaseinvoice> salesinvoicebill = new List<Purchaseinvoice>();
            foreach (var item in salesbill)
            {

                var sin = new Purchaseinvoice();
                var tmpqty = _context.purchaseinvoices.Where(i => i.Billno == tempseccion.Billid && i.Branch == tempseccion.Branch && i.Productname == item.Productname).FirstOrDefault();
                var returnqty = _context.tmppurchasereturns.Where(i => i.Billno == tempseccion.Billid && i.Branch == tempseccion.Branch && i.Productname == item.Productname).FirstOrDefault();

                sin.Productname = item.Productname;
                sin.Category = item.Category;
                sin.Subcategory = item.Subcategory;
                sin.Color = item.Color;
                sin.Brand = item.Brand;
                sin.Rate = item.Rate;
                sin.Hsncode = item.Hsncode;
                sin.Amount = item.Amount;
                var qty = 0;
                if (returnqty != null)
                {
                    qty = tmpqty.Quantity - returnqty.Quantity;
                }
                else
                {
                    qty = tmpqty.Quantity;
                }

                if (qty != 0)
                {
                    sin.Quantity = qty;
                    totalqtys = totalqtys + qty;
                    var tm = item.Rate * qty;
                    toamt = +tm;
                    ViewBag.totalqty = totalqtys;
                    ViewBag.totalamt = toamt;
                    sin.Amount = item.Rate * qty;
                    sin.Billno = nobill;

                    sin.Billdate = DateTime.UtcNow;
                    sin.Billby = Name;
                    sin.Branch = Branch;
                    salesinvoicebill.Add(sin);

                }


            }
            if (salesinvoicebill != null)
            {
                _context.purchaseinvoices.AddRange(salesinvoicebill);
            }


            Purchaseinvoicesummery sis = new Purchaseinvoicesummery();

            var file = _context.purchaseinvoicesummeries.Where(i => i.Billid == tempseccion.Billid).FirstOrDefault();
          

            sis.Billid = nobill;
            sis.Totalqty = file.Totalqty - tempseccion.Totalqty;
            sis.Totalamount = file.Totalamount - tempseccion.Totalamount ;
            if (file.Gst =="GST")
            {
                sis.Gst = "GST";
            }
            else
            {
                sis.Gst = "NOGST";
            }
           
           

            sis.Paymenttype = tempseccion.Paymenttype;
            sis.Refcode = tempseccion.Refcode;
          
            sis.Balance = 0;
          
            sis.Billdate = DateTime.UtcNow;
            sis.Billby = Name;
            if (file.Balance == 0)
            {
                sis.status = "Close";
            }
            else
            {
                sis.status = "Pending";
            }
           
            sis.Vendorrname = tempseccion.Customername;
            sis.Mobilenumber = tempseccion.Mobilenumber;
            sis.Address = tempseccion.Address;
            sis.paid = tempseccion.Paid;
            sis.Branch = Branch;
            sis.upload = file.upload;


            if (sis != null)
            {
                _context.purchaseinvoicesummeries.Add(sis);

            }
            //Store Histry
            var billno = tempseccion.Billid;
            Purchasereturnpaymenthistry cph = new Purchasereturnpaymenthistry();
            cph.Mobile = tempseccion.Mobilenumber;
            cph.Customername = tempseccion.Customername;
            cph.Address = tempseccion.Address;
            cph.paymenttype = tempseccion.Paymenttype;
            cph.Payment = tempseccion.Paid;
            cph.Recivedby = Name;
            cph.Paiddate = DateTime.UtcNow;
            cph.Balance = tempseccion.Balance;
            cph.refno = tempseccion.Refcode;
            cph.Branch = Branch;
            cph.billid = billno;
            _context.purchasereturnpaymenthistries.Add(cph);

            var cleartmp = _context.tmppurchasereturns.Where(i => i.Billno == tempseccion.Billid && i.Branch == cph.Branch).ToList();
            _context.tmppurchasereturns.RemoveRange(cleartmp);


            var statusclose = _context.purchaseinvoicesummeries.Where(i => i.Billid == tempseccion.Billid && i.Branch == cph.Branch).FirstOrDefault();
            statusclose.status = "Return";
            _context.purchaseinvoicesummeries.Update(statusclose);
            _context.purchasereturns.AddRange(salesinvoice);
            _context.purchasereturnsummeries.Add(tempseccion);

            _context.SaveChanges();
            return Json(new { success = true, message = "Save successful." });

        }
        //[HttpDelete]
        public IActionResult deletetmpPurchasereturn(int id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            var app = _context.tmppurchasereturns.Where(i => i.Id == id && i.Branch == Branch).FirstOrDefault();
            _context.tmppurchasereturns.Remove(app);
            _context.SaveChanges();

            return Json(new { success = true, message = "Delete successful." });
        }

        [HttpPost]
        public IActionResult SbillAddPurchaseTmp(Tmppurchasereturn tmpsalesreturn)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            var Name = ViewBag.data;
            tmpsalesreturn.Billdate = DateTime.UtcNow;
            tmpsalesreturn.Billby = Name;
            ViewBag.branch = HttpContext.Session.GetString("branch");
            var Branch = ViewBag.branch;
            tmpsalesreturn.Branch = Branch;
            var amount = tmpsalesreturn.Rate * tmpsalesreturn.Quantity;
            tmpsalesreturn.Amount = amount;
            _context.tmppurchasereturns.Add(tmpsalesreturn);
            _context.SaveChanges();
            return Json(new { success = true, message = "Save successfull." });
        }


        public IActionResult printpurchasereturn(int id)
        {

            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();
            ViewBag.CustomerPending = pendingcustomer.Count();
            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();
            ViewBag.VendorPending = pendingvendor.Count();
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            loadtemp load = new loadtemp();

            var billno = _context.purchasereturnsummeries.Where(i => i.Billid == id).FirstOrDefault(); //bill No         


            if (billno != null)
            {

                var tmpsale = _context.purchasereturns.Where(i => i.Branch == Branch && i.Billno == id).ToList();

                foreach (var item in tmpsale)
                {

                    load.purchasereturns.Add(item);
                }
                var tmpsummery = _context.purchasereturnsummeries.Where(i => i.Billid == id).ToList();


                foreach (var item in tmpsummery)
                {
                    var istdate = TimeZoneInfo.ConvertTimeFromUtc(item.Billdate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    ViewBag.billdate = istdate;
                    load.purchasereturnsummeries.Add(item);
                }







            }
            return View(load);
        }


    }
}
