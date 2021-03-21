namespace Bakery.Services.Application.Models
{
    public class Exceptions
    {
        public static string UnauthorizedException => "Unauthorized";
        public static string NotFoundException => "Not Found";
    }
    
    public class Result<T>
    {
        protected Result() { }

        public Result(T value, string exception = null)
        {
            Value = value;
            Exception = exception;
        }

        public T Value { get; protected set; }
        public string Exception { get; protected set; }
        public bool IsSuccessful => string.IsNullOrWhiteSpace(Exception);
        

        public static Result<T> Create(T value)
        {
            return new Result<T>(value, null);
        }

        public static Result<T> Fail(string exception)
        {
            return new Result<T>() { Exception = exception };
        }
    }
}