using System;
using NkochnevCore.Infrastructure.Data;

namespace NkochnevCore.Infrastructure.Domain
{
	public class AuthTokenDomain : BaseDomain
	{
		public string RefreshToken { get; set; }
		public DateTime Expiresin { get; set; }
		public string JwtToken { get; set; }
		public bool RefreshTokenValid { get; set; }
	}
}