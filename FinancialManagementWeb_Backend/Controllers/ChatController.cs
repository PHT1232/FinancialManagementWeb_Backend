using EntityFramework.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectModel.Chats;
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

        [Route("SendMessage")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Chat chat)
        {
            if (chat == null)
            {
                return BadRequest("Chat is null.");
            }
            try
            {
                Chat chat1 = new Chat();
                chat1.ChatMessage = chat.ChatMessage;
                await _chatRepository.Add(chat1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var list = await _chatRepository.GetAll();
            await _hubContext.Clients.All.SendAsync("TransferChartData", list);
            return Ok(chat);
        }
    }
}
