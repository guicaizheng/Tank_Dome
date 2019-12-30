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
        Image img = Image.FromFile("image\\wall.png");
        public Image CreateWall(int walltepy)
        {
             switch (walltepy)
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
                    case 10:
                        img = Image.FromFile("image\\explore.png");
                    break;
             }
            return img;
        }
    }
}
