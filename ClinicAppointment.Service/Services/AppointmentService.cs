using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Dbcontext;
using ClinicAppointment.Data.Enums;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dto;
using ClinicAppointment.Service.Dtos;
using ClinicAppointment.Service.IServices;
using ClinicAppointment.Service.Validators.Department;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Service.Services
{
    public class AppointmentService(ClinicAppointmentDbcontext _context,
        IDoctorService _doctorService, IPatientService _patientService) : IAppointmentService
    {
        public async Task<PagedResult<Appointment>> GetAllAsync(PaginationParameter pagination)
        {
            var appointments = await _context.Appointments.Where(d => d.DeletedAt == null).ToListAsync();
            return await PaginationHelper.ToPagedAsync(appointments, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<Appointment> GetByIdAsync(Guid id)
        {

            if (id == Guid.Empty)
                throw new ArgumentException("Invalid appointment ID.", nameof(id));

            var appointment = await _context.Appointments.FirstOrDefaultAsync(d => d.Id == id);

            if (appointment == null)
                throw new KeyNotFoundException($"Appointment with ID '{id}' was not found.");

            if (appointment.DeletedAt is not null)
                throw new InvalidOperationException($"Appointment with ID '{id}' was deleted on {appointment.DeletedAt}.");

            return appointment;
        }
        public async Task<Guid> AddAsync(AppointmentDto dto)
        {
            var doctorId = await _doctorService.GetByIdAsync(dto.DoctorId);
            if (doctorId == null)
            {
                throw new KeyNotFoundException($"Appointment with ID '{doctorId}' was not found.");

            }
            var patientId = await _patientService.GetByIdAsync(dto.PatientId);
            if (patientId == null)
            {
                throw new KeyNotFoundException($"Appointment with ID '{patientId}' was not found.");

            }
            var confilct = await _context.Appointments.AnyAsync(
             a => a.DoctorId == dto.DoctorId &&
             a.AppointmentDate == dto.AppointmentDate &&
             a.Status != AppointmentStatus.Completed
            );
            if (confilct)
            {
                throw new InvalidOperationException("Doctor already has apoointment at this same time");

            }

            //if (dto.AppointmentDate < DateTime.UtcNow)
            //{
            //    throw new InvalidOperationException("Can not bookin the past");
            //}

            var appointment = new Appointment
            {
                AppointmentDate = dto.AppointmentDate,
                Status = dto.Status,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Notes = dto.Notes,
            };
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment.Id;
        }
        public async Task<Guid> UpdateAsync(Guid id, AppointmentDto dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid appointment ID.", nameof(id));

            var appointment = await _context.Appointments.FirstOrDefaultAsync(d => d.Id == id);

            if (appointment == null)
                throw new KeyNotFoundException($"Appointment with ID '{id}' was not found.");

            if (appointment.DeletedAt is not null)
                throw new InvalidOperationException($"Appointment with ID '{id}' was deleted on {appointment.DeletedAt}.");

            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.Notes = dto.Notes;
            appointment.Status = dto.Status;
            appointment.PatientId = dto.PatientId;

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment.Id;
        }
        public async Task SoftDeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid appointment ID.", nameof(id));

            var appointment = await _context.Appointments.FirstOrDefaultAsync(d => d.Id == id);

            if (appointment == null)
                throw new KeyNotFoundException($"Appointment with ID '{id}' was not found.");

            if (appointment.DeletedAt is not null)
                throw new InvalidOperationException($"Appointment with ID '{id}' was deleted on {appointment.DeletedAt}.");

            appointment.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        public async Task HardDeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid appointment ID.", nameof(id));

            var appointment = await _context.Appointments.FirstOrDefaultAsync(d => d.Id == id);

            if (appointment == null)
                throw new KeyNotFoundException($"Appointment with ID '{id}' was not found.");

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<Appointment> GetAppointmentWithDoctortId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid doctor ID.", nameof(id));

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with ID '{id}' was not found.");
            var appointment = await _context.Appointments.Include(x => x.Doctor)
                .FirstOrDefaultAsync(x => x.DoctorId == id);

            return appointment;
        }
        public async Task<Appointment> GetAppointmentWithPatientId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid patient ID.", nameof(id));

            var patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == id);

            if (patient == null)
                throw new KeyNotFoundException($"Doctor with ID '{id}' was not found.");

            var appointment = await _context.Appointments.Include(x => x.Patient)
                .FirstOrDefaultAsync(x=>x.PatientId==id);
            return appointment;

        }
    }
}

