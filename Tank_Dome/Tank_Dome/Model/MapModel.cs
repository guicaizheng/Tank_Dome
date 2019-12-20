using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_Dome.Model
{
    class MapModel
    {
        int Maptyep=0;
        private int[,] Map = new int[10, 10];   //墙砖地图
        private int[,] TMap = new int[10, 10];  //坦克，墙砖地图

        public MapModel(int Maptyep)
        {
           switch(Maptyep)
           {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
    }
}
