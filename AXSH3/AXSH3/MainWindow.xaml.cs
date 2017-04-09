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
        List<Room> roomList = new List<Room>();
        List<Tags> untrackTagList = new List<Tags>();
        List<Tags> referenceTagList = new List<Tags>();
        List<List> allReferenceTag = new List<List>();
        List<String> referenceTagId = new List<string>();
        ArrayList atennuList = new ArrayList();
        List<ArrayList> sortTemp = new List<ArrayList>();
        double[] referenceTagX = { 1, 2, 3, 1, 3, 1, 2, 3 };
        double[] referenceTagY = { 1, 1, 1, 2, 2, 3, 3, 3 };
    
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
            //name of reader added to the roomnamelist
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
            roomNameList.Add("000011111111");
            roomNameList.Add("000022222222");
            roomNameList.Add("000033333333");
            roomNameList.Add("000044444444");
            roomNameList.Add("000055555555");
            roomNameList.Add("000066666666");
            roomNameList.Add("000077777777");
            roomNameList.Add("000088888888");
            roomNameList.Add("000099999999");
            roomNameList.Add("0000AAAAAAAA");
            roomNameList.Add("0000BBBBBBBB");
            roomNameList.Add("0000CCCCCCCC");

            //the grid of the room
            for (int i = 1; i < 7; i++)
            {
                Room r = new Room(i, 2 * i - 1, 2);
                r.setReader1(roomNameList[2 * i - 2]);
                r.setReader2(roomNameList[2 * i - 1]);
                roomList.Add(r);
            }
            for (int i = 7; i < 13; i++)
            {
                Room r = new Room(i, 2 * i - 1, 10);
                r.setReader1(roomNameList[2 * i - 2]);
                r.setReader2(roomNameList[2 * i - 1]);
                roomList.Add(r);
            }
            /**
            //假设将24个阅读器全部加入到里面
            MacList.Add("speedwayr-10-74-87");
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
             
            //initialize the readers
            for (int i = 0; i < 24; i++)
            {
                    SpeedwayReader Reader = new SpeedwayReader();
                    ReaderList.Add(Reader);
                    
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

            //initialize the images
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
            for (int m = 0; m < 12; m++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Tags tag = new Tags(referenceTagX[i], referenceTagY[i], referenceTagId[i]);
                    tag.setRoomNumber(m + 1);
                    referenceTagList.Add(tag);
                }
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
            //unrack tag
            for (int i = 0; i < id.Length; i++)
            {
                if (id[rnd.Next(0, 5)].Equals(id[i]))
                {
                    
                    for (int m = 0; m < 5; m++)
                    {
                        double rssiRandom = rnd.NextDouble() * (-25) - 35;
                        int antennaPortNumber = rnd.Next(1, 5);  
                        untrackTagList[i].setTagrssi(rssiRandom, antennaPortNumber);
                    } 
                    string readerIdentity = roomNameList[rnd.Next(0, 25)];
                     for (int p = 0; p < roomList.Count; p++)
                     {
                         if ((roomList[p].getReader1()).Equals(readerIdentity) || (roomList[p].getReader2()).Equals(readerIdentity))
                         {
                             untrackTagList[i].setRoomNumber(roomList[p].getRoomNumber());
                         }
                     }
                }
            }
            //reference tag
                for(int k=0;k<referenceTagId.Count ;k++)
                {
                     if(referenceTagId[rnd.Next (0,9)].Equals(referenceTagId[k]))
                     {
                         double referenceRssi=rnd .NextDouble()*(-25)-35;
                         int referencecAntennaport=rnd.Next(0,5);
                         referenceTagList[k].setTagrssi(referenceRssi ,referencecAntennaport );           
                     }
                }
                  
                calGetXandY(untrackTagList, referenceTagList);
                //the count of untracktag is the same as imagelist number 
               //but the number of image havenot make well
                for (int i = 0; i < Imagelist .Count ; i++)
                {
                    ((Image)Imagelist[i]).SetValue(Grid.RowProperty, (int)(untrackTagList[i].getX()) + roomList[i].getX());
                    ((Image)Imagelist[i]).SetValue(Grid.ColumnProperty, 3 - (int)(untrackTagList[i].getY()) + roomList[i].getY());
                }
               
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
        private void updateListbox(List<Tags> list)
        {
            // Loop through each tag is the list and add it to the Listbox.              
            foreach (var tag in list)
            {
                listTags.Items.Add("老人" + tag.getId() + "在" + tag.getRoomNumber() + "号房间" + "x:" + tag.getX() + "y:" + tag.getY());
            }
        }
        /**
        private void updateListbox(List<Tag> list)
        {
            // Loop through each tag is the list and add it to the Listbox.              
            foreach (var tag in list)
            {
                for (int i = 0; i < roomNameList.Count ;i++ )
                {
                    if (tag.ReaderIdentity.Equals(roomNameList[i]))
                    {
                        if (i % 2 == 0) { i = (i + 2) % 2; }
                        else i = (i + 1) % 2; 
                        listTags.Items.Add("老人" + tag.Epc + "在 " +i+"号房间");
                    }
                }
            }
        }
        **/
        /**
        public  void OnTagsReported(object sender, TagsReportedEventArgs args)
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
                        for(int p=0;p<roomList.Count;p++){
                        if((roomList[p].getReader1()).Equals(tag.ReaderIdentity )||(roomList[p].getReader2()).Equals(tag.ReaderIdentity )){
                        untrackTagList [i].setRoomNumber(roomList[p].getRoomNumber());
                        }
                        }
                       
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
            calGetXandY(untrackTagList,referenceTagList);
            for (int i = 0; i < untrackTagList.Count; i++)
            {
                ((Image)Imagelist[i]).SetValue(Grid.RowProperty,(int)(untrackTagList[i].getX()) + roomList[i].getX());
                ((Image)Imagelist[i]).SetValue(Grid.ColumnProperty, 3-(int)(untrackTagList[i].getY()) +roomList[i].getY());
            }
        }
        **/
        //calculate the position of the 
        private void calGetXandY(List<Tags> untrackList, List<Tags> referenceList)
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
                List<ArrayList> weightList = sort(li);
                double x = 0;
                double y = 0;
                for (int m = 0; m < 3; m++)
                {
                    x += referenceList[(int)weightList[m][0]].getX() * (int)weightList[m][1];
                    y += referenceList[(int)weightList[m][0]].getY() * (int)weightList[m][1];
                    untrackList[i].setX(x);
                    untrackList[i].setY(y);
                }
            }
        }
        //sort the weight
        public List<ArrayList> sort(List<double> sort)
        {
            sortTemp.Clear();
            for (int i = 0; i < sort.Count; i++)
            {
                ArrayList a = new ArrayList();
                a.Add(i);
                a.Add(sort[i]);
                sortTemp.Add(a);
            }
            int max = (int)sortTemp[0][1];
            int index = 0;
            for (int m = 0; m < sortTemp.Count - 1; m++)
            {
                for (int i = m + 1; i < sortTemp.Count; i++)
                {
                    if ((int)sortTemp[i][1] > max)
                    {
                        max = (int)sortTemp[i][1];
                        index = i;
                    }
                }
                sortTemp[m][0] = index;
                int temp = (int)sortTemp[m][1];
                sortTemp[m][1] = max;
                sortTemp[index][0] = m;
                sortTemp[index][1] = temp;
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
       
    }
}
