using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Application.UseCases.DTO
{
    public class ManufacterDto : BaseDto
    {
        public string Name { get; set; }
        public int NumberOfVessels { get; set; }
    }

    public class CreateManufacterDto
    {
        public string Name { get; set; }
    }
}
