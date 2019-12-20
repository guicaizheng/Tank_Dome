using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank_Dome.Model;

namespace Tank_Dome
{
    class Tank
    {
        private int width;      //坦克宽度
        private int height;     //坦克高度
        private int left;       //坦克位置的横坐标
        private int top;        //坦克位置的纵坐标
        private int type;       //坦克的类型
        private int direct=0;     //方向：0:上  1:下  2:左  3:右
        public ArrayList bList = new ArrayList();   //子弹序列

        public int Left
        {
            get
            {
                return left;
            }
            set
            {
                if (left >= 0 && left <= 9)
                    left = value;
            }
        }
        public int Top
        {
            get
            {
                return top;
            }
            set
            {
                if (top >= 0 && top <= 9)
                    top = value;
            }
        }
        public int Type//坦克类型
        {
            get
            {
                return type;
            }
            set
            {
                if (top >= 1 && top <= 5)
                    type = value;
            }
        }
        public int Direct { get => direct; set => direct = value; }//坦克方向

        public Tank(int tank_type)//产生随机方向
        {
            Random r = new Random();
            this.direct = r.Next(0, 4); //0-3
            this.width = 60;
            this.height = 60;
            this.left = r.Next(0, 10);  //0-9
            this.top = r.Next(0, 10);   //0-9
            this.type = tank_type;
        }

        public void newDirect()//碰到障碍物时 得到新方向
        {
            Random r = new Random();
            int new_Direct = r.Next(0, 4);
            while (this.direct == new_Direct)
                new_Direct = r.Next(0, 4);
            this.direct = new_Direct;
        }

        public void Draw(Graphics g, int type)
        {
            Image tank = Image.FromFile("source\\mytank0.png");
         
            if (type == 2) tank = Image.FromFile(@"2.jpg");
            if (type == 3) tank = Image.FromFile(@"2.jpg");
            if (type == 4) tank = Image.FromFile(@"2.jpg");
            if (type == 5) tank = Image.FromFile(@"2.jpg");
            if (type == 6)
            {
                switch (direct)
                {
                    case 0:
                        tank = Image.FromFile("source\\mytank0.png");
                        break;
                    case 1:
                        tank = Image.FromFile("source\\mytank1.png");
                        break;
                    case 2:
                        tank = Image.FromFile("source\\mytank2.png");
                        break;
                    case 3:
                        tank = Image.FromFile("source\\mytank3.png");
                        break;
                }
            }
            Rectangle destRect = new Rectangle(this.left * width, this.top * height, width, height);
            Rectangle srcRect = new Rectangle(0, 0, width, height);
            g.DrawImage(tank, destRect, srcRect, GraphicsUnit.Pixel);
        }

        public void Explore(Graphics g)
        {
            Rectangle destRect = new Rectangle(this.left * width, this.top * height, width, height);
            Rectangle srcRect = new Rectangle(0, 0, width, height);
            Image tank = Image.FromFile(@"F:\.NET\Coursedesign\Tank_Dome\Tank_Dome\source\0.jpg");
            g.DrawImage(tank, destRect, srcRect, GraphicsUnit.Pixel);

            //PlaySound.Play("Sound/Explode.wav");

        }

        public void fire()
        {
            bullet b = new bullet(this.type);
            b.Direct = this.Direct;
            b.Top = this.Top;
            b.Left = this.Left;
            bList.Add(b);
            //if (this.type == 6) PlaySound.Play("Sound/Shoot.wav");
        }

        public void MoveBullet(ref int[,] Map)
        {
            for (int i = bList.Count - 1; i>= 0; i--)//遍历子弹序列
                                                      //for(int i= 0; i<bList.ComCount;i++ )
            {
                bullet t = ((bullet)bList[i]);
                //移动以前
                if (t.Left < 0 || t.Left > 9 || t.Top < 0 || t.Top > 9)
                //超出边界
                {
                    bList.RemoveAt(i); continue;
                }
                if (Map[t.Left, t.Top] != 0 && Map[t.Left, t.Top] != this.type)
                {
                    bList.RemoveAt(i);
                    if (t.hitE(Map[t.Left, t.Top]))
                        Map[t.Left, t.Top] = -1;
                    continue;
                }
                t.move();
                if (t.Left < 0 || t.Left > 9 || t.Top < 0 || t.Top > 9)
                //超出边界
                {
                    bList.RemoveAt(i); continue;
                }
                if (Map[t.Left, t.Top] != 0)
                //遇到物体
                {
                    bList.RemoveAt(i);
                    if (t.hitE(Map[t.Left, t.Top]))
                        Map[t.Left, t.Top] = -1;
                    continue;
                }
            }
        }

        public void DrawBullet(Graphics g, int[,] Map)
        {
            MoveBullet(ref Map);
            foreach (bullet t in bList)
                t.Draw(g);
        }
    }
}
