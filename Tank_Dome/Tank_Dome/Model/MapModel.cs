using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_Dome.Model
{
    class MapModel
    {
        int Maptyep = 0;
        private int[,] Map = new int[13, 13];   //墙砖地图
        private int[,] TMap = new int[13, 13];  //坦克，墙砖地图

        public MapModel(int Maptyep)
        {
           switch(Maptyep)
           {
                case 1:
                    Map = new int[13, 13]
                    {
                        {0,0,0,0,0,0,0,0,0,0,0,0,0},
                        {0,1,0,1,0,1,0,1,0,1,0,1,0},
                        {0,1,0,1,0,1,0,1,0,1,0,1,0},
                        {0,1,0,1,0,1,2,1,0,1,0,1,0},
                        {0,1,0,1,0,0,0,0,0,1,0,1,0},
                        {2,0,0,0,0,1,0,1,0,0,0,0,2},
                        {1,0,1,1,0,0,0,0,0,1,1,0,1},
                        {0,0,0,0,0,1,0,1,0,0,0,0,0},
                        {0,1,0,1,0,1,1,1,0,1,0,1,0},
                        {0,1,0,1,0,1,0,1,0,1,0,1,0},
                        {0,1,0,1,0,1,0,1,0,1,0,1,0},
                        {0,1,0,1,0,1,1,1,0,1,0,1,0},
                        {0,0,0,0,0,1,9,1,0,0,0,0,0}
                    };
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        public int[,] Map1 { get => Map; set => Map = value; }
        public int[,] TMap1 { get => TMap; set => TMap = value; }
    }
}
