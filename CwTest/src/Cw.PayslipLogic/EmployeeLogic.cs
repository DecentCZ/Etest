using Cw.PayslipData;
using Cw.PayslipLogic.Interfaces;
using Cw.PayslipCommon;
using Cw.PayslipContract;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cw.PayslipLogic
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly PayslipDBContext _context;

        public EmployeeLogic(PayslipDBContext context)
        {
            this._context = context;
        }
        public async Task<BaseResponse> AddEmployee(Employee employee)
        {
            var obj = new BaseResponse();
            try
            {
                var employeeExist = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
                if (employeeExist != null)
                {
                    obj.Message = PayslipConstants.ErrorEmployeeIdExisting;
                    return obj;
                }
                var empData = new Cw.PayslipData.Model.Employee
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Salary = employee.Salary
                };
                await _context.AddAsync(empData);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return obj;
        }
    }
}
