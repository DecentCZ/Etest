using Cw.PayslipContract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cw.PayslipLogic.Interfaces
{
    public interface IEmployeeLogic
    {
        Task<BaseResponse> AddEmployee(Employee employee);

    }
}
