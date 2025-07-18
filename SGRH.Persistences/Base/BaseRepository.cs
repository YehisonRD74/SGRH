using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;

namespace SGRH.Persistences.Base
{
    public abstract class BaseRepository<T>
    {
        protected readonly ILogger<T>? logger;

        protected BaseRepository(ILogger<T>? logger)
        {
            this.logger = logger;
        }

        protected BaseRepository() {}

        protected void LogInformation(string? message, params object[] args)
        {
            logger?.LogInformation(message ?? string.Empty, args);
        }

        protected void LogError(Exception? exception, string? message, params object[] args)
        {
            logger?.LogError(exception ?? new Exception("Error desconocido"), message ?? "Error", args);
        }

        protected async Task<OperationResult<TResult>> TryCatchAsync<TResult>(Func<Task<OperationResult<TResult>>> action, string actionName)
        {
            try
            {
                LogInformation("{Action} iniciado", actionName);
                return await action();
            }
            catch (Exception? ex)
            {
                LogError(ex, "Error en {Action}", actionName);
                return OperationResult<TResult>.Failure($"Error en {actionName}: {ex?.Message}");
            }
        }

        protected OperationResult<TResult> TryCatch<TResult>(Func<OperationResult<TResult>> action, string actionName)
        {
            try
            {
                LogInformation("{Action} iniciado", actionName);
                return action();
            }
            catch (Exception? ex)
            {
                LogError(ex, "Error en {Action}", actionName);
                return OperationResult<TResult>.Failure($"Error en {actionName}: {ex?.Message}");
            }
        }
    }
}