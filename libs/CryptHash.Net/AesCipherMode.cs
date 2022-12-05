using System.Security.Cryptography;

namespace Infinite.AddEncryptedJsonToConfiguration.libs.CryptHash.Net
{
    public enum AesCipherMode { CBC = CipherMode.CBC, ECB = CipherMode.ECB, OFB = CipherMode.OFB, CFB = CipherMode.CFB, CTS = CipherMode.CTS, GCM };
}