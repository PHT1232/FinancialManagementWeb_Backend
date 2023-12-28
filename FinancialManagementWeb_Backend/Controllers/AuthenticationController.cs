using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TeamManagementProject_Backend.Controllers
{

    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public string Login()
        {
            var tokenString = GenerateJSONWebToken();
            return tokenString;
        }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"]
                , null
                , expires: DateTime.Now.AddMinutes(120)
                , signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
