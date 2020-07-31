using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class CreditCustomerDetails
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Customer Name  is required")]
        [Display(Name = "Customer Name")]
        public String Customername { get; set; }
        [Required(ErrorMessage = "Customer Number  is required")]
        [Display(Name = "Mobile Number")]
        public string Mobilenumber { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Emailid { get; set; }

        [Required(ErrorMessage = "Address  is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        public long Points { get; set; }
        public int Shopid { get; set; }
        public DateTime Entrydate { get; set; }
        public string Entryby { get; set; }
        public string Branch { get; set; }
    }
}
