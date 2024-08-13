using Microsoft.AspNetCore.Http;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IFirebaseStorageService
    {
        Task<(bool, string)> UploadKaraokeVideoAsync(IFormFile videoFile);
        Task<bool> RemoveKaraokeVideoAsync(string fileName);
    }
}
