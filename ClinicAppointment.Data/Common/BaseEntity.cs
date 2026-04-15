namespace ClinicAppointment.Data.Common
{
    public class BaseEntity
    { public Guid Id { get; set; }

     public DateTime? DeletedAt { get; set; }
    }
}
