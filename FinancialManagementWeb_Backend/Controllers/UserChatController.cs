using EntityFramework.DbEntities.Chats;
using EntityFramework.Repository;
using EntityFramework.Repository.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TeamManagementProject_Backend.Controllers.HubClass;

namespace TeamManagementProject_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user/chat")]
    public class UserChatController : ControllerBase
    {
        //private readonly IHubContext<ChatHub> _hubContext;
        private readonly IChatRepository _chatRepository;

        public UserChatController(IChatRepository chatRepository) 
        {
            _chatRepository = chatRepository;
        }

        [Authorize]
        [Route("GetRecentChat")]
        [HttpGet]
        public async Task<IActionResult> GetRecentChatUser(string userId)
        {
            IEnumerable<Chat> recentUserId = await _chatRepository.GetRecentChatUser(userId);
            return Ok(recentUserId);
        }
    }
}
