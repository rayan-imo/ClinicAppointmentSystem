using ClinicAppointment.Data.Common;
using ClinicAppointment.Data.Enums;

namespace ClinicAppointment.Data.Models
{
    public class Appointment:BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Notes { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
