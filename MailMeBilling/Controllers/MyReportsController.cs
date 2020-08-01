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
           
         
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
          
            string Branch = ViewBag.branch;
            var sr = _context.salesinvoicesummery.Where(i => i.Branch == Branch && i.status !="Return" ).ToList();
            return View(sr);
        }
        public IActionResult Purchasereport()
        {
           
            var sr = _context.purchaseinvoicesummeries.Where(i => i.status !="Return").ToList();
            return View(sr);
        }

        public IActionResult Customerpayment(string id)
        {
          
            var list = _context.customerdetails.Where(i => i.Mobilenumber == id).FirstOrDefault();
            var bill = _context.salesinvoicesummery.Where(i => i.Mobilenumber == id).ToList();
            ViewBag.billsummery = bill;
            var histry = _context.customerpaymenthistry.Where(i => i.Mobile == id).ToList();
            ViewBag.histry = histry;


            return View(list);
        }
        public IActionResult Updatepayments(Customerpaymenthistry cph)
        {
          
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
           // cph.Paiddate =  DateTime.UtcNow;
            cph.Recivedby = Name;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            var Branch = ViewBag.branch;
            cph.Branch = Branch;

          
            var billamount = _context.salesinvoicesummery.Where(i => i.Billid == cph.billid).FirstOrDefault();
           decimal paid = cph.Payment;
            decimal cal = billamount.Balance - paid;
            decimal calamt = billamount.Paid + paid;
            billamount.Balance = cal;
            billamount.Paid = calamt;
            cph.total = billamount.Totalamount;

            if (cal == 0)
            {
                billamount.status = "Close";
            }
            var billhis = _context.customerpaymenthistry.Add(cph);
            _context.salesinvoicesummery.Update(billamount);
            _context.SaveChanges();



            return Json(new { success = true, message = "Save successfull." });
        }

        public IActionResult Vendorpayment(string id)
        {
           
            var list = _context.vendor.Where(i => i.Mobilenumber == id).FirstOrDefault();
            var bill = _context.purchaseinvoicesummeries.Where(i => i.Mobilenumber == id).ToList();
            ViewBag.billsummery = bill;
            var histry = _context.vendorpayments.Where(i => i.Mobile == id).ToList();
            ViewBag.histry = histry;


            return View(list);
        }
        public IActionResult Updatevendorpayments(Vendorpayment cph)
        {
          
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
            //cph.Paiddate =  DateTime.UtcNow;
            cph.Recivedby = Name;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            var Branch = ViewBag.branch;
            cph.Branch = Branch;

            var billhis = _context.vendorpayments.Add(cph);
            var billamount = _context.purchaseinvoicesummeries.Where(i => i.Billid == cph.billid).FirstOrDefault();
            decimal paid = cph.Payment;
            decimal cal = billamount.Balance - paid;
            billamount.Balance = cal;

            decimal calamt = billamount.paid + paid;
           
            billamount.paid = calamt;
            if (cal == 0)
            {
                billamount.status = "Close";
            }
            _context.purchaseinvoicesummeries.Update(billamount);
            Creditpaymenthistry cph1 = new Creditpaymenthistry();
            cph1.Mobile = cph.Mobile;
            cph1.Customername = cph.name;
            cph1.Address = cph.Address;
            cph1.paymenttype = cph.paymenttype;
            cph1.Payment = cph.Balance;
            cph1.Recivedby = Name;
            cph1.Paiddate = DateTime.UtcNow;
            cph1.Balance = cph.Balance;
            cph1.refno = cph.refno;
            cph1.Branch = Branch;
            cph1.total = cph.total;
            cph1.billid = cph.billid;
            _context.creditpaymenthistries.Add(cph1);

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
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
          
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
                    
                    TimeZoneInfo timeZone = TZConvert.GetTimeZoneInfo("India Standard Time");
                    var isdate = TimeZoneInfo.ConvertTime(item.Billdate, timeZone);
                    ViewBag.billdate = isdate;
                    load.salesinvoicesummeries.Add(item);
                        }
                  






            }
            return View(load);
        }

        public IActionResult printsr(int id)
        {

            
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
         
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
           
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            string Branch = ViewBag.branch;
            var sr = _context.salesinvoicesummery.Where(i => i.status == "Return" && i.Branch == Branch).ToList();
            return View(sr);
        }
     
        public IActionResult Salesreturnbill(int id)
        {

          
          
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
          ;
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
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
            tempseccion.Billdate = DateTime.UtcNow;
            tempseccion.Billby = Name;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
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
            sis.Refcode = salessummery.Refcode;
            sis.discount = 0;
            sis.Balance = tempseccion.Balance;
            sis.nettotal = sis.Totalamount + gstcal;
            sis.Billdate = DateTime.UtcNow;
            sis.Billby = Name;
            if (sis.Balance !=0)
            {
                sis.status = "Pending";
            }
            else
            {
                sis.status = "Close";
            }
           
            sis.Customername = tempseccion.Customername;
            sis.Mobilenumber = tempseccion.Mobilenumber;
            sis.Address = tempseccion.Address;
            sis.Paid = tempseccion.Paid;
            sis.Branch = Branch;


            if (sis != null)
            {
                _context.salesinvoicesummery.Add(sis);
               
            }
            Customerpaymenthistry cph1 = new Customerpaymenthistry();
            cph1.Mobile = tempseccion.Mobilenumber;
            cph1.Customername = tempseccion.Customername;
            cph1.Address = tempseccion.Address;
            cph1.paymenttype = tempseccion.Paymenttype;
            cph1.Payment = tempseccion.Paid;
            cph1.Recivedby = Name;
            cph1.Paiddate = DateTime.UtcNow;
            cph1.Balance = tempseccion.Balance;
            cph1.refno = tempseccion.Refcode;
            cph1.Branch = Branch;
            cph1.total = sis.nettotal;
            cph1.billid = nobill;
            _context.customerpaymenthistry.Add(cph1);
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
        public IActionResult salesreturn(int id)
        {
            var checking = _context.salesinvoicesummery.Where(i => i.Billid == id && i.status == "Return").FirstOrDefault();
            if (checking == null)
            {
                ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
                string Branch = ViewBag.branch;
                ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
                var Name = ViewBag.data;
                var bill = _context.salesinvoicesummery.Where(i => i.Billid == id && i.Branch == Branch).FirstOrDefault();
                bill.status = "Return";
                _context.salesinvoicesummery.Update(bill);


                var tmp = _context.salesinvoices.Where(i => i.Billno == id && i.Branch == Branch).ToList();

                foreach (var item in tmp)
                {
                    var qty = item.Productname;
                    var prgb = _context.product.Where(p => p.productname == qty).SingleOrDefault();
                    var pquantity = prgb.stock + item.Quantity;
                    prgb.stock = pquantity;
                    _context.product.Update(prgb);


                }

                var mob = bill.Mobilenumber;
                creditnote cn = new creditnote();
                cn.cdate = DateTime.UtcNow;
                cn.branch = Branch;
                cn.totalamount = bill.Paid;
                cn.person = "customer";
                cn.mobilenumber = bill.Mobilenumber;
                cn.name = bill.Customername;
                cn.address = bill.Address;
                cn.addby = Name;
                cn.paymenttype = bill.Paymenttype;
                cn.refno = bill.Refcode;
                cn.particular = "Sales Return Amount for B.No " + id;
                _context.creditnote.Add(cn);
                _context.SaveChanges();
                var billno = cn.cid;

                Creditpaymenthistry cph = new Creditpaymenthistry();
                cph.Mobile = bill.Mobilenumber;
                cph.Customername = bill.Customername;
                cph.Address = bill.Address;
                cph.paymenttype = bill.Paymenttype;
                cph.Payment = bill.Paid;
                cph.Recivedby = Name;
                cph.Paiddate = DateTime.UtcNow;
                cph.Balance = bill.Paid;
                cph.refno = bill.Refcode;
                cph.Branch = Branch;
                cph.total = bill.Paid;
                cph.billid = billno;
                _context.creditpaymenthistries.Add(cph);
                _context.SaveChanges();
                return RedirectToAction("Index", "Salesinvoice");

            }
           

            return RedirectToAction("Salesreport","MyReports");
        
        }
        public IActionResult purchasereturn(int id)
        {
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            string Branch = ViewBag.branch;
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
            var bill = _context.purchaseinvoicesummeries.Where(i => i.Billid == id && i.Branch == Branch).FirstOrDefault();
            bill.status = "Return";
            _context.purchaseinvoicesummeries.Update(bill);
        
            creditnote cn = new creditnote();
            cn.cdate = DateTime.UtcNow;
            cn.branch = Branch;
            cn.totalamount = bill.Totalamount - bill.Balance;
            cn.person = "vendor";
            cn.mobilenumber = bill.Mobilenumber;
            cn.name = bill.Vendorrname;
            cn.address = bill.Address;
            cn.addby = Name;
            cn.paymenttype = bill.Paymenttype;
            cn.refno = bill.Refcode;
            cn.particular = "Purchase Return Amount for B.No " + id;
            _context.creditnote.Add(cn);
            _context.SaveChanges();
            var billno = cn.cid;

            Creditpaymenthistry cph = new Creditpaymenthistry();
            cph.Mobile = bill.Mobilenumber;
            cph.Customername = bill.Vendorrname;
            cph.Address = bill.Address;
            cph.paymenttype = bill.Paymenttype;
            cph.Payment = bill.Balance;
            cph.Recivedby = Name;
            cph.Paiddate = DateTime.UtcNow;
            cph.Balance = bill.Balance;
            cph.refno = bill.Refcode;
            cph.Branch = Branch;
            cph.total = bill.Totalamount;
            
            cph.billid = billno;
            _context.creditpaymenthistries.Add(cph);
          
            var tmp = _context.salesinvoices.Where(i => i.Billno == id && i.Branch == Branch).ToList();
            foreach (var item in tmp)
            {
                var qty = item.Productname;
                var prgb = _context.product.Where(p => p.productname == qty).SingleOrDefault();
                var pquantity = prgb.stock - item.Quantity;
                prgb.stock = pquantity;
                _context.product.Update(prgb);
            }          
           
            _context.SaveChanges();

            return RedirectToAction("Index", "Purchaseinvoice");

        }
        public IActionResult deletetmpreturn(int id)
        {
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
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
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
            tmpsalesreturn.Billdate = DateTime.UtcNow;
            tmpsalesreturn.Billby = Name;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
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
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            ViewBag.roll = HttpContext.Session.GetString("roll");
            var sr = _context.purchaseinvoicesummeries.Where(i => i.status =="Return").ToList();
            return View(sr);
        }
        public IActionResult Purchasereturnbill(int id)
        {

           
         
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
         
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
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
            tempseccion.Billdate = DateTime.UtcNow;
            tempseccion.Billby = Name;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
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
          
            Vendorpayment cph1 = new Vendorpayment();
            cph1.Mobile = tempseccion.Mobilenumber;
            cph1.name = tempseccion.Customername;
            cph1.Address = tempseccion.Address;
            cph1.paymenttype = tempseccion.Paymenttype;
            cph1.Payment = tempseccion.Paid;
            cph1.Recivedby = Name;
            cph1.Paiddate = DateTime.UtcNow;
            cph1.Balance = tempseccion.Balance;
            cph1.refno = tempseccion.Refcode;
            cph1.Branch = Branch;
            cph1.total = tempseccion.Totalamount;
            cph1.billid = billno;
            _context.vendorpayments.Add(cph1);

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
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
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
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            var Name = ViewBag.data;
            tmpsalesreturn.Billdate = DateTime.UtcNow;
            tmpsalesreturn.Billby = Name;
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
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

          
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
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
                    // var istdate = TimeZoneInfo.ConvertTimeFromUtc(item.Billdate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    TimeZoneInfo timeZone = TZConvert.GetTimeZoneInfo("India Standard Time");
                    var isdate = TimeZoneInfo.ConvertTime(item.Billdate, timeZone);
                    ViewBag.billdate = isdate;
                    load.purchasereturnsummeries.Add(item);
                }







            }
            return View(load);
        }

        public IActionResult Salestatement()
        {
           
          
            var sr = _context.customerdetails.ToList().Distinct();
            return View(sr);
        }

        public IActionResult viewcustomerstatement(string Mobnumber)

        {
            
          

            cusstatement cusstatement = new cusstatement();

            var customerdetil = _context.customerdetails.Where(i => i.Mobilenumber == Mobnumber).FirstOrDefault();

            var sumofamount = _context.salesinvoicesummery.Where(i => i.Mobilenumber == Mobnumber && i.status != "Return").Sum(i => i.Totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;

            var sumofreturn = _context.salesinvoicesummery.Where(i => i.Mobilenumber == Mobnumber && i.status =="Return").Sum(i => i.Balance).ToString();
            ViewBag.sumofreturn = sumofreturn;
            var sumofreturntotal = _context.salesinvoicesummery.Where(i => i.Mobilenumber == Mobnumber && i.status == "Return").Sum(i => i.Paid).ToString();
            ViewBag.sumofreturntotal = sumofreturntotal;

            cusstatement.customerdetails.Add(customerdetil);
            var returnlist = _context.salesinvoicesummery.Where(i => i.Mobilenumber == Mobnumber ).ToList();
            if (returnlist.Count != 0)
            {
                foreach (var billno in returnlist)
                {
                    var paymenthistry = _context.customerpaymenthistry.Where(i => i.Mobile == Mobnumber && i.billid == billno.Billid).ToList();
                    foreach (var item in paymenthistry)
                    {
                        cusstatement.customerpaymenthistries.Add(item);
                    }
                }
            }     var creditnote = _context.creditnote.Where(i => i.mobilenumber == Mobnumber).ToList();
                    foreach (var item in creditnote)
                    {
                var creditnotehis = _context.creditpaymenthistries.Where(i => i.Mobile == Mobnumber && i.billid == item.cid).ToList();
                foreach (var itemc in creditnotehis)
                {
                    cusstatement.creditpaymenthistries.Add(itemc);

                }
                cusstatement.creditnotes.Add(item);

                    }
          

           



            return View(cusstatement);
        }


        public IActionResult Purshasestatement()
        {
           
            var sr = _context.vendor.ToList().Distinct();
            return View(sr);
        }

        public IActionResult viewvendorstatement(string Mobnumber)
        {
           

            cusstatement cusstatement = new cusstatement();

            var customerdetil = _context.vendor.Where(i => i.Mobilenumber == Mobnumber).FirstOrDefault();

            var sumofamount = _context.purchaseinvoicesummeries.Where(i => i.Mobilenumber == Mobnumber && i.status !="Return" ).Sum(i => i.Totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;         

            var sumofreturnbill = _context.purchaseinvoicesummeries.Where(i => i.Mobilenumber == Mobnumber && i.status == "Return").ToList();
            ViewBag.sumofreturn = sumofreturnbill;

            var sumofreturntotal = _context.purchaseinvoicesummeries.Where(i => i.Mobilenumber == Mobnumber && i.status == "Return").Sum(i => i.Totalamount).ToString();
            ViewBag.sumofreturntotal = sumofreturntotal;

            cusstatement.vendors.Add(customerdetil);
            var paymenthistry = _context.vendorpayments.Where(i => i.Mobile == Mobnumber).ToList();
            foreach (var item in paymenthistry)
            {

                cusstatement.vendorpayments.Add(item);
            }
            var creditnote = _context.creditnote.Where(i => i.mobilenumber == Mobnumber).ToList();
            foreach (var item in creditnote)
            {
                var creditnotehis = _context.creditpaymenthistries.Where(i => i.Mobile == Mobnumber && i.billid == item.cid).ToList();
                foreach (var itemc in creditnotehis)
                {
                    cusstatement.creditpaymenthistries.Add(itemc);

                }
                cusstatement.creditnotes.Add(item);

            }

            return View(cusstatement);
        }

        public IActionResult Todaysalesreport()
        {
            DateTime todaydate = DateTime.UtcNow;
          
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
          
            string Branch = ViewBag.branch;

            var todaysale = _context.salesinvoicesummery.Where(i => i.Branch == Branch && i.Billdate.Date == todaydate.Date && i.status != "Return").ToList();
            var sumofamount = _context.salesinvoicesummery.Where( i => i.status != "Return" && i.Branch == Branch && i.Billdate.Date == todaydate.Date).Sum(i => i.Totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;

            return View(todaysale);
        }
        public IActionResult Weeksalesreport()
        {
            DateTime todaydate = DateTime.UtcNow;
           
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
         
            string Branch = ViewBag.branch;
            DateTime week = DateTime.UtcNow.AddDays(-7);
            var todaysale = _context.salesinvoicesummery.Where(i => i.Branch == Branch && i.Billdate.Date >= week.Date && i.Billdate.Date <= todaydate.Date && i.status != "Return").ToList();
            var sumofamount = _context.salesinvoicesummery.Where(i => i.status != "Return" && i.Branch == Branch && i.Billdate.Date >= week.Date && i.Billdate.Date <=todaydate.Date).Sum(i => i.Totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;

            return View(todaysale);
        }
        public IActionResult Monthsalesreport()
        {
            DateTime todaydate = DateTime.UtcNow;
          
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
         
            string Branch = ViewBag.branch;
            DateTime week = DateTime.UtcNow.AddDays(-30);
            var todaysale = _context.salesinvoicesummery.Where(i => i.Branch == Branch && i.Billdate.Date >= week.Date && i.Billdate.Date <= todaydate.Date && i.status != "Return").ToList();
            var sumofamount = _context.salesinvoicesummery.Where(i => i.status != "Return" && i.Branch == Branch && i.Billdate.Date >= week.Date && i.Billdate.Date <= todaydate.Date).Sum(i => i.Totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;

            return View(todaysale);
        }
        public IActionResult Yearsalesreport()
        {
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-365);
          
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime week = DateTime.UtcNow.AddDays(-30);
            var todaysale = _context.salesinvoicesummery.Where(i => i.Branch == Branch && i.Billdate.Date >= week.Date && i.Billdate.Date <= todaydate.Date && i.status != "Return").ToList();
            var sumofamount = _context.salesinvoicesummery.Where(i => i.status != "Return" && i.Branch == Branch && i.Billdate.Date >= week.Date && i.Billdate.Date <= todaydate.Date).Sum(i => i.Totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;

            return View(todaysale);
        }
        public IActionResult cCreditustomerpayment(int id ,string mob)
        {


            ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            ViewBag.roll = HttpContext.Session.GetString("roll");
            var list = _context.customerdetails.Where(i => i.Mobilenumber == mob).FirstOrDefault();
            var bill = _context.creditnote.Where(i => i.cid == id).ToList();
            ViewBag.billsummery = bill;
            var histry = _context.creditpaymenthistries.Where(i => i.billid == id).ToList();
            ViewBag.histry = histry;
            ViewBag.lastbalance = _context.creditpaymenthistries.OrderByDescending(i => i.id).Where(i => i.billid == id).FirstOrDefault();

            return View(list);
        }

        public IActionResult creditUpdatepayments(Creditpaymenthistry cph)
        {
            
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            var Name = ViewBag.data;
            var Branch = ViewBag.branch;
            if (cph.Paiddate == null)
            {
                cph.Paiddate = DateTime.UtcNow;

            }
            cph.Recivedby = Name;         
            
            cph.Branch = Branch;


            var billamount = _context.creditnote.Where(i => i.cid == cph.billid).FirstOrDefault();
            decimal paid = cph.Payment;
            decimal cal = cph.Balance;          
            cph.total = cal;
            var billhis = _context.creditpaymenthistries.Add(cph);
            billamount.totalamount = cal;
            _context.creditnote.Update(billamount);

            _context.SaveChanges();



            return Json(new { success = true, message = "Save successfull." });
        }

        public IActionResult customsalesreport(DateTime fromdate ,DateTime todate)
        {
          
          
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
           
            string Branch = ViewBag.branch;

            var cussale = _context.salesinvoicesummery.Where(i => i.Branch == Branch && i.Billdate.Date >= fromdate.Date && i.Billdate.Date <= todate.Date  && i.status != "Return").ToList();
            var allsale = _context.salesinvoicesummery.Where(i => i.status != "Return").ToList();
            var sumofamount = _context.salesinvoicesummery.Where(i => i.status != "Return" && i.Branch == Branch && i.Billdate.Date >= fromdate.Date && i.Billdate.Date <= todate.Date).Sum(i => i.Totalamount).ToString();
            var allamount = _context.salesinvoicesummery.Where(i => i.status != "Return" && i.Branch == Branch ).Sum(i => i.Totalamount).ToString();
            if (sumofamount != "0")
            {
                ViewBag.sumofcustomerbuyamount = sumofamount;
            }
            else
            {
                ViewBag.sumofcustomerbuyamount = allamount;
            }
           
            if (cussale.Count != 0)
            {
                return View(cussale);
            }
            else
            {
                return View(allsale);
            }
           
        }

        public IActionResult custompurchasereport(DateTime fromdate, DateTime todate)
        {
          
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
           
            string Branch = ViewBag.branch;

            var cussale = _context.purchaseinvoicesummeries.Where(i => i.Branch == Branch && i.Billdate.Date >= fromdate.Date && i.Billdate.Date <= todate.Date && i.status != "Return").ToList();
            var allsale = _context.purchaseinvoicesummeries.Where(i => i.status != "Return").ToList();
            var sumofamount = _context.purchaseinvoicesummeries.Where(i => i.status != "Return" && i.Branch == Branch && i.Billdate.Date >= fromdate.Date && i.Billdate.Date <= todate.Date).Sum(i => i.Totalamount).ToString();
            var allamount = _context.purchaseinvoicesummeries.Where(i => i.status != "Return" && i.Branch == Branch).Sum(i => i.Totalamount).ToString();
            if (sumofamount != "0")
            {
                ViewBag.sumofcustomerbuyamount = sumofamount;
            }
            else
            {
                ViewBag.sumofcustomerbuyamount = allamount;
            }

            if (cussale.Count != 0)
            {
                return View(cussale);
            }
            else
            {
                return View(allsale);
            }

        }

        public IActionResult Expense(DateTime fromdate, DateTime todate)
        {
          
        
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
          
            string Branch = ViewBag.branch;

            var cussale = _context.expens.Where(i => i.branch == Branch && i.entrydate.Date >= fromdate.Date && i.entrydate.Date <= todate.Date).ToList();
            var allsale = _context.expens.ToList();
            var sumofamount = _context.expens.Where(i =>  i.branch == Branch && i.entrydate.Date >= fromdate.Date && i.entrydate.Date <= todate.Date).Sum(i => i.amount).ToString();
            var allamount = _context.expens.Where(i =>  i.branch == Branch).Sum(i => i.amount).ToString();
            if (sumofamount != "0")
            {
                ViewBag.sumofcustomerbuyamount = sumofamount;
            }
            else
            {
                ViewBag.sumofcustomerbuyamount = allamount;
            }

            if (cussale.Count != 0)
            {
                return View(cussale);
            }
            else
            {
                return View(allsale);
            }

        }

        public IActionResult productstock()
        {
            
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
          
            string Branch = ViewBag.branch;

           
            ViewBag.allsale = _context.product.ToList();
          
            var allamount = _context.product.Where(i => i.Branch == Branch).Sum(i => i.Salesrate).ToString();
            var allstock = _context.product.Where(i => i.Branch == Branch).Sum(i => i.stock).ToString();

            ViewBag.sumofamount = allamount;
            ViewBag.sumofstock = allstock;


            return View();
            

        }

        public IActionResult customcreditreport(DateTime fromdate, DateTime todate)
        {


            ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);

            string Branch = ViewBag.branch;
          
            ViewBag.customer = _context.creditnote.Where(i => i.person == "customer").ToList();
            var cussale = _context.creditpaymenthistries.Where(i => i.Branch == Branch && i.Paiddate.Date >= fromdate.Date && i.Paiddate.Date <= todate.Date ).ToList();
            var allsale = _context.creditpaymenthistries.Where(i => i.Branch == Branch).ToList();
          

           
            var sumofamount = _context.creditpaymenthistries.Where(i => i.Branch == Branch && i.Paiddate.Date >= fromdate.Date && i.Paiddate.Date <= todate.Date).Sum(i => i.total).ToString();
            var allamount = _context.creditpaymenthistries.Where(i =>  i.Branch == Branch).Sum(i => i.total).ToString();

            var q = (from pd in _context.creditpaymenthistries
                     join od in _context.creditnote on pd.billid equals od.cid                   
                     orderby od.cid
                     select new
                     {
                         od.totalamount,
                        
                     }).Sum(i => i.totalamount).ToString();
            if (sumofamount != "0")
            {
                ViewBag.sumofcustomerbuyamount = sumofamount;
            }
            else
            {
                ViewBag.sumofcustomerbuyamount = allamount;
            }

            if (cussale.Count != 0)
            {
                return View(cussale);
            }
            else
            {
                return View(allsale);
            }

        }

       



    }
}
