using Cw.PayslipData;
using Cw.PayslipLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cw.PayslipData;
using Cw.PayslipLogic.Interfaces;
using Cw.PayslipCommon;
using Cw.PayslipContract;
using FluentAssertions;

namespace Cw.PayslipService.Tests
{
    public class EmployeeLogicTest : BaseTest
    {

        [Fact]
        public async void AddNewEmployee()
        {
            using (var context = new PayslipDBContext(_opt))
            {
                var empLogic = new EmployeeLogic(context);
                await empLogic.AddEmployee(_empParam1);
                var employees = await context.Employee.AnyAsync();
                employees.Should().BeTrue();
            }
        }

        [Fact]
        public async void AddSameId_Fail()
        {
            using (var context = new PayslipDBContext(_opt))
            {
                var empLogic = new EmployeeLogic(context);
                await empLogic.AddEmployee(_empParam1);
                await empLogic.AddEmployee(_empParam1b);
                var employees = await context.Employee.CountAsync();
                employees.Should().Be(1);
            }
        }
    }
}
