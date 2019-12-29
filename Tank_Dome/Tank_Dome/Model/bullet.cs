using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_Dome.Model
{
    class bullet
    {
        private int top;            //子弹坐标(Left,Top)
        private int left;
        private int direct;         //子弹的行进方向
        private int width = 30;
        private int height = 30;
        private bool type;          //已方子弹为true,敌方子弹为false

        public bullet(int type)     //子弹类构造函数
        {
            
            if (type == 6)          //己方
            {
                this.type = true;
            }
            else
                this.type = false;  //敌方
        }

        public int Top { get => top; set => top = value; }
        public int Left { get => left; set => left = value; }
        public int Direct { get => direct; set => direct = value; }

        public void Draw(Graphics g)
        {
            Image bulletImage;
            if (type == true)
                bulletImage = Image.FromFile(@"image\\tankbuttle.png");  //己方子弹
            else
                bulletImage = Image.FromFile(@"image\\enemybuttle.png"); //敌方子弹

            //得到绘制这个子弹图形的矩形区城
            Rectangle destRect = new Rectangle(left * width, top * height, width, height);
            Rectangle srcRect = new Rectangle(0, 0, width, height);
            g.DrawImage(bulletImage, destRect, srcRect, GraphicsUnit.Pixel);
        }

        public void move()
        {
            switch (Direct)
            {
                case 0:
                    Top--;break;
                case 1:
                    Top++;break;
                case 2:
                    Left--;break;
                case 3:
                    Left++;break;
            }
        }

        public bool hitE(int tanktype)  //是否击中敌方坦克
        {
            if (type == false)          //敌方子弹
                if (tanktype >= 2 && tanktype <= 5)     //坦克类型(2-5为敌方,6为己方)
                    return false;
                else
                    return true;

            if (type == true)           //己方子弹
                if (tanktype == 6)
                    return false;
                else
                    return true;

            return false;
        }

        public bool hitWall(int map)
        {
            bool hitflag=false;
            if(map == 1 || map == 9)
            {
                hitflag = true;
            }
            return hitflag;
        }
    }
}