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
using System.Data.OleDb;


namespace WpfApplication2
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string userName = username.Text;
            string passWord = password.Password;

            string connStr = "provider=Microsoft.ACE.OLEDB.12.0;Data Source = C:\\Users\\黄小仙儿\\Documents\\Visual Studio 2012\\Projects\\WpfApplication2\\WpfApplication2\\Manager.accdb";
            OleDbConnection con;
            con = new OleDbConnection(connStr);
            string a = "select * from Manager where UserName='" + userName + "' and PassWord='" + passWord + "'";

            OleDbCommand cmd = new OleDbCommand(a, con);
            con.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            if (userName == "" || passWord == "") //判断输入是否为空
            {
                MessageBox.Show("请填写用户名和密码");
            }

            else
            {
                if (rd.Read())//判断是否存在用户输入的用户名和密码
                {
                    MessageBox.Show("登陆成功");
                    con.Close();
                    this.Hide();
                    MainWindow NewWindow = new MainWindow();
                    NewWindow.Show(); 
                }

                else
                {
                    MessageBox.Show("用户名或密码有误");
                    con.Close();
                    username.Text = "";
                    password.Password = "";
                }
            }



           
         
        }
    }
}
