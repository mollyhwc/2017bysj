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
    /// chart.xaml 的交互逻辑
    /// </summary>
    public partial class chart : Window
    {
        public chart()
        {
            InitializeComponent();
            showColumnChart();
        }
        private void showColumnChart()
        {
            List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
            valueList.Add(new KeyValuePair<string, int>("餐厅", 60));
            valueList.Add(new KeyValuePair<string, int>("乒乓球室", 20));
            valueList.Add(new KeyValuePair<string, int>("舞蹈室", 50));
            valueList.Add(new KeyValuePair<string, int>("棋牌室", 30));


            //Setting data for column chart
            columnChart.DataContext = valueList;

            // Setting data for pie chart
            pieChart.DataContext = valueList;


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
