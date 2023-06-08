using BarmenYachting.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Application.Logging
{
    public interface IDbLogger
    {
        void Log(string action, string data);
    }
}
