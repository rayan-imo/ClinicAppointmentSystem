using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Data.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
