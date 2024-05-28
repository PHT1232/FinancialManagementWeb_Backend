using EntityFramework.DbEntities.Chats;
using EntityFramework.Repository;
using EntityFramework.Repository.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly UserManager<IdentityUser> _userManager;

        public UserChatController(IChatRepository chatRepository
            , UserManager<IdentityUser> userManager) 
        {
            _chatRepository = chatRepository;
            _userManager = userManager;
        }

        [Authorize]
        [Route("GetRecentChat")]
        [HttpGet]
        public async Task<IActionResult> GetRecentChatUser(string userId)
        {
            IEnumerable<IdentityUser> recentUserId = await _chatRepository.GetRecentChatUser(userId);
            IEnumerable<IdentityUser> recentUser = from user in recentUserId
                                                   join userInDb in _userManager.Users
                                                   on user.Id equals userInDb.Id
                                                   select userInDb;

            return Ok(recentUser);
        }

        [AllowAnonymous]
        [Route("GetUploadFolder")]
        [HttpGet]
        public IActionResult GetUploadFolder()
        {
            return Ok(AppFolders.UserProfilePictures);
        }
    }
}
