using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class RolleDetails
    {
        [Key]
        public int Employeeid { get; set; }

        [Required(ErrorMessage = "Employee Name  is required")]
        [Display(Name ="Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employee Date of birth  is required")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Mobile Number  is required")]
        [Display(Name = "Mobile Number")]
        public string Mobilenumber { get; set; }
      
        [Required(ErrorMessage = "Address  is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "Address")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Gender  is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }



        [Required(ErrorMessage = "Role  is required")]
        [Display(Name = "Role")]
        public string Roll { get; set; }

        [Required(ErrorMessage = "Email  is required")]      
        [Display(Name = "UserId")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string Comfirmpassword { get; set; }

        public DateTime Entrydate { get; set; }
        public string Entryby { get; set; }
        [Required(ErrorMessage = "Branch  is required")]
        [Display(Name = "Branch")]
        public string Branch { get; set; }
    }
}
