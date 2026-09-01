using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace GymManagementBLL.Services.Attachments
{
    public class AttatchmentService(IWebHostEnvironment webHost) : IAttachmentService
    {

        private readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long MaxFileSize = 5 * 1024 * 1024;
        public string? Upload(string FolderName, IFormFile File)
        {
            try
            {
                if (FolderName is null || File is null || File.Length == 0) return null;
                if (File.Length > MaxFileSize) return null;

                string extension = Path.GetExtension(File.FileName).ToLower();
                if (!AllowedExtensions.Contains(extension)) return null;

                var folderPath = Path.Combine(webHost.WebRootPath, "Images", FolderName);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + extension;   // ✅ Fix 1
                var filePath = Path.Combine(folderPath, fileName);       // ✅ Fix 2

                using var fileStream = new FileStream(filePath, FileMode.Create);
                File.CopyTo(fileStream);

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public bool Delete(string FileName, string FolderName)
        {
            try
            {
                if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FolderName)) return false;
                var FullFilePathToDelete = Path.Combine(webHost.WebRootPath, "Images", FolderName, FileName);

                if (File.Exists(FullFilePathToDelete))
                {
                    File.Delete(FullFilePathToDelete);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

    }
}
