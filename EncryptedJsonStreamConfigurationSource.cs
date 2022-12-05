using Microsoft.Extensions.Configuration;

namespace Infinite.AddEncryptedJsonToConfiguration
{
    /// <summary>
    /// Represents an encrypted JSON file as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class EncryptedJsonStreamConfigurationSource : StreamConfigurationSource
    {
        /// <summary>
        /// Encryption key used  to decrypt the configuration file
        /// </summary>
        internal byte[] Key { get; set; }

        /// <summary>
        /// Builds the <see cref="EncryptedJsonStreamConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>An <see cref="EncryptedJsonStreamConfigurationProvider"/></returns>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
            => new EncryptedJsonStreamConfigurationProvider(this);
    }
}