namespace api_sylvainbreton.Services.Interfaces
{
    public interface IServiceResult
    {
        bool Success { get; }
        string Message { get; }
        object Data { get; }
    }
}
