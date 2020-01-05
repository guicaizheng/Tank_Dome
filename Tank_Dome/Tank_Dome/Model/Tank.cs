using System;
using System.Collections;
using System.Drawing;
using Tank_Dome.Model;

namespace Tank_Dome
{
    class Tank
    {
        private int width;          //坦克宽度
        private int height;         //坦克高度
        private int left;           //坦克位置的横坐标
        private int top;            //坦克位置的纵坐标
        private int type;           //坦克的类型
        private int life=2;         //坦克的生命
        private int buttle_num = 0; //子弹的数量
        private int direct=0;       //方向：0:上  1:下  2:左  3:右
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
                if (type >= 5 && type <= 8)
                    type = value;
            }
        }
        public int Direct { get => direct; set => direct = value; }//坦克方向
        public int Buttle_num { get => buttle_num; set => buttle_num = value; }
        public int Life { get => life; set => life = value; }

        public Tank(int tank_type, int left, int top)//产生随机方向
        {
            Random r = new Random();
            this.direct = r.Next(0, 4); //0-3
            this.width = 30;
            this.height = 30;
            this.left = left;
            this.top = top;
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

        public void Explore(Graphics g)                 //爆炸
        {
            Playsound.Play("Sound/tankCrack.wav");
            Rectangle destRect = new Rectangle(this.left * width, this.top * height, width, height);
            Rectangle srcRect = new Rectangle(0, 0, width, height);
            Image tank = Image.FromFile(@"image\\explore.png");
            g.DrawImage(tank, destRect, srcRect, GraphicsUnit.Pixel);
        }

        public void fire()
        {
            bullet b = new bullet(this.type);
            b.Direct = this.Direct;
            b.Top = this.Top;
            b.Left = this.Left;
            bList.Add(b);
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
            this.buttle_num--;
            Playsound.Play("Sound/buttleCrack.wav");
            continue;
        }
        if (t.hitWall(Map[t.Top, t.Left]))
        {
            bList.RemoveAt(i);
            this.buttle_num--;
            Playsound.Play("Sound/buttleCrack.wav");
            if (Map[t.Top, t.Left] == 9)            //基地爆炸
            {
                Map[t.Top, t.Left] = 10;
            }
            else                                    //砖墙被打掉
            {
                Map[t.Top, t.Left] = 0;
            }
            continue;
        }
        if (Map[t.Top, t.Left] != 0 && Map[t.Top, t.Left] != this.type)
        {
            bList.RemoveAt(i);
            this.buttle_num--;
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
            this.buttle_num--;
            Playsound.Play("Sound/buttleCrack.wav");
            continue;
        }
        if (Map[t.Top, t.Left] != 0)//遇到物体
        {
            bList.RemoveAt(i);
            this.buttle_num--;
            if (t.hitWall(Map[t.Top, t.Left]))
            {
                Playsound.Play("Sound/buttleCrack.wav");
                if (Map[t.Top, t.Left] == 9)            //基地爆炸
                {
                    Map[t.Top, t.Left] = 10;
                }
                else                                    //砖墙被打掉
                {
                    Map[t.Top, t.Left] = 0;
                }
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
