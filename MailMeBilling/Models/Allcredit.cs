using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailMeBilling.Models
{
    public class Allcredit
    {
        public List<creditnote> creditnotes { get; set; } = new List<creditnote>();

        public List<Salesinvoicesummery> salesinvoicesummeries { get; set; } = new List<Salesinvoicesummery>();

        public List<Purchaseinvoicesummery> purchaseinvoicesummeries { get; set; } = new List<Purchaseinvoicesummery>();
    }
}
