using BusinessLayer.ResponseModels;
using DataLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.User;
using DataLayer.Repository;
using AutoMapper;

namespace BusinessLayer.Service.Interface
{
    public class UserService : IUserService
    {
        private readonly IUserRepositoty _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUserRepositoty userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }


        public string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }
        public string GeneratePassword()
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            var bytes = new byte[8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            var password = new string(bytes.Select(b => characters[b % characters.Length]).ToArray());
            return password;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            byte[] hash = new byte[20];
            Array.Copy(hashBytes, 16, hash, 0, 20);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] computedHash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hash[i] != computedHash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public string GenerateJwtToken(string username, string roleName, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, roleName),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<BaseResponse> SendMailAccount(string email, string password)
        {
            try
            {

                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("luuhiep16092002@gmail.com", "ljdx zvbn zljh xopr");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("SportShop@gmail.com");
                mailMessage.To.Add(email);
                mailMessage.Subject = "VERIFY YOUR ACCOUNT";

                mailMessage.Body = @"
<html>
<head>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.6;
    }
    .container {
      padding: 20px;
      background-color: #f4f4f4;
      border: 1px solid #ddd;
      border-radius: 5px;
      max-width: 600px;
      margin: 0 auto;
    }
    .header {
      font-size: 20px;
      font-weight: bold;
      text-align: center;
      margin-bottom: 20px;
    }
    .content {
      font-size: 16px;
      color: #333;
    }
    .footer {
      font-size: 12px;
      color: #888;
      text-align: center;
      margin-top: 20px;
    }
    .highlight {
      color: #007BFF;
      font-weight: bold;
    }
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>Welcome to our Exchange Web!</div>
    <div class='content'>
      <p>Please click on the link to verify your account.</p>
<a href=""https://t-shirt-football.vercel.app/" + email + @""">click here</a>
      <p>This is the login account and password if you need to login with userId and password.</p>
      <p>Email: <span class='highlight'>" + email + @"</span></p>
      <p>Password: <span class='highlight'>" + password + @"</span></p>
    </div>
    <div class='footer'>
      &copy; 2024 Sport Shop. All rights reserved.
    </div>
  </div>
</body>
</html>";

                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);

                return new BaseResponse
                {
                    Code = 200,
                    Message = "Send succeed."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = 400,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }

        public async Task<BaseResponse<LoginResponseModel>> RegisterUser(LoginRequestModel model)
        {
            try
            {
                User checkExit = await _userRepository.GetUserByEmail(model.Email);
                if (checkExit != null) 
                {
                    return new BaseResponse<LoginResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "User has been exits!"
                    };
                }
                string password = GeneratePassword();
                string haskPassword = HashPassword(password);
                var User = _mapper.Map<User>(model);
                User.Status = false;
                User.CreatedDate = DateTime.Now;
                User.UserName = model.Email;
                User.Password = haskPassword;
                User.RoleName = "User";
                bool check = await _userRepository.CreateUser(User);
                if (!check) 
                {
                    return new BaseResponse<LoginResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                await SendMailAccount(model.Email , password);

                var response = _mapper.Map<LoginResponseModel>(User);
                return new BaseResponse<LoginResponseModel>(){
                    Code = 201,
                    Success = true,
                    Message = "Register success. Please go to mail and verify account!",
                    Data = response
                };              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse> VerifyAcccount(string email)
        {
            try
            {
                User user = await _userRepository.GetUserByEmail(email);
                user.Status = true;
                user.ModifiedDate = DateTime.Now;
                if (user == null)
                {
                    return new BaseResponse()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not Found User!"
                    };
                }
                bool check = await _userRepository.UpdateUser(user);
                if (check) 
                {
                    return new BaseResponse()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Verify user success!"
                    };
                }
                else
                {
                    return new BaseResponse()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                                    
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
