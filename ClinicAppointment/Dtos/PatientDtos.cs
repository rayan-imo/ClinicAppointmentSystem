using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Dtos
{
    public class PatientDtos
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
