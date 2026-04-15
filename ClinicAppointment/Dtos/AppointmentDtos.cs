using ClinicAppointment.Data.Enums;

namespace ClinicAppointment.Dtos
{
    public class AppointmentDtos
    {
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
    }
}
