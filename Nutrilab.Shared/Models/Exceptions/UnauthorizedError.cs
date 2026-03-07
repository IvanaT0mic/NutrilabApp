using Nutrilab.Shared.Models.Exceptions;
using System.Net;

namespace Shared.Models.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string msg) : base(msg, HttpStatusCode.Unauthorized)
        {
        }
    }
}
