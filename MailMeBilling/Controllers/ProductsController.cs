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
using Xunit;
using System.IO;
using Newtonsoft.Json;
using TimeZoneConverter;

namespace MailMeBilling.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
          
            return View();
        }
        //public async Task<IActionResult> Index()
        //{
       
        //    var list = await _context.product.ToListAsync();
        //    return View(list);
        //}

        // GET: Products
        #region API CALLS
        [HttpGet]
        public IActionResult getall()
        {
            ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
          
            string Branch = ViewBag.branch;
            return Json(new { data = _context.product.Select( i => new {i.productid,  i.productname ,i.Category ,i.SubcCategory,i.Salesrate, i.Hsncode,i.stock,i.Color,i.Brand, i.Branch }).Where(i => i.Branch == Branch).ToList() });
        }

        #endregion
        [HttpGet]
        public JsonResult getpic(int id)
        {
            var deatils = _context.product.Where(c => c.productid == id).SingleOrDefault();
            return new JsonResult(deatils);

           // return Json(new { data = _context.product.Where(i => i.productid == id).FirstOrDefault() });
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.product
                .FirstOrDefaultAsync(m => m.productid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
          
            Allproduct all = new Allproduct();
            var cat = _context.category.ToList();
            foreach (var item in cat)
            {
                all.categories.Add(item);
            }
            var subcat = _context.subcategory.ToList();
            foreach (var item in subcat)
            {
                all.subCategories.Add(item);
            }
            var color = _context.color.ToList();
            foreach (var item in color)
            {
                all.colors.Add(item);
            }

            var brands = _context.brand.ToList();
            foreach (var item in brands)
            {
                all.brands.Add(item);
            }
            return View(all);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("productid,productname,Category,SubcCategory,Color,Brand,Hsncode,Purchaserate,Salesrate,stock")] Product product, List<IFormFile> productimg)
        {
          
            foreach (var item in productimg)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        product.productimage = stream.ToArray();
                    }
                }
            }
           
            if (ModelState.IsValid)
            {
                 ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
                var Name = ViewBag.data;
                product.Entrydate =  DateTime.UtcNow;
                product.Entryby = Name;
                product.Barcode = "Null";
                  ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
                var Branch = ViewBag.branch;
                product.Branch = Branch;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
             ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
              ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.product.FindAsync(id);
            Allproduct all = new Allproduct();
            var cat = _context.category.ToList();
            foreach (var item in cat)
            {
                all.categories.Add(item);
            }
            var subcat = _context.subcategory.ToList();
            foreach (var item in subcat)
            {
                all.subCategories.Add(item);
            }
            var color = _context.color.ToList();
            foreach (var item in color)
            {
                all.colors.Add(item);
            }


            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, Product product, List<IFormFile> productimg)
        {
            ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            string Branch = ViewBag.branch;
            var stock = await _context.product.Where(i => i.Branch == Branch && i.productid == id).FirstOrDefaultAsync();
            if (stock != null)
            {

            
            var addstock = stock.stock + product.stock;
            if (productimg.Count != 0)
            {


                foreach (var item in productimg)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            stock.productimage = stream.ToArray();
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {

                    ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
                    var Name = ViewBag.data;
                    stock.Entrydate = DateTime.UtcNow;
                    stock.Entryby = Name;
                    product.Barcode = "Null";

                    product.Branch = Branch;
                    stock.Purchaserate = product.Purchaserate;
                    stock.Salesrate = product.Salesrate;
                    stock.stock = addstock;
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.productid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // return RedirectToAction(nameof(Index));
                return View(stock);
            }
        }
            return View(stock);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.product
                .FirstOrDefaultAsync(m => m.productid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.product.FindAsync(id);
            _context.product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.product.Any(e => e.productid == id);
        }
        public JsonResult fillpname(string mob)
        {

            var deatils = _context.product.Where(c => c.productname == mob).SingleOrDefault();
            return new JsonResult(deatils);

        }



        public IActionResult Stocktracation()
        {

            ViewBag.data = HttpContext.Session.GetObject(SD.Sessionname);
            ViewBag.branch = HttpContext.Session.GetObject(SD.Statusbranch);
            ViewBag.roll = HttpContext.Session.GetObject(SD.Statusroll);


            string Branch = ViewBag.branch;
            loadtemp load = new loadtemp();

            ViewBag.Branch = _context.branch.Where(i => i.Branchname == Branch).FirstOrDefault();

            var billno = _context.salesinvoicesummery.OrderByDescending(i => i.Billid).Where(i => i.Branch == Branch).FirstOrDefault(); //bill No
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
                    var tmpsummery = _context.salesinvoicesummery.Where(i => i.Billid == bi && i.Branch == Branch).ToList();
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
