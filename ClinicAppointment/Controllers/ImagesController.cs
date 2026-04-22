using ClinicAppointment.Dtos;
using ClinicAppointment.Service.Dto;
using ClinicAppointment.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(IImageService _imageService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]UploadImageDto dto)
        {
            if (dto.Image == null)
            {
                return BadRequest("Image is required");
               
            }
            var imageUrl=await _imageService.UploadImageAsync(dto);
            return Ok(new
            {
                message = "Image uploaded successfuly",
                path=imageUrl
            });

        }
    }
}

