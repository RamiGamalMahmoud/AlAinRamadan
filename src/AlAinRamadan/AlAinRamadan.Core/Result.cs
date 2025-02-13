namespace AlAinRamadan.Core
{
    public class Result<T>
    {
        public Result(T result)
        {
            Value = result;
        }
        public T Value { get; init; }
    }

    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T result) : base(result)
        {
        }
    }

    public class ErrorResult<T> : Result<T>
    {
        public ErrorResult(T result) : base(result)
        {
        }
    }
}
