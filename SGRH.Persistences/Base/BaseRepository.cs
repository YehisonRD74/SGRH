using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SGRH.Persistences.Base
{
   
    public abstract class BaseRepository<T>
    {
        protected readonly ILogger<T> _logger;

        protected BaseRepository(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        protected void LogError(Exception exception, string message, params object[] args)
        {
            _logger.LogError(exception, message, args);
        }
        
    }
}