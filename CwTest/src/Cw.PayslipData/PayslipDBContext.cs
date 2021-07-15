using Cw.PayslipData.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cw.PayslipData
{
    public class PayslipDBContext : DbContext
    {
        public PayslipDBContext(DbContextOptions<PayslipDBContext> options) : base(options)
        { 
        
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Payslip> Payslip { get; set; }
    }
}
