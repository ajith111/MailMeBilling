using Org.BouncyCastle.Utilities.IO.Pem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class DshbordVM
    {
        public List<CustomerDetails> customerDetails { get; set; } = new List<CustomerDetails>();
        public List<Vendor> vendors { get; set; } = new List<Vendor>();
        public List<Purchaseinvoicesummery> purchaseinvoicesummery { get; set; } = new List<Purchaseinvoicesummery>();
        public List<Salesinvoicesummery> salesinvoicesummeries { get; set; } = new List<Salesinvoicesummery>();

        public List<Expens> Expens { get; set; } = new List<Expens>();

    }
}
