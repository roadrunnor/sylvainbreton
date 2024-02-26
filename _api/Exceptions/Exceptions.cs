// Filename: Exceptions.cs
namespace api_sylvainbreton.Exceptions
{
    public class BadRequestException(string message) : Exception(message)
    {
    }

    public class NotFoundException(string message) : Exception(message)
    {
    }

    public class InternalServerErrorException(string message) : Exception(message)
    {
    }
}
