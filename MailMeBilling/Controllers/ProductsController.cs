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

namespace MailMeBilling.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    ViewBag.data = HttpContext.Session.GetString("name");
        //    ViewBag.branch = HttpContext.Session.GetString("branch");
        //    ViewBag.roll = HttpContext.Session.GetString("roll");
        //    string Branch = ViewBag.branch;
        //    DateTime todaydate = DateTime.UtcNow;
        //    DateTime dateStart = DateTime.UtcNow.AddDays(-15);
        //    var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

        //    ViewBag.CustomerPending = pendingcustomer.Count();

        //    var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

        //     ViewBag.VendorPending = pendingvendor.Count();
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate = DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            return View(await _context.product.ToListAsync());
        }

        // GET: Products
        #region API CALLS
        [HttpGet]
        public IActionResult getall()
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

           ViewBag.VendorPending = pendingvendor.Count();
             //return Json(JsonConvert.SerializeObject(_context.product.ToList()));
            // return View( _context.product.ToList());
            return Json(new { data = _context.product.ToList() });
        }
        #endregion

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
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
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
            ViewBag.roll = HttpContext.Session.GetString("roll");
            string Branch = ViewBag.branch;
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
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
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();
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
            ViewBag.VendorPending = pendingvendor.Count();
            if (ModelState.IsValid)
            {
                ViewBag.data = HttpContext.Session.GetString("name");
                var Name = ViewBag.data;
                product.Entrydate =  DateTime.UtcNow;
                product.Entryby = Name;
                product.Barcode = "Null";
                ViewBag.branch = HttpContext.Session.GetString("branch");
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
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
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
       
        public async Task<IActionResult> Edit(int id,  Product product, List<IFormFile> productimg)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
          
            ViewBag.roll = HttpContext.Session.GetString("roll");
           
            DateTime todaydate =  DateTime.UtcNow;
            DateTime dateStart = DateTime.UtcNow.AddDays(-15);
            var pendingcustomer = _context.salesinvoicesummery.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.CustomerPending = pendingcustomer.Count();

            var pendingvendor = _context.purchaseinvoicesummeries.Where(p => p.status == "Pending" && p.Billdate >= dateStart && p.Billdate <= todaydate).ToList();

            ViewBag.VendorPending = pendingvendor.Count();
            var stock = await _context.product.FindAsync(id);
           
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
                   
                    ViewBag.data = HttpContext.Session.GetString("name");
                    var Name = ViewBag.data;
                    stock.Entrydate =  DateTime.UtcNow;
                    stock.Entryby = Name;
                    product.Barcode = "Null";
                    ViewBag.branch = HttpContext.Session.GetString("branch");
                    var Branch = ViewBag.branch;
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
            return View(stock);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.data = HttpContext.Session.GetString("name");
            ViewBag.branch = HttpContext.Session.GetString("branch");
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
    }
}
