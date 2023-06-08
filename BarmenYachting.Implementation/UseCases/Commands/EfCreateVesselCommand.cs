using BarmenYachting.Application.DTO;
using BarmenYachting.Application.UseCases.Commands;
using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.DataAccess;
using BarmenYachting.Domain;
using BarmenYachting.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Implementation.UseCases.Commands
{
    public class EfCreateVesselCommand : EfUseCase, ICreateVesselCommand
    {
        private CreateVesselValidator   _validator;
        public EfCreateVesselCommand(BarmenYachtingDbContext context, CreateVesselValidator validator) : base(context) {
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Create Vessel";

        public string Description => "Create Vessel using entity framework.";

        public void Execute(CreateVesselDto request)
        {
            _validator.ValidateAndThrow(request);

            var vessel = new Vessel
            {
                Model = request.Model,
                Price = request.Price,
                Width = request.Width,
                Height = request.Height,
                Length = request.Length,
                ManufacterId = request.ManufacterId,
                TypeId = request.TypeId
            };

            Context.Vessels.Add(vessel);

            Context.SaveChanges();
        }
    }
}
