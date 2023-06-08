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
    public class EfGetVesselsQuery : EfUseCase, IGetVesselsQuery
    {
        public EfGetVesselsQuery(BarmenYachtingDbContext context) : base(context) { }

        public int Id => 11;

        public string Name => "Search Vessels";

        public string Description => "Search vessels using Entity Framework.";

        public PagedResponse<VesselDto> Execute(BasePagedSearch search)
        {
            var vesselsQuery = Context.Vessels.Where(x => x.IsActive);

            var kw = search.Keyword;


            if (!string.IsNullOrEmpty(kw))
            {
                vesselsQuery = vesselsQuery.Where(x => x.Model.Contains(kw));
            }


            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.PerPage = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<VesselDto>();
            response.TotalCount = vesselsQuery.Count();
            response.Data = vesselsQuery.Skip(toSkip).Take(search.PerPage.Value).Select(x => new VesselDto
            {
                ManufacterName = x.Manufacter.Name,
                Type = x.Type.Name,
                Model = x.Model,
                Price = x.Price,
                Width = x.Width,
                Height = x.Height,
                Length = x.Length
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
