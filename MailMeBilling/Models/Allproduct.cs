using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Allproduct
    {
        public int productid { get; set; }

        [Display(Name = "BarCode")]
        public string Barcode { get; set; }
        [Required(ErrorMessage = "Product Number  is required")]
        [Display(Name = "Product Number")]
        public string productname { get; set; }

        [Display(Name = "HSN Code")]
        public string Hsncode { get; set; }
        [Required(ErrorMessage = " Category  is required")]
        [Display(Name = " Category")]
        public string Category { get; set; }

        [Display(Name = " Brand")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "SubcCategory  is required")]
        [Display(Name = "SubcCategory")]
        public string SubcCategory { get; set; }

        [Required(ErrorMessage = "Color  is required")]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Purchase Rate")]
        public decimal Purchaserate { get; set; }

        [Display(Name = "Sales Rate")]
        public decimal Salesrate { get; set; }
        [Display(Name = "Stock")]
        public long stock { get; set; }

        public DateTime Entrydate { get; set; }
        public string Entryby { get; set; }
        public List<Category> categories { get; set; } = new List<Category>();
        public List<SubCategory> subCategories { get; set; } = new List<SubCategory>();
        public List<Color> colors { get; set; } = new List<Color>();
        public List<Brand> brands { get; set; } = new List<Brand>();
    }
}
