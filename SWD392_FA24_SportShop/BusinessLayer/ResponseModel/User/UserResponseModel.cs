using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.User
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public string? Gender { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RatingCount { get; set; }
        public bool IsDelete { get; set; }
        public bool IsVerify { get; set; }
        public bool Status { get; set; }
    }
}
