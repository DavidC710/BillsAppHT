
namespace BillsApp.Application.UseCases.Common
{
    public class Result<T>
    {
        public Result(T data, ResultType resultType, params string[] errors)
        {
            this.Data = data;
            this.Errors = errors;
            this.ResultType = resultType;
        }

        public ResultType ResultType { get; }

        public IEnumerable<string> Errors { get; }

        public T Data { get; }
    }
}
