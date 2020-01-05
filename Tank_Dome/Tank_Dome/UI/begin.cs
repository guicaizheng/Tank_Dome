using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank_Dome
{
    public partial class begin : Form
    {
        public begin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(1);
            f.ShowDialog(this);
            this.Close();
        }

        private void level1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(1);
            f.ShowDialog(this);
            this.Close();
        }

        private void level2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(2);
            f.ShowDialog(this);
            this.Close();
        }

        private void level3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(3);
            f.ShowDialog(this);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("W:前进,S:后退,A:向左,D:向右,Space:发射子弹");
        }

        //private void button2_Click(object sender, EventArgs e)
        //{

        //    float[] time = new float[4];
        //    StreamReader r = new StreamReader("time.txt");
        //    int i = 0;
        //    while (float.TryParse(r.ReadLine(), out time[i]))
        //    {
        //        i++;
        //    }
        //    r.Close();

        //    MessageBox.Show(time[0].ToString()+ "\n" + time[1].ToString()+"\n"+ time[2].ToString()+"\n");
        //}
    }
}
