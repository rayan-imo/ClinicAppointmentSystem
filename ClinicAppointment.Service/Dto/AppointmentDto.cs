using ClinicAppointment.Data.Enums;
using ClinicAppointment.Data.Models;

namespace ClinicAppointment.Service.Dto
{
    public class AppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
    }

}
