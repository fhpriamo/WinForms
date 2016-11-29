using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyPhotoAlbum;

namespace MyPhotoControls
{
    public partial class PhotoEditDialog : MyPhotoControls.BaseEditDialog
    {
        private Photograph Photo { get; set; }
        private AlbumManager Manager { get; set; }

        protected PhotoEditDialog()
        {
            InitializeComponent();
        }

        protected PhotoEditDialog(Photograph photo) : this()
        {
            if (photo == null)
                throw new ArgumentNullException("photo argument cannot be null.");

            InitializeDialog(photo);
        }

        protected PhotoEditDialog(AlbumManager manager) : this()
        {
            if (manager == null)
                throw new ArgumentNullException("manager paramenter cannot be null.");

            Manager = manager;
            InitializeDialog(Manager.Current);
        }

        protected override void ResetDialog()
        {
            if (Photo != null)
            {
                txtPhotoFile.Text = Photo.FileName;
                txtCaption.Text = Photo.Caption;
                txtDateTaken.Text = Photo.DateTaken.ToString();
                txtPhotographer.Text = Photo.Photographer;
                txtNotes.Text = Photo.Notes;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                SaveSettings();
            }
        }

        private void SaveSettings()
        {
            if (Photo != null)
            {
                Photo.Caption = txtCaption.Text;
                Photo.Photographer = txtPhotographer.Text;
                Photo.Notes = txtNotes.Text;

                // On parse error, do not set date
                try
                {
                    Photo.DateTaken = DateTime.Parse(txtDateTaken.Text);
                }
                catch (FormatException)
                {

                }
            }
        }

        private void InitializeDialog(Photograph photo)
        {
            Photo = photo;
            ResetDialog();
        }
    }
}
