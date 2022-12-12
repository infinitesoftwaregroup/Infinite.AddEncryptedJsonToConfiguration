using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
namespace Infinite.AddEncryptedJsonToConfiguration
{
    /// <summary>
    /// A collection of helper methods to make working with AES encryption easier.
    /// </summary>
    public static class AesEncryptionHelpers
    {
        public static CryptoStream DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));

            // Declare the string used to hold
            // the decrypted text.

            // Create an Aes object
            // with the specified key and IV.
            using var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = key[8..12].Concat(key[4..8]).Concat(key[12..16]).Concat(key[0..4]).ToArray();


            // Create a decryptor to perform the stream transform.
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using var msDecrypt = new MemoryStream(cipherText);
            var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            // using var srDecrypt = new StreamReader(csDecrypt);
            // // Read the decrypted bytes from the decrypting stream
            // // and place them in a string.
            // var plaintext = srDecrypt.ReadToEnd();

            return csDecrypt;
        }
    }
}
