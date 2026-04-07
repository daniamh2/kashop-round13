using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KASHOP.dal.DTO.Request;
using KASHOP.dal.DTO.Response;
using KASHOP.dal.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static System.Net.WebRequestMethods;
using ForgotPasswordRequest = KASHOP.dal.DTO.Request.ForgotPasswordRequest;
using LoginRequest = KASHOP.dal.DTO.Request.LoginRequest;
using RegisterRequest = KASHOP.dal.DTO.Request.RegisterRequest;
using ResetPasswordRequest = KASHOP.dal.DTO.Request.ResetPasswordRequest;

namespace KASHOP.bll.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(UserManager<ApplicationUser> userManager , 
            IEmailSender emailSender , IConfiguration configuration 
            ,IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }//clr



        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);
              if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);
                var mailUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}" +
                    $"/api/Account/confirmEmail?token={token}&userId={user.Id}";
                await _emailSender.SendEmailAsync(request.Email, "welcome", $"<h1>welcome,{request.UserName}</h1>"+
                    $"<a href='{mailUrl}'>confirm</a>");

                return new RegisterResponse() { Success = true, Message = "success" };
            }

            return new RegisterResponse()
            {
                Success = false,
                Message = "error",
                Errors = result.Errors.Select(p => p.Description).ToList()
            };
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return new LoginResponse() { Success = false, Message = "invalid email" };

            if(!await _userManager.IsEmailConfirmedAsync(user)) return new LoginResponse() {
                Success = false, Message = "email isnt confirmed" };

            var result = await _userManager.CheckPasswordAsync(user,request.Password);
            if (!result) { 
                return new LoginResponse() { Success = false, Message = "invalid password" };

            }
            return new LoginResponse() { Success = true, Message = "success", AccessToken=await generateAccessToken(user) };
        }
        private async Task<string> generateAccessToken(ApplicationUser user) {
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim (ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<bool> confirmEmailAsync(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return false;
            var result =await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded) return true;
            return false;
        }

        public async Task <ForgotPasswordResponse> RequestPasswordReset(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
            {
                return new ForgotPasswordResponse()
                {
                    Message = "Email Not Found",
                    Success = false
                };
            }
            var random = new Random();
            var code = random.Next(1000,9999).ToString();
            user.PasswordResetCode = code;
            user.PasswordResetCodeExpiry = DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(request.Email, "reset password", $"<p> reset password is {code}</p>");
            return new ForgotPasswordResponse()
            {
                Success = true,
                Message = "code sent to  your email"
            };

        }
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new ResetPasswordResponse()
                {
                    Message = "Email Not Found",
                    Success = false
                };
            }
            else if (user.PasswordResetCode != request.code)
            {
                return new ResetPasswordResponse()
                {
                    Message = "invalid code",
                    Success = false
                };

            }
            else if (user.PasswordResetCodeExpiry < DateTime.UtcNow)
            {
                return new ResetPasswordResponse()
                {
                    Message = "code Expired",
                    Success = false
                };

            }
            var isSamePassword = await _userManager.CheckPasswordAsync(user, request.NewPassword);
            if (isSamePassword)
            {
                return new ResetPasswordResponse()
                {
                    Success = false,
                    Message = "new password must be different from old password"
                };

            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!result.Succeeded)
            {
                return new ResetPasswordResponse()
                {
                    Message = "password reset failed",
                    Success = false
                };
            }
            await _emailSender.SendEmailAsync(request.Email, "change password", $"<p>your password is changed</p>");
            return new ResetPasswordResponse()
            {
                Message = "password reset succefully",
                Success = true
            };

        }
    }

}