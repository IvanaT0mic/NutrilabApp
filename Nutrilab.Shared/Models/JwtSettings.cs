namespace Nutrilab.Shared.Models
{
    public class JwtSettings
    {
        public string AccessTokenSecret { get; set; }
        public int AccessTokenExpTime { get; set; }
    }
}
