using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank_Dome.Model;

namespace Tank_Dome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int eCount = 0;                 //敌方坦克数量
        private int eMaxCount = 10;             //敌方坦克的最大数量
        private string path;                    //应用程序路径
        private Tank eTank;
        private ArrayList eTanks = new ArrayList(10);

        private Tank MyTank = new Tank(6);      //己方坦克为6
        private int width = 60;
        private int[,] Map = new int[10, 10];   //墙砖地图
        private int[,] TMap = new int[10, 10];  //坦克，墙砖地图
        private int Score = 0;

        private void Form1_Load(object sender, System.EventArgs e)
        {
            pictureBox1.Width = 10 * width;
            pictureBox1.Height = 10 * width;
            path = Application.StartupPath;
            Random r = new Random();
            for(int x = 0; x < 10; x += 2)
                for(int y = 0; y < 10; y += 2)
                {
                    //0表示空地,1表示墙砖
                    Map[x, y] = r.Next(0, 2);
                }
            Map[4, 9] = 0;
            MyTank.Top = 9;
            MyTank.Left = 4;
            MyTank.Direct = 0;
            //DragWall(pictureBox1.CreateGraphics());
        }

        private void DragWall(Graphics g)   //画游戏地图
        {
            Image Wallimage = Image.FromFile(@"F:\College_Data\.Net\Tank_Dome\Tank_Dome\Tank_Dome\source\Map.png");
            Point p = new Point(0, 0);      //************
            g.DrawImage(Wallimage,p);
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                {
                    if(Map[x,y]==1)
                    {
                        //绘制这个墙砖块的矩形区域
                        Rectangle Rect = new Rectangle(x * width, y * width, width, width);
                        g.DrawImage(Wallimage, Rect);
                    }
                }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)   //方向键盘控制
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (MyTank.Top == 0 || Map[MyTank.Left, MyTank.Top - 1] == 1
                    || Meet_Tank(MyTank.Left, MyTank.Top - 1))      //遇到墙砖或坦克
                        ;                                           //不动
                    if (MyTank.Direct == 0)
                        MyTank.Top--;
                    MyTank.Direct = 0;
                    break;
                case Keys.Down:                                     //下
                    if (MyTank.Top == 9 || Map[MyTank.Left, MyTank.Top + 1] == 1
                    || Meet_Tank(MyTank.Left, MyTank.Top + 1))      //遇到墙砖或坦克
                        ;                                           //不动
                    else if (MyTank.Direct == 1)
                        MyTank.Top++;
                    MyTank.Direct = 1;
                    break;
                case Keys.Left:                                     //左
                    if(MyTank.Left == 0 || Map[MyTank.Left - 1, MyTank.Top] == 1
                    || Meet_Tank(MyTank.Left - 1, MyTank.Top))	    //遇到墙砖或坦克
				        ;										    //不动
			else if (MyTank.Direct == 2)
                        MyTank.Left--;
                    MyTank.Direct = 2;
                    break;
                case Keys.Right:                                    //右
                    if (MyTank.Left == 9 || Map[MyTank.Left + 1, MyTank.Top] == 1
                    || Meet_Tank(MyTank.Left + 1, MyTank.Top))      //遇到墙砖或坦克
                        ;                                           //不动
                    else if (MyTank.Direct == 3)
                        MyTank.Left++;
                    MyTank.Direct = 3;
                    break;
                case Keys.Space:                                    //按空格键发射子弹			
                    MyTank.fire();
                    break;
            }
            pictureBox1.Invalidate();                            //重画游戏面板区域
            //lblX.Text = "X坐标:" + MyTank.Left + " Y坐标:" + MyTank.Top;
        }

        private bool Meet_Tank(int left, int top)               //判断某坐标处是否有坦克
        {
            foreach(Tank t in eTanks)                           //遍历地方
            {
                if (left == t.Left && top == t.Top)             //遇到坦克
                    return true;
            }
            if (left == MyTank.Left && top == MyTank.Top)       //遇到游戏方坦克
                return true;
            return false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //修改含坦克信息的地图
            for(int x = 0; x < 10; x++)
                for(int y = 0; y < 10; y++)
                {
                    if (Map[x, y] == 1)
                        TMap[x, y] = 1;         //墙砖
                    else
                        TMap[x, y] = 0;         //0表示空地
                }

            for(int i = 0; i < eTanks.Count; i++)
            {
                if (eTanks[i] != null)
                {
                    int x = ((Tank)eTanks[i]).Left;
                    int y = ((Tank)eTanks[i]).Top;
                    TMap[x, y] = ((Tank)eTanks[i]).Type;    //此处为敌方坦克
                }
            }
            TMap[MyTank.Left, MyTank.Top] = MyTank.Type;    //此处为己方坦克(6)

            //重画游戏界面
            DragWall(e.Graphics);                           //画墙砖
            for(int i = 0; i < eTanks.Count; i++)           //画敌方坦克及子弹
            {
                if (eTanks[i] != null)
                {
                    Tank t = (Tank)eTanks[i];
                    t.Draw(e.Graphics, t.Type);
                    t.DrawBullet(e.Graphics, TMap);
                }
            }
            MyTank.Draw(e.Graphics, MyTank.Type);           //画己方坦克
            MyTank.DrawBullet(e.Graphics, TMap);            //画己方子弹
            //处理爆炸
            for(int i = 0; i< eTanks.Count; i++)            //画敌方坦克爆炸
            {
                if (eTanks[i] != null)
                {
                    Tank t = (Tank)eTanks[i];
                    if (TMap[t.Left, t.Top] == -1)
                    {
                        t.Explore(e.Graphics);
                        eTanks.RemoveAt(i);
                        i--;
                        TMap[t.Left, t.Top] = 0;
                        Score += 100;
                    }
                }
            }
            if(TMap[MyTank.Left,MyTank.Top] == -1)
            {
                MyTank.Explore(e.Graphics);
                TMap[MyTank.Left, MyTank.Top] = 0;
                timer1.Enabled = false;              //游戏结束
            }
            CheckWin();
        }


        private void CheckWin()
        {
            if(eTanks.Count == 0 && eCount == eMaxCount)     //胜利
            {
                //PlaySound.Play("aaaaaa");                   //过关后播放音乐
                Console.WriteLine("过关！");
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Tank t in eTanks)
            {
                switch (t.Direct)           //0表示上，1表示下,2表示左,3表示右
                {
                    case 0:                 //向上
                    if (t.Top == 0 || Map[t.Left,t.Top - 1] == 1
                    || Meet_Tank(t.Left, t.Top - 1)) //遇到墙砖或坦克
                        t.newDirect();              //坦克转向
                    else
                        t.Top--;
                    break;
                    case 1 :                                //向下
                        if (t.Top == 9 || Map[t.Left, t.Top + 1] == 1
                        || Meet_Tank(t.Left, t.Top + 1))    //遇到墙砖或坦克
                            t.newDirect();                  //坦克转向
                        else 
                            t.Top++;
                        break;
                    case 2:                                 //向左
                        if (t.Left == 0 || Map[t.Left - 1, t.Top] == 1
                         | Meet_Tank(t.Left - 1, t.Top))    //遇 到墙砖或坦克
                            t.newDirect();                  //坦克转向
                        else
                            t.Left--;
                        break;
                    case 3:                                 //向右
                        if (t.Left == 9 || Map[t.Left + 1, t.Top] == 1
                        || Meet_Tank(t.Left + 1, t.Top)) //遇到墙砖或坦克
                            t.newDirect();            //坦克转向
                        else
                            t.Left++;
                        break;
                }
                Random r = new Random();
                int fire_bool = r.Next(0, 8);           //0-7
                if (fire_bool == t.Direct)
                    t.fire();
            }
            pictureBox1.Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)    //定时产生新的敌方坦克
        {
            if (eCount < eMaxCount)                             //敌方坦克最大值
            {
                eTank = new Tank(3);
                eTanks.Add(eTank);
                eCount++;
            }
            else
                timer2.Enabled = false;                         //不在产生新的敌方坦克
        }
    }
}
