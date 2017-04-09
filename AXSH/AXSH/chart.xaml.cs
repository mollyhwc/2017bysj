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

namespace AXSH
{
    /// <summary>
    /// chart.xaml 的交互逻辑
    /// </summary>
    public partial class chart : Window
    {
        public chart()
        {
            InitializeComponent();
        }
        private void showChart()
        {
            List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();
            MyValue.Add(new KeyValuePair<string, int>("Administration", 20));
            MyValue.Add(new KeyValuePair<string, int>("Management", 36));
            MyValue.Add(new KeyValuePair<string, int>("Development", 89));
            MyValue.Add(new KeyValuePair<string, int>("Support", 270));
            MyValue.Add(new KeyValuePair<string, int>("Sales", 140));

            ColumnChart1.DataContext = MyValue;
            AreaChart1.DataContext = MyValue;
            LineChart1.DataContext = MyValue;
            PieChart1.DataContext = MyValue;
            BarChart1.DataContext = MyValue;
            BubbleSeries1.DataContext = MyValue;
            ScatterSeries1.DataContext = MyValue;
        }
    }
}
