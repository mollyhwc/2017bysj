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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data.OleDb;
using System.Collections;
using Impinj.OctaneSdk;

/*首先对于房间应该存上下两列，然后根据实际的位置除以房屋的宽度以得到具体展示的格子
 * 然后在reader中是实时监测的，所以应该set
 * 5个天线 8个引用标签 一个房间放置两个阅读器
 * 
 * 
 * */
namespace AXSH3
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
        List<Tags> untrackTagList = new List<Tags>();
        List<Tags> referenceTagList = new List<Tags>();
        List<String> referenceTagId = new List<string>();
        ArrayList atennuList = new ArrayList();
        List<List> sortTemp = new List<List>();
        double[] referenceTagX = { 1, 2, 3, 1, 3, 1, 2, 3 };
        double[] referenceTagY = { 1, 1, 1, 2, 2, 3, 3, 3 };
        public static List<PersonInformation> personList = new List<PersonInformation>();

        string[] id = { "2222", "3333", "4444", "5555", "6666" };

        DispatcherTimer timer = new DispatcherTimer();

        // Create an instance of the SpeedwayReader class.  
        private SpeedwayReader Reader = new SpeedwayReader();

        // Declare a delegate to handle tag reports.      
        // The TagsReported event handler cannot operate directly      
        // on UI elements becuase it runs in a separate thread.         
        private delegate void TagsReportedDelegate(List<Tags> tag);
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

            //the grid of the room
            for (int i = 1; i < 7; i++)
            {
                ArrayList room = RoomInformationList(roomNameList[i - 1], 2, 2 * (i - 1));
                roomList.Add(room);
            }
            for (int i = 7; i < 13; i++)
            {
                ArrayList room = RoomInformationList(roomNameList[i - 1], 9, (2 * ((i - 6) - 1)));
                roomList.Add(room);
            }
            /**
            //假设将24个阅读器全部加入到里面
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
            **/

            //初始化每个人的信息。
            for (int i = 0; i < 5; i++)
            {
                PersonInformation p = new PersonInformation(id[i], 0.0, 0.0);
                personList.Add(p);
            }
            for (int i = 2; i < 7; i++)
            {
                Image image = new Image();
                Imagelist.Add(image);
                grad1.Children.Add(image);
                image.Source = new BitmapImage(new Uri(@"Resources\" + i + ".jpg", UriKind.Relative));
                image.Stretch = Stretch.None;
            }
            referenceTagId.Add("0000-0000-0000");
            referenceTagId.Add("1111-1111-1111");
            referenceTagId.Add("2222-2222-2222");
            referenceTagId.Add("3333-3333-3333");
            referenceTagId.Add("4444-4444-4444");
            referenceTagId.Add("5555-5555-5555");
            referenceTagId.Add("6666-6666-6666");
            referenceTagId.Add("7777-7777-7777");
            referenceTagId.Add("8888-8888-8888");

            //intilize reference tags' information
            for (int i = 0; i < 8; i++)
            {
                Tags tag = new Tags(referenceTagX[i], referenceTagY[i], referenceTagId[i]);
                referenceTagList.Add(tag);
            }
            //initilize untracked tags' information
            for (int i = 0; i < 5; i++)
            {
                Tags tag = new Tags(0, 0, id[i]);
                untrackTagList.Add(tag);
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
                    //这个地方具体怎么写还要看情况。。。。


                    //待议
                    int imagex = (int)Math.Round(personList[i].getX(), 0);
                    int imagey = (int)Math.Round(personList[i].getY(), 0);
                    ((Image)Imagelist[i]).SetValue(Grid.RowProperty, (int)colRow[0] + (int)roomList[roomIndex][1]);
                    ((Image)Imagelist[i]).SetValue(Grid.ColumnProperty, (int)colRow[1] + (int)roomList[roomIndex][2]);
                }
            }
            updateListbox(personList);
        }

        //对每个房间进行初始化
        public ArrayList RoomInformationList(String ID, int startX, int startY)
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
        private void updateListbox(List<ArrayList> list)
        {
            // Loop through each tag is the list and add it to the Listbox.              
            foreach (var tag in list)
            {
                listTags.Items.Add("老人" + tag[0] + "在" + tag[5] + "号房间");
            }
        }

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
            foreach (Tag tag in args.TagReport.Tags)
            {
                for (int i = 0; i < untrackTagList.Count; i++)
                {
                    if (tag.Epc.Equals(untrackTagList[i].getId()))
                    {
                        untrackTagList[i].setTagrssi(tag.PeakRssiInDbm, tag.AntennaPortNumber);
                    }
                }
                for (int j = 0; j < referenceTagList.Count; j++)
                {
                    if (tag.Epc.Equals(referenceTagList[j].getId()))
                    {
                        referenceTagList[j].setTagrssi(tag.PeakRssiInDbm, tag.AntennaPortNumber);
                    }
                }
            }

            ((Image)Imagelist[i]).SetValue(Grid.RowProperty, (int)colRow[0] + (int)roomList[roomIndex][1]);
            ((Image)Imagelist[i]).SetValue(Grid.ColumnProperty, (int)colRow[1] + (int)roomList[roomIndex][2]);

        }

        //calculate the position of the 
        public void calGetXandY(List<Tags> untrackList, List<Tags> referenceList)
        {
            List<double> li;

            for (int i = 0; i < untrackList.Count; i++)
            {
                List<double> l = untrackList[i].getRssiList();
                for (int j = 0; j < referenceList.Count; j++)
                {
                    List<Double> d = referenceList[j].getRssiList();
                    double sum = 0;
                    for (int k = 0; k < d.Count; k++)
                    {
                        sum += (l[k] - d[k]) * (l[k] - d[k]);
                    }
                    untrackList[i].setTagrssi(sum, j);
                }
                li = untrackList[i].getWeight();
                //need sort
                double x = 0;
                double y = 0;
                for (int m = 0; m < 3; m++)
                {
                    x += referenceList[m].getX() * untrackList[i].getWeight()[m];
                    y += referenceList[m].getY() * untrackList[i].getWeight()[m];
                    untrackList[i].setX(x);
                    untrackList[i].setY(y);
                }
            }


        }
        //sort the weight
        public List<List> sort(List<double> sort) {
            sortTemp.Clear();
            List<double> temp = new List<double>();
            double max = sort[0];
            for(int m=1;m<sort.Count;m++){
                for (int i = 1; i < sort.Count; i++) {
                if (sort[i] > max) { 
                }
            }
            }
            return sortTemp;
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
        //save the rssi of untrackTag to untrackTagList
        public void updateUntrackList()
        {

        }
        //匹配ID进行信息的修改
        public void setInformation(string id, double x, double y)//这里的room根据房间来定位
        {
            for (int i = 0; i < personList.Count; i++)
            {
                if (personList[i].getId().Equals(id))
                {
                    personList[i].setX(x);
                    personList[i].setY(y);
                }
            }
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
