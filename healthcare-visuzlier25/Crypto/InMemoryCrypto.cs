using healthcare_visuzlier25.Config;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace healthcare_visuzlier25.Crypto
{
    public class InMemoryCrypto : ICryptoProvider, IDisposable
    {
        private ILogger<InMemoryCrypto> _logger;
        private readonly SessionCryptoConfig _cryptoConfig;
        // The Key material is only for that session
        private readonly Aes myAes;//= Aes.Create();
        private bool isDisposed = false;
        public InMemoryCrypto(ILogger<InMemoryCrypto> logger, IOptions<SessionCryptoConfig> cryptoConfig)
        {
            _logger = logger;
            _cryptoConfig = cryptoConfig.Value;
            byte[] keyBytes = Encoding.UTF8.GetBytes(_cryptoConfig.Key);
            byte[] IvBytes = Encoding.UTF8.GetBytes(_cryptoConfig.IV);
            myAes = Aes.Create();
            myAes.Key = keyBytes;
            myAes.IV = IvBytes;
        }
        public byte[] DecryptRaw(byte[] bytes)
        {
            return Util.Crypto.Decrypt_Aes(bytes, myAes.Key, myAes.IV);
        }
        public byte[] EncryptRaw(byte[] bytes)
        {
            return Util.Crypto.Encrypt_Aes(bytes, myAes.Key, myAes.IV);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                myAes.Dispose();
                isDisposed = true;
            }
        }

        ~InMemoryCrypto()
        {
            Dispose();
        }



    }
}
