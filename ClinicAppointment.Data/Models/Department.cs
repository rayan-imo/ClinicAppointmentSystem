using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Data.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Floor { get; set; }
        public string Description { get; set; }
        public List<Doctor> Doctors { get; set; }

    }
}
