using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class OverallVM
    {
        public List<CustomerDetails> customerdetails { get; set; } = new List<CustomerDetails>();
        public List<Vendor> vendors { get; set; } = new List<Vendor>();
        public List<Salesinvoicesummery> salesinvoicesummeries { get; set; } = new List<Salesinvoicesummery>();
        public List<creditnote> creditnotes { get; set; } = new List<creditnote>();

        public List<Purchaseinvoicesummery> purchaseinvoicesummeries { get; set; } = new List<Purchaseinvoicesummery>();
    }
}
