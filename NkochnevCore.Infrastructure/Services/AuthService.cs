using System.Linq;
using NkochnevCore.Infrastructure.Data;
using NkochnevCore.Infrastructure.Domain;
using NkochnevCore.Infrastructure.Services.Interfaces;

namespace NkochnevCore.Infrastructure.Services
{
	public class AuthService : IAuthService
	{
		private readonly IRepository<AuthDomain> _authRepository;
		private readonly IEncryptionService _encryptionService;

		public AuthService(IRepository<AuthDomain> authRepository, IEncryptionService encryptionService)
		{
			_authRepository = authRepository;
			_encryptionService = encryptionService;
		}

		public bool ValidatePass(string password)
		{
		    return true;
			var allHashs = _authRepository.Table.ToList();
			return allHashs.Any(authDomain => _encryptionService.Validate(password, authDomain.PassHash));
		}
	}
}