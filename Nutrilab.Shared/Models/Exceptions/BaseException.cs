using Nutrilab.Shared.Models.Exceptions.Models;
using System.Net;
using System.Text.Json;

namespace Nutrilab.Shared.Models.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string msg, HttpStatusCode status) : base(SerializeErrorResponse(msg, (int)status)) { }

        private static string SerializeErrorResponse(string msg, int status)
        {
            var errorResponse = new ErrorResponse
            {
                Msg = msg,
                Status = status
            };
            var json = JsonSerializer.Serialize(errorResponse);
            return json;
        }
    }
}
