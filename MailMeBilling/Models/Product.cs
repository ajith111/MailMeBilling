﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Product
    {
        [Key]
        public int productid { get; set; }
       
        [Display(Name = "BarCode")]
        public string Barcode { get; set; }
        [Required(ErrorMessage = "Product Name  is required")]
        [Display(Name = "Product Name")]
        public string productname { get; set; }
        [Required(ErrorMessage = " Category  is required")]
        [Display(Name = " Category")]
        public string Category { get; set; }
       
        public Byte[] productimage { get; set; }
        [Required(ErrorMessage = "SubCategory  is required")]
        [Display(Name = "SubCategory")]
        public string SubcCategory { get; set; }

        [Required(ErrorMessage = "Color  is required")]
        [Display(Name = "Color")]
        public string Color { get; set; }
        [Display(Name = "Brand")]
        public string Brand { get; set; }
        [Display(Name = "HSN Code")]
        public string Hsncode { get; set; }

        [Display(Name = "Purchase Rate")]
        public decimal Purchaserate { get; set; }
        
        [Display(Name = "Sales Rate")]
        public decimal Salesrate { get; set; }
        [Display(Name = "Stock")]
        public long stock { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Entrydate { get; set; }
        public string Entryby { get; set; }
        public string Branch { get; set; }
    }
}
