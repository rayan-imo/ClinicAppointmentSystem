using Microsoft.AspNetCore.Http;

namespace ClinicAppointment.Service.Dto
{
    public class UploadImageDto
    {
        public IFormFile Image { get; set; }
    }
}
