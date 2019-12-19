using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_Dome
{
    class Tank
    {
        private int width;      //坦克宽度
        private int height;     //坦克高度
        private int left;       //坦克位置的横坐标
        private int top;        //坦克位置的纵坐标
        private int type;       //坦克的类型
        private int direct;     //方向：0:上  1:下  2:左  3:右
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

        public Tank(int tank_type)
        {
            Random r = new Random();
            this.direct = r.Next(0, 4); //0-3
            this.width = 32;
            this.height = 32;
            this.left = r.Next(0, 10);  //0-9
            this.top = r.Next(0, 10);   //0-9
            this.type = tank_type;
        }
    }
}
