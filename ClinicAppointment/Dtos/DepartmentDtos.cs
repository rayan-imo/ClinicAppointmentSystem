using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Dtos
{
    public class DepartmentDtos
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Floor { get; set; }
        public string? Description { get; set; }
    }
}
