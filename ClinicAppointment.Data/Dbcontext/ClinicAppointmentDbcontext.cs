using ClinicAppointment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Data.Dbcontext
{
    public class ClinicAppointmentDbcontext : DbContext
    {
        public ClinicAppointmentDbcontext(DbContextOptions<ClinicAppointmentDbcontext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                  .HasIndex(d => d.Email).IsUnique();

            modelBuilder.Entity<Patient>()
                 .HasIndex(p=>p.Phone).IsUnique();

            modelBuilder.Entity<Department>()
            .HasIndex(d=>d.Name).IsUnique();


        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }

    }
}
