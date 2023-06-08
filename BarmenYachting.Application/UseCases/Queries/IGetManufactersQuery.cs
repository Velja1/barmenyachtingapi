using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Application.UseCases.Queries
{
    public interface IGetManufactersQuery : IQuery<BasePagedSearch, PagedResponse<ManufacterDto>>
    {
    }
}
