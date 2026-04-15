using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dto;

namespace ClinicAppointment.Service.IServices
{
    public interface IAppointmentService
    {
        public Task<PagedResult<Appointment>> GetAllAsync(PaginationParameter pagination);
        public Task<Appointment> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(AppointmentDto dto);
        public Task<Guid> UpdateAsync(Guid id, AppointmentDto dto);
        public Task SoftDeleteAsync(Guid id);
        public Task HardDeleteAsync(Guid id);
        public Task<Appointment> GetAppointmentWithPatientId(Guid id);
        public Task<Appointment> GetAppointmentWithDoctorId(Guid id);
    }
}
