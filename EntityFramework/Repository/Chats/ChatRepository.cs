using EntityFramework.DbEntities.Chats;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repository.Chats
{
    public class ChatRepository : IChatRepository
    {
        private ProjectDbContext _dbContext;

        public ChatRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(ChatMessages entity)
        {
            await _dbContext.Chats.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(ChatMessages entity)
        {

            throw new NotImplementedException();
        }

        public async Task<ChatMessages> Get(long id)
        {
            ChatMessages chat = await _dbContext.Chats.FirstOrDefaultAsync(x => x.Id == id);
            return chat;
        }

        public async Task<IEnumerable<ChatMessages>> GetAll()
        {
            List<ChatMessages> chats = await _dbContext.Chats.ToListAsync();
            return chats;
        }

        public async Task<IEnumerable<ChatSession>> GetRecentChatUser(string userId)
        {
            List<ChatSession> recentUserFromChatRepo = await _dbContext.ChatSession
                .Where(e => e.FirstUserId == userId || e.SecondUserId == userId)
                .ToListAsync();

            return recentUserFromChatRepo;
        }

        public async Task<IEnumerable<ChatMessages>> GetRecentChatMessagesUser(long chatSessionId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ChatMessages entity, long id)
        {
            throw new NotImplementedException();
        }
    }
}
