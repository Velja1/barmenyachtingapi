using BarmenYachting.Application.DTO;
using BarmenYachting.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Application.UseCases.Commands
{
    public interface ICreateVesselCommand : ICommand<CreateVesselDto>
    {
    }
}
