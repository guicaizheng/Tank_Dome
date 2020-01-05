﻿using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tank_Dome.Model;

namespace Tank_Dome
{
    public partial class Form1 : Form, Key
    {

        private int level = 0;
        private MapModel Map;
        public Form1(int maptype)
        {
            InitializeComponent();
            level = maptype;
            Map = new MapModel(level); //墙砖地图
        }
        
        private int eCount = 0;                 //敌方坦克数量
        private int eMaxCount = 5;              //敌方坦克的最大数量
        private string path;                    //应用程序路径
        private Tank eTank;
        private ArrayList eTanks = new ArrayList();
        private Wall wall=new Wall();
        private Tank MyTank = new Tank(6,4,12);  //己方坦克为6
        private int width = 30;
        
        private int[,] TMap = new int[13, 13];  //坦克，墙砖地图
        private int Score = 0;                  //分数
        private DateTime dt1;
        private DateTime dt2;

        private void Form1_Load(object sender, System.EventArgs e)
        {
            pictureBox1.Width = 13 * width;
            pictureBox1.Height = 13 * width;
            path = Application.StartupPath;
            Map.Map1[4, 12] = 0;
            MyTank.Top = 12;
            MyTank.Left = 4;
            MyTank.Direct = 0;
            label1.Text = "X:" + MyTank.Left + "Y:" + MyTank.Top;
            dt1 = System.DateTime.Now;
            Playsound.Play("Sound/start.wav");
        }

        private void DragWall(Graphics g)   //画游戏地图
        {
            Image Wallimage;
            for (int x = 0; x < 13; x++)
                for (int y = 0; y < 13; y++)
                {
                    if(Map.Map1[x,y]>=1)
                    {
                        //绘制这个墙砖块的矩形区域
                        Wallimage=wall.CreateWall(Map.Map1[x, y]);
                        Rectangle Rect = new Rectangle(y * width, x * width, width, width);
                        g.DrawImage(Wallimage, Rect);
                    }
                }
        }
        
