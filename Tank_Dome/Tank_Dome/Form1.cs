using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank_Dome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void a(Graphics g)
        {
            Image a = Image.FromFile(@"E:\图片\465.png");

            Rectangle destRect = new Rectangle(100, 100, 300, 200);
            Rectangle srcRect = new Rectangle(50, 50, 800, 500);

            Point p = new Point(100,100);
            //g.DrawImage(a, destRect, srcRect, GraphicsUnit.Pixel);
            g.DrawImage(a, 70, 70, 200, 200);
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            a(e.Graphics);
            
        }

    }
}
