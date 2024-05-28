using EntityFramework.DbEntities.Pictures;
using EntityFramework.Repository.Pictures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamManagementProject_Backend.Global;

namespace TeamManagementProject_Backend.Controllers.Upload
{
    [Authorize]
    [Route("api/upload/[action]")]
    public class UploadController : ControllerBase
    {
        private readonly IPicturesRepository _picturesRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UploadController(IPicturesRepository picturesRepository
            , UserManager<IdentityUser> userManager) 
        {
            _picturesRepository = picturesRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserProfileUpload(string username)
        {
            var validateUser = await _userManager.FindByNameAsync(username);

            if (validateUser == null)
            {
                throw new Exception("Không tồn tại người dùng!");
            }

            string filePath = Path.Combine(AppFolders.UserProfilePictures, validateUser.Id);

            UserProfilePicture picture = new UserProfilePicture();
            picture.Url = SingleUploadHandler(filePath);
            picture.UserId = validateUser.Id;
            picture.Updated = DateTime.Now;

            await _picturesRepository.AddUserProfile(picture);

            return Ok();
        }

        [HttpPost]
        public IActionResult Delete(string filePath)
        {
            GlobalFunction.DeleteFile(filePath);

            return Ok();
        }

        private string SingleUploadHandler(string filePath)
        {
            string fileNameForUpload = "";
            
            if (Request.Form.Files == null || Request.Form.Files.Count == 0)
            {
                return fileNameForUpload;
            }

            var file = Request.Form.Files.Single();

            fileNameForUpload = GlobalFunction.SaveFile(filePath, file);

            return fileNameForUpload;
        }
    }
}
