using ClinicAppointment.Data.Enums;

namespace ClinicAppointment.Service.Dto
{
    public class PaymentDto
    {
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public AmountMethodType Method { get; set; }
        public PaymentStatus Status { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
