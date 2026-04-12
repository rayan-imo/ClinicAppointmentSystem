using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dtos;

namespace ClinicAppointment.Service.IServices
{
    public interface IPatientService
    {
        public Task<PagedResult<Patient>> GetAllAsync(PaginationParameter pagination);
        public Task<Patient> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(PatientDto dto);
        public Task<Guid> UpdateAsync(Guid id, PatientDto dto);
        public Task SoftDeleteAsync(Guid id);
        public Task HardDeleteAsync(Guid id);
    }
}
