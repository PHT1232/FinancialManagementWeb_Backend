using EntityFramework.DbEntities.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repository.Pictures
{
    public class PictureRepository : IPicturesRepository
    {
        private ProjectDbContext _dbContext;

        public PictureRepository(ProjectDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task AddGroupProfile(GroupProfilePicture picture)
        {
            await _dbContext.GroupProfilePicture.AddAsync(picture);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUserProfile(UserProfilePicture picture)
        {
            await _dbContext.UserProfilePicture.AddAsync(picture);
            await _dbContext.SaveChangesAsync();
        }
    }
}
