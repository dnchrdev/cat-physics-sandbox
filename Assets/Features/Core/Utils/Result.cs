namespace Feature.Core
{
    public sealed class Result
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        private Result(bool isSuccess, string? error = null)
        {
            IsSuccess = isSuccess;
            Message = error;
        }

        public static Result Success() => new(true);
        public static Result Failure(string Error) => new(false, Error);
    }
    public sealed class Result<T>
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public T? Value { get; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
        }
        private Result(string error)
        {
            IsSuccess = false;
            Message = error;
        }

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(string message) => new(message);
    }
}