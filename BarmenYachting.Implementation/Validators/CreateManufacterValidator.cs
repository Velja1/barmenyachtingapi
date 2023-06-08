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
    public class CreateManufacterValidator : AbstractValidator<CreateManufacterDto>
    {
        public CreateManufacterValidator(BarmenYachtingDbContext _context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required parameter.")
                .MinimumLength(3).WithMessage("Minimum number of characters is 3.")
                .Must(x => !_context.Manufacters.Any(m => m.Name == x)).WithMessage("Manufacter: {PropertyValue} already exists.");
        }
    }
}
