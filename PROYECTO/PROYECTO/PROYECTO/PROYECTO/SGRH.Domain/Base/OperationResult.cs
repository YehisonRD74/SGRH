using SGRH._Domain.Entites;

namespace SGRH._Domain.Base;

public class OperationResult<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public bool IsDisable { get; set; }

    public static OperationResult<T> Success(T data, string message = "", string updatedBy = "", DateTime? updatedAt = null)
    {
        return new OperationResult<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt ?? DateTime.UtcNow
        };
    }

    public static OperationResult<T> Failure(string message, string updatedBy = "", DateTime? updatedAt = null)
    {
        return new OperationResult<T>
        {
            IsSuccess = false,
            Message = message,
            Data = default,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt ?? DateTime.UtcNow
        };
    }

   
}