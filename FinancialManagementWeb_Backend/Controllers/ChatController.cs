using EntityFramework.DbEntities.Chats;
using EntityFramework.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TeamManagementProject_Backend.Controllers.HubClass;

namespace TeamManagementProject_Backend.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IDataRepository<Chat> _chatRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IDataRepository<Chat> chatRepository
            , IHubContext<ChatHub> hubContext)
        {
            _chatRepository = chatRepository;
            _hubContext = hubContext;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllChat()
        {
            IEnumerable<Chat> chats = await _chatRepository.GetAll();
            return Ok(chats);
        }

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

        [Route("SendGroupMessage")]
        [HttpPost]
        public async Task<IActionResult> SendGroupMessage([FromBody] ChatModel chatModel)
        {
            if (chatModel == null)
            {
                return BadRequest("Chat is null.");
            }
            try
            {
                Chat chat = new Chat();
                chat.UserSentId = chatModel.UserSentId;
                chat.UserOrGroupReceivedId = chatModel.UserOrGroupReceivedId;
                chat.ChatMessage = chatModel.ChatMessage;
                chat.CreatedDate = new DateTime();
                chat.ModifiedDate = new DateTime();
                await _chatRepository.Add(chat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var list = await _chatRepository.GetAll();
            await _hubContext.Clients.All.SendAsync("TransferChartData", list);
            return Ok();
        }
    }
}
