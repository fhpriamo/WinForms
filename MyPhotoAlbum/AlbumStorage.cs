using System;
using System.IO;

namespace MyPhotoAlbum
{
    public class AlbumStorageException : Exception
    {
        public AlbumStorageException() : base() { }
        public AlbumStorageException(string msg) : base(msg) { }
        public AlbumStorageException(string msg, Exception inner) : base(msg, inner) { }
    }

    public class AlbumStorage
    {
        static private int CurrentVersion = 63;

        static public void WriteAlbum(PhotoAlbum album, string path)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(path, false);
                sw.WriteLine(CurrentVersion.ToString());

                // Store each photo separately
                foreach (Photograph p in album)
                {
                    WritePhoto(sw, p);
                }
                    
                // Reset changed after all photos written
                album.HasChanged = false;
            }
            catch (UnauthorizedAccessException uax)
            {
                throw new AlbumStorageException("Unable to access album " + path, uax);
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        static private void WritePhoto(StreamWriter sw, Photograph p)
        {
            sw.WriteLine(p.FileName);
            sw.WriteLine(p.Caption != null ? p.Caption : "");
            sw.WriteLine(p.DateTaken.ToString());
            sw.WriteLine(p.Photographer != null ? p.Photographer : "");
            sw.WriteLine(p.Notes != null ? p.Notes : "");
        }

        static public PhotoAlbum ReadAlbum(string path)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path);
                string version = sr.ReadLine();
                PhotoAlbum album = new PhotoAlbum();

                switch (version)
                {
                    case "63":
                        ReadAlbumV63(sr, album);
                        break;
                    default:
                        throw new AlbumStorageException("Unrecognized album version");
                }

                album.HasChanged = false;

                return album;
            }
            catch (FileNotFoundException fnx)
            {
                throw new AlbumStorageException("Unable to read album " + path, fnx);
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        static private void ReadAlbumV63(StreamReader sr, PhotoAlbum album)
        {
            // Read each photo into album.
            Photograph p;
            do
            {
                p = ReadPhotoV63(sr);
                if (p != null)
                {
                    album.Add(p);
                }
                    
            } while (p != null);
        }

        static private Photograph ReadPhotoV63(StreamReader sr)
        {
            // Presume at the start of photo
            string file = sr.ReadLine();

            if (file == null || file.Length == 0)
            {
                return null;
            }
                
            // File not null, should find photo

            Photograph p = new Photograph(file);
            p.Caption = sr.ReadLine();
            p.DateTaken = DateTime.Parse(sr.ReadLine());
            p.Photographer = sr.ReadLine();
            p.Notes = sr.ReadLine();
            return p;
        }
    }
}