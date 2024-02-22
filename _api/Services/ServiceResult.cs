namespace api_sylvainbreton.Services
{
    using api_sylvainbreton.Services.Interfaces;

    public class ServiceResult<T> : IServiceResult<T>
    {
        public bool Success { get; }
        public T Data { get; }
        public string ErrorMessage { get; }
        public int StatusCode { get; }

        // Constructor for a successful result
        public ServiceResult(T data)
        {
            Success = true;
            Data = data;
            ErrorMessage = string.Empty;
            StatusCode = 200; // OK
        }

        // Constructor for a failed result
        public ServiceResult(string errorMessage, int statusCode = 500)
        {
            Success = false;
            Data = default;
            ErrorMessage = errorMessage;
            StatusCode = statusCode; // Default to 500 Internal Server Error
        }

        // New constructor that accepts all four parameters
        public ServiceResult(bool success, T data, string errorMessage, int statusCode)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}
