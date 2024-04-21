using EntityFramework.DbEntities.Pictures;
using EntityFramework.Repository.Pictures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> UserProfileUpload(UserProfilePicture userProfile)
        {
            string fileFolderPath = Path.Combine(AppFolders.UserProfilePictures + @"/");

            return Ok();
        }

        private async Task<List<string>> UploadHandler(string fileFolderPath)
        {
            List<string> files = new List<string>();
            
            if (Request.Form.Files == null || Request.Form.Files.Count == 0)
            {
                return files;
            }
            
            foreach (var file in Request.Form.Files)
            {
                files.Add(GlobalFunction.SaveFile(fileFolderPath, file));
            }

            return await Task.FromResult(files);
        }
    }
}
