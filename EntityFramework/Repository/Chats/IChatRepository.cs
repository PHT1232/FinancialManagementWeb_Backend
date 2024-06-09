﻿using EntityFramework.DbEntities.Chats;
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
        Task<IEnumerable<ChatMessages>> GetAll();
        Task<ChatMessages> Get(long id);
        Task<IEnumerable<ChatSession>> GetRecentChatUser(string userId);
        Task Add(ChatMessages entity);
        Task Update(ChatMessages entity, long id);
        void Delete(ChatMessages entity);
    }
}
