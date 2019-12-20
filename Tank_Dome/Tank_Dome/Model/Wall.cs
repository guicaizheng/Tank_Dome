using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_Dome.Model
{
    class Wall
    {
        int Walltepy;
        Image img;
        public Image CreateWall(int walltepy)
        {
             switch (Walltepy)
             {
                    case 1:
                        img = Image.FromFile("image\\wall.png");
                        break;
                    case 2:
                        img = Image.FromFile("image\\Map.png");
                        break;
                    case 3:
                        img = Image.FromFile("image\\grass.png");
                        break;
                    case 4:
                        img = Image.FromFile("image\\water.png");
                        break;
                    case 9:
                        img = Image.FromFile("image\\base1.png");
                        break;
            }
            return img;
        }
    }
}
