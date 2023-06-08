using BarmenYachting.Application.Logging;
using BarmenYachting.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BarmenYachting.Implementation
{
    public class UseCaseHandler
    {
        private IDbLogger _dbLogger;

        public UseCaseHandler(IDbLogger dbLogger)
        {
            _dbLogger = dbLogger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                
                command.Execute(data);
                
                stopwatch.Stop();

                _dbLogger.Log("Successfully executed command: "+command.Name,"Duration: "+ stopwatch.ElapsedMilliseconds + " ms.");
            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error running command: "+command.Name, ex.Message);
                throw;
            }
        }

        public TResponse HandleQuery<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest data)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var response = query.Execute(data);

                stopwatch.Stop();

                _dbLogger.Log("Successfully executed query: " + query.Name, "Duration: " + stopwatch.ElapsedMilliseconds + " ms.");

                return response;
            }
            catch (Exception ex)
            {
                _dbLogger.Log("Error running query: " + query.Name, ex.Message);
                throw;
            }
        }
    }
}
