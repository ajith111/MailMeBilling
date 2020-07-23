using Org.BouncyCastle.Utilities.IO.Pem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Expens
    {
        [Key]
        public int eid { get; set; }
        [Display(Name ="Reason")]
        public string reason { get; set; }
        [Display(Name ="Amount")]
        public decimal amount { get; set; }
        [Display(Name = "Vehicle No")]
        public string regno { get; set; }
        [Display(Name = "Driver Name")]
        public string drivername { get; set; }
        [Display(Name = "Mobile")]
        public string drmobile { get; set; }
        [Display(Name = "Tpye of Vehicle")]
        public string vtype { get; set; }
        [Display(Name = "Total Kilometer")]
        public string totalkg { get; set; }
        [Display(Name = "Driver IC")]
        public string dic { get; set; }
        [Display(Name = "Fuel Station")]
        public string fulestation { get; set; }
        [Display(Name = "Fuel Type")]
        public string ftype { get; set; }
        [Display(Name = "Employee Name")]
        public string employeename { get; set; }
        [Display(Name = "Reason of Give")]
        public string resonemp { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        [Display(Name = "Mobile Number")]
        public string empmobile { get; set; }
        [Display(Name = "Id proof")]
        public byte[] idpro { get; set; }

        public string branch { get; set; }
        [Display(Name ="Entry Date")]
        public DateTime entrydate { get; set; }
        [Display(Name = "Entry By")]
        public string entryby { get; set; }
    }
}
