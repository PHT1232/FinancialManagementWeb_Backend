using EntityFramework.DbEntities.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repository.Pictures
{
    public interface IPicturesRepository
    {
        Task AddGroupProfile(GroupProfilePicture picture);
        Task AddUserProfile(UserProfilePicture picture);
        Task<string> GetProfilePicture(string userId);
        Task<List<UserProfilePicture>> GetAll();
    }
}