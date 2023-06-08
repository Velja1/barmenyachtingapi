using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.DataAccess;
using FluentValidation;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterDto>
    {
        public RegisterUserValidator(BarmenYachtingDbContext _context)
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required parameter.")
                .EmailAddress().WithMessage("Email format is invalid.")
                .Must(x => !_context.Users.Any(u => u.Email == x)).WithMessage("Email address {PropertyValue} is already in use.");

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required parameter.")
                .MinimumLength(3).WithMessage("Minimum number of characters for username is 3.")
                .MaximumLength(12).WithMessage("Maximum number of characters for username 12.")
                .Matches("^(?=[a-zA-Z0-9._]{3,12}$)(?!.*[_.]{2})[^_.].*[^_.]$")
                .WithMessage("Username format is invalid.");

            var imePrezimeRegex = @"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$";
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required parameter.")
                .Matches(imePrezimeRegex).WithMessage("Name format is invalid.");

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Lastname is required parameter")
                .Matches(imePrezimeRegex).WithMessage("Lastname format is invalid");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required paremeter")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage("Password must contain minimum 8 characters, one uppercase, and one lowercase letter, number and special character");
        }
    }
}
