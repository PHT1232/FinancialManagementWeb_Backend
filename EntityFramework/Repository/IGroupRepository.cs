using EntityFramework.DbEntities.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repository
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAllByUserId(string UserId);
        Task<Group> Get(string id);
        Task Add(Group entity);
        Task Update(Group entity, string id);
        void Delete(Group entity);
        Task AddRoleForUser(string GroupId, string UserId);
    }
}
