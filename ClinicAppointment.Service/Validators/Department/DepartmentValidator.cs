using ClinicAppointment.Data.Dbcontext;
using ClinicAppointment.Service.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Service.Validators.Department
{
    public class DepartmentValidator : AbstractValidator<DepartmentDto>
    {
        private ClinicAppointmentDbcontext _context;
        public DepartmentValidator(ClinicAppointmentDbcontext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name isRequired.")
                .Must(IsUnique).WithMessage("Email must be unique");

            RuleFor(x => x.Floor)
                .NotEmpty().WithMessage("Floor is Required.");
        }


        private bool IsUnique(string name)
        {
            var result = _context.Departments.FirstOrDefault(d => d.Name == name && d.DeletedAt == null);
            if (result == null)
                return true;
            return false;
        }

    }
}

