using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoAlbum
{
    class CryptoWriter : StreamWriter
    {
        private CryptoTextBase _base;
        private CryptoTextBase CryptoBase { get { return _base; } }

        public CryptoWriter(string path, string password) : base(path)
        {
            if (path == null || path.Length == 0)
                throw new ArgumentNullException("path");
            if (password == null || password.Length == 0)
                throw new ArgumentNullException("password");
            _base = new CryptoTextBase(password);
        }

        public override void WriteLine(string value)
        {
            string encrypted = CryptoBase.ProcessText(value, true);
            base.WriteLine(encrypted);
        }

        public void WriteUnencryptedLine(string value)
        {
            base.WriteLine(value);
        }
    }
}
