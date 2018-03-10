using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NkochnevCore.WebApi.Controllers
{
	public class AuthOptions
	{
		public const string ISSUER = "nkochnev"; // издатель токена
		public const string AUDIENCE = "nkochnev.ru"; // потребитель токена
		const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
		public const int LIFETIME = 1; // время жизни токена - 10 минут
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}