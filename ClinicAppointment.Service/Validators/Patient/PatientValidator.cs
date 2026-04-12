

using ClinicAppointment.Data.Dbcontext;
using ClinicAppointment.Service.Dtos;
using FluentValidation;

namespace ClinicAppointment.Service.Validators.Patient
{
    public class PatientValidator : AbstractValidator<PatientDto>
    {
        private ClinicAppointmentDbcontext _context;
        public PatientValidator(ClinicAppointmentDbcontext context)
        {
            _context = context;

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Name isRequired.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("DateOfBirth isRequired.") ;

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number isRequired.")
                .Must(BeUniquePhone).WithMessage("Phone number must be unique");

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Please entre your email")
                 .EmailAddress().WithMessage("Invalid email format. Please use format: name@example.com")
                 .Must(BeUniqueEmail).WithMessage("Email must be unique");

        }


        private bool BeUniquePhone(string phone)
        {
            var result = _context.Patients.FirstOrDefault(d => d.Phone == phone && d.DeletedAt == null);
            if (result == null)
                return true;
            return false;
        }
        private bool BeUniqueEmail(string email)
        {
            var result = _context.Patients.FirstOrDefault(d => d.Email == email && d.DeletedAt == null);
            if (result == null)
                return true;
            return false;
        }
    }
}
