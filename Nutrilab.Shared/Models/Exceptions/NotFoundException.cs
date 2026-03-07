using System.Net;

namespace Nutrilab.Shared.Models.Exceptions
{
    /// <summary>
    /// Not Found Exception
    /// </summary>
    public class NotFoundException : BaseException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public NotFoundException(string msg) : base(msg, HttpStatusCode.NotFound)
        {
        }
    }
}
