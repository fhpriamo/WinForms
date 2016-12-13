using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoAlbum
{
    class CryptoTextBase
    {
        // A larger salt would provide more secure encryption here
        public readonly byte[] SaltBytes = { 0x39, 0x38, 0x14, 0x05, 0x68 };
        private byte[] _pwd;
        private MemoryStream _ms;
        private CryptoStream _cs;

        protected Byte[] Password { get { return _pwd; } }
        protected MemoryStream MStream { get { return _ms; } set { _ms = value; } }
        protected CryptoStream CStream { get { return _cs; } set { _cs = value; } }

        public CryptoTextBase(string password)
        {
            if (password == null || password.Length == 0)
                throw new ArgumentNullException("password");

            _pwd = Encoding.UTF8.GetBytes(password);
        }

        /// <summary>
        /// Encrypt or decrypt a given string
        /// </summary>
        public string ProcessText(string text, bool encrypt)
        {
            // Encode text as byte array
            byte[] bytes = encrypt ? Encoding.UTF8.GetBytes(text) : Convert.FromBase64String(text);
            MStream = new MemoryStream();

            // Create default symmetric algorithm for cryption
            SymmetricAlgorithm alg = SymmetricAlgorithm.Create();
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, SaltBytes);
            alg.Key = pdb.GetBytes(alg.KeySize / 8);
            alg.IV = pdb.GetBytes(alg.BlockSize / 8);
            ICryptoTransform transform = encrypt ? alg.CreateEncryptor() : alg.CreateDecryptor();

            // Create cryptographic stream
            CStream = new CryptoStream(MStream, transform, CryptoStreamMode.Write);

            // Encrypt data and flush result to buffer
            CStream.Write(bytes, 0, bytes.Length);
            CStream.FlushFinalBlock();

            // Retrieve the resulting bytes
            byte[] result = MStream.ToArray();

            // Convert result to a string
            return encrypt ? Convert.ToBase64String(result) : Encoding.UTF8.GetString(result);
        }
    }
}