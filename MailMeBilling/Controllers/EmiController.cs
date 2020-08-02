using System;
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
    public class EmiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: creditnotes
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.creditnote.ToListAsync());
        }
        public async Task<IActionResult> CustomerIndex()
        {
            
            return View(await _context.eminotes.Where(i => i.person == "customer").ToListAsync());
        }

        public async Task<IActionResult> VendorIndex()
        {
          
            return View(await _context.eminotes.Where(i => i.person == "vendor").ToListAsync());
        }

        // GET: creditnotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var creditnote = await _context.eminotes
                .FirstOrDefaultAsync(m => m.cid == id);
            if (creditnote == null)
            {
                return NotFound();
            }

            return View(creditnote);
        }

        // GET: creditnotes/Create
        public IActionResult AddEmi()
        {
           
            return View();
        }

        // POST: creditnotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cid,cdate,balance,paid,person,particular,totalamount,name,mobilenumber,address,paymenttype,refno")] Eminote creditnote)
        {
           
         
            if (ModelState.IsValid)
            {
                 ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
                var Name = ViewBag.data;              
                  ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
                string Branch = ViewBag.branch;
                creditnote.addby = Name;
                creditnote.branch = Branch;
                if (creditnote.cdate == null)
                {
                    creditnote.cdate = DateTime.UtcNow;
                }
              
                _context.eminotes.Add(creditnote);
                await _context.SaveChangesAsync();
                var cno = creditnote.cid;
                if (creditnote.person =="customer")
                {
                    var checkcustomer = _context.creditcustomers.Where(i => i.Mobilenumber == creditnote.mobilenumber).FirstOrDefault();
                    if (checkcustomer == null)
                    {
                        CreditCustomerDetails cd = new CreditCustomerDetails();
                        cd.Mobilenumber = creditnote.mobilenumber;
                        cd.Customername = creditnote.name;
                        cd.Address = creditnote.address;
                        cd.Branch = Branch;
                        cd.Entrydate = DateTime.UtcNow;
                        cd.Entryby = Name;
                        _context.creditcustomers.Add(cd);
                        _context.SaveChanges();

                    }
                        Emipaymenthistry cph1 = new Emipaymenthistry();
                        cph1.Mobile = creditnote.mobilenumber;
                        cph1.Customername = creditnote.name;
                        cph1.Address = creditnote.address;
                        cph1.paymenttype = creditnote.paymenttype;
                        cph1.Payment = creditnote.paid;
                        cph1.Recivedby = Name;
                        if (creditnote.cdate == null)
                        {
                            cph1.Paiddate = DateTime.UtcNow;
                        }
                        cph1.Paiddate = creditnote.cdate;
                        cph1.Balance = creditnote.balance;
                       
                        cph1.refno = creditnote.refno;
                        cph1.Branch = Branch;
                        cph1.total = creditnote.totalamount;
                      
                        cph1.billid = cno;
                        _context.emipaymenthistries.Add(cph1);

                     
                        _context.SaveChanges();
                    

                }

                if (creditnote.person == "vendor")
                {
                    var checkvendor = _context.creditvendors.Where(i => i.Mobilenumber == creditnote.mobilenumber).FirstOrDefault();
                    if (checkvendor == null)
                    {
                        CreditVendor cd = new CreditVendor();
                        cd.Mobilenumber = creditnote.mobilenumber;
                        cd.Name = creditnote.name;
                        cd.Address = creditnote.address;
                        cd.Branch = Branch;
                        cd.Entrydate = DateTime.UtcNow;
                        cd.Entryby = Name;
                        _context.creditvendors.Add(cd);
                        _context.SaveChanges();
                    }
                    Emipaymenthistry cph1 = new Emipaymenthistry();
                    cph1.Mobile = creditnote.mobilenumber;
                    cph1.Customername = creditnote.name;
                    cph1.Address = creditnote.address;
                    cph1.paymenttype = creditnote.paymenttype;
                    cph1.Payment = creditnote.paid;
                    cph1.Recivedby = Name;
                    if (creditnote.cdate == null)
                    {
                        cph1.Paiddate = DateTime.UtcNow;
                    }
                    cph1.Paiddate = creditnote.cdate;
                    cph1.Balance = creditnote.balance;

                    cph1.refno = creditnote.refno;
                    cph1.Branch = Branch;
                    cph1.total = creditnote.totalamount;

                    cph1.billid = cno;
                    _context.emipaymenthistries.Add(cph1);

                    _context.SaveChanges();
                }

                if (creditnote.person == "vendor")
                {
                    return RedirectToAction(nameof(VendorIndex));
                }
                else
                {
                    return RedirectToAction(nameof(CustomerIndex));
                }
            }
            return View();
        }

        // GET: creditnotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           
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
          
            if (id == null)
            {
                return NotFound();
            }

            var creditnote = await _context.eminotes
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
            var creditnote = await _context.eminotes.FindAsync(id);
            _context.eminotes.Remove(creditnote);
            var role = creditnote.person;
            await _context.SaveChangesAsync();
            if (role == "vendor")
            {
                return RedirectToAction(nameof(VendorIndex));
            }
            else
            {
                return RedirectToAction(nameof(CustomerIndex));
            }
          
        }

        private bool creditnoteExists(int id)
        {
            return _context.creditnote.Any(e => e.cid == id);
        }
        [HttpGet]
        public JsonResult fillcusdetails(string mob)
        {
          
            var deatils = _context.customerdetails.Where(c => c.Mobilenumber == mob).SingleOrDefault();
            var vdetils = _context.vendor.Where(c => c.Mobilenumber == mob).SingleOrDefault();

            if (vdetils != null)
            {
                return new JsonResult(vdetils);
            }
            return new JsonResult(deatils);

        }
        public IActionResult Creditnotepayment(int id, string mob)
        {


            ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            ViewBag.roll = HttpContext.Session.GetObject(SD.Statusroll);
            var list = _context.customerdetails.Where(i => i.Mobilenumber == mob).FirstOrDefault();
            var bill = _context.eminotes.Where(i => i.cid == id).ToList();
            ViewBag.billsummery = bill;
            var histry = _context.emipaymenthistries.Where(i => i.billid == id).ToList();
            ViewBag.histry = histry;
            ViewBag.lastbalance = _context.eminotes.Where(i => i.cid == id).FirstOrDefault();

            return View(list);
        }

        public IActionResult UpdateCreditnotepayment(Emipaymenthistry cph)
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


            var billamount = _context.eminotes.Where(i => i.cid == cph.billid).FirstOrDefault();
            decimal paid = cph.Payment;
            decimal cal = cph.Balance;
            cph.total = billamount.totalamount;
            cph.Payment = cph.Payment;
            var billhis = _context.emipaymenthistries.Add(cph);
            billamount.balance = cal;
            billamount.paid = billamount.paid + cph.Payment;
            _context.eminotes.Update(billamount);

            _context.SaveChanges();



            return Json(new { success = true, message = "Save successfull." });
        }
        public IActionResult viewcustomerstatement(string Mobnumber)

        {     Comcredit cusstatement = new Comcredit();

            var customerdetil = _context.creditcustomers.Where(i => i.Mobilenumber == Mobnumber).FirstOrDefault();

            var sumofamount = _context.eminotes.Where(i => i.mobilenumber == Mobnumber ).Sum(i => i.totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;

            var sumofreturn = _context.eminotes.Where(i => i.mobilenumber == Mobnumber ).Sum(i => i.balance).ToString();
            ViewBag.sumofreturn = sumofreturn;

            var sumofreturntotal = _context.eminotes.Where(i => i.mobilenumber == Mobnumber ).Sum(i => i.paid).ToString();
            ViewBag.sumofreturntotal = sumofreturntotal;

            cusstatement.creditcustomerdetails.Add(customerdetil);
          
            var creditnote = _context.eminotes.Where(i => i.mobilenumber == Mobnumber).ToList();
            foreach (var item in creditnote)
            {
                var creditnotehis = _context.emipaymenthistries.Where(i => i.Mobile == Mobnumber && i.billid == item.cid).ToList();
                foreach (var itemc in creditnotehis)
                {
                    cusstatement.emipaymenthistries.Add(itemc);

                }

                cusstatement.eminotes.Add(item);
            }
            return View(cusstatement);
        }

        public IActionResult Creditcustomerstatement()
        {


            var sr = _context.creditcustomers.ToList().Distinct();
            return View(sr);
        }
        public IActionResult Creditvendorstatement()
        {

            var sr = _context.creditvendors.ToList().Distinct();
            return View(sr);
        }
        public IActionResult viewvendorstatement(string Mobnumber)
        {


            Comcredit cusstatement = new Comcredit();

            var customerdetil = _context.creditvendors.Where(i => i.Mobilenumber == Mobnumber).FirstOrDefault();

            var sumofamount = _context.eminotes.Where(i => i.mobilenumber == Mobnumber).Sum(i => i.totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;

            var sumofreturn = _context.eminotes.Where(i => i.mobilenumber == Mobnumber).Sum(i => i.balance).ToString();
            ViewBag.sumofreturn = sumofreturn;

            var sumofreturntotal = _context.eminotes.Where(i => i.mobilenumber == Mobnumber).Sum(i => i.paid).ToString();
            ViewBag.sumofreturntotal = sumofreturntotal;

            cusstatement.creditvendordetails.Add(customerdetil);

            var creditnote = _context.eminotes.Where(i => i.mobilenumber == Mobnumber).ToList();
            foreach (var item in creditnote)
            {
                var creditnotehis = _context.emipaymenthistries.Where(i => i.Mobile == Mobnumber && i.billid == item.cid).ToList();
                foreach (var itemc in creditnotehis)
                {
                    cusstatement.emipaymenthistries.Add(itemc);

                }

                cusstatement.eminotes.Add(item);
            }
            return View(cusstatement);
        }


        public IActionResult overallviewcus()
        {
            Comcredit cusstatement = new Comcredit();        

            var sumofamount = _context.creditnote.Where(i => i.person == "customer").Sum(i => i.totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;

            var sumofreturn = _context.creditnote.Where(i => i.person == "customer").Sum(i => i.Balance).ToString();
            ViewBag.sumofreturn = sumofreturn;

            var sumofreturntotal = _context.creditnote.Where(i => i.person == "customer").Sum(i => i.Paid).ToString();
            ViewBag.sumofreturntotal = sumofreturntotal;

            

            var creditnote = _context.creditnote.Where(i => i.person == "customer").ToList();
            foreach (var item in creditnote)
            {
               
                cusstatement.creditnotes.Add(item);
            }
            return View(cusstatement);
        }


        public IActionResult overallviewvr()
        {
            Comcredit cusstatement = new Comcredit();

            var sumofamount = _context.creditnote.Where(i => i.person == "vendor").Sum(i => i.totalamount).ToString();
            ViewBag.sumofcustomerbuyamount = sumofamount;

            var sumofreturn = _context.creditnote.Where(i => i.person == "vendor").Sum(i => i.Balance).ToString();
            ViewBag.sumofreturn = sumofreturn;

            var sumofreturntotal = _context.creditnote.Where(i => i.person == "vendor").Sum(i => i.Paid).ToString();
            ViewBag.sumofreturntotal = sumofreturntotal;



            var creditnote = _context.creditnote.Where(i => i.person == "vendor").ToList();
            foreach (var item in creditnote)
            {

                cusstatement.creditnotes.Add(item);
            }
            return View(cusstatement);
        }

    }
}
