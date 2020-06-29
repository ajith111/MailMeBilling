using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Customerpaymenthistry
    {
        [Key]
        public int id { get; set; }
        public int billid { get; set; }
        public string Customername { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string paymenttype { get; set; }
        public string refno { get; set; }
        public decimal Payment { get; set; }
        public decimal total { get; set; }
        public decimal Balance { get; set; }
        public DateTime Paiddate { get; set; }
        public string Recivedby { get; set; }
        public string Branch { get; set; }
    }
}
