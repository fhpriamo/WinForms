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

        private static class CurrentDate
        {
            public static DateTime Parse(string input)
            {
                DateTime result = DateTime.Parse(input);
                if (result > DateTime.Now)
                {
                    throw new FormatException("The given date is in the future.");
                }

                return result;
            }
        }

        protected PhotoEditDialog()
        {
            InitializeComponent();
        }

        public PhotoEditDialog(Photograph photo) : this()
        {
            if (photo == null)
                throw new ArgumentNullException("photo argument cannot be null.");

            InitializeDialog(photo);
        }

        public PhotoEditDialog(AlbumManager manager) : this()
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
                mskDateTaken.Text = Photo.DateTaken.ToString();
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
                    Photo.DateTaken = DateTime.Parse(mskDateTaken.Text);
                }
                catch (FormatException)
                {

                }
            }
        }

        private void InitializeDialog(Photograph photo)
        {
            mskDateTaken.ValidatingType = typeof(CurrentDate);
            Photo = photo;
            ResetDialog();
        }

        private void txtCaption_TextChanged(object sender, EventArgs e)
        {
            Text = txtCaption.Text + " - Properties";
        }

        private void mskDateTaken_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                DialogResult result = MessageBox.Show("The Date Taken entry is invalid or in the future and may be ignored. Do you want to correct this?", "Photo Properties", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                e.Cancel = (result == DialogResult.Yes);
            }
        }
    }
}
