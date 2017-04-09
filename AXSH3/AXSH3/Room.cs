using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSH3
{
    class Room
    {
        
        private double x;
        private double y;
        int number;
        String reader1;
        String reader2;

        public Room(int number,double x,double y) {
            this.number = number;
            this.x = x;
            this.y = y;
        }
        public double getX() {
            return x;
        }
        public double getY() {
            return y;
        }
        
        public void setX(double x){
            this.x = x;
        }
        public void setY(double y) {
            this.y = y;
        }
        public void setReader1(String reader1) {
            this.reader1 = reader1;
        }
        public void setReader2(String reader2) {
            this.reader2 = reader2;
        }
        public String getReader1() {
            return reader1;
        }
        public String getReader2()
        {
            return reader2;
        }
        public int getRoomNumber() {
            return number;
        }
    }
}
