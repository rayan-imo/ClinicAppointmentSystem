using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dtos;

namespace ClinicAppointment.Service.IServices
{
    public interface IDoctorService
    {
        public Task<PagedResult<Doctor>> GetAllAsync(PaginationParameter pagination);
        public Task<Doctor> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(DoctorDto dto);
        public Task<Guid> UpdateAsync(Guid id, DoctorDto dto);
        public Task SoftDeleteAsync(Guid id);
        public Task HardDeleteAsync(Guid id);
    }
}
