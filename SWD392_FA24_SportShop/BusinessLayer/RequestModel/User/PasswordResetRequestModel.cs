using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.User
{
    public class PasswordResetRequestModel
    {
        public string Email { get; set; } = null!;
        public string VerificationCode { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
