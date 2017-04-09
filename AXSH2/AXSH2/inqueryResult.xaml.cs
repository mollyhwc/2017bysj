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

namespace AXSH2
{
    /// <summary>
    /// inqueryResult.xaml 的交互逻辑
    /// </summary>
    public partial class inqueryResult : Window
    {
        public inqueryResult()
        {
            InitializeComponent();
        }
        public void setValue(String name, String sex, String phone, String address, String roomId, String age, String pos)
        {
            id.Text = name;
            this.sex.Text = sex;
            this.roomId.Text = roomId;
            this.phone.Text = phone;
            this.address.Text = address;
            this.age.Text = age;
            this.pos.Text = pos;
        }

    }
}
