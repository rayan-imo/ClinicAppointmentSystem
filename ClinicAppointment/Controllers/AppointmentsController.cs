using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Dtos;
using ClinicAppointment.Service.Dto;
using ClinicAppointment.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController(IAppointmentService _appointmentService
        ,IPatientService _patientService,IDoctorService _doctorService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<AppointmentDtos>> GetAll([FromQuery] PaginationParameter pagination)
        {
            var result = await _appointmentService.GetAllAsync(pagination);

            return Ok(result);

        }
        [HttpGet("id")]
        public async Task<ActionResult<AppointmentDtos>> GetByIdAsync(Guid id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment is null)
            {
                return NotFound($"Appointment with ID {id} not found");
            }
            return Ok(appointment);
        }
        [HttpPost]
        public async Task<ActionResult<AppointmentDtos>> AddAsync(AppointmentDto dto)
        {
            var appointment = await _appointmentService.AddAsync(dto);
            return Ok(appointment);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentDtos>> UpdateAsync(Guid id, AppointmentDto dto)
        {
            var appointmentId = await _appointmentService.UpdateAsync(id, dto);
            if (appointmentId == Guid.Empty)
            {
                return NotFound($"Doctor with ID {id} not found");
            }
            return Ok(appointmentId);
        }
        [HttpPut("soft/{id}")]
        public async Task<ActionResult> SoftDeleteAsync(Guid id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
                return NotFound($"Appointment with ID {id} not found");

            await _appointmentService.SoftDeleteAsync(id);
            return Ok();
        }
        [HttpDelete("hard/{id}")]
        public async Task<ActionResult> HardDeleteAsync(Guid id)
        {
            var appointmentId = await _appointmentService.GetByIdAsync(id);
            if (appointmentId == null)
                return NotFound($"Appointment with ID {id} not found");

            await _appointmentService.HardDeleteAsync(id);
            return Ok();
        }
        [HttpGet("patient/{id}")]
        public async Task<ActionResult<AppointmentDtos>>GetAppointmentWithPatientId(Guid id)
        {
            var patientId = await _patientService.GetByIdAsync(id);
            if (patientId == null)
                return NotFound($"Patient with ID {patientId} not found");

           var result= await _appointmentService.GetAppointmentWithPatientId(id);
            return Ok(result);
        }
        [HttpGet("doctor/{id}")]
        public async Task<ActionResult<AppointmentDtos>> GetAppointmentWithDoctorId(Guid id)
        {
            var doctorId = await _doctorService.GetByIdAsync(id);
            if (doctorId == null)
                return NotFound($"Doctor with ID {doctorId} not found");

            var result = await _appointmentService.GetAppointmentWithDoctorId(id);
            return Ok(result);

        }

    }
} 
