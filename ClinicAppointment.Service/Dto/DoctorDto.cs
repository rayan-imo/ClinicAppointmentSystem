using ClinicAppointment.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Service.Dtos
{
    public class DoctorDto
    {
        public string FullName { get; set; }
        public string? Specialty { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
