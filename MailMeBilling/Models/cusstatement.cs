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
    }
}
