using System;

namespace NkochnevCore.Infrastructure
{
    public class AuthToken
    {
        public string accessToken { get; set; }
        public DateTime expiresIn { get; set; }
        public string refreshToken { get; set; }
    }
}