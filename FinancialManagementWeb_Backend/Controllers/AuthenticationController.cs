using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectModel.AuthModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace TeamManagementProject_Backend.Controllers
{

    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config
            , UserManager<IdentityUser> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ApplicationUser model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.UserName);
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var tokenString = GenerateJSONWebToken(authClaims);
                LoginInfo loginInfo = new LoginInfo()
                {
                    UserName = user.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(tokenString),
                    Exp = tokenString.ValidTo,
                };
                return Ok(loginInfo);
            } else
            {
                throw new ("Wrong password or username");
            }
        }

        [Authorize(Roles = ApplicationRole.Admin)]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] ApplicationUser model)
        {
            var databaseUser = await _userManager.FindByNameAsync(model.UserName);
            if (databaseUser != null)
            {
                return BadRequest("User exist");
            }

            IdentityUser user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Register failed");
            }

            return Ok("Register successful");
        }

        [Route("admin-register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromBody] ApplicationUser model)
        {
            var databaseUser = await _userManager.FindByNameAsync(model.UserName);
            if (databaseUser != null)
            {
                return BadRequest("User exist");
            }

            IdentityUser user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Register failed");
            }

            if (!await _roleManager.RoleExistsAsync(ApplicationRole.Admin))
                await _roleManager.CreateAsync(new IdentityRole(ApplicationRole.Admin));
            if (!await _roleManager.RoleExistsAsync(ApplicationRole.User))
                await _roleManager.CreateAsync(new IdentityRole(ApplicationRole.User));

            if (await _roleManager.RoleExistsAsync(ApplicationRole.Admin))
            {
                await _userManager.AddToRoleAsync(user, ApplicationRole.Admin);
            }
            if (await _roleManager.RoleExistsAsync(ApplicationRole.Admin))
            {
                await _userManager.AddToRoleAsync(user, ApplicationRole.User);
            }
            return Ok("User created successfully!");

        }

        private JwtSecurityToken GenerateJSONWebToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [AllowAnonymous]
        [Route("GetErrored")]
        [HttpGet]
        public IActionResult Getbitch()
        {
            throw new Exception("Get Error");
        }
    }
}
