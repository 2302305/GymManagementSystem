using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.Services.Attachments
{
    public interface IAttachmentService
    {
        string? Upload(string FolderName, IFormFile File);
        bool Delete(string FileName, string FolderName);
    }
}
