using BarmenYachting.Application.UseCases.Commands;
using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.DataAccess;
using BarmenYachting.Domain;
using BarmenYachting.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Implementation.UseCases.Commands
{
    public class EfCreateManufacterCommand : EfUseCase, ICreateManufacterCommand
    {
        private CreateManufacterValidator   _validator;
        public EfCreateManufacterCommand(BarmenYachtingDbContext context, CreateManufacterValidator validator) : base(context) {
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Create Manufacter";

        public string Description => "Create manufacter using entity framework.";

        public void Execute(CreateManufacterDto request)
        {
            _validator.ValidateAndThrow(request);

            var manufacter = new Manufacter
            {
                Name = request.Name
            };

            Context.Manufacters.Add(manufacter);

            Context.SaveChanges();
        }
    }
}
