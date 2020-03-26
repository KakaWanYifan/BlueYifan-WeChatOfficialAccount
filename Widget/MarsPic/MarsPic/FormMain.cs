using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using MySql;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarsPic
{
    public partial class FormMain : Form
    {
        private Thread thread_Mars;
        public delegate void delegation(string cur);

        MySQL msql = new MySQL("blue");

        public FormMain()
        {
            InitializeComponent();
        }

        public void InsertMars()
        {
            JObject jo;
            JArray ja;
            for(int sol_para = 1;sol_para <= 1850;sol_para++)
            {
                //string sql_str = "delete from blue.mars where sol = '" + sol_para + "'";
                //msql.Set(sql_str);
                string url = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol=" + sol_para.ToString() + "&api_key=icBzEa0TOhgaeCRaTrFO3nh9uxSChoBUEXAB3MgH";
                string str = GetUrlResponse(url);
                jo = JObject.Parse(str);
                ja = JArray.Parse(jo["photos"].ToString());
                foreach(JObject item in ja)
                {
                    string earth_date = item["earth_date"].ToString();
                    string sol = item["sol"].ToString();
                    string img_id = item["id"].ToString();
                    string img_name = item["camera"]["full_name"].ToString();
                    string img_src = item["img_src"].ToString();
                    string sql = "insert into blue.mars " +
                                 "(earth_date,sol,img_id,img_name,img_src,update_time) " +
                                 "values " +
                                 "('" + earth_date + "','" + sol + "','" + img_id + "','" + img_name + "','" + img_src + "',CURRENT_DATE)";
                    msql.Set(sql);
                }
                delegation d_a = new delegation(GApp_Add);
                lb_Status.Invoke(d_a, sol_para.ToString());
            }
            MessageBox.Show("完成！");
        }

        public void GApp_Add(string cur)
        {
            lb_Status.Items.Add(DateTime.Now.ToString() + "   " + cur);
            lb_Status.TopIndex = lb_Status.Items.Count - 1;
        }

        public void GApp_Clear(string cur)
        {
            lb_Status.Items.Clear();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            thread_Mars = new Thread(new ThreadStart(InsertMars));
            thread_Mars.IsBackground = true; // 定义为后台线程，后台线程将会随着主线程的退出而退出。
            thread_Mars.Start();
        }

        public string GetUrlResponse(string url)
        {
            string res = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //GET请求
            request.Method = "get";
            request.ContentType = "application/json; charset=utf-8";
            request.ReadWriteTimeout = 5000;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding("UTF-8"));
            //返回内容
            res = sr.ReadToEnd();
            return res;
        }
    }
}
