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

namespace WpfApplication2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ArrayList list1 = new ArrayList(); 
        public MainWindow()
        {
            InitializeComponent();

            Random rnd = new Random();
           
            for (int i = 2; i < 6; i++)
            {
                Image image = new Image();
                list1.Add(image);
                grad1.Children.Add(image);
                image.Source = new BitmapImage(new Uri(@"Resources\" + i + ".jpg", UriKind.Relative));
            }
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(changePosition);  //你的事件
            timer.Start();
            
         }
         void changePosition(object sender, EventArgs e)
        {
            Random rnd = new Random();

            for (int i = 2; i < 6; i++)
            {
               
                int temp1 = rnd.Next(2, 7);
                int temp2 = rnd.Next(2, 7);
                Console.WriteLine("{0},{1}",temp1,temp2);
       
              ((Image)list1[i-2]).SetValue(Grid.RowProperty, temp1- 2);
              ((Image)list1[i - 2]).SetValue(Grid.ColumnProperty, temp2 - 2);
            }
        }

         private void Button_Click_1(object sender, RoutedEventArgs e)
         {
             this.Hide();
             InqueryPage inqueryWindow = new InqueryPage();
             inqueryWindow.Show(); 
         }

    }

}
