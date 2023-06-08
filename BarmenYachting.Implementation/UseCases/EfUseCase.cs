using BarmenYachting.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected EfUseCase(BarmenYachtingDbContext context)
        {
            Context = context;
        }

        protected BarmenYachtingDbContext Context { get; }
    }
}
