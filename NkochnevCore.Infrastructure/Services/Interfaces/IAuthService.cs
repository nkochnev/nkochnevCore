using NkochnevCore.WebApi.Controllers;

namespace NkochnevCore.Infrastructure.Services.Interfaces
{
	public interface IAuthService
	{
		bool ValidatePass(string password);
		AuthToken CreateToken();
		AuthToken RefreshToken(string refreshToken);
	}
}