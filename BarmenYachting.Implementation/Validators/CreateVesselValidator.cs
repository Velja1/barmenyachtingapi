using BarmenYachting.Application.DTO;
using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Implementation.Validators
{
    public class CreateVesselValidator : AbstractValidator<CreateVesselDto>
    {
        public CreateVesselValidator(BarmenYachtingDbContext _context)
        {
            RuleFor(x => x.Model)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Model is required parameter.")
                .MinimumLength(3).WithMessage("Minimum number of characters is 3.")
                .Must(x => !_context.Vessels.Any(m => m.Model == x)).WithMessage("Model: {PropertyValue} already exists.");

            RuleFor(x => x.Price)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Price is required parameter")
                .GreaterThan(0).WithMessage("Price must not be less than or equal to 0.");

            RuleFor(x => x.Width)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Width is required parameter")
                .GreaterThan(0).WithMessage("Width must not be less than or equal to 0.");

            RuleFor(x => x.Height)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Height is required parameter")
                .GreaterThan(0).WithMessage("Height must not be less than or equal to 0.");

            RuleFor(x => x.Length)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Length is required parameter")
                .GreaterThan(0).WithMessage("Length must not be less than or equal to 0.");

            RuleFor(x => x.ManufacterId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("ManufacterId is required parameter")
                .Must(x => _context.Manufacters.Any(u => u.Id == x)).WithMessage("Manufacter with ID: {PropertyValue} not exists.");

            RuleFor(x => x.TypeId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("TypeId is required parameter")
                .Must(x => _context.Types.Any(u => u.Id == x)).WithMessage("Type with ID: {PropertyValue} not exists.");
        }
    }
}
