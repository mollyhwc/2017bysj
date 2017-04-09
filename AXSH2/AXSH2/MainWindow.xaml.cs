using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data.OleDb;
using Impinj.OctaneSdk;
using System.IO;
namespace AXSH2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// acording to the relationship of rssi and distance 
    public partial class MainWindow : Window
    {

        ArrayList Imagelist = new ArrayList();
        ArrayList ReaderList = new ArrayList();
        ArrayList MacList = new ArrayList();
        List<String> roomNameList = new List<String>();
        List<ArrayList> roomList = new List<ArrayList>();
        List<ArrayList> atennuList = new List<ArrayList>();
        List<ArrayList> readerList = new List<ArrayList>();
        public List<PersonInformation> array1 = new List<PersonInformation>();
        string[] id = { "2222", "3333", "4444", "5555", "6666" };
        int[] antennaX = { 0, 0, 0, 9, 9, 9, 19, 19, 19 };
        int[] antennaY = { 0, 9, 19, 0, 9, 19, 0, 9, 19 };
        DispatcherTimer timer = new DispatcherTimer();

        // Create an instance of the SpeedwayReader class.  
        private SpeedwayReader Reader = new SpeedwayReader();

        // Declare a delegate to handle tag reports.      
        // The TagsReported event handler cannot operate directly      
        // on UI elements becuase it runs in a separate thread.         
        private delegate void TagsReportedDelegate(List<Tag> tag);
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += Window_Closing;
            //将阅读器与房间对应起来 模拟

            roomNameList.Add("111111111111");
            roomNameList.Add("222222222222");
            roomNameList.Add("333333333333");
            roomNameList.Add("444444444444");
            roomNameList.Add("555555555555");
            roomNameList.Add("666666666666");
            roomNameList.Add("777777777777");
            roomNameList.Add("888888888888");
            roomNameList.Add("999999999999");
            roomNameList.Add("AAAAAAAAAAAA");
            roomNameList.Add("BBBBBBBBBBBB");
            roomNameList.Add("CCCCCCCCCCCC");

            //initialize reader
            for (int i = 0; i < roomNameList.Count; i++)
            {
                ArrayList reader = new ArrayList();
                reader.Add(roomNameList[i]);
                reader.Add(i / 3);//(0,1,2)--0/(1,2,3)--1/(3,4,5)--2/(4,5,6)--3以此类推，实际上12个房间需要36个阅读器
                readerList.Add(reader);
            }

            //initialize room[the roomId is from 1-12]
            //1-7 行号2  餐厅这一排21 8-12 行号42
            for (int i = 1; i < 7; i++)
            {
                ArrayList room = RoomOrAntennaInformationList(i, 2, 1 + (i - 1) * 20);
                roomList.Add(room);
            }
            for (int i = 7; i < 13; i++)
            {
                ArrayList room = RoomOrAntennaInformationList(i, 42, (20 * (i - 7) + 1));
                roomList.Add(room);
            }
            //initialize antenna 
            for (int i = 0; i < 9; i++)
            {
                ArrayList antenna = RoomOrAntennaInformationList(i + 1, antennaX[i], antennaY[i]);
                atennuList.Add(antenna);
            }
            //初始化每个人的信息。
            for (int i = 0; i < 5; i++)
            {
                PersonInformation p = new PersonInformation(id[i], 0, 0, 0);

                array1.Add(p);
            }
            for (int i = 2; i < 7; i++)
            {
                Image image = new Image();
                Imagelist.Add(image);
                grad1.Children.Add(image);
                image.Source = new BitmapImage(new Uri(@"Resources\" + i + ".jpg", UriKind.Relative));
                image.Stretch = Stretch.None;
            }

            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(rssiDetection);  //你的事件
            timer.Start();


            /**
            //假设将12个阅读器全部加入到里面
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
            MacList.Add("SpeedwayR-xx-xx-xx");
             
           //初始化阅读器
            for (int i = 0; i < 12; i++)
            {
                    SpeedwayReader Reader = new SpeedwayReader();
                    ReaderList.Add(Reader);
                    Console.WriteLine(ReaderList[i]);
                    
                try
                {
                    // Connect to the reader.        
                    // Replace "SpeedwayR-xx-xx-xx" with your       
                    // reader's host name or IP address.      
                    SpeedwayReader readertemp = (SpeedwayReader)ReaderList[i];
                    string macString = (String)MacList[i];
                    readertemp.Connect(macString);
                  
                    // Remove all settings from the reader.              
                    readertemp.ClearSettings();

                    // Get the factory default settings            
                    // We'll use these as a starting point              
                    // and then modify the settings we're              
                    // interested in          
                    Settings settings = readertemp.QueryFactorySettings();
                    settings.Report.IncludeAntennaPortNumber = true;
                    settings.Report.Mode = ReportMode.Individual;
                    readertemp.ApplySettings(settings);

                    // Assign the TagsReported handler.      
                    // This specifies which function to call    
                    // when tags reports are available.         
                    // This function will in turn call a delegate    
                    // to update the UI (Listbox).      
                    readertemp.TagsReported += new EventHandler<TagsReportedEventArgs>(OnTagsReported);
                }
                catch (OctaneSdkException ex)
                {
                    // An Octane SDK exception occurred. Handle it here.       
                    System.Diagnostics.Trace.WriteLine("An Octane SDK exception has occured : {0}", ex.Message);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine("An exception has occured : {0}", ex.Message);
                    }            
                }
            */
        }

        //time stamp
        public String GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        //monitor the detected rssi of each person 对每一个人进行循环 设置他们的rssi 首先判断时间戳
        //如果相同 则看是否已经设置了三个rssi 设置达到三个为止
        //时间不同 经该对象的
        void rssiDetection(object sender, EventArgs e)
        {
            Random rnd = new Random();

            for (int i = 0; i < id.Length; i++)
            {

                if (GetTimeStamp().Equals(array1[i].getTime()))
                {//if time stamp is eaqual
                    int listNowCount = array1[i].getNumList();
                    if (listNowCount == 3)
                    {
                        continue;
                    }
                    else
                    {
                        int detctedCount = rnd.Next(2, 6);
                        int loopTime = detctedCount > (3 - listNowCount) ? (3 - listNowCount) : detctedCount;
                        addRssi(loopTime, i);
                    }
                }
                else
                {
                    array1[i].setListEmpty();
                    int detctedCount = rnd.Next(2, 6);
                    if (detctedCount > 3)
                    {
                        addRssi(3, i);
                    }
                    else
                    {
                        addRssi(detctedCount, i);
                    }
                }

                array1[i].setTime(GetTimeStamp());
                int[] pos = calculatePos((PersonInformation)array1[i]);
                if (pos[0] != -1 && pos[1] != -1)
                {
                    ((Image)Imagelist[i]).SetValue(Grid.RowProperty, pos[0]);
                    ((Image)Imagelist[i]).SetValue(Grid.ColumnProperty, pos[1]);
                }
            }
            updateListbox(array1);
        }


        //the detected rssi add to the personInformation
        private void addRssi(int loopTime, int index)
        {
            int atennaNum;
            double rssi;
            Random rnd = new Random();
            int roomId = rnd.Next(1, 13);
            ArrayList tempList = new ArrayList();
            for (int j = 0; j < loopTime; j++)
            {
                atennaNum = rnd.Next(1, 5);
                while (tempList.Contains(atennaNum))
                {
                    atennaNum = rnd.Next(1, 5);
                }
                tempList.Add(atennaNum);

                rssi = rnd.NextDouble() * (-28) - 38;
                ArrayList rssiCondition = new ArrayList();
                rssiCondition.Add(rssi);
                rssiCondition.Add(atennaNum);
                rssiCondition.Add(roomId);
                array1[index].addListElement(rssiCondition);
            }
        }

        //calculate the position(xpos,ypos) 
        public int[] calculatePos(PersonInformation p)
        {
            int[] pos = new int[2];
            List<Round> round = new List<Round>();
            if (p.getNumList() == 3)
            {
                List<ArrayList> posList = p.getList();
                for (int i = 0; i < 3; i++)
                {
                    double distance = formula((double)posList[i][0]);
                    int roomx = 0;
                    int roomy = 0;
                    //find xpos and ypos of room
                    for (int j = 0; j < roomList.Count; j++)
                    {
                        if ((int)roomList[j][0] == (int)posList[i][2])
                        {
                            roomx = (int)roomList[j][1];
                            roomy = (int)roomList[j][2];
                            break;
                        }
                    }
                    //find xpos and ypos of antenna
                    int antennax = 0;
                    int antennay = 0;
                    for (int k = 0; k < atennuList.Count; k++)
                    {
                        if ((int)atennuList[k][0] == (int)posList[i][1])
                        {
                            antennax = (int)atennuList[k][1];
                            antennay = (int)atennuList[k][2];
                            break;
                        }
                    }
                    int x = roomx + antennax;
                    int y = roomy + antennay;
                    Round r = new Round(x * 20, y * 20, distance);
                    round.Add(r);

                }
                Coordinate coor = Centroid.triCentroid((Round)round[0], (Round)round[1], (Round)round[1]);
                if (coor != null)
                {
                    pos[0] = (int)((double)coor.getX());
                    pos[1] = (int)((double)coor.getY());
                    return pos;
                }

                pos[0] = -1;
                pos[1] = -1;
                return pos;
            }
            pos[0] = -1;
            pos[1] = -1;
            return pos;
        }
        //the formula of calcualte rssi and distance
        public double formula(double rssi)
        {
            double distance = Math.Pow(10, (rssi + 35.297) / (-14.794));
            return distance;
        }

        //对每个房间或者天线进行初始化
        public ArrayList RoomOrAntennaInformationList(int ID, int startX, int startY)
        {
            ArrayList room = new ArrayList();
            room.Add(ID);
            room.Add(startX);
            room.Add(startY);
            return room;
        }


        private void Button_Find(object sender, RoutedEventArgs e)
        {
            InqueryPage inqueryWindow = new InqueryPage();
            inqueryWindow.Show();
        }

        //start
        private void Button_Start(object sender, RoutedEventArgs e)
        {
            /**
            try
            { // Don't call the Start method if the        
                // reader is already running. 
                if (!Reader.QueryStatus().IsSingulating)
                {
                    // Start reading.                    
                    Reader.Start();
                }
            }
            catch (OctaneSdkException ex)
            {
                // An Octane SDK exception occurred. Handle it here.     
                System.Diagnostics.Trace.WriteLine("An Octane SDK exception has occured : {0}",
                    ex.Message);
            }
            catch (Exception ex)
            {
                // A general exception occurred. Handle it here.            
                System.Diagnostics.Trace.WriteLine("An exception has occured : {0}", ex.Message);
            }
            **/
            timer.Start();
        }

        //simulation
        private void updateListbox(List<PersonInformation> list)
        {
            // Loop through each tag is the list and add it to the Listbox.              
            foreach (var tag in list)
            {
                listTags.Items.Add("老人" + tag.getId() + "在" + tag.getRoomId() + "号房间坐标是（" + tag.getxpos() + "," + tag.getypos() + ")");
            }
        }

        private void updateListbox(List<Tag> list)
        {
            // Loop through each tag is the list and add it to the Listbox.   
            int roomNumber;
            int xpos = 0;
            int ypos = 0;
            foreach (var tag in list)
            {

                for (int i = 0; i < roomList.Count; i++)
                {
                    if (tag.ReaderIdentity.Equals(roomNameList[i][0]))
                    {
                        roomNumber = roomNameList[i][1];
                    }
                }
                for (int k = 0; k < array1.Count; k++)
                {
                    if (tag.Epc.Equals(array1[k].getId()))
                    {
                        xpos = array1[k].getxpos();
                        xpos = array1[k].getypos();
                    }
                }
                listTags.Items.Add("老人" + tag.Epc + "在" + roomNameList + "号房间坐标(" + xpos + "," + ypos + ")");
            }
        }

        private void OnTagsReported(object sender, TagsReportedEventArgs args)
        {
            TagsReportedDelegate del = new TagsReportedDelegate(updateListbox);
            this.Dispatcher.BeginInvoke(del, args.TagReport.Tags);
            
            foreach (Tag tag in args.TagReport.Tags)
            {
                int index = 0;
                int roomNumber = 1;
                for (int i = 0; i < roomList.Count; i++)
                {
                    if (tag.ReaderIdentity.Equals(roomNameList[i][0]))
                    {
                        roomNumber = roomNameList[i][1];
                    }
                }

                //get the index of tag in array1
                for (index = 0; index < array1.Count; index++)
                {
                    if (array1[index].getId().Equals(tag.Epc))
                    {
                        break;
                    }
                }
                //modify rssi
                if (GetTimeStamp().Equals(array1[index].getTime()))
                {
                    //if time stamp is eaqual
                    int listNowCount = array1[index].getNumList();
                    if (listNowCount < 3)
                    {
                        ArrayList rssiCondition = new ArrayList();
                        rssiCondition.Add(tag.PeakRssiInDbm);
                        rssiCondition.Add(tag.AntennaPortNumber);
                        rssiCondition.Add(roomNumber);
                        array1[index].addListElement(rssiCondition);
                    }
                }
                else
                {
                    ArrayList rssiCondition = new ArrayList();
                    rssiCondition.Add(tag.PeakRssiInDbm);
                    rssiCondition.Add(tag.AntennaPortNumber);
                    rssiCondition.Add(roomNumber);
                    array1[index].addListElement(rssiCondition);
                }

                array1[index].setTime(GetTimeStamp());
                int[] pos = calculatePos((PersonInformation)array1[index]);
                if (pos[0] != -1 && pos[1] != -1)
                {
                    ((Image)Imagelist[index]).SetValue(Grid.RowProperty, pos[0]);
                    ((Image)Imagelist[index]).SetValue(Grid.ColumnProperty, pos[1]);
                }
            }
        }    

        //stop
        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            /*
                try{                
                    // Don't call the Stop method if the   
                    // reader is already stopped.         
                    if (Reader.QueryStatus().IsSingulating)         
                    { 
                        Reader.Stop();   
                    }             
                }         
                catch (OctaneSdkException ex)        
                {  // An Octane SDK exception occurred. Handle it here.     
                    System.Diagnostics.Trace.              
                    WriteLine("An Octane SDK exception has occured : {0}",ex.Message); }           
                    catch (Exception ex) {          
                    // A general exception occurred. Handle it here.    
                    System.Diagnostics.Trace.            
                    WriteLine("An exception has occured : {0}", ex.Message);       
                }   
                * */
            timer.Stop();
        }

        //clear
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear all the readings from the Listbox.          
            listTags.Items.Clear();
        }

        //close
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            try
            {
                // Don't call the Stop method if the       
                // reader is already stopped.

                if (Reader.QueryStatus().IsSingulating)
                {
                    Reader.Stop();
                }
                // Disconnect from the reader.      
                Reader.Disconnect();
            }
            catch (OctaneSdkException ex)
            {   // An Octane SDK exception occurred. Handle it here.     
                System.Diagnostics.Trace.WriteLine("An Octane SDK exception has occured : {0}", ex.Message);
            }
            catch (Exception ex)
            {
                // A general exception occurred. Handle it here.      
                System.Diagnostics.Trace.
                WriteLine("An exception has occured : {0}", ex.Message);
            }
            Application.Current.Shutdown();
        }

        //return array1;
        public List<PersonInformation> getArray()
        {
            return array1;
        }
    }
}
