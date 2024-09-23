using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.User
{
    public class RegisterRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? ImgUrl { get; set; }
    }
}
