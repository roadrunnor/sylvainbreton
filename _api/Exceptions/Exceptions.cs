namespace api_sylvainbreton.Exceptions
{
    public class Exceptions
    {
        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message) { }
        }

        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

        public class InternalServerErrorException : Exception
        {
            public InternalServerErrorException(string message) : base(message) { }
        }
    }
}
