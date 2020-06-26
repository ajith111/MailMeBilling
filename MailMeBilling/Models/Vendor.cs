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
        public long Mobilenumber { get; set; }
        [Required(ErrorMessage = "Bank Name  is required")]
        [Display(Name = "Bank Name")]
        public string Bankname { get; set; }
        [Required(ErrorMessage = "Account Number  is required")]
        [Display(Name = "Account Number")]
        public string Accountnumber { get; set; }
        [Required(ErrorMessage = "IFSC Code  is required")]
        [Display(Name = "IFSC Code")]
        public string Ifsccode { get; set; }
        [Required(ErrorMessage = "Branch  is required")]
        [Display(Name = "Branch Number")]
        public string bankbranch { get; set; }
        public string Branch { get; set; }
    }
}
