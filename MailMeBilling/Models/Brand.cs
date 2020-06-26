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

        public string Brandname { get; set; }
        public string Branddescription { get; set; }
        public string Branch { get; set; }
    }
}
