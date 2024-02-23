namespace api_sylvainbreton.Services.Interfaces
{
    public interface IServiceResult<T>
    {
        bool Success { get; }
        T Data { get; }
        string ErrorMessage { get; }
        int StatusCode { get; }
        PaginationDetails Pagination { get; }
    }

}
