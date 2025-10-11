using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.FileHelper
{
    public static class FileHelper
    {
        /// <summary>
        /// Uploads a file to a specific folder inside wwwroot. 
        /// Creates the folder automatically if it doesn't exist.
        /// </summary>
        public static string UploadFile(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            // 1. Define folder path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            // 2. Ensure the folder exists
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 3. Generate a unique file name (to avoid name collisions)
            string extension = Path.GetExtension(file.FileName);
            string fileName = $"{Guid.NewGuid()}{extension}";

            // 4. Combine full file path
            string filePath = Path.Combine(folderPath, fileName);

            // 5. Save the file safely
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            // 6. Return the stored file name
            return fileName;
        }

        /// <summary>
        /// Deletes a file safely from a specific folder if it exists.
        /// </summary>
        public static void DeleteFile(string fileName, string folderName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        /// Checks if a file exists in a specific folder.
        /// </summary>
        public static bool FileExists(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);
            return File.Exists(filePath);
        }
    }

}
