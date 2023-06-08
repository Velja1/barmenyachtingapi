using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.Application.UseCases.DTO.Searches;
using BarmenYachting.Application.UseCases.Queries;
using BarmenYachting.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Implementation.UseCases.Queries
{
    public class EfGetManufactersQuery : EfUseCase, IGetManufactersQuery
    {
        public EfGetManufactersQuery(BarmenYachtingDbContext context) : base(context) { }

        public int Id => 1;

        public string Name => "Search Manufacters";

        public string Description => "Search manufacters using Entity Framework.";



        public PagedResponse<ManufacterDto> Execute(BasePagedSearch search)
        {
            var query = Context.Manufacters.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
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

            var response = new PagedResponse<ManufacterDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new ManufacterDto
            {
                Id = x.Id,
                Name = x.Name,
                NumberOfVessels = x.Vessels.Count()
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
