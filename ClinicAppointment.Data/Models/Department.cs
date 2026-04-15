using ClinicAppointment.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Data.Models
{
    public class Department :BaseEntity 
    {
        [Required]
        public string Name { get; set; }
        public string Floor { get; set; }
        public string? Description { get; set; }
        public List<Doctor> Doctors { get; set; }

    }
}
