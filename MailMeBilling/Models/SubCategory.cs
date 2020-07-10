using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class SubCategory
    {
        [Key]
        public int Subcategoryid { get; set; }
        [Display(Name ="SubCategory")]
        public string Subcatagory { get; set; }
        [Display(Name = "SubCategory Discription")]
        public string Subcatagorydiscription { get; set; }
        public string Branch { get; set; }
    }
}
