using System.IO;

namespace TeamManagementProject_Backend.Global
{
    public static class GlobalFunction
    {
        private static byte[] GetAllBytes(Stream stream)
        {
            using MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }

        public static string SaveFile(string folderPath, IFormFile importFile)
        {
            byte[] fileBytes;
            
            using (var stream = importFile.OpenReadStream())
            {
                fileBytes = GetAllBytes(stream);
            }

            string uploadFileName = importFile.FileName;

            string uploadFilePath = Path.Combine(folderPath + @"\" + uploadFileName);

            //if (!Directory.Exists(uploadFilePath))
            //{
            //    Directory.CreateDirectory(uploadFilePath);
            //}

            //FileStream fileStream = new FileStream(uploadFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);


            using (FileStream fileStream = System.IO.File.Create(uploadFilePath))
            {
                importFile.CopyTo(fileStream);
            }

            //File.SetAttributes(AppFolders.UserProfilePictures, FileAttributes.Normal);
            //File.WriteAllBytes(AppFolders.UserProfilePictures, fileBytes);

            return uploadFileName;
        }
    }
}
