using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NkochnevCore.Infrastructure.Services.Interfaces;
using NkochnevCore.WebApi.Models;

namespace NkochnevCore.WebApi.Controllers
{
	[Produces("application/json")]
	[Route("api/Auth")]
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		public ActionResult Auth([FromBody]AuthRequestModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			var passIsCorrect = _authService.ValidatePass(model.pass);

			if (!passIsCorrect)
			{
				return BadRequest("invalid pass");
			}

			var token = _authService.CreateToken();

			return Ok(token);
		}

		[HttpPost]
		[Route("refresh")]
		public ActionResult RefreshToken([FromBody]RefreshTokenRequestModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			var newToken = _authService.RefreshToken(model.RefreshToken);
			return Ok(newToken);
		}
	}
}