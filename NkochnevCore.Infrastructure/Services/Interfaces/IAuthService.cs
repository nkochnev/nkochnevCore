namespace NkochnevCore.Infrastructure.Services.Interfaces
{
	public interface IAuthService
	{
		bool ValidatePass(string password);
	}
}