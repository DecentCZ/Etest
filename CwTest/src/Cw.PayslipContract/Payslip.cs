using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw.PayslipContract
{
    public class Payslip: BaseResponse
    {
        public int EmployeeId { get; set; }
        public double SalaryNet { get; set; }
        public double SalaryGross { get; set; }
        public double Tax { get; set; }
        public DateTime PaySlipDate { get; set; }
    }
}
