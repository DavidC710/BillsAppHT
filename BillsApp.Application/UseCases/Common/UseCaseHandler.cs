
namespace BillsApp.Application.UseCases.Common
{
    public class UseCaseHandler
    {
        public Result<T> NotFound<T>(string error = null)
        {
            return new Result<T>(default, ResultType.NotFound, error ?? "Not Found.");
        }

        public Result<T> PermissionDenied<T>(string error = null)
        {
            return new Result<T>(default, ResultType.PermissionDenied, error ?? "Permission Denied.");
        }

        public Result<T> Invalid<T>(string error = null)
        {
            return new Result<T>(default, ResultType.Invalid, error ?? "Invalid.");
        }

        public Result<T> Succeded<T>(T data)
        {
            return new Result<T>(data, ResultType.Ok);
        }

    }
}
