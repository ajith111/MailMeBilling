using MailMeBilling.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class creditnote
    {
        [Key]
        public int cid { get; set; }
        [Required]
        [Display(Name ="Date")]
        [DataType(DataType.Date)]
        public DateTime cdate { get; set; }
        [Display(Name ="Person")]
        public string person { get; set; }
        [Display(Name = "Reason")]
        public string particular { get; set; }
        [Display(Name ="Payment type")]
        public string paymenttype { get; set; }
        [Display(Name = "Ref No")]
        public string refno { get; set; }
        [Display(Name = "Amount")]
        public decimal totalamount { get; set; }
        [Display(Name = "Paid")]
        public decimal Paid { get; set; }
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Mobile")]
        public string mobilenumber { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }

        public string addby { get; set; }
    public string  branch { get; set; }
}
}
