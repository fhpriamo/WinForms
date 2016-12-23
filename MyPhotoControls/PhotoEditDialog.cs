using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyPhotoAlbum;
using System.Collections.Specialized;

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

        protected override void ResetDialog()
        {
            // Fill combo box with photographers in album
            comboPhotographer.BeginUpdate();
            comboPhotographer.Items.Clear();

            if (Manager != null)
            {
                StringCollection coll = Manager.Photographers;
                foreach (string s in coll)
                    comboPhotographer.Items.Add(s);
            }
            else
            {
                comboPhotographer.Items.Add(Photo.Photographer);
            }

            comboPhotographer.EndUpdate();

            // Initialize form contents
            Photograph photo = Photo;

            if (photo != null)
            {
                txtPhotoFile.Text = photo.FileName;
                txtCaption.Text = photo.Caption;
                dtpDateTaken.Value = photo.DateTaken;
                comboPhotographer.Text = photo.Photographer;
                txtNotes.Text = photo.Notes;
            }
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
                Photo.Photographer = comboPhotographer.Text;
                Photo.Notes = txtNotes.Text;

                Photo.DateTaken = dtpDateTaken.Value;
            }
        }

        private void InitializeDialog(Photograph photo)
        {
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

        private void comboPhotographer_Leave(object sender, EventArgs e)
        {
            string person = comboPhotographer.Text;
            if (!comboPhotographer.Items.Contains(person))
                comboPhotographer.Items.Add(person);
        }
    }
}
