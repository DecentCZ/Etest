using Cw.PayslipCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cw.PayslipLogic.Interfaces
{
    public interface IPayslipConfigLogic
    {
        List<TaxRates> TaxRates { get; set; }
    }
}
