using System.Net;

namespace Nutrilab.Shared.Models.Exceptions
{
    /// <summary>
    /// Bad Request Exception
    /// </summary>
    public class BadRequestException : BaseException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public BadRequestException(string msg) : base(msg, HttpStatusCode.BadRequest)
        {
        }
    }
}
