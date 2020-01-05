﻿namespace Tank_Dome.Model
{
    class MapModel
    {
        private int[,] Map = new int[13, 13];   //墙砖地图

        public MapModel(int Maptype)
        {
           switch(Maptype)//地图选择
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
                    Map = new int[13, 13]
                    {
                        {0,0,0,2,0,0,0,2,0,0,0,0,0},
                        {0,1,0,2,0,0,0,1,0,1,0,1,0},
                        {0,1,0,0,0,0,1,1,0,1,2,1,0},
                        {0,0,0,0,0,0,0,0,0,2,0,0,0},
                        {3,0,0,0,0,0,2,0,0,1,3,1,2},
                        {3,3,0,0,0,1,0,0,2,0,3,0,0},
                        {0,1,1,1,3,3,3,2,0,0,3,1,0},
                        {0,0,0,2,3,1,0,1,0,1,0,1,0},
                        {2,1,0,2,0,1,0,1,0,0,0,1,0},
                        {0,1,0,1,0,1,1,1,0,1,2,1,0},
                        {0,1,0,1,0,0,0,0,0,0,0,0,0},
                        {0,0,0,0,0,1,1,1,0,1,0,1,0},
                        {0,1,0,1,0,1,9,1,0,1,1,1,0}
                    };
                    break;
                case 3:
                    Map = new int[13, 13]
                    {
                        {0,3,0,0,0,0,0,0,0,0,0,3,0},
                        {3,3,0,0,0,1,1,1,0,0,0,0,3},
                        {3,0,0,0,1,1,1,1,1,1,1,0,2},
                        {2,0,0,1,1,1,1,1,1,1,1,0,0},
                        {0,0,0,1,1,0,0,1,0,1,0,0,0},
                        {4,0,0,0,2,0,0,0,2,0,0,0,0},
                        {0,0,1,0,1,0,0,0,1,0,0,4,4},
                        {0,0,0,1,1,1,1,1,1,1,0,0,0},
                        {0,0,1,1,1,0,0,0,1,0,0,0,0},
                        {0,0,0,1,1,1,1,1,1,1,0,0,0},
                        {0,1,0,1,0,0,0,0,0,1,0,0,3},
                        {3,0,0,1,0,1,1,1,0,1,0,3,3},
                        {2,3,0,0,0,1,9,1,0,0,3,3,0}
                    };
                    break;
            }
        }

        public int[,] Map1 { get => Map; set => Map = value; }
    }
}
