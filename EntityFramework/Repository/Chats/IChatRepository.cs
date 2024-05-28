using EntityFramework.DbEntities.Chats;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repository.Chats
{
    public interface IChatRepository
    {
        Task<IEnumerable<Chat>> GetAll();
        Task<Chat> Get(long id);
        Task<IEnumerable<IdentityUser>> GetRecentChatUser(string userId);
        Task Add(Chat entity);
        Task Update(Chat entity, long id);
        void Delete(Chat entity);
    }
}
