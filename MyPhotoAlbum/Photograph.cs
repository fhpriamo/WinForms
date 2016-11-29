using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoAlbum
{
    public class Photograph : IDisposable
    {
        private string filename;
        private Bitmap bitmap;
        private string caption = "";
        private string photographer = "";
        private DateTime dateTaken = DateTime.Now;
        private string notes = "";
        //private bool hasChanged = true;

        public Photograph(string filename)
        {
            this.filename = filename;
            bitmap = null;

            Caption = System.IO.Path.GetFileNameWithoutExtension(filename);
            HasChanged = true;
            Photographer = "";
            DateTaken = DateTime.Now;
            Notes = "";
        }


        public string FileName
        {
            get
            {
                return filename;
            }
        }

        public Bitmap Image
        {
            private set { }
            get
            {
                if (bitmap == null)
                    bitmap = new Bitmap(filename);

                return bitmap;
            }
        }

        public bool HasChanged
        {
            get;
            internal set;
        }

        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                if (caption != value)
                {
                    caption = value;
                    HasChanged = true;
                }
            }
        }

        public string Photographer
        {
            get
            {
                return photographer;
            }
            set
            {
                if (photographer != value)
                {
                    photographer = value;
                    HasChanged = true;
                }
            }

        }

        public DateTime DateTaken
        {
            get
            {
                return dateTaken;
            }
            set
            {
                if (dateTaken != value)
                {
                    dateTaken = value;
                    HasChanged = true;
                }
            }
        }

        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                if (notes != value)
                {
                    notes = value;
                    HasChanged = true;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Photograph)
            {
                Photograph p = (Photograph) obj;
                return (FileName.Equals(p.FileName, StringComparison.InvariantCultureIgnoreCase));
            }

            return false;
        }

        public override string ToString()
        {
            return FileName;
        }

        public override int GetHashCode()
        {
            return FileName.ToLowerInvariant().GetHashCode();
        }

        public void ReleaseImage()
        {
            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }
        }

        public void Dispose()
        {
            ReleaseImage();
        }
    }
}
