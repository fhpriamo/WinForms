using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPhotoAlbum;
using MyPhotoControls;

namespace MyPhotos
{
    public partial class MainForm : Form
    {
        private AlbumManager Manager { get; set;  }
        private PixelDialog PixelForm;

        public MainForm()
        {
            InitializeComponent();

            NewAlbum();
            //SetTitle();

            // Example of explicitly using a provider method (this could've been done on the properties window of VS)
            flybyProvider.SetFlybyText(menuFileSave, "Save the current album.");
        }

        private void NewAlbum()
        {
            if (Manager == null || SaveAndCloseAlbum())
            {
                Manager = new AlbumManager();
                DisplayAlbum();
            }
        }

        private void SaveAlbum(string name)
        {
            try
            {
                Manager.Save(name, true);
            }
            catch (AlbumStorageException asex)
            {
                string msg = String.Format("Unable to save album {0} ({1})\n\n"
                    + "Do you wish to save the album "
                    + "under an alternate name?",
                    name, asex.Message);

                DialogResult result = MessageBox.Show(msg, "Unable to Save",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    SaveAsAlbum();
                }
                    
            }
        }

        private void SaveAlbum()
        {
            if (string.IsNullOrEmpty(Manager.FullName))
            {
                SaveAsAlbum();
            }
            else
            {
                SaveAlbum(Manager.FullName);
            }
        }

        private void SaveAsAlbum()
        {
            saveFileDialog1.InitialDirectory = AlbumManager.DefaultPath;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveAlbum(saveFileDialog1.FileName);
                SetTitleBar();
            }

            saveFileDialog1.Dispose();

        }

        private bool SaveAndCloseAlbum()
        {
            DialogResult result = AlbumController.AskForSave(Manager);
            if (result == DialogResult.Yes)
                SaveAlbum();
            else if (result == DialogResult.Cancel)
                return false; // do not close

            // Close the album and return true
            if (Manager.Album != null)
            {
                Manager.Album.Dispose();
            }

            Manager = new AlbumManager();
            SetTitleBar();

            return true;

        }

        private void SetTitleBar()
        {
            Version ver = new Version(Application.ProductVersion);

            string name = Manager.FullName;

            this.Text = String.Format("{2} - MyPhotos {0:0}.{1:0}",
                ver.Major, ver.Minor, String.IsNullOrEmpty(name) ? "Untitled" : name);
        }

