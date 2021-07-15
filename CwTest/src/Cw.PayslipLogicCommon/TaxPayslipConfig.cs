using System;
using System.Collections.Generic;
using System.Text;

namespace Cw.PayslipCommon
{
    public class TaxPayslipConfig
    {
        public List<TaxRates> TaxRates { get; set; }
    }

    public class TaxRates
    {
        public double Salary { get; set; }
        public double Tax { get; set; }
    }
}
