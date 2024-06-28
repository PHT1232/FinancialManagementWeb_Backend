﻿using EntityFramework.DbEntities;
using EntityFramework.DbEntities.Chats;
using EntityFramework.Repository;
using EntityFramework.Repository.Chats;
using EntityFramework.Repository.Pictures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjectModel.AuthModel;
using ProjectModel.ChatModels;
using TeamManagementProject_Backend.Controllers.Hubs;
using TeamManagementProject_Backend.Global;

namespace TeamManagementProject_Backend.Controllers
{
    [ApiController]
    [Route("api/user/chat")]
    public class UserChatController : ControllerBase
    {
        //private readonly IHubContext<ChatHub> _hubContext;
        private readonly IChatRepository _chatRepository;
        private readonly IPicturesRepository _picturesRepository;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public UserChatController(IChatRepository chatRepository
            , IPicturesRepository pictureRepository
            , UserManager<CustomUser> userManager
            , IHubContext<ChatHub> hubContext) 
        {
            _chatRepository = chatRepository;
            _picturesRepository = pictureRepository;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        [AllowAnonymous]
        [Route("SendMessage")]
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageModel chatModel)
        {
            if (chatModel == null)
            {
                throw new("Chat is this real ?");
            }
            
            long chatSessionid = chatModel.ChatSessionId;

            if (chatSessionid == 0)
            {
                ChatSession chatSession = new ChatSession
                {
                    FirstUserId = chatModel.SentId,
                    SecondUserId = chatModel.ReceivedId,
                    CreatedDate = DateTime.Now,
                };
                chatSessionid = await _chatRepository.AddSessionAndGetId(chatSession);
            }

            ChatMessages chat = new ChatMessages
            {
                ChatSessionId = chatSessionid,
                ChatMessage = chatModel.Message,
                CreatedDate = DateTime.Now
            };

            await _chatRepository.AddMessages(chat);

            var list = await _chatRepository.GetAll();
            
            try {
                await _hubContext.Clients.User(chatModel.ReceivedId).SendAsync("MessageListener", chatModel.ReceivedId, chatModel.Message);

            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            return Ok();
        }

        [Authorize]
        [Route("GetRecentUserChat")]
        [HttpGet]
        public async Task<IActionResult> GetRecentChatUser(string userId)
        {
            // IEnumerable<ChatSession> recentUserId = await _chatRepository.GetRecentChatUser(userId);
            // IEnumerable<IdentityUser> recentUser = from user in recentUserId
            //                                        join userInDb in _userManager.Users
            //                                       <CustomUser> on userId = user.FirstUserId == null ? user.SecondUserId : user.FirstUserId equals userInDb.Id
            //                                        select userInDb;
            throw new NotImplementedException();
            // return Ok(recentUser);
        }

        [Authorize]
        [Route("GetRecentChatMessage")]
        [HttpGet]
        public async Task<IActionResult> GetRecentChatMessage(string chatSessionId) 
        {

            throw new NotImplementedException();
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
