using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public int Left//左
        {
            get
            {
                return left;
            }
            set
            {
                if (left >= 0 && left <= 12)
                    left = value;
            }
        }
        public int Top//上
        {
            get
            {
                return top;
            }
            set
            {
                if (top >= 0 && top <= 12)
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

        public Tank(int tank_type,int left,int top)//产生随机方向
        {
            Random r = new Random();
            this.direct = r.Next(0, 4); //0-3
            this.width = 30;
            this.height = 30;
            this.left = left;//0-9
            this.top = top;//0-9
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
            Image tank = Image.FromFile("image\\mytank0.png");
         
            if (type == 5)
            {
                switch (direct)
                {
                    case 0:
                        tank = Image.FromFile("image\\tank0.png");
                        break;
                    case 1:
                        tank = Image.FromFile("image\\tank1.png");
                        break;
                    case 2:
                        tank = Image.FromFile("image\\tank2.png");
                        break;
                    case 3:
                        tank = Image.FromFile("image\\tank3.png");
                        break;
                }
            }
            if (type == 6)
            {
                switch (direct)
                {
                    case 0:
                        tank = Image.FromFile("image\\mytank0.png");
                        break;
                    case 1:
                        tank = Image.FromFile("image\\mytank1.png");
                        break;
                    case 2:
                        tank = Image.FromFile("image\\mytank2.png");
                        break;
                    case 3:
                        tank = Image.FromFile("image\\mytank3.png");
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
            Image tank = Image.FromFile(@"image\\base2.png");
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
            //if (this.type == 6)
            //    Playsound.Play("Sound/attack.wav");
        }

        public void MoveBullet(ref int[,] Map)
        {
            for (int i = bList.Count - 1; i>= 0; i--)//遍历子弹序列
            {
                bullet t = ((bullet)bList[i]);
                //移动以前
                if (t.Left < 0 || t.Left > 12 || t.Top < 0 || t.Top > 12)//超出边界
                {
                    bList.RemoveAt(i);
                    Playsound.Play("Sound/buttleCrack.wav");
                    continue;
                }
                if (t.hitWall(Map[t.Top, t.Left]))
                {
                    bList.RemoveAt(i);
                    Playsound.Play("Sound/buttleCrack.wav");
                    Map[t.Top, t.Left] = 0;
                    continue;
                }
                if (Map[t.Top, t.Left] != 0 && Map[t.Top, t.Left] != this.type)
                {
                    bList.RemoveAt(i);
                    Playsound.Play("Sound/buttleCrack.wav");
                    if (t.hitE(Map[t.Top, t.Left]))
                        Map[t.Top, t.Left] = -1;
                    continue;
                }
                
                t.move();

                //移动以后
                if (t.Left < 0 || t.Left > 12 || t.Top < 0 || t.Top > 12)//超出边界
                {
                    bList.RemoveAt(i);
                    Playsound.Play("Sound/buttleCrack.wav");
                    continue;
                }
                if (Map[t.Top, t.Left] != 0)//遇到物体
                {
                    bList.RemoveAt(i);
                    if (t.hitWall(Map[t.Top, t.Left]))
                    {
                        Playsound.Play("Sound/buttleCrack.wav");
                        Map[t.Top, t.Left] = 0;
                    }
                    else if (t.hitE(Map[t.Top, t.Left]))
                    {
                        Map[t.Top, t.Left] = -1;
                    }
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
