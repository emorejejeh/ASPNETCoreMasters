using ASPNetCoreMastersTodoList.Api.BindingModels;
using DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCoreMastersTodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Authentication _authentication;
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IOptions<Authentication> authentication,
                               IOptions<JwtOptions> jwtOptions,
                               UserManager<IdentityUser> userManager,
                                ILogger<UsersController> logger)
        {
            _authentication = authentication.Value;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            _logger.LogInformation("Registering User - {RequestTime}", DateTime.Now);
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                return Ok(new { code, email = model.Email });
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Confirm(ConfirmBindingModel model)
        {
            _logger.LogInformation("Confirming User - {RequestTime}", DateTime.Now);
            var user = await _userManager.FindByEmailAsync(model.Email);
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));

            if (user == null || user.EmailConfirmed)
            {
                return BadRequest();
            }
            else if((await _userManager.ConfirmEmailAsync(user, code)).Succeeded)
            {
                return Ok("Your email is confirmed");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            _logger.LogInformation("Logging in User - {RequestTime}", DateTime.Now);
            IActionResult actionResult;

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                actionResult = NotFound(new { errors = new[] { $"User with email '{model.Email}' is not found" } });
            }
            else if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!user.EmailConfirmed)
                {
                    actionResult = BadRequest(new { errors = new[] { $"Email is not confirmed. Please go to your email account." } });
                }
                else
                {
                    var token = GenerateTokenAsync(user);
                    actionResult = Ok(new { jwt = "bearer " + token });
                }
            }
            else
            {
                actionResult = BadRequest(new { errors = new[] { $"User password is not valid." } });
            }

            return actionResult;
        }

        private string GenerateTokenAsync(IdentityUser user)
        {
            _logger.LogInformation("Generating Token - {RequestTime}", DateTime.Now);
            IList<Claim> userClaims = new List<Claim>
            {
                new Claim("Username", user.UserName),
                new Claim("Email", user.Email)
            };

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMonths(1),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(_jwtOptions.SecurityKey, SecurityAlgorithms.HmacSha256
                )));

        }
    }
}
