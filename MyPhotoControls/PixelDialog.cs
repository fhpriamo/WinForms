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

namespace MyPhotoControls
{
    public partial class PixelDialog : Form
    {
        public PixelDialog()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ClearPixelData()
        {
            SetPixelData(0, 0, 0, 0, 0);
        }

        private void SetPixelData(int x, int y, int red, int green, int blue)
        {
            lblX.Text = x.ToString();
            lblY.Text = y.ToString();
            lblRed.Text = red.ToString();
            lblGreen.Text = green.ToString();
            lblBlue.Text = blue.ToString();
        }

        public void UpdatePixelData(int xPos, int yPos, Bitmap bmp, // displayed bitmap
            Rectangle displayRect, // area of display
            Rectangle bmpRect, // area of bitmap
            PictureBoxSizeMode sizeMode)
        {
            // Determine (x,y) position within image
            int x = 0, y = 0;
            switch (sizeMode)
            {
                case PictureBoxSizeMode.AutoSize:
                case PictureBoxSizeMode.CenterImage:
                    throw new NotSupportedException("The AutoSize and CenterImage size modes are not supported at this time.");
                case PictureBoxSizeMode.Normal:
                    // Rectangle coords are image coords
                    if (xPos >= bmp.Width || yPos >= bmp.Height)
                        return; // position outside image
                    x = xPos - bmpRect.X;
                    y = yPos - bmpRect.Y;
                    break;
                case PictureBoxSizeMode.Zoom:
                    // Determine image rectangle
                    Rectangle r2 = ImageUtility.ScaleToFit(bmp, displayRect);
                    if (!r2.Contains(xPos, yPos))
                        return; // position outside image
                    x = (xPos - r2.Left) * bmp.Width / r2.Width;
                    y = (yPos - r2.Top) * bmp.Height / r2.Height;
                    break;
            }

            // Extract color at calculated position
            Color c = bmp.GetPixel(x, y);
            // Update dialog values
            SetPixelData(x, y, c.R, c.G, c.B);
        }
    }
}
