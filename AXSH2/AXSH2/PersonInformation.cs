using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AXSH2
{
    public class PersonInformation
    {
        String id;
        int roomId;
        int xpos;
        int ypos;
        String timeStamp;//define timeStamp
        List<ArrayList> positionList = new List<ArrayList>();
        //constructor
        public PersonInformation(String id, int roomId, int xpos, int ypos)
        {
            this.id = id;
            this.roomId = roomId;
            this.xpos = xpos;
            this.ypos = ypos;
        }

        //get nums of rssi
        public int getNumList()
        {
            return positionList.Count;
        }
        //set nums of rssi
        public void setListEmpty()
        {
            positionList.Clear();
        }
        //add element into list<ArrayList>a
        public void addListElement(ArrayList r)
        {
            positionList.Add(r);
        }

        //get TimeTamp
        public String getTime()
        {
            return timeStamp;
        }

        //set timeStamp
        public void setTime(String time)
        {
            timeStamp = time;
        }
        //return the list which store rssi,antenna and roomId by order
        public List<ArrayList> getList()
        {
            return positionList;
        }
    }
}
