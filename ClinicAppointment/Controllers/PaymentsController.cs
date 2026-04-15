using ClinicAppointment.Data.Common.Pagination;
using ClinicAppointment.Data.Enums;
using ClinicAppointment.Dtos;
using ClinicAppointment.Service.Dto;
using ClinicAppointment.Service.Dtos;
using ClinicAppointment.Service.IServices;
using ClinicAppointment.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IPaymentService _paymentService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaymentDtos>> GetAll([FromQuery] PaginationParameter pagination)
        {
            var result = await _paymentService.GetAllAsync(pagination);

            return Ok(result);

        }
        [HttpGet("id")]
        public async Task<ActionResult<PaymentDtos>> GetByIdAsync(Guid id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment is null)
            {
                return NotFound($"Payment with ID {id} not found");
            }
            return Ok(payment);
        }
        [HttpPost]
        public async Task<ActionResult<PaymentDtos>> AddAsync(PaymentDto dto)
        {
            var payment = await _paymentService.AddAsync(dto);
            return Ok(payment);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDtos>> UpdateAsync(Guid id, PaymentDto dto)
        {
            var paymentId = await _paymentService.UpdateAsync(id, dto);
            if (paymentId == Guid.Empty)
            {
                return NotFound($"Payment with ID {id} not found");
            }
            return Ok(paymentId);
        }
        [HttpGet("payment_with_apointmentdetalis/{id}")]
        public async Task<ActionResult<PaymentDtos>> SoftDeleteAsync(Guid id)
        {
            var paymentId = await _paymentService.GetByIdAsync(id);
            if (paymentId == null)
                return NotFound($"Payment with ID {id} not found");

           var result= await _paymentService.GetPaymentWithAppointmentDetails(id);
            return Ok(result);
        }
        [HttpGet("filter_by_status/{id}")]
        public async Task<ActionResult<PaymentDtos>> FilterByStatus(Guid id,PaymentStatus status)
        {
            var paymentId = await _paymentService.GetByIdAsync(id);
            if (paymentId == null)
                return NotFound($"Payment with ID {id} not found");

            var result = await _paymentService.FilterByStatus(id,status);
            return Ok(result);
        }
    }
}
