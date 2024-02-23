using api_sylvainbreton.Services.Interfaces;

namespace api_sylvainbreton.Services
{
    public class ServiceResult<T> : IServiceResult<T>
    {
        public bool Success { get; private set; }
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; }
        public int StatusCode { get; private set; }
        public PaginationDetails Pagination { get; private set; }

        // Success result with data and optional pagination
        public ServiceResult(T data, PaginationDetails pagination = null)
        {
            Success = true;
            Data = data;
            Pagination = pagination;
            ErrorMessage = string.Empty;
            StatusCode = 200; // OK
        }

        // Failure result with error message and status code
        public ServiceResult(string errorMessage, int statusCode = 500)
        {
            Success = false;
            Data = default;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        // Constructor to handle failure without data
        public ServiceResult(bool success, T data, string errorMessage, int statusCode)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
            Pagination = null;
        }

    }
}
