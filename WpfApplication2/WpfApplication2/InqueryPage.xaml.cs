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

namespace WpfApplication2
{
    /// <summary>
    /// InqueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class InqueryPage : Window
    {
        public InqueryPage()
        {
            InitializeComponent();
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            String oldId = ID.Text;
            List<ArrayList> array = MainWindow.array1;
            for (int i = 0; i < array.Count; i++)
            {
                if (oldId.Equals(array[i][0]))
                {
                    MessageBox.Show("您好，编号" + array[i][0] + "的老人在" + ((int)array[i][5]).ToString() + "号房间");
                }
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
