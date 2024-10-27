using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.User
{
    public class ChangePasswordRequestModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
