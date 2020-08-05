using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Salesinvoicesummery
    {
        [Key]
        public int id { get; set; }
        public int Billid { get; set; }
         public int Totalqty { get; set; }
        public decimal Totalamount { get; set; }
        public int Gst { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public string Paymenttype { get; set; }
        public string Refcode { get; set; }
        public decimal Paid { get; set; } //pending
        public decimal discount { get; set; }
        public decimal Balance { get; set; }
        public decimal nettotal { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Billdate { get; set; }
        public string Billby { get; set; }
        public string status { get; set; }
        public string Customername { get; set; }
        public string Mobilenumber { get; set; }
        public string Address { get; set; }
        public string ntow { get; set; }
        public string Branch { get; set; }


    }
}
