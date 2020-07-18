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
        public string branch { get; set; }
        [Display(Name ="Entry Date")]
        public DateTime entrydate { get; set; }
        [Display(Name = "Entry By")]
        public string entryby { get; set; }
    }
}
