using System.Security.Cryptography;

namespace EncryptionChallengeBT.Services
{
    public interface IPasswordService
    {
        string CreatePassword(string userid);
        bool CheckPassword(string password, string userid);
        string ComputeHash(string input, HashAlgorithm hashAlgorithm);

    }
}
