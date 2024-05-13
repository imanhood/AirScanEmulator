using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirScanEmulator
{
    public class TouchPictureBox : PictureBox
    {
        private bool isDragging = false;
        private Point offset;
        public int HorizontalCenter => this.Left + this.Width / 2;
        public int VerticalCenter => this.Top + this.Height / 2;
        public TouchPictureBox()
        {
            this.MouseDown += TouchPictureBox_MouseDown;
            this.MouseMove += TouchPictureBox_MouseMove;
            this.MouseUp += TouchPictureBox_MouseUp;
        }
        private void TouchPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging
            isDragging = true;
            offset = e.Location;
        }

        private void TouchPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Move the PictureBox if dragging
            if (isDragging)
            {
                PictureBox pb = sender as PictureBox;
                pb.Left = e.X + pb.Left - offset.X;
                pb.Top = e.Y + pb.Top - offset.Y;
            }
        }

        private void TouchPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging
            isDragging = false;
        }
    }
}
