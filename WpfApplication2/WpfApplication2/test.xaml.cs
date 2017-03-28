using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using System.Data.OleDb;

namespace WpfApplication2
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        List<ArrayList> array1 = new List<ArrayList>();
        string []id={"1222","3333","4444","5555","666"};
        int []a=new int[2];

        public Window1()
        {
           // InitializeComponent();
       
        
            //初始化每个人的信息。
            for (int i = 0; i < 5; i++)
            {
                array1.Add(personInformation(id[i], 0, 0, 0 , 0 ,0 ,0,0));
            }
            
            for (int i = 0; i < array1.Count; i++)
            {
                Console.WriteLine("这是"+i);
                for (int j = 0; j < array1[0].Count; j++) {
                    Console.Write(array1[i][j]+" ");
                }
                Console.WriteLine();
            }
            for (int i = 0; i < array1.Capacity; i++) { 
            
            }
             foreach(ArrayList a in array1){
                Console.Write(a);
             }
            int[,] pos = new int[,] { { -21,-32,-45,-60},{ 1,2,3,4} };
            List<ArrayList> RssiList = new List<ArrayList> ();
            sortPosition(pos);
            //将天线数按着信号强度打出来
            for (int i = 0; i < pos.GetLength(1); i++) { 
                Console.WriteLine(pos[1,i]); }
                
            
        }
        //对每一个人的信息进行初始化
        public ArrayList personInformation(string ID, int rssi1, int rssi2,int rssi3, int rssi4, int rooomNum,int xpos,int ypos)
        {
            ArrayList person = new ArrayList();
            person.Add(ID);
            person.Add(rssi1);//
            person.Add(rssi2);//rssi
            person.Add(rssi3);//tianxian shumu
            person.Add(rssi4);
            person.Add(rooomNum);
            person.Add(xpos);
            person.Add(ypos);
            return person;
        }
        //匹配ID进行信息的修改
        public void  setInformation(string id,int ssr1,int ssr2,int ssr3,int ssr4,int room)//这里的room根据房间来定位
        {
            for (int i = 0; i < array1.Count; i++)
            { 
                if(array1[i][0].Equals(id))
                {
                array1[i][1]=ssr1;
                array1[i][2]=ssr2;
                array1[i][3]=ssr3;
                array1[i][4] = ssr4;
                array1[i][5] = room;
                }
                    
             }
        }

        //计算位置的算法
      
        public int[] nowPosition() {
            // y=kx+d
            return a;
        }
        //判断是在哪个区域
        public int[] getColRow(int [,]pos) {
            int[] colRow=new int[2];//index0表示行，index1表示列
            if (pos[1, 0] == 1 && pos[1, 1] == 2 || pos[1, 0] == 2 && pos[1, 1] == 1)
            {
                colRow[0] = 0;
                colRow[1] = 0;
            }
            else if (pos[1, 0] == 2 && pos[1, 1] == 3 || pos[1, 0] == 3 && pos[1, 1] == 2)
            {
                colRow[0] = 0;
                colRow[1] = 1;
            }
            else if (pos[1, 0] == 4 && pos[1, 1] == 1 || pos[1, 0] == 1 && pos[1, 1] == 4) {
                colRow[0] = 0;
                colRow[1] = 0;
            }
            else if (pos[1, 0] == 3 && pos[1, 1] == 4 || pos[1, 0] == 4 && pos[1, 1] == 3)
            {
                colRow[0] = 1;
                colRow[1] = 1;

            }
            else if(pos[1,0]==1&& pos[1,1]>-40){
                colRow[0] = 0;
                colRow[1] = 0;
            }
            else if (pos[1, 0] == 2 && pos[1, 1] > -40)
            {
                colRow[0] = 0;
                colRow[1] = 1;
            }
            else if (pos[1, 0] == 3 && pos[1, 1] > -40)
            {
                colRow[0] = 1;
                colRow[1] = 1;
            }
            else if (pos[1, 0] == 4 && pos[1, 1] > -40)
            {
                colRow[0] = 1;
                colRow[1] = 0;
            }
            return colRow;
        }
        //对获得的RSSI进行大小的排序
        public int[ ,] sortPosition(int [ ,]pos){
            int temp = 0;
            int tempTianxian = 0;
            for (int i = 0; i < pos.GetLength(1)-1;i++ )
            {
                for (int j = i + 1; j < pos.GetLength(1); j++)
                {

                    if (pos[0,j] > pos[0,i]) {
                        temp=pos[0,i];
                        pos[0,i] = pos[0,j];
                        pos[0,j] = temp;
                        tempTianxian = pos[1,i];
                        pos[1,i] = pos[1,j];
                        pos[1,j] = tempTianxian;

                    }
                }
            }
        return pos;
        }
    }
}
