using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Dtos;
using ClinicAppointment.Service.Dtos;
using ClinicAppointment.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController(IDoctorService _doctorService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<DoctorDtos>> GetAll([FromQuery] PaginationParameter pagination)
        {
            var result = await _doctorService.GetAllAsync(pagination);

            return Ok(result);

        }
        [HttpGet("id")]
        public async Task<ActionResult<DoctorDtos>> GetByIdAsync(Guid id)
        {
            var doctors = await _doctorService.GetByIdAsync(id);
            if (doctors is null)
            {
                return NotFound($"User with ID {id} not found");
            }
            return Ok(doctors);
        }
        [HttpPost]
        public async Task<ActionResult<DoctorDtos>> AddAsync(DoctorDto dto)
        {
            var doctor = await _doctorService.AddAsync(dto);
            return Ok(doctor);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDtos>> UpdateAsync(Guid id, DoctorDto dto)
        { 
            var doctorId = await _doctorService.UpdateAsync(id, dto);
            if (doctorId == Guid.Empty)
            {
                return NotFound($"Doctor with ID {id} not found");
            }
            return Ok(doctorId);
        }
        [HttpPut("soft/{id}")]
        public async Task<ActionResult> SoftDeleteAsync(Guid id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
                return NotFound($"Doctor with ID {id} not found");

            await _doctorService.SoftDeleteAsync(id);
            return Ok();
        }
        [HttpDelete("hard/{id}")]
        public async Task<ActionResult> HardDeleteAsync(Guid id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
                return NotFound($"Doctor with ID {id} not found");

            await _doctorService.HardDeleteAsync(id);
            return Ok();
        }
    }

}

