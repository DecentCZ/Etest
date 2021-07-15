using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cw.PayslipContract
{
    public class PayslipRequest
    {
        [Required]
        public int EmployeeId { get; set; }
    }
}
