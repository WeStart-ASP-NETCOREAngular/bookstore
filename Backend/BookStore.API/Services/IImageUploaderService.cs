namespace BookStore.API.Services
{
    public interface IImageUploaderService
    {
        Task ResizeImage(string filePath, string uploadedFolder, string fileName);
        Task<string> UploadImageAsync(IFormFile file);
    }
}
