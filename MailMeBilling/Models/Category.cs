using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Category
    {
        [Key]
        public int categoryid { get; set; }
        [Required(ErrorMessage = "Category  is required")]
        [Display(Name = "Category")]
        public string Categorys { get; set; }
        [Required(ErrorMessage = "Discription  is required")]
        [Display(Name = "Category Discription")]
        public string Categorydiscription { get; set; }
        public string Branch { get; set; }
    }
}
