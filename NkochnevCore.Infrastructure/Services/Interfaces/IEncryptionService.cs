namespace NkochnevCore.Infrastructure.Services.Interfaces
{
    public interface IEncryptionService
    {
        bool Validate(string password, string testhash);

        string CreateHash(string password);
    }
}