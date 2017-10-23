using System;

namespace Pictest.Model
{
    public class ApiResult<T>
    {
        public ApiResult(T data)
        {
            Data = data;
        }

        public ApiResult(Exception ex)
        {
            Error = new ErrorResult
            {
                Error = ex.Source,
                Message = ex.Message
            };
        }

        public ErrorResult Error { get; }
        public T Data { get; }
    }

    public class ErrorResult
    {
        public string Error { get; set; }
        public string Message { get; set; }
    }
}