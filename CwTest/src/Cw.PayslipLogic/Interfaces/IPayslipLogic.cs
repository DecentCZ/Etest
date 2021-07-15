using Cw.PayslipContract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cw.PayslipLogic.Interfaces
{
    public interface IPayslipLogic
    {
        Task<Payslip> GetPayslip(PayslipRequest payslip);

        Payslip ComputePayslip(Cw.PayslipData.Model.Employee employee);

        double ComputeTax(Cw.PayslipData.Model.Employee employee);

    }
}
