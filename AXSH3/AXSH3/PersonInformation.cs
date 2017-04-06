using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSH3
{
    class PersonInformation
    {
        private String id;
        private double x;
        private double y;
        private List<double> rssiList;
        private String roomNumber;
        public PersonInformation(String id,double x,double y) {
            this.id = id;
            this.x = x;
            this.y = y;
        }
        public double getX() {
            return x;
        }
        public double getY() {
            return y;
        }
        public String getId() {
            return id;
        }
        public void setX(double x){
            this.x = x;
        }
        public void setY(double y) {
            this.y = y;
        }
        public void setRssi(double rssi, int numberAntenna)
        {
            rssiList[numberAntenna - 1] = rssi;
        }
        public void setRoomNumber(String roomNumber) {
            this.roomNumber = roomNumber;
        }
        public String getRoomNumber() {
            return roomNumber;
        }
        public List<double> getRssi() {
            return rssiList;
        }
    }
}
