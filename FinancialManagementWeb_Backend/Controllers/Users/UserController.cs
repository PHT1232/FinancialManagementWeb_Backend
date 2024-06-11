using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EntityFramework.DbEntities;
using EntityFramework.Repository.Pictures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectModel.AuthModel;

namespace TeamManagementProject_Backend.Controllers.Users 
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly UserManager<CustomUser> _userManager;
        private readonly IPicturesRepository _picturesRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public UserController(UserManager<CustomUser> userManager
            , IPicturesRepository pictureRepository
            , RoleManager<IdentityRole> roleManager
            , IConfiguration config) 
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _picturesRepository = pictureRepository;
            _config = config;
        }
    
        [Authorize(Roles = ApplicationRole.Admin)]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] ApplicationUser model)
        {
            var databaseUser = await _userManager.FindByNameAsync(model.UserName);
            if (databaseUser != null)
            {
                throw new("Người dùng đã tồn tại");
            }

            databaseUser = await _userManager.FindByEmailAsync(model.Email);
            if (databaseUser != null)
            {
                throw new("Email này đã được sử dụng");
            }

            CustomUser user = new()
            {
                UserRealName = model.UserRealName,
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, ApplicationRole.User);

            if (!result.Succeeded)
            {
                throw new("Đăng ký thất bại");
            }

            return Ok("Tạo người dùng mới thành công!");
        }

        [AllowAnonymous]
        [Route("admin-register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromBody] ApplicationUser model)
        {
            var databaseUser = await _userManager.FindByNameAsync(model.UserName);
            if (databaseUser != null)
            {
                throw new("Người dùng đã tồn tại");
            }

            CustomUser user = new()
            {
                UserRealName = "admin",
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new("Đăng ký thất bại");
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
            return Ok("Tạo người dùng mới thành công!");
        }

        [Authorize(Roles = ApplicationRole.Admin)]
        [Route("GetUsersForDisplay")]
        [HttpGet]
        public async Task<IActionResult> GetUsersForDisplay() 
        {
            var users = await _userManager.GetUsersInRoleAsync(ApplicationRole.User);

            var userDisplays = new List<UserDisplay>();

            foreach (var user in users) {
                try {
                    var picture = await _picturesRepository.GetProfilePicture(user.Id);
                    userDisplays.Add(new UserDisplay{
                                UserId = user.Id,
                                Email = user.Email,
                                UserName = user.UserName,
                                UserProfile = picture,
                                Role = "User",
                             });
                } catch (Exception) {
                    continue;
                }
            }

            return Ok(userDisplays);   
        }
    }
}