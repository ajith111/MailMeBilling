using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Color
    {
        [Key]
        public int Colorid { get; set; }
        public string Colors { get; set; }
        public string Colorsdscription { get; set; }
        public string Branch { get; set; }
    }
}
