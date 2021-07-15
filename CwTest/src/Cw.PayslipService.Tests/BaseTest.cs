using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cw.PayslipData;
using Cw.PayslipLogic.Interfaces;
using Cw.PayslipCommon;
using Cw.PayslipContract;
using Microsoft.EntityFrameworkCore;
using Cw.PayslipLogic;
using System.Threading.Tasks;

namespace Cw.PayslipService.Tests
{
    public class BaseTest
    {
        protected readonly DbContextOptions<PayslipDBContext> _opt;
        protected readonly Employee _empParam1;
        protected readonly Employee _empParam1b;
        protected readonly Employee _empParam2;
        protected readonly PayslipConfigLogic _taxPayslipConfig;
        protected readonly Cw.PayslipData.Model.Employee _empData1;
        protected readonly Cw.PayslipData.Model.Employee _empData2;
        protected readonly Cw.PayslipData.Model.Employee _empData0Salary;
        protected readonly PayslipRequest _payslipRequest1;
        protected readonly PayslipRequest _payslipRequest4;

        public BaseTest()
        {
            _payslipRequest1 = new PayslipRequest { EmployeeId = 1};
            _payslipRequest4 = new PayslipRequest { EmployeeId = 4};
            _taxPayslipConfig = new PayslipConfigLogic();
            _taxPayslipConfig.TaxRates.Add(new TaxRates { Salary = 150000, Tax = 40 });
            _taxPayslipConfig.TaxRates.Add(new TaxRates { Salary = 0, Tax = 30 });
            var bld = new DbContextOptionsBuilder<PayslipDBContext>();
            bld.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _opt = bld.Options;
            _empParam1 = new Employee { EmployeeId = 1, FirstName = "Test", LastName = "TestL", Salary = 150000 };
            _empParam1b = new Employee { EmployeeId = 1, FirstName = "Testb", LastName = "TestLb", Salary = 1000 };
            _empParam2 = new Employee { EmployeeId = 2, FirstName = "Test2", LastName = "TestL2", Salary = 150001 };
            
            _empData1 = new Cw.PayslipData.Model.Employee { EmployeeId = 1, FirstName = "Test", LastName = "TestL", Salary = 150000 };
            _empData2 = new Cw.PayslipData.Model.Employee { EmployeeId = 2, FirstName = "Test2", LastName = "TestL2", Salary = 150001 };
            _empData0Salary = new Cw.PayslipData.Model.Employee { EmployeeId = 3, FirstName = "Test3", LastName = "TestL3", Salary = 0 };

        }

        public async Task<bool> SetupEmployee(PayslipDBContext context)
        {
            context.Employee.Add(_empData1);
            context.Employee.Add(_empData2);
            context.Employee.Add(_empData0Salary);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
