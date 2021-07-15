using Cw.PayslipLogic.Interfaces;
using Cw.PayslipContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw.PayslipCommon;

namespace Cw.PayslipService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeLogic _employeeLogic;

        public EmployeeController(IEmployeeLogic employeeLogic)
        {
            this._employeeLogic = employeeLogic;
        }

        /// <summary>
        /// If unable to generate token please use eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4ifQ.BDsneVxa0sqtkwMEnVLVRKYLLIOElZsPn58vpzxot8A
        /// Can be generated using api/Payslip/GenerateToken
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [Authorize(Roles = PayslipConstants.Role)]
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee emp)
        {
            try
            {
                var empResponse = await _employeeLogic.AddEmployee(emp);
                return Ok(empResponse);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
