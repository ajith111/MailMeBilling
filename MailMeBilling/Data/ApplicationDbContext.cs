using System;
using System.Collections.Generic;
using System.Text;
using MailMeBilling.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MailMeBilling.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CustomerDetails> customerdetails { get; set; }
        public DbSet<RolleDetails> employeedetails { get; set; }
        public DbSet<MailMeBilling.Models.Login> login { get; set; }
        public DbSet<MailMeBilling.Models.Category> category { get; set; }
        public DbSet<MailMeBilling.Models.Product> product { get; set; }
        public DbSet<MailMeBilling.Models.SubCategory> subcategory { get; set; }
        public DbSet<MailMeBilling.Models.Color> color { get; set; }
        public DbSet<MailMeBilling.Models.Vendor> vendor { get; set; }
        public DbSet<MailMeBilling.Models.Brand> brand { get; set; }
        public DbSet<tempseccion> tempseccions { get; set; }
        public DbSet<Salesinvoicesummery> salesinvoicesummery { get; set; }
        public DbSet<Salesinvoice> salesinvoices { get; set; }
        public DbSet<MailMeBilling.Models.Branch> branch { get; set; }
        public DbSet<Purchaseinvoice> purchaseinvoices { get; set; }

        public DbSet<Tmppurchase> tmppurchases { get; set; }
        public DbSet<Purchaseinvoicesummery> purchaseinvoicesummeries { get; set; }


    }
}