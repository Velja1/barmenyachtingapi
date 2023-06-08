using BarmenYachting.Application.Exceptions;
using BarmenYachting.Application.UseCases.Commands;
using BarmenYachting.DataAccess;
using BarmenYachting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Implementation.UseCases.Commands
{
    public class EfDeleteVesselCommand : EfUseCase, IDeleteVesselCommand
    {
        public EfDeleteVesselCommand(BarmenYachtingDbContext context) : base(context)
        {
        }

        public int Id => 14;

        public string Name => "Delete Vessel";

        public string Description => "Delete Vessel";

        public void Execute(int request)
        {
            var vessel = Context.Vessels.FirstOrDefault(x => x.Id == request && x.IsActive);

            if (vessel == null)
            {
                throw new EntityNotFoundException(nameof(Vessel), request);
            }

            Context.Vessels.Remove(vessel);

            Context.SaveChanges();
        }
    }
}
