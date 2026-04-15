using ClinicAppointment.Data.Common;
using ClinicAppointment.Data.Enums;

namespace ClinicAppointment.Data.Models
{
    public class Payment :BaseEntity
    {
        public DateTime PaymentDate { get; set; }
        public decimal Amount {  get; set; }
        public AmountMethodType Method {  get; set; }
        public PaymentStatus Status { get; set; }
        public Guid AppointmentId {  get; set; }
        public Appointment Appointment { get; set; }

    }
}
