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

        public async Task Add(Chat entity)
        {
            await _dbContext.Chats.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(Chat entity)
        {

            throw new NotImplementedException();
        }

        public async Task<Chat> Get(long id)
        {
            Chat chat = await _dbContext.Chats.FirstOrDefaultAsync(x => x.Id == id);
            return chat;
        }

        public async Task<IEnumerable<Chat>> GetAll()
        {
            List<Chat> chats = await _dbContext.Chats.ToListAsync();
            return chats;
        }

        public async Task<IEnumerable<Chat>> GetRecentChatUser(string userId)
        {
            List<Chat> recentUserChat = await _dbContext.Chats
                .Where(e => e.UserSentId == userId || e.UserOrGroupReceivedId == userId).ToListAsync();

            return recentUserChat;
        }

        public async Task Update(Chat entity, long id)
        {
            throw new NotImplementedException();
        }
    }
}
