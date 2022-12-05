﻿using System.IO;

namespace Infinite.AddEncryptedJsonToConfiguration
{
    internal static class StreamExtensions
    {
        public static byte[] ToBytes(this Stream input)
        {
            using var ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
