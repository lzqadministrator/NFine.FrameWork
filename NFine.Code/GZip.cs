﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace NFine.Code
{
    public class GZip
    {
        public static string Compress(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }
            byte[] buffer = Encoding.Default.GetBytes(text);
            return Convert.ToBase64String(Compress(buffer));
        }

        public static byte[] Compress(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }
            using (var ms = new MemoryStream())
            {
                using (var zip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    zip.Write(buffer, 0, buffer.Length);
                }
                return ms.ToArray();
            }
        }

        public static string Decompress(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            byte[] buffer = Convert.FromBase64String(text);
            using (var ms = new MemoryStream(buffer))
            {
                using (var zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(zip))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static byte[] Decompress(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }
            return Decompress(new MemoryStream(buffer));
        }

        public static byte[] Compress(Stream stream)
        {
            if (stream == null || stream.Length == 0)
            {
                return null;
            }
            return Compress(StreamToBytes(stream));
        }

        public static byte[] Decompress(Stream stream)
        {
            if (stream == null || stream.Length == 0)
            {
                return null;
            }
            using (var zip = new GZipStream(stream, CompressionMode.Decompress))
            {
                using (var reader = new StreamReader(zip))
                {
                    return Encoding.Default.GetBytes(reader.ReadToEnd());
                }
            }
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var buffer = new Byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
