using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dtos;

namespace ClinicAppointment.Service.IServices
{
    public interface IDepartmentService
    {
        public Task<PagedResult<Department>> GetAllAsync(PaginationParameter pagination);
        public Task<Department> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(DepartmentDto dto);
        public Task<Guid> UpdateAsync(Guid id, DepartmentDto dto);
        public Task SoftDeleteAsync(Guid id);
        public Task HardDeleteAsync(Guid id);
    }
}
