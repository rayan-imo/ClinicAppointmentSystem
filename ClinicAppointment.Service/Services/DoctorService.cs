using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Dbcontext;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dtos;
using ClinicAppointment.Service.IServices;
using ClinicAppointment.Service.Validators.Doctor;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Service.Services
{
    public class DoctorService(ClinicAppointmentDbcontext _context ):IDoctorService
    {
        public async Task<PagedResult<Doctor>> GetAllAsync(PaginationParameter pagination)
        {
            var doctors = await _context.Doctors.Where(d=>d.DeletedAt==null).ToListAsync();
            return await PaginationHelper.ToPagedAsync(doctors, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<Doctor> GetByIdAsync(Guid id)
        {

            if (id == Guid.Empty)
                throw new ArgumentException("Invalid doctor ID.", nameof(id));

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d=>d.Id==id);

            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with ID '{id}' was not found.");

            if (doctor.DeletedAt is not null)
                throw new InvalidOperationException($"Doctor with ID '{id}' was deleted on {doctor.DeletedAt}.");

            return doctor;
        }
        public async Task<Guid> AddAsync(DoctorDto dto)
        {
            await new DoctorValidator(_context).ValidateAndThrowAsync(dto);
            var doctor = new Doctor
            {
                FullName= dto.FullName,
                Email= dto.Email,
                Specialty= dto.Specialty,
                DepartmentId= dto.DepartmentId
            };
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return doctor.Id;
        }
        public async Task<Guid> UpdateAsync(Guid id, DoctorDto dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid doctor ID.", nameof(id));

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with ID '{id}' was not found.");

            if (doctor.DeletedAt is not null)
                throw new InvalidOperationException($"Doctor with ID '{id}' was deleted on {doctor.DeletedAt}.");

            await new DoctorValidator(_context).ValidateAndThrowAsync(dto);
            doctor.FullName= dto.FullName;
            doctor.Email= dto.Email;
            doctor.Specialty= dto.Specialty;
            doctor.DepartmentId= dto.DepartmentId;
           
             _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return doctor.Id;
        }
        public async Task SoftDeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid doctor ID.", nameof(id));

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with ID '{id}' was not found.");

            if (doctor.DeletedAt is not null)
                throw new InvalidOperationException($"Doctor with ID '{id}' was deleted on {doctor.DeletedAt}.");

            doctor.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        public async Task HardDeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid doctor ID.", nameof(id));

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                throw new KeyNotFoundException($"Doctor with ID '{id}' was not found.");

            _context.Doctors.Remove(doctor); 
            await _context.SaveChangesAsync();
        }
    }
}
