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
        private Tank mytank = new Tank(3);
        private int width = 32;
        private int[,] Map = new int[10, 10];
        private int[,] TMap = new int[10, 10];
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Width = 10 * width;
            pictureBox1.Height = 10 * width;
            Random r = new Random();
            for(int x=0;x<10;x+=2)
                for(int y=0;y<10;y+=2)
                {
                    //0,1
                    Map[x, y] = r.Next(0, 2);
                }
            Map[4, 9] = 0;
            mytank.Top = 9;
            mytank.Left = 4;
            mytank.Direct = 0;
            DragWall(pictureBox1.CreateGraphics());
        }
        private void DragWall(Graphics g)
        {
            Image Wallimage = Image.FromFile("0.jpg");
            Point p = new Point(0, 0);
            g.DrawImage(Wallimage,p);
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                {
                    if(Map[x,y]==1)
                    {
                        Rectangle Rect = new Rectangle(x * width,y * width,width,width);
                        g.DrawImage(Wallimage,Rect);
                    }
                }
        }



    }
}
