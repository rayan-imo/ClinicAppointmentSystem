using ClinicAppointment.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Dtos
{
    public class PaymentDtos
    {
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        [EnumDataType(typeof(AmountMethodType))]
        public AmountMethodType Method { get; set; }
        [EnumDataType(typeof(PaymentStatus))]
        public PaymentStatus Status { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
