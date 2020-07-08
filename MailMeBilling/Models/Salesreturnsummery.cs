using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Salesreturnsummery
    {
        [Key]
        public int id { get; set; }
        public int Billid { get; set; }
        public int Totalqty { get; set; }
        public decimal Totalamount { get; set; }
        public int Gst { get; set; }       
        public string Paymenttype { get; set; }
        public string Refcode { get; set; }
        public decimal Paid { get; set; }
        public decimal Balance { get; set; }      
       
        public string Customername { get; set; }
        public string Mobilenumber { get; set; }
        public string Address { get; set; }
        public DateTime Billdate { get; set; }
        public string Billby { get; set; }
        public string status { get; set; }
        public string Branch { get; set; }

    }
}
