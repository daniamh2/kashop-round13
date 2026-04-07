using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.dal.DTO.Request
{
    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; }
        public string Email { get; set; }
        public string code { get; set; }
    }
}
