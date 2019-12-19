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
            test();
        }
        public void test()
        {
            Graphics g;
            MessageBox.Show("1");
            g = this.CreateGraphics();
            Image tank = Image.FromFile("0.jpg");
            pictureBox1.Image = tank;
            Rectangle a = new Rectangle(100, 100, 100, 100);
            Rectangle b = new Rectangle(100, 100, 100, 100);
            Point p = new Point(100, 100);
            g.DrawImage(tank,p);
            
        }

    }
}
