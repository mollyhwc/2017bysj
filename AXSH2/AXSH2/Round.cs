using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSH2
{
    class Round
    {
        private double x;
        /*圆心纵坐标*/
        private double y;
        /*半径*/
        private double r;

        public Round(double x, double y, double r)
        {

            this.x = x;
            this.y = y;
            this.r = r;
        }

        public double getX()
        {
            return x;
        }

        public void setX(double x)
        {
            this.x = x;
        }

        public double getY()
        {
            return y;
        }

        public void setY(double y)
        {
            this.y = y;
        }

        public double getR()
        {
            return r;
        }

        public void setR(double r)
        {
            this.r = r;
        }
    }
}
