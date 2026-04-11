using ClinicAppointment.Data.Enums;

namespace ClinicAppointment.Data.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount {  get; set; }
        public AmountMethod Method {  get; set; }
        public PaymentStatus Status { get; set; }
        public Guid AppointmentId {  get; set; }
        public Appointment Appointment { get; set; }

    }
}
