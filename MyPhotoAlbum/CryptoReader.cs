using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoAlbum
{
    class CryptoReader : StreamReader
    {
        private CryptoTextBase _base;
        private CryptoTextBase CryptoBase { get { return _base; } }

        public CryptoReader(string path, string password) : base(path)
        {
            if (path == null || path.Length == 0)
                throw new ArgumentNullException("path");
            if (password == null || password.Length == 0)
                throw new ArgumentNullException("password");
            _base = new CryptoTextBase(password);
        }

        public override string ReadLine()
        {
            string encrypted = base.ReadLine();
            if (encrypted == null || encrypted.Length == 0)
                return encrypted;
            else
                return CryptoBase.ProcessText(encrypted, false);
        }

        public string ReadUnencryptedLine()
        {
            return base.ReadLine();
        }
    }
}
