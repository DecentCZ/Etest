using Cw.PayslipData;
using Cw.PayslipCommon;
using Cw.PayslipContract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Cw.PayslipLogic.Interfaces;

namespace Cw.PayslipLogic
{
    public class PayslipLogic : IPayslipLogic
    {

        private readonly IPayslipConfigLogic _taxPayslipConfig;
        private readonly PayslipDBContext _context;

        public PayslipLogic(IPayslipConfigLogic payslipConfigLogic, PayslipDBContext context)
        {
            this._taxPayslipConfig = payslipConfigLogic;
            this._context = context;
        }
        public async Task<Payslip> GetPayslip(PayslipRequest payslipRequest)
        {
            var payslip = new Payslip();
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == payslipRequest.EmployeeId);
            if (employee == null)
            {
                payslip.Message = $"EmployeeId {payslipRequest.EmployeeId} {PayslipConstants.ErrorRecordNotFound}";
                return payslip;
            }
            payslip = ComputePayslip(employee);
            return payslip;
        }
        public Payslip ComputePayslip(Cw.PayslipData.Model.Employee employee)
        {
            var taxAmount = employee.Salary > 0 ? ComputeTax(employee) : 0;
            var payslip = new Payslip
            {
                EmployeeId = employee.EmployeeId,
                PaySlipDate = DateTime.UtcNow,
                SalaryGross = employee.Salary,
                SalaryNet = Math.Round(employee.Salary - taxAmount, 2),
                Tax = Math.Round(taxAmount, 2)
            };
            return payslip;
        }

        public double ComputeTax(Cw.PayslipData.Model.Employee employee)
        {
            var taxRate = _taxPayslipConfig.TaxRates.OrderByDescending(o => o.Salary).FirstOrDefault(s => s.Salary < employee.Salary);
            return employee.Salary * (taxRate.Tax * .01);
        }


    }
}
