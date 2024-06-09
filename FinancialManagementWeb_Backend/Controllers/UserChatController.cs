using EntityFramework.DbEntities.Chats;
using EntityFramework.Repository;
using EntityFramework.Repository.Chats;
using EntityFramework.Repository.Pictures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectModel.AuthModel;
using ProjectModel.ChatModels;
using TeamManagementProject_Backend.Controllers.Hubs;
using TeamManagementProject_Backend.Global;

namespace TeamManagementProject_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user/chat")]
    public class UserChatController : ControllerBase
    {
        //private readonly IHubContext<ChatHub> _hubContext;
        private readonly IChatRepository _chatRepository;
        private readonly IPicturesRepository _picturesRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UserChatController(IChatRepository chatRepository
            , IPicturesRepository pictureRepository
            , UserManager<IdentityUser> userManager) 
        {
            _chatRepository = chatRepository;
            _picturesRepository = pictureRepository;
            _userManager = userManager;
        }

        [Authorize]
        [Route("GetRecentUserChat")]
        [HttpGet]
        public async Task<IActionResult> GetRecentChatUser(string userId)
        {
            IEnumerable<ChatSession> recentUserId = await _chatRepository.GetRecentChatUser(userId);
            IEnumerable<IdentityUser> recentUser = from user in recentUserId
                                                   join userInDb in _userManager.Users
                                                   on userId = user.FirstUserId == null ? user.SecondUserId : user.FirstUserId equals userInDb.Id
                                                   select userInDb;

            return Ok(recentUser);
        }

        [Authorize]
        [Route("GetRecentChatMessage")]
        [HttpGet]
        public async Task<IActionResult> GetRecentChatMessage(string chatSessionId) 
        {
            
        }

        [AllowAnonymous]
        [Route("GetUploadFolder")]
        [HttpGet]
        public IActionResult GetUploadFolder()
        {
            return Ok(AppFolders.UserProfilePictures);
        }

        [Route("SearchUsers")]
        [HttpGet]
        public async Task<IActionResult> SearchUsers(string searchValues) {
            var users = await _userManager.Users.Where(user => user.UserName.Contains(searchValues) || user.Email.Contains(searchValues) || user.Id.Contains(searchValues)).ToListAsync();
            // var users = await _userManager.Users.Where(user => user.UserName.Contains(searchValues)).ToListAsync();
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
