using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw.PayslipData.Model
{
    public class Payslip
    {
        [Key]
        public int PayslipId { get; set; }
        public int EmployeeId { get; set; }
        public double SalaryNet { get; set; }
        public double SalaryGross { get; set; }
        public double Tax { get; set; }
        public double Super { get; set; }
        public DateTime PaySlipDate { get; set; }
    }
}
