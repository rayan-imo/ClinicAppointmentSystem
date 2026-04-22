using ClinicAppointment.Service.Dto;
using Microsoft.AspNetCore.Http;

namespace ClinicAppointment.Service.IServices
{
    public interface  IImageService
    {
      public Task<string> UploadImageAsync(UploadImageDto dto);
    }
}
