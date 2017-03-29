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

namespace AXSH2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        ArrayList Imagelist = new ArrayList();
        ArrayList ReaderList = new ArrayList();
        ArrayList MacList = new ArrayList();
        List<String> roomNameList = new List<String>();
        List<ArrayList> roomList = new List<ArrayList>();
        List<ArrayList> atennuList = new List<ArrayList>();
        public static List<ArrayList> array1 = new List<ArrayList>();
        string[] id = { "2222", "3333", "4444", "5555", "6666" };
        int[] antennaX ={ 1,1,1,5,5,5,9,9,9};
        int[] antennaY = { 1, 5, 9, 1, 5, 9, 1, 5, 9 };
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
            //初始化房间信息
            for (int i = 1; i < 7; i++)
            {
                ArrayList room = RoomOrAntennaInformationList(roomNameList[i - 1], 2, 2 * i - 1);
                roomList.Add(room);
            }
            for (int i = 7; i < 13; i++)
            {
                ArrayList room = RoomOrAntennaInformationList(roomNameList[i - 1], 9, (2 * (i - 6) - 1));
                roomList.Add(room);
            }
            //初始化每个房间的天线信息
            for (int i = 0; i < 9; i++) { 
            ArrayList antenna= RoomOrAntennaInformationList((i+1).ToString(), antennaX[i], antennaY[i]);
            atennuList.Add(antenna);
            }
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

                //初始化每个人的信息。
                for (int i = 0; i < 5; i++)
                {
                    array1.Add(personInformation(id[i], 22, 0, 0, 0, 0, 0, 0));
                }
            for (int i = 2; i < 6; i++)
            {
                Image image = new Image();
                Imagelist.Add(image);
                grad1.Children.Add(image);
                image.Source = new BitmapImage(new Uri(@"Resources\" + i + ".jpg", UriKind.Relative));
                Console.Write(image.Source);
                image.Stretch = Stretch.None;
            }
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(changePosition);  //你的事件
            timer.Start();

        }

        void changePosition(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < id.Length; i++)
            {
                if (id[rnd.Next(0, 5)].Equals(id[i]))
                {
                    int roomIndex = rnd.Next(0, 12);
                    if (i == 4) {
                        Console.WriteLine(id[i]+roomIndex);
                    }
                    setRssi(i, rnd.NextDouble() * (-25) - 35, rnd.Next(1, 5), (String)roomList[roomIndex][0]);
                    double[,] pos = new double[2, 4];
                    for (int j = 1; j < array1[0].Count - 3; j++)
                    {
                        pos[0, j - 1] = (double)array1[i][j];
                        pos[1, j - 1] = j;
                    }
                    sortPosition(pos);
                    double[] colRow = getColRow(pos);
                    array1[i][6] = (int)colRow[0];
                    array1[i][7] = (int)colRow[1];
                    ((Image)Imagelist[i]).SetValue(Grid.RowProperty, (int)colRow[0] + (int)roomList[roomIndex][1]);
                    ((Image)Imagelist[i]).SetValue(Grid.ColumnProperty, (int)colRow[1] + (int)roomList[roomIndex][2]);
                }
            }
            updateListbox(array1);
        }
     
        //对每个房间或者天线进行初始化
        public ArrayList RoomOrAntennaInformationList(String ID, int startX, int startY)
        {
            ArrayList room = new ArrayList();
            room.Add(ID);
            room.Add(startX);
            room.Add(startY);
            return room;
        }

        //对每一个人的信息进行初始化
        public ArrayList personInformation(string ID, double rssi1, double rssi2, double rssi3, double rssi4, int rooomNum, int xpos, int ypos)
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
        private void updateListbox(List<ArrayList> list)
        {
            // Loop through each tag is the list and add it to the Listbox.              
            foreach (var tag in list)
            {
                listTags.Items.Add("老人" + tag[0] + "在" + tag[5] + "号房间");
            }
        }
        /* 
            private void updateListbox(List<Tag> list)
                {
                    // Loop through each tag is the list and add it to the Listbox.              
                    foreach (var tag in list)
                    {
                        listTags.Items.Add(tag.Epc + ", " + tag.AntennaPortNumber);     
                    }
                }
       
                private void OnTagsReported(object sender, TagsReportedEventArgs args)
                {         
                    TagsReportedDelegate del = new TagsReportedDelegate(updateListbox);
                    this.Dispatcher.BeginInvoke(del, args.TagReport.Tags);
                    foreach (Tag tag in args.TagReport.Tags) {
                        for (int i = 0; i < id.Length; i++) { 
                        if(tag.Epc.Equals(id[i])){
                            setRssi(i,tag.PeakRssiInDbm,tag.AntennaPortNumber,(String)tag.ReaderIdentity);
                            double[,] pos = new double[2, 4];
            
                                for (int j = 1; j < array1[0].Count - 3; j++)
                                {
                                    pos[0, j-1] = (int)array1[i][j];
                                    pos[1, j - 1] = j;
                                }
                                sortPosition(pos);
                                double[] colRow=getColRow(pos);
                                array1[i][6] =(int) colRow[0];
                                array1[i][7] = (int)colRow[1];
                    ((Image)Imagelist[i]).SetValue(Grid.RowProperty, (int)colRow[0] + (int)roomList[roomIndex][1]);
                    ((Image)Imagelist[i]).SetValue(Grid.ColumnProperty, (int)colRow[1] + (int)roomList[roomIndex][2]);             
                            }
                        }
                    }
                }
            */

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

        //匹配ID进行信息的修改
        public void setInformation(string id, double ssr1, double ssr2, double ssr3, double ssr4, int room)//这里的room根据房间来定位
        {
            for (int i = 0; i < array1.Count; i++)
            {
                if (array1[i][0].Equals(id))
                {
                    array1[i][1] = ssr1;
                    array1[i][2] = ssr2;
                    array1[i][3] = ssr3;
                    array1[i][4] = ssr4;
                    array1[i][5] = room;
                }
            }
        }
        //设置天线对应的rssi
        public void setRssi(int number, double rssi, int numberAntenna, String roomNumber)
        {
            array1[number][numberAntenna] = rssi;
            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomNumber.Equals(roomList[i][0]))
                {
                    array1[number][5] = i + 1;
                    Console.WriteLine("函数"+(i+1).ToString());
                  
                    break;
                }
            }
        }
        //判断是在哪个区域
        public double[] getColRow(double[,] pos)
        {
            double[] colRow = new double[2];//index0表示行，index1表示列
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
            else if (pos[1, 0] == 4 && pos[1, 1] == 1 || pos[1, 0] == 1 && pos[1, 1] == 4)
            {
                colRow[0] = 0;
                colRow[1] = 0;
            }
            else if (pos[1, 0] == 3 && pos[1, 1] == 4 || pos[1, 0] == 4 && pos[1, 1] == 3)
            {
                colRow[0] = 1;
                colRow[1] = 1;

            }
            else if (pos[1, 0] == 1 && pos[1, 1] > -40)
            {
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
        //二维数组 RSSI 在第一行 天线在第二行
        public double[,] sortPosition(double[,] pos)
        {
            double temp = 0;
            double tempTianxian = 0;
            for (int i = 0; i < pos.GetLength(1) - 1; i++)
            {
                for (int j = i + 1; j < pos.GetLength(1); j++)
                {
                    if (pos[0, j] > pos[0, i])
                    {
                        temp = pos[0, i];
                        pos[0, i] = pos[0, j];
                        pos[0, j] = temp;
                        tempTianxian = pos[1, i];
                        pos[1, i] = pos[1, j];
                        pos[1, j] = tempTianxian;
                    }
                }
            }
            return pos;
        }
    }
}
