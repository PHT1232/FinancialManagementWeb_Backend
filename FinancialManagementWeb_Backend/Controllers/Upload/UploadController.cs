using EntityFramework.DbEntities.Pictures;
using EntityFramework.Repository.Pictures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectModel.UploadModels;
using TeamManagementProject_Backend.Global;

namespace TeamManagementProject_Backend.Controllers.Upload
{
    /// <summary>
    /// make this into a repository
    /// </summary>
    [Authorize]
    [Route("api/upload/[action]")]
    public class UploadController : ControllerBase
    {
        private IPicturesRepository _picturesRepository;

        public UploadController(IPicturesRepository picturesRepository) 
        {
            _picturesRepository = picturesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UserProfileUpload(UserProfileModel userProfile)
        {
            string filePath = Path.Combine(AppFolders.ProfilePictures);

            UserProfilePicture picture = new UserProfilePicture();
            picture.Url = SingleUploadHandler(filePath);
            picture.Username = userProfile.Username;
            picture.Updated = DateTime.Now;

            await _picturesRepository.AddUserProfile(picture);

            return Ok();
        }

        private string SingleUploadHandler(string filePath)
        {
            string fileFolderPath = "";
            
            if (Request.Form.Files == null || Request.Form.Files.Count == 0)
            {
                return fileFolderPath;
            }

            var file = Request.Form.Files.Single();

            fileFolderPath = GlobalFunction.SaveFile(filePath, file);

            return fileFolderPath;
        }
    }
}
