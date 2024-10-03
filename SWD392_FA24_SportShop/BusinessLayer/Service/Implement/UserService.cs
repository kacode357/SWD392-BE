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
using Newtonsoft.Json.Linq;
using Google.Apis.Auth;
using X.PagedList;
using System.Linq;

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
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }
        public string GeneratePassword()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }         
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }    
        }

        public string GenerateJwtToken(string username, string roleName, int userId)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }      
        }

        public async Task<BaseResponse> SendMailWithPassword(string email, string password)
        {
            try
            {
                User user = await _userRepository.GetUserByEmail(email);
                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("luuhiep16092002@gmail.com", "ljdx zvbn zljh xopr");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("luuhiep16092002@gmail.com");
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
    <div class='header'>Welcome to our Sport Shop Web!</div>
    <div class='content'>
      <p>Please click on the link to verify your account.</p>
<a href=""https://t-shirt-football.vercel.app/verifyemail/" + user.Id + @""">click here</a>
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

        public async Task<BaseResponse> SendMailWithoutPassword(string email)
        {
            try
            {
                User user = await _userRepository.GetUserByEmail(email);

                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("luuhiep16092002@gmail.com", "ljdx zvbn zljh xopr");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("luuhiep16092002@gmail.com");
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
<a href=""https://t-shirt-football.vercel.app/verifyemail/" + user.Id + @""">click here</a>
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

        public async Task<BaseResponse<UserResponseModel>> RegisterUserByEmail(string googleId)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleId);
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(payload.ExpirationTimeSeconds.Value).UtcDateTime;
                var currentTime = DateTime.UtcNow;
                if (currentTime > expirationTime)
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 401,
                        Success = false,
                        Message = "Google id expired!."
                    };
                }

                string email = payload.Email;
                User checkExit = await _userRepository.GetUserByEmail(email);

                if (checkExit != null) 
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "User has been exit!.",
                    };
                }
                string password = GeneratePassword();
                string hashPassword = HashPassword(password);
                User user = new User()
                {
                    UserName = payload.Name,
                    Email = email,
                    ImgUrl = payload.Picture,
                    Password = hashPassword,
                    CreatedDate = DateTime.UtcNow,
                    RoleName = "User",
                    IsDelete = false,
                    IsVerify = false,
                    Status = true,
                };
                bool check = await _userRepository.CreateUser(user);
                if (!check) 
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                await SendMailWithPassword(email , password);

                var response = _mapper.Map<UserResponseModel>(user);
                return new BaseResponse<UserResponseModel>(){
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

        public async Task<BaseResponse> VerifyAcccount(int id)
        {
            try
            {
                User user = await _userRepository.GetUserById(id);
                user.IsVerify = true;
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

        public async Task<BaseResponse<LoginResponseModel>> Login(LoginRequestModel model)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(model.Email);
                if (user != null)
                {
                    if (VerifyPassword(model.Password, user.Password))
                    {
                        if (user.IsVerify == false)
                        {
                            return new BaseResponse<LoginResponseModel>()
                            {
                                Code = 401,
                                Success = false,
                                Message = "Email not verified!.",
                                Data = null,
                            };
                        }

                        if (user.IsDelete == true)
                        {
                            return new BaseResponse<LoginResponseModel>()
                            {
                                Code = 401,
                                Success = false,
                                Message = "User has been delete!.",
                                Data = null,
                            };
                        }

                        string token = GenerateJwtToken(user.UserName, user.RoleName, user.Id);
                        return new BaseResponse<LoginResponseModel>()
                        {
                            Code = 200,
                            Success = true,
                            Message = "Login success!",
                            Data = new LoginResponseModel()
                            {
                                token = token,
                                user = _mapper.Map<UserResponseModel>(user)
                            },
                        };
                    }
                    else
                    {
                        return new BaseResponse<LoginResponseModel>()
                        {
                            Code = 404,
                            Success = false,
                            Message = "Incorrect User or password!"
                        };
                    }
                }
                else
                {
                    return new BaseResponse<LoginResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Incorrect User or password!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<LoginResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<LoginResponseModel>> LoginMail(string googleId)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleId);
                var email = payload.Email;
                var user = await _userRepository.GetUserByEmail(email);
                if (user != null)
                {
                    if (user.IsVerify == false)
                    {
                        return new BaseResponse<LoginResponseModel>()
                        {
                            Code = 401,
                            Success = false,
                            Message = "Email not verified!.",
                            Data = null,
                        };
                    }

                    if (user.IsDelete == true)
                    {
                        return new BaseResponse<LoginResponseModel>()
                        {
                            Code = 401,
                            Success = false,
                            Message = "User has been delete!.",
                            Data = null,
                        };
                    }

                    string token = GenerateJwtToken(user.UserName, user.RoleName, user.Id);
                    return new BaseResponse<LoginResponseModel>()
                    {
                         Code = 200,
                         Success = true,
                         Message = "Login success!",
                         Data = new LoginResponseModel()
                         {
                             token = token,
                             user = _mapper.Map<UserResponseModel>(user)
                         },
                    };
                }
                else
                {
                    return new BaseResponse<LoginResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = null,
                        Data = null,
                    };
                }       
            }
            catch (Exception ex)
            {
                return new BaseResponse<LoginResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!.",
                    Data = null,
                };
            }
            
        }

        public async Task<DynamicResponse<UserResponseModel>> GetListUser(GetAllUserRequestModel model)
        {
            try
            {
                var listUser = await _userRepository.GetAllUser();
                if(!string.IsNullOrEmpty(model.keyWord))
                {
                    List<User> listUserByName = listUser.Where(u => u.UserName.Contains(model.keyWord)).ToList();

                    List<User> listUserByEmail = listUser.Where(u => u.Email.Contains(model.keyWord)).ToList();

                    listUser = listUserByName
                               .Concat(listUserByEmail)
                               .GroupBy(u => u.Id)
                               .Select(g => g.First())
                               .ToList();
                }
                if (!string.IsNullOrEmpty(model.role))
                {
                    if(!model.role.Equals("ALL") && !model.role.Equals("all") && !model.role.Equals("All"))
                    {
                        listUser = listUser.Where(u => u.RoleName.Equals(model.role)).ToList();
                    }          
                }
                if (model.status != null)
                {
                    listUser = listUser.Where(u => u.Status == model.status).ToList();
                }
                if (model.is_Verify != null)
                {
                    listUser = listUser.Where(u => u.IsVerify == model.is_Verify).ToList();
                }
                if (model.is_Delete != null)
                {
                    listUser = listUser.Where(u => u.IsDelete == model.is_Delete).ToList();
                }
                    var result = _mapper.Map<List<UserResponseModel>>(listUser);

                // Nếu không có lỗi, thực hiện phân trang
                var pagedUsers = result// Giả sử result là danh sách người dùng
                    .OrderBy(u => u.Id) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<UserResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<UserResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pagedUsers.PageNumber,
                            Size = pagedUsers.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pagedUsers.PageCount,
                            TotalItem = pagedUsers.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = model.role,
                            status = model.status,
                            is_Verify = model.is_Verify,
                            is_Delete = model.is_Delete,
                        },
                        PageData = pagedUsers.ToList(),
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<UserResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = null,
                    Data = null,
                };
            }
        }

        public async Task<BaseResponse<UserResponseModel>> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user != null)
                {
                    var result = _mapper.Map<UserResponseModel>(user);
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = null,
                        Data = result
                    };
                }
                else
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found User!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<UserResponseModel>> UpdateUser(int id, UpdateRequestModel model)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user != null)
                {
                    var result = _mapper.Map(model,user);
                    result.ModifiedDate = DateTime.Now;
                    await _userRepository.UpdateUser(result);
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Update success!.",
                        Data = _mapper.Map<UserResponseModel>(result)
                    };
                }
                else
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found User!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<UserResponseModel>> DeleteUser(int id, bool status)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user != null)
                {
                    user.ModifiedDate = DateTime.Now;
                    user.IsDelete = status;
                    await _userRepository.UpdateUser(user);
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Delete success!.",
                        Data = _mapper.Map<UserResponseModel>(user)
                    };
                }
                else
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found User!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<UserResponseModel>> RegisterUser(RegisterRequestModel model)
        {
            try
            {
                User checkExit = await _userRepository.GetUserByEmail(model.Email);
                if (checkExit != null)
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "User has been exits!"
                    };
                }
                string hashPassword = HashPassword(model.Password);
                var User = _mapper.Map<User>(model);
                User.Status = true;
                User.IsDelete = false;
                User.IsVerify = false;
                User.CreatedDate = DateTime.Now;
                User.Password = hashPassword;
                User.RoleName = "User";
                bool check = await _userRepository.CreateUser(User);
                if (!check)
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                await SendMailWithoutPassword(model.Email);

                var response = _mapper.Map<UserResponseModel>(User);
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Register success. Please go to mail and verify account!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<UserResponseModel>> CreateAccountAdmin(string email, string password, string name)
        {
            try
            {
                User checkExit = await _userRepository.GetUserByEmail(email);
                if (checkExit != null)
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "User has been exits!"
                    };
                }
                string hashPassword = HashPassword(password);
                User user = new User()
                {
                    Email = email,
                    UserName = name,
                    Status = true,
                    CreatedDate = DateTime.Now,
                    Password = hashPassword,
                    RoleName = "Admin",
                    IsVerify = true,
                    IsDelete = false,
                };
                bool check = await _userRepository.CreateUser(user);
                if (!check)
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                var response = _mapper.Map<UserResponseModel>(user);
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Register admin success!.",
                    Data = response
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }
        public async Task<BaseResponse<UserResponseModel>> CreateAccountStaff(string email, string password, string name)
        {
            try
            {
                User checkExit = await _userRepository.GetUserByEmail(email);
                if (checkExit != null)
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "User has been exits!"
                    };
                }
                string hashPassword = HashPassword(password);
                User user = new User()
                {
                    Email = email,
                    UserName = name,
                    Status = true,
                    CreatedDate = DateTime.Now,
                    Password = hashPassword,
                    RoleName = "Staff",
                    IsVerify = true,
                    IsDelete = false,
                };
                bool check = await _userRepository.CreateUser(user);
                if (!check)
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                var response = _mapper.Map<UserResponseModel>(user);
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Register admin success!.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }
        public async Task<BaseResponse<UserResponseModel>> CreateAccountManager(string email, string password, string name)
        {
            try
            {
                User checkExit = await _userRepository.GetUserByEmail(email);
                if (checkExit != null)
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "User has been exits!"
                    };
                }
                string hashPassword = HashPassword(password);
                User user = new User()
                {
                    Email = email,
                    UserName = name,
                    Status = true,
                    CreatedDate = DateTime.Now,
                    Password = hashPassword,
                    RoleName = "Manager",
                    IsVerify = true,
                    IsDelete = false,
                };
                bool check = await _userRepository.CreateUser(user);
                if (!check)
                {
                    return new BaseResponse<UserResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                var response = _mapper.Map<UserResponseModel>(user);
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Register admin success!.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }
    }
}
