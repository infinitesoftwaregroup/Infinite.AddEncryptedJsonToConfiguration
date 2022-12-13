using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Infinite.AddEncryptedJsonToConfiguration
{
    /// <summary>
    /// An Encrypted JSON file based <see cref="EncryptedJsonConfigurationProvider"/>.
    /// </summary>
    public class EncryptedJsonConfigurationProvider : FileConfigurationProvider
    {
        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public EncryptedJsonConfigurationProvider(EncryptedJsonConfigurationSource source) : base(source) { }

        /// <summary>
        /// Loads JSON configuration key/values from a stream into a provider.
        /// </summary>
        public override void Load()
        {
            var source = (EncryptedJsonConfigurationSource)Source;

            try
            {
                var text = Convert.FromBase64String(File.ReadAllText(source.Path));
                var settings = AesEncryptionHelpers.DecryptStringFromBytes_Aes(text, source.Key);
                
                Data = EncryptedJsonConfigurationFileParser.Parse(new MemoryStream(settings));
            }
            catch (JsonException e)
            {
                throw new FormatException("Could not parse the encrypted JSON file", e);
            }
        }

        /// <summary>
        /// Loads JSON configuration key/values from a stream into a provider.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public override void Load(Stream stream)
        {
            var source = (EncryptedJsonConfigurationSource)Source;

            try
            {
                var encryptedSettings = stream.ToBytes();
                var settings = AesEncryptionHelpers.DecryptStringFromBytes_Aes(encryptedSettings, source.Key);
                
                Data = EncryptedJsonConfigurationFileParser.Parse(new MemoryStream(settings));
            }
            catch (JsonException e)
            {
                throw new FormatException("Could not parse the encrypted JSON file", e);
            }
        }
    }
}
