using KASHOP.bll.Service;
using KASHOP.dal.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.pl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService) {
            _authenticationService = authenticationService;
        
        }
        [HttpPost("register")]
        public async Task<IActionResult> Rigester(RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            return Ok(result);
        }
        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("confirmEmail")]
        public async Task<IActionResult> confirmEmail(string token,string userId)
        {
            var isConfirmed = await _authenticationService.confirmEmailAsync(token, userId);
            if (isConfirmed) return Ok();
            return BadRequest();

        }
        [HttpPost("sendCode")]
        public async Task<IActionResult> RequestPasswordResetCode(ForgotPasswordRequest request)
        {
            var result = await _authenticationService.RequestPasswordReset(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var result = await _authenticationService.ResetPasswordAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }





    }
}
