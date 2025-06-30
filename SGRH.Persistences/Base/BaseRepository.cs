using SGRH._Domain.Base; 
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SGRH.Persistences.Base
{
    public abstract class BaseRepository<T>
    {
        protected readonly ILogger<T> logger;

        protected BaseRepository(ILogger<T> logger)
        {
            logger = logger;
        }

        protected BaseRepository()
        {
            throw new NotImplementedException();
        }

        protected void LogInformation(string message, params object[] args)
        {
            logger.LogInformation(message, args);
        }

        protected void LogError(Exception exception, string message, params object[] args)
        {
            logger.LogError(exception, message, args);
        }

        protected async Task<OperationResult> TryCatchAsync(Func<Task<OperationResult>> action, string actionName)
        {
            try
            {
                LogInformation("{Action} iniciado", actionName);
                return await action();
            }
            catch (Exception ex)
            {
                LogError(ex, "Error en {Action}", actionName);
                return OperationResult.Failure($"Error en {actionName}");
            }
        }

        protected OperationResult TryCatch(Func<OperationResult> action, string actionName)
        {
            try
            {
                LogInformation("{Action} iniciado", actionName);
                return action();
            }
            catch (Exception ex)
            {
                LogError(ex, "Error en {Action}", actionName);
                return OperationResult.Failure($"Error en {actionName}");
            }
        }
    }
}