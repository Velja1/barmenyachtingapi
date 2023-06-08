using BarmenYachting.Application.DTO;
using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.Application.UseCases.DTO.Searches;
using BarmenYachting.Application.UseCases.Queries;
using BarmenYachting.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Implementation.UseCases.Queries
{
    public class EfGetVesselQuery : EfUseCase, IGetVesselQuery
    {
        public EfGetVesselQuery(BarmenYachtingDbContext context) : base(context) { }

        public int Id => 12;

        public string Name => "Get Vessel";

        public string Description => "Get Vessel using Entity Framework.";

        public VesselDto Execute(int id)
        {
            var vessel = Context.Vessels.Include(x => x.Manufacter).Include(x => x.Type).FirstOrDefault(x => x.Id == id && x.IsActive);

            if (vessel == null)
            {
                return null;
            }

            var obj = new VesselDto
            {
                ManufacterName = vessel.Manufacter.Name,
                Type = vessel.Type.Name,
                Model = vessel.Model,
                Price = vessel.Price,
                Width = vessel.Width,
                Height = vessel.Height,
                Length = vessel.Length
            };

            return obj;
        }
    }
}
