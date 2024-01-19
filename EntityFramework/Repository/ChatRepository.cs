using Microsoft.EntityFrameworkCore;
using ProjectModel.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repository
{
    public class ChatRepository : IDataRepository<Chat>
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

        public async Task Delete(Chat entity)
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

        public async Task Update(Chat entity, long id)
        {
            throw new NotImplementedException();
        }
    }
}
