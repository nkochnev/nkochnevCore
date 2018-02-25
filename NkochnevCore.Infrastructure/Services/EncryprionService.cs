using System;
using System.Security.Cryptography;
using NkochnevCore.Infrastructure.Services.Interfaces;

namespace NkochnevCore.Infrastructure.Services
{
	public class EncryptionService : IEncryptionService
	{
		public const int SALT_BYTE_SIZE = 24;
		public const int HASH_BYTE_SIZE = 24;
		public const int PBKDF2_ITERATIONS = 1000;

		public const int ITERATION_INDEX = 0;
		public const int SALT_INDEX = 1;
		public const int PBKDF2_INDEX = 2;

		/// <summary>
		///     Проверка, что указанный пароль соотвтествует указанному хэшу
		/// </summary>
		/// <param name="password"></param>
		/// <param name="testhash"></param>
		/// <returns></returns>
		public bool Validate(string password, string testhash)
		{
			// Extract the parameters from the hash
			char[] delimiter = {':'};
			var split = testhash.Split(delimiter);
			var iterations = int.Parse(split[ITERATION_INDEX]);
			var salt = Convert.FromBase64String(split[SALT_INDEX]);
			var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

			var testHash = PBKDF2(password, salt, iterations, hash.Length);
			return SlowEquals(hash, testHash);
		}

		private static bool SlowEquals(byte[] a, byte[] b)
		{
			var diff = (uint) a.Length ^ (uint) b.Length;
			for (var i = 0; i < a.Length && i < b.Length; i++)
				diff |= (uint) (a[i] ^ b[i]);
			return diff == 0;
		}

		private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
		{
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
			pbkdf2.IterationCount = iterations;
			return pbkdf2.GetBytes(outputBytes);
		}
	}
}