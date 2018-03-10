using System;

namespace NkochnevCore.WebApi.Controllers
{
	public class AuthToken
	{
		public string accessToken { get; set; }
		public DateTime expiresIn { get; set; }
		public string refreshToken { get; set; }
	}
}