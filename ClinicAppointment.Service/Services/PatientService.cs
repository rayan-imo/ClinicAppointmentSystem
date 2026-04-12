using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Dbcontext;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dtos;
using ClinicAppointment.Service.IServices;
using ClinicAppointment.Service.Validators.Doctor;
using ClinicAppointment.Service.Validators.Patient;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Service.Services
{
    public class PatientService(ClinicAppointmentDbcontext _context):IPatientService
    {
        public async Task<PagedResult<Patient>> GetAllAsync(PaginationParameter pagination)
        {
            var patients = await _context.Patients.Where(d => d.DeletedAt == null).ToListAsync();
            return await PaginationHelper.ToPagedAsync(patients, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<Patient> GetByIdAsync(Guid id)
        {

            if (id == Guid.Empty)
                throw new ArgumentException("Invalid patient ID.", nameof(id));

            var patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == id);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID '{id}' was not found.");

            if (patient.DeletedAt is not null)
                throw new InvalidOperationException($"Patient with ID '{id}' was deleted on {patient.DeletedAt}.");

            return patient;
        }
        public async Task<Guid> AddAsync(PatientDto dto)
        {
            await new PatientValidator(_context).ValidateAndThrowAsync(dto);
            var patient = new Patient
            {
                FullName = dto.FullName,
                Email = dto.Email,
                DateOfBirth= dto.DateOfBirth,
                Phone=dto.Phone,
            };
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient.Id;
        }
        public async Task<Guid> UpdateAsync(Guid id, PatientDto dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid patient ID.", nameof(id));

            var patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == id);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID '{id}' was not found.");

            if (patient.DeletedAt is not null)
                throw new InvalidOperationException($"Patient with ID '{id}' was deleted on {patient.DeletedAt}.");

            await new PatientValidator(_context).ValidateAndThrowAsync(dto);
            patient.FullName = dto.FullName;
            patient.Email = dto.Email;
            patient.DateOfBirth= dto.DateOfBirth;
            patient.Phone = dto.Phone;

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return patient.Id;
        }
        public async Task SoftDeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid patient ID.", nameof(id));

            var patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == id);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID '{id}' was not found.");

            if (patient.DeletedAt is not null)
                throw new InvalidOperationException($"Patient with ID '{id}' was deleted on {patient.DeletedAt}.");

            patient.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        public async Task HardDeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid patient ID.", nameof(id));

            var patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == id);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with ID '{id}' was not found.");

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}

