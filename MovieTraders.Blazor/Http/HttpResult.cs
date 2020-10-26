using System.Net;

namespace MovieTraders.Blazor.Http
{
    /// <summary>
    /// Wrapper class for returning status code with T result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResult<T> : HttpResult
    {
        public T Value { set; get; }

    }

    public class HttpResult
    {
        public HttpStatusCode StatusCode { set; get; }

        public string ErrorResult { set; get; }

        public bool IsSuccess
        {
            get
            {
                if ((int)StatusCode < 200)
                {
                    return false;
                }
                if ((int)StatusCode > 299)
                {
                    return false;
                }
                if (ErrorResult != null)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