        private void SetTitle()
        {
            Version ver = new Version(Application.ProductVersion);
            Text = string.Format("MyPhoto {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menu_NotImplemented(object sender, EventArgs e)
        {
            MessageBox.Show("This menu is still not implemented.");
        }

        private void ProcessImageClick(ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;

            string enumVal = item.Tag as string;
            if (enumVal != null)
            {
                pbxPhoto.SizeMode = (PictureBoxSizeMode) Enum.Parse(typeof(PictureBoxSizeMode), enumVal);
            }
        }

        private void DisplayAlbum()
        {
            pbxPhoto.Image = Manager.CurrentImage;
            SetStatusStrip();
            SetTitle();
            Point p = pbxPhoto.PointToClient(Form.MousePosition);
            UpdatePixelDialog(p.X, p.Y);
        }

        private void SetStatusStrip()
        {
            if (pbxPhoto.Image != null)
            {
                string fileName = Manager.Current.FileName;
                statusInfo.Text = fileName;
                statusInfo.ToolTipText = fileName;
                statusImageSize.Text = string.Format("{0:#}x{1:#}", pbxPhoto.Image.Width, pbxPhoto.Image.Height);
            }
            else
            {
                statusInfo.Text = null;
                statusImageSize.Text = null;
                statusAlbumPos.Text = null;    
            }
        }
    

        private void menuImage_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ProcessImageClick(e);
        }

        private void ProcessImageOpening(ToolStripDropDownItem menuImage)
        {
            if (menuImage != null)
            {
                string enumVal = pbxPhoto.SizeMode.ToString();
                foreach (ToolStripMenuItem item in menuImage.DropDownItems)
                {
                    item.Enabled = (pbxPhoto.Image != null);
                    item.Checked = item.Tag.Equals(enumVal);
                }
            }
        }


        private void menuImage_DropDownOpening(object sender, EventArgs e)
        {
            ProcessImageOpening(sender as ToolStripDropDownItem);
        }



        private void menuFileNew_Click(object sender, EventArgs e)
        {
            NewAlbum();
        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            string path = null;
            string password = null;
            if (AlbumController.OpenAlbumDialog(ref path, ref password))
            {
                // Close existing album
                if (!SaveAndCloseAlbum())
                {
                    return; // Close cancelled
                }
                    
                try
                {
                    Manager = new AlbumManager(path, password);
                }
                catch (AlbumStorageException asex)
                {
                    string msg = String.Format("Unable to open album file {0}\n({1})", path, asex.Message);
                    MessageBox.Show(msg, "Unable to Open");
                    Manager = new AlbumManager();
                }

                DisplayAlbum();
            }
        }

        private void menuFileSave_Click(object sender, EventArgs e)
        {
            SaveAlbum();
        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            string path = null;
            if (AlbumController.SaveAlbumDialog(ref path))
            {
                // Save the album under the new name
                SaveAlbum(path);
                // Update title bar to include new name
                SetTitleBar();
            }
        }

        private void menuEditAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Add Photos";
            dlg.Multiselect = true;
            dlg.Filter
            = "Image Files (JPEG, GIF, BMP, etc.)|"
            + "*.jpg;*.jpeg;*.gif;*.bmp;"
            + "*.tif;*.tiff;*.png|"
            + "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
            + "GIF files (*.gif)|*.gif|"
            + "BMP files (*.bmp)|*.bmp|"
            + "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
            + "PNG files (*.png)|*.png|"
            + "All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.CurrentDirectory;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] files = dlg.FileNames;

                int index = 0;
                foreach (string s in files)
                {
                    Photograph photo = new Photograph(s);

                    // Add the file (if not already present)
                    index = Manager.Album.IndexOf(photo);
                    if (index < 0)
                    {
                        Manager.Album.Add(photo);
                    }                       
                    else
                    {
                        photo.Dispose(); // photo already there
                    }
                        
                }

                Manager.Index = Manager.Album.Count - 1;

                dlg.Dispose();

                DisplayAlbum();
            }
        }

        private void menuEditRemove_Click(object sender, EventArgs e)
        {
            if (Manager.Album.Count > 0)
            {
                string msg = string.Format("Are you sure you want to remove photo {0} from the album?", Manager.Current.Caption);

                DialogResult result = MessageBox.Show(msg, "Confirm Removal", MessageBoxButtons.OKCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.OK)
                {
                    Manager.Album.RemoveAt(Manager.Index);
                    DisplayAlbum();
                }
            }
        }

        private void menuViewNext_Click(object sender, EventArgs e)
        {
            if (Manager.Index < Manager.Album.Count - 1)
            {
                Manager.Index++;
                DisplayAlbum();
            }
        }

        private void menuViewPrevious_Click(object sender, EventArgs e)
        {
            if (Manager.Index > 0)
            {
                Manager.Index--;
                DisplayAlbum();
            }
        }

        private void ctxMenuPhoto_Opening(object sender, CancelEventArgs e)
        {
            menuViewNext.Enabled= (Manager.Index < Manager.Album.Count - 1);
            menuViewPrevious.Enabled = (Manager.Index > 0);
            menuPhotoProps.Enabled = (Manager.Current != null);
            menuAlbumProps.Enabled = (Manager.Album != null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SaveAndCloseAlbum() == false)
            {
                e.Cancel = true;
            }
        }

        private void menuViewPixelData_Click(object sender, EventArgs e)
        {
            if (PixelForm == null || PixelForm.IsDisposed)
            {
                PixelForm = new PixelDialog();
                PixelForm.Owner = this;

                PixelForm.Show();

                Point p = pbxPhoto.PointToClient(Form.MousePosition);
                UpdatePixelDialog(p.X, p.Y);
                
            }
        }

        private void UpdatePixelDialog(int x, int y)
        {
            if (PixelForm != null && PixelForm.Visible)
            {
                Bitmap bmp = (Bitmap) Manager.CurrentImage;
                PixelForm.Text = (Manager.Current == null ? "Pixel Data" : Manager.Current.Caption);

                if (bmp == null || !pbxPhoto. DisplayRectangle.Contains(x, y))
                    PixelForm.ClearPixelData();
                else 
                    PixelForm.UpdatePixelData(x, y, bmp, pbxPhoto.DisplayRectangle, new Rectangle(0, 0, bmp.Width, bmp.Height), pbxPhoto.SizeMode);
            }
        }

        private void pbxPhoto_MouseMove(object sender, MouseEventArgs e)
        {
            UpdatePixelDialog(e.X, e.Y);
        }

        private void menuPhotoProps_Click(object sender, EventArgs e)
        {
            if (Manager.Current == null)
                return;

            using (var dlg = new PhotoEditDialog(Manager))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DisplayAlbum();
                }
            }

        }

        private void menuAlbumProps_Click(object sender, EventArgs e)
        {
            if (Manager.Album == null)
                return;

            using (AlbumEditDialog dlg = new AlbumEditDialog(Manager))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DisplayAlbum();
                }
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '+':
                    menuViewNext.PerformClick();
                    e.Handled = true;
                    break;
                case '-':
                    menuViewPrevious.PerformClick();
                    e.Handled = true;
                    break;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.PageUp:
                    menuViewPrevious.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.PageDown:
                    menuViewNext.PerformClick();
                    e.Handled = true;
                    break;
            }
            
        }
    }
}
