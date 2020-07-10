using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Brand Name")]
        public string Brandname { get; set; }
        [Display(Name = "Brand Discription")]
        public string Branddescription { get; set; }
        public string Branch { get; set; }
    }
}
