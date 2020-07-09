using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Tmppurchasereturn
    {
        [Key]
        public int Id { get; set; }
        public string Productname { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Hsncode { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int Billno { get; set; }
        public DateTime Billdate { get; set; }
        public string Billby { get; set; }
        public string Branch { get; set; }
        public string Reasion { get; set; }
    }
}
