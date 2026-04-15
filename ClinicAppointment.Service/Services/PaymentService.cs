using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Dbcontext;
using ClinicAppointment.Data.Enums;
using ClinicAppointment.Data.Models;
using ClinicAppointment.Service.Dto;
using ClinicAppointment.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Service.Services
{
    public class PaymentService(ClinicAppointmentDbcontext _context) : IPaymentService
    {
        public async Task<PagedResult<Payment>> GetAllAsync(PaginationParameter pagination)
        {
            var payments = await _context.Payments.Where(d => d.DeletedAt == null).ToListAsync();
            return await PaginationHelper.ToPagedAsync(payments, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<Payment> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid payment ID.", nameof(id));

            var department = await _context.Payments.FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                throw new KeyNotFoundException($"Payment with ID '{id}' was not found.");

            if (department.DeletedAt is not null)
                throw new InvalidOperationException($"Payment with ID '{id}' was deleted on {department.DeletedAt}.");

            return department;
        }
        public async Task<Guid> AddAsync(PaymentDto dto)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == dto.AppointmentId);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment was not found.");
            }
            if (appointment.Status != AppointmentStatus.Completed)
            {
                throw new KeyNotFoundException($"Appointment  not completed");
            }
            var payment = new Payment
            {
                Amount = dto.Amount,
                PaymentDate = dto.PaymentDate,
                Method = dto.Method,
                Status = dto.Status,
                AppointmentId = dto.AppointmentId

            };
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment.Id;
        }
        public async Task<Guid> UpdateAsync(Guid id, PaymentDto dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid payment ID.", nameof(id));

            var payment = await _context.Payments.FirstOrDefaultAsync(d => d.Id == id);

            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID '{id}' was not found.");

            if (payment.DeletedAt is not null)
                throw new InvalidOperationException($"Payment with ID '{id}' was deleted on {payment.DeletedAt}.");

            payment.Status = dto.Status;
            payment.Method = dto.Method;
            payment.Amount = dto.Amount;
            payment.PaymentDate = dto.PaymentDate;
            payment.AppointmentId = dto.AppointmentId;

            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment.Id;

        }

        public async Task<object> GetPaymentWithAppointmentDetails(Guid id)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(d => d.Id == id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID '{id}' was not found.");

            if (payment.DeletedAt is not null)
                throw new InvalidOperationException($"Payment with ID '{id}' was deleted on {payment.DeletedAt}.");

            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == payment.AppointmentId);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment was not found.");
            }
            var result = await _context.Payments.Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Amount,
                    p.PaymentDate,
                    p.Status,
                    AppointmentDate = p.Appointment.AppointmentDate,
                    DoctorName = p.Appointment.Doctor.FullName,
                    PatientName = p.Appointment.Patient.FullName
                }).FirstOrDefaultAsync();

            return result;
        }
        public async Task<Payment> FilterByStatus(Guid id, PaymentStatus status)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(d => d.Id == id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID '{id}' was not found.");

            if (payment.DeletedAt is not null)
                throw new InvalidOperationException($"Payment with ID '{id}' was deleted on {payment.DeletedAt}.");
            var result = _context.Payments.Where(p => p.Status == status).FirstOrDefault();
            return result;
        }
    }
}


