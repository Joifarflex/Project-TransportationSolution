using TransportationSolution.Enum;
using TransportationSolution.Model;

namespace TransportationSolution.Common
{
    public class Response
    {
        public string message { get; private set; }
        public ResponseStatus status { get; private set; }

        public Response Success(string message)
        {
            this.message = message;
            this.status = ResponseStatus.Success;

            return this;
        }

        public Response Fail(string message)
        {
            this.message = message;
            this.status = ResponseStatus.Fail;

            return this;
        }

        public Response BadRequest(string message)
        {
            this.message = message;
            this.status = ResponseStatus.Bad_Request;

            return this;
        }
    }

    public class Response<T> where T : class
    {
        public string message { get; set; }
        public T data { get; set; }
        public ResponseStatus status { get; set; }

        public Response<T> Fail(string message, T? data = default)
        {
            this.message = message;
            this.data = data;
            this.status = ResponseStatus.Fail;

            return this;
        }

        public Response<T> BadRequest(string message)
        {
            this.message = message;
            this.status = ResponseStatus.Bad_Request;

            return this;
        }

        public Response<T> Success(string message, T? data = default)
        {
            this.message = message;
            this.data = data;
            this.status = ResponseStatus.Success;

            return this;
        }
    }
}
