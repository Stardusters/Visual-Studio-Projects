using CloudinaryDotNet.Actions;

namespace RunGroupWebApp.Interfaces
{
    public interface IPhoteService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
