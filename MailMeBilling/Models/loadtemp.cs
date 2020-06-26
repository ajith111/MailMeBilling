using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class loadtemp
    {
        [Key]
        public int Id { get; set; }

        public string Productname { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Branch { get; set; }
        public List<tempseccion> tempseccions { get; set; } = new List<tempseccion>();
        public List<tempseccion> ptempseccions { get; set; } = new List<tempseccion>();
        public List<Salesinvoicesummery> salesinvoicesummeries { get; set; } = new List<Salesinvoicesummery>();

        public List<Tmppurchase> tmppurchases { get; set; } = new List<Tmppurchase>();
        public List<Salesinvoice> salesinvoices { get; set; } = new List<Salesinvoice>();

        public List<Purchaseinvoicesummery> purchaseinvoicesummerys { get; set; } = new List<Purchaseinvoicesummery>();

    }
}
