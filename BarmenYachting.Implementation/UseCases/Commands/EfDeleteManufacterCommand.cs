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
    public class EfDeleteManufacterCommand : EfUseCase, IDeleteManufacterCommand
    {
        public EfDeleteManufacterCommand(BarmenYachtingDbContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "Delete manufacter";

        public string Description => "Delete manufacter and all vessels associated with it";

        public void Execute(int request)
        {
            var manufacter = Context.Manufacters.FirstOrDefault(x => x.Id == request && x.IsActive);

            if (manufacter == null)
            {
                throw new EntityNotFoundException(nameof(Manufacter), request);
            }

            var vessels = Context.Vessels.Where(x=>x.Manufacter == manufacter).ToList();

            if (vessels.Any())
            {
                Context.Vessels.RemoveRange(vessels);
            }

            Context.Manufacters.Remove(manufacter);

            Context.SaveChanges();
        }
    }
}
