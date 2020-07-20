using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class cusstatement
    {
        public List<CustomerDetails> customerdetails { get; set; } = new List<CustomerDetails>();
        public List<Customerpaymenthistry> customerpaymenthistries { get; set; } = new List<Customerpaymenthistry>();

        public List<Salesinvoicesummery> salesinvoicesummeries { get; set; } = new List<Salesinvoicesummery>();

        public List<Vendor> vendors { get; set; } = new List<Vendor>();

        public List<Vendorpayment> vendorpayments { get; set; } = new List<Vendorpayment>();

        public List<Purchaseinvoicesummery> purchaseinvoicesummeries { get; set; } = new List<Purchaseinvoicesummery>();

        public List<creditnote> creditnotes { get; set; } = new List<creditnote>();
        public List<Creditpaymenthistry> creditpaymenthistries { get; set; } = new List<Creditpaymenthistry>();
    }
}
