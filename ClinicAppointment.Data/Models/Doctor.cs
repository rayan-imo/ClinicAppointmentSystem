namespace ClinicAppointment.Data.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
