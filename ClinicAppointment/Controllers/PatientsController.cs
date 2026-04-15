using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Dtos;
using ClinicAppointment.Service.Dtos;
using ClinicAppointment.Service.IServices;
using ClinicAppointment.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController(IPatientService _patientService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PatientDtos>> GetAll([FromQuery] PaginationParameter pagination)
        {
            var result = await _patientService.GetAllAsync(pagination);

            return Ok(result);

        }
        [HttpGet("id")]
        public async Task<ActionResult<PatientDtos>> GetByIdAsync(Guid id)
        {
            var patients = await _patientService.GetByIdAsync(id);
            if (patients is null)
            {
                return NotFound($"Patient with ID {id} not found");
            }
            return Ok(patients);
        }
        [HttpPost]
        public async Task<ActionResult<PatientDtos>> AddAsync(PatientDto dto)
        {
            var patient = await _patientService.AddAsync(dto);
            return Ok(patient);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDtos>> UpdateAsync(Guid id, PatientDto dto)
        {
            var patientId = await _patientService.UpdateAsync(id, dto);
            if (patientId == Guid.Empty)
            {
                return NotFound($"Patient with ID {id} not found");
            }
            return Ok(patientId);
        }
        [HttpPut("soft/{id}")]
        public async Task<ActionResult> SoftDeleteAsync(Guid id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound($"Patient with ID {id} not found");

            await _patientService.SoftDeleteAsync(id);
            return Ok();
        }
        [HttpDelete("hard/{id}")]
        public async Task<ActionResult> HardDeleteAsync(Guid id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound($"Doctor with ID {id} not found");

            await _patientService.HardDeleteAsync(id);
            return Ok();
        }
    }

}

