using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Vendor
    {
        [Key]
        public int vendorId { get; set; }
        [Required(ErrorMessage = "Vendor Name  is required")]
        [Display(Name = "Vendor Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mobile Number  is required")]
        [Display(Name = "Mobile Number")]
        public string Mobilenumber { get; set; }
        [Required(ErrorMessage = "Address  is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }


      
        [Display(Name = "Bank Name")]
        public string Bankname { get; set; }
      
        [Display(Name = "Account Number")]
        public string Accountnumber { get; set; }
       
        [Display(Name = "IFSC Code")]
        public string Ifsccode { get; set; }
       
        [Display(Name = "Branch ")]
        public string bankbranch { get; set; }
        public string Branch { get; set; }
        public DateTime Entrydate { get; set; }
        public string Entryby { get; set; }
    }
}
