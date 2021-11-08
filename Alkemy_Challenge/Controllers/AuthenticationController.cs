using Alkemy_Challenge.Entities;
using Alkemy_Challenge.Interfaces;
using Alkemy_Challenge.ViewModels.Auth;
using Alkemy_Challenge.ViewModels.Auth.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signManager, RoleManager<IdentityRole> roleManager, IMailService mailService)
        {
            _userManager = userManager;
            _signManager = signManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequestViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            
            if (userExists != null)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"User creation failed for {model.Username}"
                });
            }

            var user = new User()
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        message = $"User creation failed!  ERROR: {String.Join(", ",result.Errors.Select(x=>x.Description))}"
                    });
            }

            await _mailService.SendEmail(model.Username);

            return Ok(new
            {
                Status = "Success",
                message = $"User creation Successfully!"
            });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterRequestViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"User creation failed for {model.Username}"
                });
            }

            var user = new User()
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        message = $"User creation failed!  ERROR: {String.Join(", ", result.Errors.Select(x => x.Description))}"
                    });
            }

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));
            
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            await _userManager.AddToRoleAsync(user,"User");


            return Ok(new
            {
                Status = "Success",
                message = $"User creation Successfully!"
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            var result = await _signManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(model.Username);

                if (currentUser.IsActive)
                {
                    return Ok(await GetToken(currentUser));
                }
            }

            return StatusCode(StatusCodes.Status401Unauthorized,
                new
                {
                    Status = "Error",
                    message = $"The usser {model.Username} is not authorized!"
                });
        }

        private async Task<LoginResponseViewModel> GetToken(User currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySuperSecretayLargaDeAUTORIZACION"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5000",
                audience: "https://localhost:5000",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256));

            return new LoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}