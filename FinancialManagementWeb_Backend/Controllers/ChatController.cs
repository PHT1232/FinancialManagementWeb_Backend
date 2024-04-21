using EntityFramework.DbEntities.Chats;
using EntityFramework.Repository;
using EntityFramework.Repository.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjectModel.ChatModels;
using TeamManagementProject_Backend.Controllers.HubClass;

namespace TeamManagementProject_Backend.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IChatRepository chatRepository
            , IHubContext<ChatHub> hubContext
            , UserManager<IdentityUser> userManager)
        {
            _chatRepository = chatRepository;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        [Authorize]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllChat()
        {
            IEnumerable<Chat> chats = await _chatRepository.GetAll();
            return Ok(chats);
        }

        [Authorize]
        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { Message = "Request Completed" });
        }

        [Authorize]
        [Route("SendMessage")]
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage chatModel)
        {
            if (chatModel == null)
            {
                throw new ("Chat is this real ?");
            }
            var user = await _userManager.FindByNameAsync(chatModel.SentId);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(chatModel.SentId);
            }

            Chat chat = new Chat();
            chat.UserSentId = chatModel.SentId;
            chat.UserOrGroupReceivedId = chatModel.ReceivedId;
            chat.ChatMessage = chatModel.Message;
            chat.CreatedDate = new DateTime().Date;
            chat.ModifiedDate = new DateTime().Date;
            await _chatRepository.Add(chat);

            var list = await _chatRepository.GetAll();
            await _hubContext.Clients.All.SendAsync("TransferChartData", list);
            return Ok();
        }
    }
}
