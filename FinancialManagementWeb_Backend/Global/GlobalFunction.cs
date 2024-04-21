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

            string uploadFilePath = Path.Combine(folderPath + @"/" + uploadFileName);

            if (!Directory.Exists(uploadFilePath))
            {
                Directory.CreateDirectory(uploadFilePath);
            }

            File.WriteAllBytes(uploadFilePath, fileBytes);

            return uploadFileName;
        }
    }
}
