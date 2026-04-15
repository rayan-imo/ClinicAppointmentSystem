using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Dbcontext;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dtos;
using ClinicAppointment.Service.IServices;
using ClinicAppointment.Service.Validators.Department;
using ClinicAppointment.Service.Validators.Doctor;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Service.Services
{
    public class DepartmentService(ClinicAppointmentDbcontext _context):IDepartmentService
    {
        public async Task<PagedResult<Department>> GetAllAsync(PaginationParameter pagination)
        {
            var departments = await _context.Departments.Where(d => d.DeletedAt == null).ToListAsync();
            return await PaginationHelper.ToPagedAsync(departments, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<Department> GetByIdAsync(Guid id)
        {

            if (id == Guid.Empty)
                throw new ArgumentException("Invalid department ID.", nameof(id));

            var payments = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (payments == null)
                throw new KeyNotFoundException($"Payment with ID '{id}' was not found.");

            if (payments.DeletedAt is not null)
                throw new InvalidOperationException($"Payment with ID '{id}' was deleted on {payments.DeletedAt}.");

            return payments;
        }
        public async Task<Guid> AddAsync(DepartmentDto dto)
        {
            await new DepartmentValidator(_context).ValidateAndThrowAsync(dto);
            var department = new Department
            {
               Name=dto.Name,
               Floor=dto.Floor,
               Description=dto.Description

            };
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department.Id;
        }
        public async Task<Guid> UpdateAsync(Guid id, DepartmentDto dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid department ID.", nameof(id));

            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID '{id}' was not found.");

            if (department.DeletedAt is not null)
                throw new InvalidOperationException($"Department with ID '{id}' was deleted on {department.DeletedAt}.");

            await new DepartmentValidator(_context).ValidateAndThrowAsync(dto);
            department.Name= dto.Name;
            department.Floor= dto.Floor;
            department.Description= dto.Description;

            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department.Id;
        }
        public async Task SoftDeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid department ID.", nameof(id));

            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID '{id}' was not found.");

            if (department.DeletedAt is not null)
                throw new InvalidOperationException($"Department with ID '{id}' was deleted on {department.DeletedAt}.");

            department.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        public async Task HardDeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid department ID.", nameof(id));

            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID '{id}' was not found.");

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }
}

