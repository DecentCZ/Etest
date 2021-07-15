using Cw.PayslipData;
using Cw.PayslipLogic.Interfaces;
using Cw.PayslipCommon;
using Cw.PayslipContract;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cw.PayslipLogic
{
    public class PayslipConfigLogic : IPayslipConfigLogic
    {
        public PayslipConfigLogic()
        {
            _TaxRates = new List<TaxRates>();
        }
        private List<TaxRates>  _TaxRates;
        public List<TaxRates>  TaxRates
        {
            get { return _TaxRates; }
            set { _TaxRates = value; }
        }

    }
}
