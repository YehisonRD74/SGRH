using SGRH._Domain.Entities;

namespace SGRH._Domain.Base
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public dynamic? Data { get; set; }

        public OperationResult()
        {
        }

        public OperationResult(bool isSuccess, string message, dynamic? data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        
        public static OperationResult Success(dynamic? data, string message = null)
        {
            return new OperationResult(true, message, data);
        }

        public static OperationResult Failure(string errorMessage)
        {
            return new OperationResult(false, errorMessage, null);
        }
    }
}