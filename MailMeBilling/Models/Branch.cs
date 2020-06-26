﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string Branchname { get; set; }
        public string BranchAddress { get; set; }
    }
}
