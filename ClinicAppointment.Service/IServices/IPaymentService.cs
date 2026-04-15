using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Enums;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dto;
using ClinicAppointment.Service.Dtos;

namespace ClinicAppointment.Service.IServices
{
    public interface IPaymentService
    {
        public Task<PagedResult<Payment>> GetAllAsync(PaginationParameter pagination);
        public Task<Payment> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(PaymentDto dto);
        public Task<Guid> UpdateAsync(Guid id, PaymentDto dto);
        public Task<object> GetPaymentWithAppointmentDetails(Guid id);
        public Task<Payment> FilterByStatus(Guid id, PaymentStatus Status);

    }
}
