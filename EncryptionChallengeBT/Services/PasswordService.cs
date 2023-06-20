using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionChallengeBT.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IMemoryCache _memoryCache;
        
        public PasswordService(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public bool CheckPassword(string password, string user)
        {
            if(_memoryCache.Get(password) as string == user)
            {
                return true;
            }
            return false;
        }

        public string ComputeHash(string input, HashAlgorithm hashAlgorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = hashAlgorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public string CreatePassword(string user)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                string hPassword = ComputeHash(user, mySHA256);

                _memoryCache.Set(user, hPassword, TimeSpan.FromSeconds(30));

                return hPassword;

            }
            
        }


     
    }
}
