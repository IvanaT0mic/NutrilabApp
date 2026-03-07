using System.Net;

namespace Nutrilab.Shared.Models.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string msg) : base(msg, HttpStatusCode.Unauthorized)
        {
        }
    }
}
