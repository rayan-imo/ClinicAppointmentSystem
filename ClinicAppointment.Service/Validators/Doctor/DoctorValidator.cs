using ClinicAppointment.Data.Dbcontext;
using ClinicAppointment.Service.Dtos;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace ClinicAppointment.Service.Validators.Doctor
{
    public class DoctorValidator : AbstractValidator<DoctorDto>
    {
        private ClinicAppointmentDbcontext _context;
        public DoctorValidator(ClinicAppointmentDbcontext context)
        {
            _context = context;

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Name isRequired.");

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Please entre your email")
                 .EmailAddress().WithMessage("Invalid email format. Please use format: name@example.com")
                 .Must(IsUnique).WithMessage("Email must be unique");

        }


        private bool IsUnique(string email)
        {
            var result = _context.Doctors.FirstOrDefault(d => d.Email == email && d.DeletedAt == null);
            if (result == null)
                return true;
            return false;
        }

    }
}

