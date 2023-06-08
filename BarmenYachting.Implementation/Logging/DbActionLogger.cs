using BarmenYachting.Application.Logging;
using BarmenYachting.Application.UseCases.DTO;
using BarmenYachting.DataAccess;
using BarmenYachting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.Implementation.Logging
{
    public class DbActionLogger : IDbLogger
    {
        private readonly BarmenYachtingDbContext _context;

        public DbActionLogger(BarmenYachtingDbContext context) 
        {
            _context = context;
        }

        public void Log(string action, string data)
        {
            var log = new Log
            {
                Action = action,
                Data = data,
                ExecutionTime = DateTime.UtcNow
            };

            _context.Logs.Add(log);
            _context.SaveChanges();
        }
    }
}
