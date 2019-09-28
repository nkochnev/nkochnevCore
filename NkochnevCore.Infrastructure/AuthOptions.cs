using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NkochnevCore.Infrastructure
{
    public class AuthOptions
    {
        public const string Issuer = "nkochnev"; // издатель токена
        public const string Audience = "nkochnev.ru"; // потребитель токена
        private const string Key = "mysupersecret_secretkey!123"; // ключ для шифрования, потом нужно вынести в конфиг
        public const int Lifetime = 10; // время жизни токена - 10 минут

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}