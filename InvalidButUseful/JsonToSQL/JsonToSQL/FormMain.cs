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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonToSQL
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
            string str = sr.ReadToEnd();
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            JArray ja = JArray.Parse(jo["result"].ToString());
            string city_name;
            string city_code;
            string abbr;
            string engine;
            string engineno;
            string classa;
            string classno;
            string regist;
            string registno;

            for (int i = 0; i < ja.Count; i++)
            {
                JObject joo = JObject.Parse(ja[i].ToString());

                city_name = joo["city_name"].ToString();
                city_code = joo["city_code"].ToString();
                abbr = joo["abbr"].ToString();
                engine = joo["engine"].ToString();
                engineno = joo["engineno"].ToString();
                classa = joo["classa"].ToString();
                classno = joo["classno"].ToString();
                regist = joo["regist"].ToString();
                registno = joo["registno"].ToString();


                string com = "INSERT INTO blue.plate (city_name,city_code,abbr,engine,engineno, classa, classno, regist, registno) values  "
                           + " ('" + city_name + "','" + city_code + "','" + abbr + "','" + engine + "','" + engineno + "','" + engineno + "','" + classno + "','" + regist + "','" + registno + "'" + ")";
                MySqlConnection myConn = new MySqlConnection("server=localhost;user id=root; password=MySQL; database=" + "blue" + "; pooling=false;port=3306");
                MySqlCommand cmd;
                cmd = myConn.CreateCommand();//sql命令对象，表示要对sql数据库执行一个sql语句
                cmd.CommandText = com;
                myConn.Open();//打开连接
                cmd.ExecuteNonQuery();
                myConn.Close();

                lb_Handle.Items.Add(DateTime.Now.ToString() + "   " + city_name);
                lb_Handle.TopIndex = lb_Handle.Items.Count - 1;
            }
            MessageBox.Show("完成");
        }
    }
}
