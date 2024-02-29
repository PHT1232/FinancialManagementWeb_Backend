using EntityFramework.DbEntities.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private ProjectDbContext _dbContext;

        public GroupRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Group entity)
        {
            await _dbContext.Groups.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUserToGroup(GroupUsers groupUser)
        {
            await _dbContext.GroupRoles.AddAsync(groupUser);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(Group entity)
        {
            _dbContext.Groups.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<Group> Get(string id)
        {
            Group group = await _dbContext.Groups.FirstOrDefaultAsync(e => e.Id == id);
            return group;
        }

        public Task<IEnumerable<Group>> GetAllByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Group entity, string id)
        {
            Group group = await _dbContext.Groups.FirstOrDefaultAsync(e => e.Id == id);
            group.Name = entity.Name;
            group.password = entity.password;
            group.IsPublic = entity.IsPublic;
            group.IconUrl = entity.IconUrl;
            group.Description = entity.Description;

            _dbContext.Groups.Update(group);
            _dbContext.SaveChanges();
        }
    }
}
