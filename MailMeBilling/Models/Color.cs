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

        [Display(Name ="Color")]
        public string Colors { get; set; }
        [Display(Name ="Color Discription")]
        public string Colorsdscription { get; set; }
        public string Branch { get; set; }
    }
}
