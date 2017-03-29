using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSH2
{
    class Coordinate
    {/*横坐标*/
        private double x;

        /*纵坐标*/
        private double y;

        public String toString()
        {
            return "坐标Coordinate为 [x=" + x + ", y=" + y + "]";
        }

        public Double getX()
        {
            return x;
        }

        public void setX(Double x)
        {
            this.x = x;
        }

        public Double getY()
        {
            return y;
        }

        public void setY(Double y)
        {
            this.y = y;
        }
    }
}
