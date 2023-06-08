using BarmenYachting.Implementation.Validators;
using BarmenYachting.Application.Emails;
using BarmenYachting.Application.UseCases.Commands;
using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.DataAccess;
using BarmenYachting.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarmenYachting.Implementation.UseCases;

namespace BarmenYachting.Implementation.UseCases.Commands
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        private readonly IEmailSender _emailSender;

        public EfRegisterUserCommand(BarmenYachtingDbContext context, RegisterUserValidator validator, IEmailSender emailSender) : base(context)
        {
            _validator = validator;
            _emailSender = emailSender;
        }

        public void Execute(RegisterDto request)
        {
            _validator.ValidateAndThrow(request);

            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FirstName   = request.FirstName,
                LastName    = request.LastName,
                Username    = request.Username,
                Email       = request.Email,
                Password    = hash,
                RoleId      = Context.Roles.First(x => x.Name == "user").Id
            };

            Context.Users.Add(user);
            Context.SaveChanges();

            _emailSender.Send(new MessageDto
            {
                From    = "BarmenYachting",
                To      = request.Email,
                Title   = "Uspesna registracija",
                Body    = "Uspesno ste se registrovali"
            });
        }

        public int Id => 4;

        public string Name => "User registration";

        public string Description => "User registration command which creates new User and send confirmation email";
    }
}
