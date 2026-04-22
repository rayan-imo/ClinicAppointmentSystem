using ClinicAppointment.Service.Dto;
using ClinicAppointment.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ClinicAppointment.Service.Services
{
    public class ImageService(IHostEnvironment _env) : IImageService
    {
        public async Task<string> UploadImageAsync(UploadImageDto dto)
        {
            if (dto.Image == null || dto.Image.Length == 0)
            {
                throw new Exception("No file was uploaded");
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            var extension = Path.GetExtension(dto.Image.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception("Unsupported file type");
            }
            var fileName = Guid.NewGuid().ToString() + extension;
            var folderPath = Path.Combine(_env.ContentRootPath, "wwwroot", "images");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {

                await dto.Image.CopyToAsync(stream);
            }
            return "/images/" + fileName;
        }
    }
}
