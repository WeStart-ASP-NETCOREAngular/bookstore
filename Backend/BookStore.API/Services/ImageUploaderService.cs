using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BookStore.API.Services
{
    public class ImageUploaderService : IImageUploaderService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageUploaderService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task ResizeImage(string filePath, string uploadedFolder, string fileName)
        {
            var folderMedi = Path.Combine(uploadedFolder, "Thumbs", "Med", fileName);
            var folderSmall = Path.Combine(uploadedFolder, "Thumbs", "Small", fileName);

            //application/pdf
            //images/jpg
            using (Image input = Image.Load(filePath))
            {

                input.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Crop, Size = new Size(400, 400) }));
                await input.SaveAsync(folderMedi);

                input.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Crop, Size = new Size(120, 120) }));
                await input.SaveAsync(folderSmall);

            }

        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var uploadFolder = Path.Combine(_environment.WebRootPath, "Images");
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            await ResizeImage(filePath, uploadFolder, fileName);
            return fileName;

        }
    }
}
