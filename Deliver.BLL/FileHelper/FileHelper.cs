using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Deliver.BLL.FileHelper
{
    public static class FileHelper
    {
        /// <summary>
        /// Uploads a file to a specific folder inside wwwroot.
        /// </summary>
        public static string UploadFile(IFormFile file, string folderName, IWebHostEnvironment env)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            // 1. Use env.WebRootPath instead of Directory.GetCurrentDirectory()
            string folderPath = Path.Combine(env.WebRootPath, folderName);

            // 2. Ensure the folder exists
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 3. Generate a unique file name
            string extension = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{extension}";

            // 4. Combine full file path
            string filePath = Path.Combine(folderPath, fileName);

            // 5. Save the file
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            // 6. Return relative path (for frontend or DB)
            return $"/{folderName}/{fileName}";
        }

        /// <summary>
        /// Deletes a file safely using IWebHostEnvironment.
        /// </summary>
        public static void DeleteFile(string fileName, string folderName, IWebHostEnvironment env)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            string filePath = Path.Combine(env.WebRootPath, folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        /// Checks if a file exists inside a folder using IWebHostEnvironment.
        /// </summary>
        public static bool FileExists(string fileName, string folderName, IWebHostEnvironment env)
        {
            string filePath = Path.Combine(env.WebRootPath, folderName, fileName);
            return File.Exists(filePath);
        }
    }
}
