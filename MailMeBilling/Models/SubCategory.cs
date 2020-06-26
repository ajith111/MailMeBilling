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
        public string Subcatagory { get; set; }
        public string Subcatagorydiscription { get; set; }
        public string Branch { get; set; }
    }
}