    public void Form1_KeyDown(object sender, KeyEventArgs e)   //方向键盘控制
    {
        switch (e.KeyCode)
        {
            case Keys.W:
                if (MyTank.Top == 0 || Isgo(MyTank.Top - 1,MyTank.Left) == 0
                || Meet_Tank(MyTank.Left, MyTank.Top - 1))  //遇到墙砖或坦克
                {  
                    MyTank.Direct = 0;                      //不动,但是转向
                }
                else
                {
                    MyTank.Top--;
                    MyTank.Direct = 0;
                }  
                break;
            case Keys.S:                                    //下
                    if (MyTank.Top == 12 || Isgo(MyTank.Top + 1, MyTank.Left) == 0
                    || Meet_Tank(MyTank.Left, MyTank.Top + 1))  //遇到墙砖或坦克
                    {
                        MyTank.Direct = 1;                      //不动,但是转向
                    }
                    else
                    {
                        MyTank.Top++;
                        MyTank.Direct = 1;
                    }
                    break;
                case Keys.A:                                    //左
                    if(MyTank.Left == 0 || Isgo(MyTank.Top , MyTank.Left - 1) == 0
                    || Meet_Tank(MyTank.Left - 1, MyTank.Top))  //遇到墙砖或坦克
                    {
                        MyTank.Direct = 2;                      //不动,但是转向
                    }
                    else
                    {
                        MyTank.Left--;
                        MyTank.Direct = 2;
                    } 
                    break;
                case Keys.D:                                    //右
                    if (MyTank.Left == 12 || Isgo(MyTank.Top, MyTank.Left + 1) == 0
                    || Meet_Tank(MyTank.Left + 1, MyTank.Top))  //遇到墙砖或坦克
                    {
                        MyTank.Direct = 3;                      //不动,但是转向
                    }
                    else
                    {
                        MyTank.Left++;
                        MyTank.Direct = 3;
                    }
                    break;
                case Keys.Space:                                //按空格键发射子弹
                    if (MyTank.Buttle_num == 0)                 //自己的子弹在场上只能同时存在一颗
                    {
                        MyTank.Buttle_num++;
                        Playsound.Play("Sound/attack.wav");
                        MyTank.fire();
                    }
                    break;
                case Keys.P:
                    label1.Text=eTanks.Count.ToString();
                    break;
            }
            pictureBox1.Invalidate();                            //重画游戏面板区域
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

        private int Isgo(int top,int left)
        {
            int ISgo=1;
            switch (Map.Map1[top,left])
            {
                case 1:
                    ISgo = 0;
                    break;
                case 2:
                    ISgo = 0;
                    break;
                case 4:
                    ISgo = 0;
                    break;
            }
            return ISgo;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            label3.Text = "关卡:" + level;
            label4.Text = "敌方坦克数量:" + eTanks.Count;
            label1.Text = "X坐标:" + MyTank.Left + " Y坐标:" + MyTank.Top;
            label2.Text = "生命值:" + MyTank.Life;

            //修改含坦克信息的地图
            for (int x = 0; x < 13; x++)
                for(int y = 0; y < 13; y++)
                {
                    if (Map.Map1[x, y] == 1)
                        TMap[x, y] = 1;         //墙砖
                    else if (Map.Map1[x, y] == 2)
                        TMap[x, y] = 2;
                    else if (Map.Map1[x, y] == 9)
                        TMap[x, y] = 9;         //基地
                    else
                        TMap[x, y] = 0;         //0表示空地
                }

            for (int i = 0; i < eTanks.Count; i++)
            {
                if (eTanks[i] != null)
                {
                    int x = ((Tank)eTanks[i]).Left;
                    int y = ((Tank)eTanks[i]).Top;
                    TMap[y, x] = ((Tank)eTanks[i]).Type;    //此处为敌方坦克
                }
            }
            TMap[MyTank.Top,MyTank.Left] = MyTank.Type;    //此处为己坦克(6)

            //重画游戏界面           
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

            for(int i = 0; i < 13; i++)
            {
                for(int j = 0; j < 13; j++)
                {
                    if(Map.Map1[i,j] == 1 && TMap[i, j] == 0)
                    {
                        Map.Map1[i, j] = 0;
                    }
                    else if (Map.Map1[i, j] == 9 && TMap[i, j] == 10)
                    {
                        Map.Map1[i, j] = 10;
                    }
                }
            }
            DragWall(e.Graphics);                            //画墙砖

            //处理爆炸
            for (int i = 0; i< eTanks.Count; i++)            //画敌方坦克爆炸
            {
                if (eTanks[i] != null)
                {
                    Tank t = (Tank)eTanks[i];
                    if (TMap[t.Top,t.Left] == -1)
                    {
                        t.Explore(e.Graphics);
                        eTanks.RemoveAt(i);
                        i--;
                        TMap[t.Top,t.Left] = 0;
                        Score += 100;
                        CheckWin();
                    }
                }
            }

            if(TMap[MyTank.Top,MyTank.Left] == -1)          //画己方坦克爆炸
            {
                if (MyTank.Life > 0)
                {
                    MyTank.Life--;
                }
                if (MyTank.Life == 0)
                {

                    pictureBox1.Invalidate();
                    MyTank.Explore(e.Graphics);
                    TMap[MyTank.Top, MyTank.Left] = 0;
                    Playsound.Play("Sound/playerCrack.wav");
                    //timer1.Enabled = false;
                    //timer3.Enabled = false;
                    System.Threading.Thread.Sleep(1000);
                    MessageBox.Show("游戏结束!");

                    this.Hide();
                    begin f = new begin();
                    f.ShowDialog();
                    this.Close();
                }
            }
            if (TMap[12, 6] == 10)         //基地爆炸
            {

                Playsound.Play("Sound/playerCrack.wav");
                pictureBox1.Invalidate();
                MessageBox.Show("基地爆炸");
                this.Hide();
                begin b = new begin();
                b.ShowDialog(this);
                this.Close();
            }
        }

        private void CheckWin()
        {
            if(eTanks.Count == 0 && eCount == eMaxCount)     //胜利
            {
                pictureBox1.Invalidate();
                Playsound.Play("Sound/prop.wav");          //过关后播放音乐
                //过关用时
                dt2 = System.DateTime.Now;
                TimeSpan ts = dt2.Subtract(dt1);

                StreamWriter f = new StreamWriter("time.txt");
                f.WriteLine((ts.TotalMilliseconds/1000).ToString());
                f.Close();
                MessageBox.Show("过关用时:" + (ts.TotalMilliseconds/1000).ToString());
                level++;
                Nextlevel();
            }
        }

        private void Nextlevel()        //下一关
        {
            if (level > 3)
            {
                MessageBox.Show("你已经通关!");
                StreamWriter f = new StreamWriter("time.txt");
                f.Write("\n");
                f.Close();
                this.Hide();
                begin b = new begin();
                b.ShowDialog(this);
                this.Close();
            }
            else
            {
                this.Hide();
                Form1 f = new Form1(level);
                f.ShowDialog(this);
                this.Close();
            }
        }
        private void paixu()
        {
            float[] time = new float[4];
            //StreamReader r = new StreamReader("time.txt");
            //int i = 0;
            //float temp;
            //while(float.TryParse(r.ReadLine(),out time[i]))
            //{
            //    i++;
            //}
            //r.Close();

            //for(int j = 0; j < 4 - 1; j++)
            //{
            //    for(int k = i; k < 4 - 1 - j; k++)
            //    {
            //        if (time[k] < time[k + 1])
            //        { /* 相邻元素比较，若逆序则交换（升序为左大于右，降序反之） */
            //            temp = time[k];
            //            time[k] = time[k + 1];
            //            time[k + 1] = temp;
            //        }
            //    }
            //}
            StreamWriter f = new StreamWriter("time.txt");
            f.WriteLine(time[0].ToString());
            f.WriteLine(time[1].ToString());
            f.WriteLine(time[2].ToString());
            f.Close();
        }
        private void timer1_Tick(object sender, EventArgs e)       //敌方坦克的行动
        {
            foreach (Tank t in eTanks)
            {
                switch (t.Direct)                           //0表示上,1表示下,2表示左,3表示右
                {
                    case 0:                                 //向上
                    if (t.Top == 0 || Isgo(t.Top - 1,t.Left) == 0
                    || Meet_Tank(t.Left, t.Top - 1))        //遇到墙砖或坦克
                        t.newDirect();                      //坦克转向
                    else
                        t.Top--;
                    break;
                    case 1 :                                //向下
                        if (t.Top == 12|| Isgo(t.Top + 1,t.Left) == 0
                        || Meet_Tank(t.Left, t.Top + 1))    //遇到墙砖或坦克
                            t.newDirect();                  //坦克转向
                        else 
                            t.Top++;
                        break;
                    case 2:                                 //向左
                        if (t.Left == 0 || Isgo(t.Top,t.Left - 1) == 0
                         | Meet_Tank(t.Left - 1, t.Top))    //遇到墙砖或坦克
                            t.newDirect();                  //坦克转向
                        else
                            t.Left--;
                        break;
                    case 3:                                 //向右
                        if (t.Left == 12 || Isgo(t.Top,t.Left + 1) == 0
                        || Meet_Tank(t.Left + 1, t.Top))    //遇到墙砖或坦克
                            t.newDirect();                  //坦克转向
                        else
                            t.Left++;
                        break;
                }
                Random r = new Random();
                int fire_bool = r.Next(0, 6);               //0-6
                if (fire_bool == t.Direct)                  //与坦克的方向一致时发射子弹
                    t.fire();
            }
            pictureBox1.Invalidate();                       //重绘游戏画面
        }

        private void timer2_Tick(object sender, EventArgs e)    //定时产生新的敌方坦克
        {
            int left, top;
            Random r = new Random();
            if (eCount < eMaxCount)                             //敌方坦克最大值
            {
                left = r.Next(0, 12);  //0-12
                top = r.Next(0,12);   //0-12
                if (Map.Map1[top,left] == 0)
                {
                    eTank = new Tank(5, left, top);
                    eTanks.Add(eTank);
                    eCount++;
                }
            }
            else
                timer2.Enabled = false;
        }

        private void timer3_Tick(object sender, EventArgs e)    //定时绘制地图
        {
            pictureBox1.Invalidate();
        }
    }
}
