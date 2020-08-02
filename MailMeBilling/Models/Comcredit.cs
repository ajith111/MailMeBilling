using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Comcredit
    {
        public List<CreditCustomerDetails> creditcustomerdetails { get; set; } = new List<CreditCustomerDetails>();
        public List<CreditVendor> creditvendordetails { get; set; } = new List<CreditVendor>();
        public List<Eminote> eminotes { get; set; } = new List<Eminote>();
        public List<Emipaymenthistry> emipaymenthistries { get; set; } = new List<Emipaymenthistry>();

        public List<creditnote> creditnotes { get; set; } = new List<creditnote>();

        public List<Creditpaymenthistry> creditpaymenthistries { get; set; } = new List<Creditpaymenthistry>();

    }
}
