using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId  is required")]
        [Display(Name = "User Id")]
        public string Userid { get; set; }
        [Required(ErrorMessage = "Password  is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
