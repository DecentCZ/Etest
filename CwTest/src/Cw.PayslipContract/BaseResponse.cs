using Cw.PayslipCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw.PayslipContract
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Message = PayslipConstants.SuccessMessage;
        }


        public string Message { get; set; }
    }
}
