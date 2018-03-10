using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using NkochnevCore.Infrastructure.Data;
using NkochnevCore.Infrastructure.Domain;
using NkochnevCore.Infrastructure.Services.Interfaces;
using NkochnevCore.WebApi.Controllers;

namespace NkochnevCore.Infrastructure.Services
{
	public class AuthService : IAuthService
	{
		private readonly IRepository<AuthDomain> _authRepository;
		private readonly IRepository<AuthTokenDomain> _tokenRepository;
		private readonly IEncryptionService _encryptionService;

		public AuthService(IRepository<AuthDomain> authRepository, IRepository<AuthTokenDomain> tokenRepository,
			IEncryptionService encryptionService)
		{
			_authRepository = authRepository;
			_tokenRepository = tokenRepository;
			_encryptionService = encryptionService;
		}

		public bool ValidatePass(string password)
		{
			var allHashs = _authRepository.Table.ToList();
			return allHashs.Any(authDomain => _encryptionService.Validate(password, authDomain.PassHash));
		}

		public AuthToken CreateToken()
		{
			var now = DateTime.Now;
			var expires = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));
			var jwt = new JwtSecurityToken(
				issuer: AuthOptions.ISSUER,
				audience: AuthOptions.AUDIENCE,
				notBefore: now,
				expires: expires,
				signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
			
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
			var refreshToken = Guid.NewGuid().ToString();

			var tokenDomain = new AuthTokenDomain()
			{
				RefreshToken = refreshToken,
				Expiresin = expires,
				JwtToken = encodedJwt,
				RefreshTokenValid = true
			};
			_tokenRepository.Insert(tokenDomain);

			return new AuthToken()
			{
				accessToken = encodedJwt,
				expiresIn = expires,
				refreshToken = refreshToken
			};
		}

		public AuthToken RefreshToken(string refreshToken)
		{
			var tokenDomain = _tokenRepository.Table.FirstOrDefault(x => x.RefreshToken == refreshToken);
			if (tokenDomain == null)
			{
				throw new Exception($"Refresh token {refreshToken} not found");
			}

			if (!tokenDomain.RefreshTokenValid)
			{
				throw new Exception($"Refresh token {refreshToken} has already been used");
			}

			tokenDomain.RefreshTokenValid = false;
			_tokenRepository.Update(tokenDomain);

			return CreateToken();
		}
	}
}