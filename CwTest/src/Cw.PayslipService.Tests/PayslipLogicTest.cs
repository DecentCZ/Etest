using Cw.PayslipData;
using Cw.PayslipLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cw.PayslipData;
using Cw.PayslipLogic;
using Cw.PayslipCommon;
using Cw.PayslipContract;
using FluentAssertions;


namespace Cw.PayslipService.Tests
{
    public class PayslipLogicTest : BaseTest
    {
        [Fact]
        public async void ComputeTaxTest()
        {

            using (var context = new PayslipDBContext(_opt))
            {
                var payslipLogic = new Cw.PayslipLogic.PayslipLogic(_taxPayslipConfig, context);
                var tax1 = payslipLogic.ComputeTax(_empData1);
                var tax2 = payslipLogic.ComputeTax(_empData2);

                tax1.Should().Be(45000);
                tax2.Should().Be(60000.4);
            }
        }

        [Fact]
        public async void ComputePayslipTest()
        {
            using (var context = new PayslipDBContext(_opt))
            {
                var payslipLogic = new Cw.PayslipLogic.PayslipLogic(_taxPayslipConfig, context);
                var salary1 = payslipLogic.ComputePayslip(_empData1);
                var salary2 = payslipLogic.ComputePayslip(_empData2);
                var salary3 = payslipLogic.ComputePayslip(_empData0Salary);

                salary1.SalaryNet.Should().Be(105000);
                salary2.SalaryNet.Should().Be(90000.6);
                salary3.SalaryNet.Should().Be(0);
            }
        }

        [Fact]
        public async void GetPayslipTest()
        {
            using (var context = new PayslipDBContext(_opt))
            {
                var b = await SetupEmployee(context);
                var payslipLogic = new Cw.PayslipLogic.PayslipLogic(_taxPayslipConfig, context);
                var payslip1 = await payslipLogic.GetPayslip(_payslipRequest1);
                var payslip3 = await payslipLogic.GetPayslip(_payslipRequest4);

                payslip1.SalaryNet.Should().Be(105000);
                payslip3.SalaryNet.Should().Be(0);
                payslip3.Message.Should().NotBe(PayslipConstants.SuccessMessage);
            }
        }

    }
}
