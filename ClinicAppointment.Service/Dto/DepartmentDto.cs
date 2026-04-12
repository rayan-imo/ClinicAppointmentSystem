using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Service.Dtos
{
    public class DepartmentDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Floor { get; set; }
        public string? Description { get; set; }
    }
}
