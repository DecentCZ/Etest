using Cw.PayslipLogic.Interfaces;
using Cw.PayslipContract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cw.PayslipCommon;
using Microsoft.AspNetCore.Authorization;

namespace Cw.PayslipService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PayslipController : Controller
    {
        private readonly IPayslipLogic _payslipLogic;

        public PayslipController(IPayslipLogic payslipLogic)
        {
            this._payslipLogic = payslipLogic;
        }


        /// <summary>
        /// Must Provide employee number of previously added employee id. Use AddEmployee first before running this
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost("CreatePayslip")]
        public async Task<IActionResult> CreatePayslip([FromBody] PayslipRequest emp)
        {
            var payslipResponse = await _payslipLogic.GetPayslip(emp);
            return Ok(payslipResponse);
        }

        /// <summary>
        /// Enable  method to generate JWT
        /// </summary>
        /// <returns></returns>
        //[HttpGet("test")]
        //public async Task<IActionResult> GenerateToken()
        //{

        //    var x = new[] { new Claim("role", PayslipConstants.Role) };
        //    var secretKey = PayslipConstants.Key;
        //    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        //    var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var jwt = new JwtSecurityToken(null, null, x, null, null, sign);
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        //    return Ok(encodedJwt);
        //}
    }
}
