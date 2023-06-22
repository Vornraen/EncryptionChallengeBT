using System.Security.Cryptography;

namespace EncryptionChallengeBT.Services
{
    public interface IPasswordService
    {
        string CreatePassword(string userId);
        bool PasswordIsValid(string password, string userId);
        string ComputeHash(string input, HashAlgorithm hashAlgorithm);

    }
}
