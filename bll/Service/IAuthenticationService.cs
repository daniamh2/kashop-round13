using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP.dal.DTO.Request;
using KASHOP.dal.DTO.Response;
using KASHOP.dal.Models;
using Microsoft.AspNetCore.Identity;

namespace KASHOP.bll.Service
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<bool> confirmEmailAsync(string token, string userId);
        Task<ForgotPasswordResponse> RequestPasswordReset(ForgotPasswordRequest request);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
