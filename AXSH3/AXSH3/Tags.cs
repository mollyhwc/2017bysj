using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSH3
{
    class Tags
    {
        private double x;
        private double y;
        private String id;
        private List<double> rssiList;
        private List<double> distinctList;
        private List<double> weight;
        private String roomNumber;
        public double getX()
        {
            return x;
        }
        public double getY()
        {
            return y;
        }
        public void setX(double x)
        {
            this.x = x;
        }
        public void setY(double y)
        {
            this.y = y;
        }
        public Tags(double x, double y, String id)
        {
            this.x = x;
            this.y = y;
            this.id = id;

            for (int i = 0; i < 8; i++)
            {
                rssiList.Add(0); //the initialize of 8 readers
                distinctList.Add(0);  //inilize the distinctList
            }
            for (int i = 0; i < 5; i++)
            {
                weight.Add(1);
            }
        }
        public void setRoomNumber(String roomNumber)
        {
            this.roomNumber = roomNumber;
        }
        public String getRoomNumber()
        {
            return roomNumber;
        }
        public void setTagrssi(double rssi, int AntennaPortNumber)
        {
            rssiList[AntennaPortNumber - 1] = rssi;

        }
        public List<double> getRssiList()
        {
            return rssiList;
        }
        public String getId()
        {
            return id;
        }
        //index should from 0 to n
        public void setDistinctList(int index, double distinct)
        {
            distinctList[index] = distinct;

        }

        //return the reference tag's weight
        public  List<double> getWeight()
        {
            double sum = 0;
            for (int i = 0; i < distinctList.Count; i++)
            {
                sum += distinctList[i]*distinctList[i];

            }
            for (int j = 0; j < weight.Count; j++) {
                double  up =  (distinctList[j] * distinctList[j]);
                double personWeight = up / sum;
                weight[j] = personWeight;
            }
            return weight;
        }
        
    }
}
