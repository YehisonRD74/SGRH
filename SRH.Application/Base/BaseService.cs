using Microsoft.Extensions.Logging;
using SGRH._Domain.Base;

public abstract class BaseService<T>
{
    protected readonly ILogger<T> _logger;

    protected BaseService(ILogger<T> logger)
    {
        _logger = logger;
    }

    protected void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    protected void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);
    }

    protected void LogError(Exception ex, string message, params object[] args)
    {
        _logger.LogError(ex, message, args);
    }
    //
    // protected async Task<OperationResult> TryCatchAsync(Func<Task<OperationResult>> action, string actionName)
    // {
    //     try
    //     {
    //         LogInformation("{Action} iniciado", actionName);
    //         return await action();
    //     }
    //     catch (Exception ex)
    //     {
    //         LogError(ex, "Error en {Action}", actionName);
    //         return OperationResult.Failure($"Error en {actionName}: {ex.Message}");
    //     }
    // }

    protected BaseService()
    {
    }
}