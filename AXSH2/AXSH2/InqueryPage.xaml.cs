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
using System.Data.OleDb;

namespace AXSH2
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
            bool isFind = false;
            String oldId = ID.Text;
            MainWindow m = new MainWindow();
            List<PersonInformation> array = m.getArray();

            for (int i = 0; i < array.Count; i++)
            {
                if(oldId .Equals (array [i].getId ()))
                {
                    string connStr = "provider=Microsoft.ACE.OLEDB.12.0;Data Source = C:\\Users\\黄小仙儿\\Documents\\Visual Studio 2012\\Projects\\AXSH2\\AXSH2\\Manager.accdb";
                    OleDbConnection con;
                    con = new OleDbConnection(connStr);
                    string a = "select * from OldManInformation where ShenFenZheng='" + oldId + "' ";
                    OleDbCommand cmd = new OleDbCommand(a, con);
                    con.Open();
                    OleDbDataReader rd = cmd.ExecuteReader();
                    inqueryResult qinsult = new inqueryResult();
                    if (rd.Read())
                    {
                        String pos="x:"+array [i].getxpos()+" ,y:"+array[i].getypos();
                        qinsult.setValue(rd["oldName"].ToString(), rd["Sex"].ToString(), rd["TelePhone"].ToString(), rd["HomeAddress"].ToString(), array[i].getRoomId().ToString(), rd["Age"].ToString(), pos);
                        isFind = true;
                    }
                    qinsult.Show();

                    break;
                }
            }
            
            if (!isFind) { MessageBox.Show("抱歉，没有找到相关的信息"); }
            
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
