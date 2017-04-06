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

namespace AXSH3
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
        //button exit
        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        //button search
        private void Button_Search(object sender, RoutedEventArgs e)
        {

        }
    }
}
