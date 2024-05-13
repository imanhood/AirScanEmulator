using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirScanEmulator
{
    public class Hand : TouchPictureBox
    {
        public Hand(Panel panel, int handSize, Random rnd)
        {
            this.BackColor = Color.FromArgb(rnd.Next(100), rnd.Next(100), rnd.Next(100)); // Set your image here
            this.Size = new System.Drawing.Size(20 * handSize, 5 * handSize);
            this.Location = new System.Drawing.Point(rnd.Next(panel.Width - this.Width), rnd.Next(panel.Height - this.Height));
            this.Growing = false;
            this.MoveUp = false;
            this.MoveLeft = false;

            panel.Controls.Add(this);
        }
        public bool Growing { get; set; }
        public bool MoveUp { get; set; }
        public bool MoveLeft { get; set; }

        public async Task Animate(Rectangle panel, int handSize, int speed, Random rnd)
        {
            var hand = this;
            if (hand.Width <= 5 * handSize && hand.Growing == false)
            {
                hand.Growing = true;
                hand.Height = 20 * handSize;
            }

            if (hand.Width >= 20 * handSize && hand.Growing == true)
            {
                hand.Growing = false;
                hand.Height = 5 * handSize;
            }

            if (hand.Height < 5 * handSize)
                hand.Height = 5 * handSize;

            if (hand.Height > 20 * handSize)
                hand.Height = 20 * handSize;

            if (rnd.Next(100) < 2)
                hand.MoveUp = !hand.MoveUp;

            if (rnd.Next(100) < 2)
                hand.MoveLeft = !hand.MoveLeft;

            if (hand.Left <= 50)
            {
                hand.MoveLeft = false;
            }

            if (hand.Top <= 50)
            {
                hand.MoveUp = false;
            }

            if (hand.Left >= panel.Width - hand.Width - 50)
            {
                hand.MoveLeft = true;
            }

            if (hand.Top >= panel.Height - hand.Height - 50)
            {
                hand.MoveUp = true;
            }

            hand.Width += (hand.Growing ? 1 : -1) * speed;
            hand.Height += (hand.Growing ? -1 : 1) * speed;
            hand.Left += (hand.MoveLeft ? -1 : 1) * speed;
            hand.Top += (hand.MoveUp ? -1 : 1) * speed;
        }
    }
}
