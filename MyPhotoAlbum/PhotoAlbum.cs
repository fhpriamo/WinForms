using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhotoAlbum
{
    public class PhotoAlbum : Collection<Photograph>, IDisposable
    {
        private bool hasChanged = false;

        public bool HasChanged
        {
            get
            {
                if (hasChanged)
                {
                    return true;
                }
                    
                foreach (Photograph p in this)
                {
                    if (p.HasChanged)
                    {
                        return true;
                    }
                }

                return false;
            }

            internal set
            {
                hasChanged = value;
                if (value == false)
                {
                    foreach (Photograph p in this)
                    {
                        p.HasChanged = false;
                    }
                }
            }
        }

        public enum DescriptorOption { FileName, Caption, DateTaken }

        public PhotoAlbum()
        {
            ClearSettings();
        }

        public string Title { get; set; }
        public DescriptorOption Descriptor { get; set; }
        public string Password { get; set; }

        private void ClearSettings()
        {
            Title = null;
            Descriptor = DescriptorOption.Caption;
        }

        public Photograph Add(string fileName)
        {
            Photograph p = new Photograph(fileName);
            base.Add(p);
            return p;
        }

        public void Dispose()
        {
            ClearSettings();
            foreach (Photograph p in this)
                p.Dispose();
        }

        protected override void ClearItems()
        {
            if (Count > 0)
            {
                Dispose();
                base.ClearItems();
                HasChanged = true;
            }
            
        }

        public string GetDescription(Photograph photo)
        {
            switch (Descriptor)
            {
                case DescriptorOption.Caption:
                    return photo.Caption;

                case DescriptorOption.DateTaken:
                    return photo.DateTaken.ToShortDateString();

                case DescriptorOption.FileName:
                    return photo.FileName;
            }

            throw new ArgumentException("Unrecognized photo descriptor option.");
        }

        public string GetDescription(int index)
        {
            return GetDescription(this[index]);
        }



        protected override void InsertItem(int index, Photograph item)
        {
            base.InsertItem(index, item);
            HasChanged = true;
        }

        protected override void RemoveItem(int index)
        {
            this[index].Dispose();
            base.RemoveItem(index);
            HasChanged = true;
        }

        protected override void SetItem(int index, Photograph item)
        {
            base.SetItem(index, item);
            HasChanged = true;
        }
    }
}