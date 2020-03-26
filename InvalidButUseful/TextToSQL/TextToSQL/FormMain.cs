using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using MySql.Data.MySqlClient;
namespace TextToSQL
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void b_Path_Click(object sender, EventArgs e)
        {
            if(ofd_Path.ShowDialog() == DialogResult.OK)
            {
                tb_Path.Text = ofd_Path.FileName;
            }
        }

        private void b_Handle_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(tb_Path.Text);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                    continue;
                string[] Arr = line.Split(',');
                foreach(string str in Arr)
                {
                    if(str != "")
                    {
                        string com = "INSERT INTO song.words (w) values  "
    + " ('" + str + "')";
                        MySqlConnection myConn = new MySqlConnection("server=localhost;user id=root; password=MySQL; database=" + "blue" + "; pooling=false;port=3306");
                        MySqlCommand cmd;
                        cmd = myConn.CreateCommand();//sql命令对象，表示要对sql数据库执行一个sql语句
                        cmd.CommandText = com;
                        myConn.Open();//打开连接
                        cmd.ExecuteNonQuery();
                        myConn.Close();
                        lb_Now.Items.Add(DateTime.Now.ToString() + "   " + Arr[0] + ":" + Arr[1]);
                        lb_Now.TopIndex = lb_Now.Items.Count - 1;
                    }
                }
            }
            MessageBox.Show("完成了");
        }
    }
}
