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
using Xfrog.Net;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Web;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FundData
{
    public partial class FormMain : Form
    {

        private Thread thread_Fund;
        private Thread thread_Turing;
        private Thread thread_Mars;
        private Thread thread_nCoV;

        private Boolean CanRunFund = true;
        private Boolean CanRunTuring = false;
        private Boolean CanRunMars = false;
        private Boolean CanRunNCOV = false;

        MySQL msql = new MySQL("blue");

        public FormMain()
        {
            InitializeComponent();
        }
                                                                                                                                                                                                                                                                                                                                                                
        public delegate void delegation(string cur);

        public void GApp_Add(string cur)
        {
            lb_Status.Items.Add(DateTime.Now.ToString() + "   " + cur);
            lb_Status.TopIndex = lb_Status.Items.Count - 1;
        }

        public void GApp_Clear(string cur)
        {
            lb_Status.Items.Clear();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //定义一个子线程
            thread_Fund = new Thread(new ThreadStart(RefreshFund));
            thread_Fund.IsBackground = true; // 定义为后台线程，后台线程将会随着主线程的退出而退出。
            thread_Turing = new Thread(new ThreadStart(RefreshTuring));
            thread_Turing.IsBackground = true;
            thread_Mars = new Thread(new ThreadStart(RefreshMars));
            thread_Mars.IsBackground = true;
            thread_nCoV = new Thread(new ThreadStart(RefreshNCOV));
            thread_nCoV.IsBackground = true;
            
        }

        private void b_Start_Click(object sender, EventArgs e)
        {
            //开一个定时器，获取当前时间。
            //做个定时器
            //t_Time.Interval = 60000;//获取当前时间，每个一分钟获取一次。
            t_Time.Interval = 1000;//测试
            t_Time.Tick += new EventHandler(t_Time_Tick);
            t_Time.Start();
            b_Start.Enabled = false;
            b_Stop.Enabled = true;

            if (cb_nCoV.CheckState == CheckState.Checked)
            {
                thread_nCoV.Start();
                tssl_Status.Text = "正在更新肺炎数据";
            }
            else
            {
                thread_nCoV.Abort();
            }

        }

        private void t_Time_Tick(object sender, EventArgs e)
        {
            if(cb_Fund.CheckState == CheckState.Checked)
            {
                if (DateTime.Now.ToString("HH:mm") == dtp_Fund.Value.ToString("HH:mm"))
                {
                    if(CanRunFund)
                    {
                        CanRunFund = false;
                        thread_Fund = new Thread(new ThreadStart(RefreshFund));
                        thread_Fund.IsBackground = true; // 定义为后台线程，后台线程将会随着主线程的退出而退出。
                        thread_Fund.Start();
                    }
                }
                else
                {
                    CanRunFund = true;
                }
            }
            if(cb_Truing.CheckState == CheckState.Checked)
            {
                if (DateTime.Now.ToString("HH:mm") == dtp_Turing.Value.ToString("HH:mm"))
                {
                    if(CanRunTuring)
                    {
                        CanRunTuring = false;
                        thread_Turing = new Thread(new ThreadStart(RefreshTuring));
                        thread_Turing.IsBackground = true; // 定义为后台线程，后台线程将会随着主线程的退出而退出。
                        thread_Turing.Start();
                    }
                }
                else
                {
                    CanRunTuring = true;
                }
            }
            if (cb_Mars.CheckState == CheckState.Checked)
            {
                if (DateTime.Now.ToString("HH:mm") == dtp_Mars.Value.ToString("HH:mm"))
                {
                    if (CanRunMars)
                    {
                        CanRunMars = false;
                        thread_Mars = new Thread(new ThreadStart(RefreshMars));
                        thread_Mars.IsBackground = true; // 定义为后台线程，后台线程将会随着主线程的退出而退出。
                        thread_Mars.Start();
                    }
                }
                else
                {
                    CanRunMars = true;
                }
            }
            if (thread_Fund.IsAlive == false && thread_Turing.IsAlive == false && thread_Mars.IsAlive == false)
            {
                tssl_Status.Text = "整装待发";
            }
            if (thread_Fund.IsAlive == true || thread_Turing.IsAlive == true || thread_Mars.IsAlive == true)
            {
                tssl_Status.Text = "正在更新数据";
            }
        }

        public void RefreshTuring()
        {
            string com = "update blue.turing set state = '1'";
            msql.I_D_U(com);
            delegation d_c = new delegation(GApp_Clear);
            lb_Status.Invoke(d_c, "");
            delegation d_a = new delegation(GApp_Add);
            lb_Status.Invoke(d_a, "更新图灵数据");
        }

        public void RefreshMars()
        {
            JObject jo;
            JArray ja;

            string url = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=2012-08-07&api_key=icBzEa0TOhgaeCRaTrFO3nh9uxSChoBUEXAB3MgH";
            string str = GetUrlResponse(url);
            jo = JObject.Parse(str);
            ja = JArray.Parse(jo["photos"].ToString());
            jo = (JObject)ja[0];
            jo = JObject.Parse(jo["rover"].ToString());
            string Max = jo["max_date"].ToString() + "|" + jo["max_sol"].ToString();
            string sql = "delete from blue.marsmax;";
            sql = sql + "insert into blue.marsmax(update_time,value) values (CURRENT_DATE,'" + Max + "')";
            msql.I_D_U(sql);

            url = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=" + jo["max_date"].ToString() + "&api_key=icBzEa0TOhgaeCRaTrFO3nh9uxSChoBUEXAB3MgH";
            str = GetUrlResponse(url);
            jo = JObject.Parse(str);
            ja = JArray.Parse(jo["photos"].ToString());
            foreach (JObject item in ja)
            {
                string earth_date = item["earth_date"].ToString();
                string sol = item["sol"].ToString();
                string img_id = item["id"].ToString();
                string img_name = item["camera"]["full_name"].ToString();
                string img_src = item["img_src"].ToString();
                sql = "delete from blue.mars where img_id = " + img_id + ";";
                sql =  sql + "insert into blue.mars " +
                             "(earth_date,sol,img_id,img_name,img_src,update_time) " +
                             "values " +
                             "('" + earth_date + "','" + sol + "','" + img_id + "','" + img_name + "','" + img_src + "',CURRENT_DATE)";
                msql.I_D_U(sql);
            }
            delegation d_c = new delegation(GApp_Clear);
            lb_Status.Invoke(d_c, "");
            delegation d_a = new delegation(GApp_Add);
            lb_Status.Invoke(d_a, "更新火星数据");
        }

        public void RefreshNCOV()
        {
            
            while(true)
            {
                NCOV();

                int delay = new Random().Next(500, 5000);
                delegation d_a = new delegation(GApp_Add);
                lb_Status.Invoke(d_a, "下次更新肺炎数据将在" + delay.ToString() + "秒后");
                Thread.Sleep(delay * 1000);
            }
        }

        public void NCOV()
        {

            delegation d_a = new delegation(GApp_Add);
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + new Random().Next().ToString() + ".json";
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx8898a07816bc5f05&secret=bfb3101627c7eff4db1b7f01e566172a";
            string str = GetUrlResponse(url);
            string access_token = JObject.Parse(str)["access_token"].ToString();
            lb_Status.Invoke(d_a, "更新肺炎数据：access_token获取成功");
            url = "https://rl.inews.qq.com/taf/travelFront";
            str = GetUrlResponse_2nd(url);
            JArray ja = JArray.Parse(JObject.Parse(str)["data"]["list"].ToString());
            JArray jaList = new JArray();
            foreach (JObject item in ja)
            {
                JObject temp = new JObject();
                temp["t_date"] = item["date"];
                temp["t_start"] = item["start"].ToString() == "" ? "数据缺失" : item["start"];
                temp["t_end"] = item["end"].ToString() == "" ? "数据缺失" : item["end"];
                switch (item["type"].ToString())
                {
                    case "1":
                        temp["t_type"] = "飞机";
                        break;
                    case "2":
                        temp["t_type"] = "火车";
                        break;
                    case "3":
                        temp["t_type"] = "地铁";
                        break;
                    case "4":
                        temp["t_type"] = "长途客车";
                        break;
                    case "5":
                        temp["t_type"] = "公交车";
                        break;
                    case "6":
                        temp["t_type"] = "出租车";
                        break;
                    case "7":
                        temp["t_type"] = "轮船";
                        break;
                    case "8":
                        temp["t_type"] = "其他公共场所";
                        break;
                }
                temp["t_no"] = item["no"].ToString() == "" ? "数据缺失" : item["no"];
                temp["t_memo"] = item["memo"].ToString() == "" ? "无" : item["memo"];
                temp["t_no_sub"] = item["no_sub"].ToString() == "" ? "数据缺失" : item["no_sub"];
                temp["t_pos_start"] = item["pos_start"].ToString() == "" ? "数据缺失" : item["pos_start"];
                temp["t_pos_end"] = item["pos_end"].ToString() == "" ? "数据缺失" : item["pos_end"];
                temp["source"] = item["source"].ToString() == "" ? "数据缺失" : item["source"];
                temp["who"] = item["who"].ToString() == "" ? "数据缺失" : item["who"];
                jaList.Add(temp);
            }
            string text = jaList.ToString().Replace("},", "}").Substring(1, jaList.ToString().Replace("},", "}").Length - 2);
            System.IO.File.WriteAllText(fileName, text);
            lb_Status.Invoke(d_a, "更新肺炎数据：爬取文件成功");
            Thread.Sleep(5000);

            //上传文件 第一步
            url = "https://api.weixin.qq.com/tcb/uploadfile?access_token=" + access_token;
            var client = new RestClient(url);
            client.Timeout = 5000;
            var request = new RestRequest(Method.POST);
            request.AddParameter("application/json", "{\"env\": \"kaka-ncov\",\"path\": \"" + fileName + "\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            lb_Status.Invoke(d_a, "更新肺炎数据：上传文件 1/3");
            //上传文件  第二步
            JObject res = JObject.Parse(response.Content);
            url = res["url"].ToString();
            string token = res["token"].ToString();
            string authorization = res["authorization"].ToString();
            string file_id = res["file_id"].ToString();
            string cos_file_id = res["cos_file_id"].ToString();
            lb_Status.Invoke(d_a, "更新肺炎数据：上传文件 2/3");

            //上传文件  第三步
            client = new RestClient(url);
            client.Timeout = 5000;
            request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("key", fileName);
            request.AddParameter("Signature", authorization);
            request.AddParameter("x-cos-security-token", token);
            request.AddParameter("x-cos-meta-fileid", cos_file_id);
            request.AddFile("file", fileName);
            response = client.Execute(request);
            lb_Status.Invoke(d_a, "更新肺炎数据：上传文件 3/3");
            Thread.Sleep(5000);

            lb_Status.Invoke(d_a, "上传文件已经完成，等待3分钟");
            Thread.Sleep(180000);

            url = "https://api.weixin.qq.com/tcb/databasecollectiondelete?access_token=" + access_token;
            client = new RestClient(url);
            client.Timeout = 5000;
            request = new RestRequest(Method.POST);
            request.AddParameter("application/json", "{\"env\": \"kaka-ncov\",\"collection_name\": \"nCoV\"}", ParameterType.RequestBody);
            response = client.Execute(request);
            lb_Status.Invoke(d_a, "更新肺炎数据：drop nCoV成功");
            Thread.Sleep(5000);

            url = "https://api.weixin.qq.com/tcb/databasecollectionadd?access_token=" + access_token;
            client = new RestClient(url);
            client.Timeout = 5000;
            request = new RestRequest(Method.POST);
            request.AddParameter("application/json", "{\"env\": \"kaka-ncov\",\"collection_name\": \"nCoV\"}", ParameterType.RequestBody);
            response = client.Execute(request);
            lb_Status.Invoke(d_a, "更新肺炎数据：create nCoV成功");
            Thread.Sleep(5000);
            
            url = "https://api.weixin.qq.com/tcb/databasemigrateimport?access_token=" + access_token;
            client = new RestClient(url);
            client.Timeout = 5000;
            request = new RestRequest(Method.POST);
            request.AddParameter("application/json", "{\"env\": \"kaka-ncov\",\"collection_name\": \"nCoV\",\"file_path\": \"" + fileName + "\",\"file_type\": 1,\"stop_on_error\": false,\"conflict_mode\": 1}", ParameterType.RequestBody);
            response = client.Execute(request);
            lb_Status.Invoke(d_a, "更新肺炎数据：更新数据成功");
            Thread.Sleep(5000);
            
            url = "https://api.weixin.qq.com/tcb/databaseupdate?access_token=" + access_token;
            client = new RestClient(url);
            client.Timeout = 5000;
            request = new RestRequest(Method.POST);
            request.AddParameter("application/json", "{\"env\": \"kaka-ncov\",\"query\": \"" + "db.collection('show').doc('show').update({data:{update : '" + DateTime.Now.ToString("MM-dd HH:mm") + "'}})" + "\"}", ParameterType.RequestBody);
            response = client.Execute(request);
            lb_Status.Invoke(d_a, "更新肺炎数据：更新日期成功");
            Thread.Sleep(5000);
            
            lb_Status.Invoke(d_a, "更新肺炎数据：所有更新完成");
        }

        public void RefreshFund()
        {
            string code; /*基金代码*/
            string name; /*基金简称*/
            string newnet; /*最新净值*/
            string totalnet; /*累计净值*/
            string dayincrease; /*日增长值*/
            string daygrowrate; /*日增长率%*/
            string weekgrowrate; /*周增长率%*/
            string monthgrowrate; /*月增长率%*/
            string time; /*最新净值时间*/
            
            DateTime updatatime;//更新时间


            delegation d_c = new delegation(GApp_Clear);
            lb_Status.Invoke(d_c,"");
            string appkey = "c5084b27fed7043fd49e98a989631c15"; //配置您申请的appkey
            string url = "http://web.juhe.cn:8080/fund/netdata/all";

            var parameters = new Dictionary<string, string>();
            parameters.Add("key", appkey);//你申请的key
            string result = sendPost(url, parameters, "get");
            Xfrog.Net.JsonObject jo = new Xfrog.Net.JsonObject(result);
            String errorCode = jo["error_code"].Value;
            if (errorCode == "0")
            {
                string strFund = JArray.Parse(jo["result"].ToString())[0].ToString();
                JObject jor = JObject.Parse(strFund);
                msql.I_D_U("delete from blue.fundnet");
                for (int i = 1; i < 1000000; i++)
                {
                    try
                    {
                        string strRow = jor[i.ToString()].ToString();
                        JObject _jor = JObject.Parse(strRow);
                        code = _jor["code"].ToString();
                        name = _jor["name"].ToString();
                        newnet = _jor["newnet"].ToString(); /*最新净值*/
                        totalnet = _jor["totalnet"].ToString(); /*累计净值*/
                        dayincrease = _jor["dayincrease"].ToString(); /*日增长值*/
                        daygrowrate = _jor["daygrowrate"].ToString(); /*日增长率%*/
                        weekgrowrate = _jor["weekgrowrate"].ToString(); /*周增长率%*/
                        monthgrowrate = _jor["monthgrowrate"].ToString(); /*月增长率%*/
                        time = _jor["time"].ToString(); /*最新净值时间*/
                        updatatime = DateTime.Now;
                        string com = "insert INTO blue.fundnet (code,name,newnet,totalnet,dayincrease,daygrowrate,weekgrowrate,monthgrowrate,time,updatatime)"
                            + "values('" + code + "','" + name + "','" + newnet + "','" + totalnet + "','" + dayincrease + "','" + daygrowrate + "%','" + weekgrowrate + "%','" + monthgrowrate + "%','" + time + "','" + updatatime + "')";
                        msql.I_D_U(com);
                        delegation d_a = new delegation(GApp_Add);
                        lb_Status.Invoke(d_a, code + "-" + name);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            else
            {
                //如果错误代码不为0，说明出现了异常。这个时候应该给我提示。
                //给我发邮件或者别的咯
            }
        }

        public string GetUrlResponse_2nd(string url)
        {
            string res = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //GET请求
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ReadWriteTimeout = 5000;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding("UTF-8"));
            res = sr.ReadToEnd();
            return res;
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

              /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <returns></returns>
        public static string Post(string url,Dictionary<string,object> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// Http (GET/POST)
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="method">请求方法</param>
        /// <returns>响应内容</returns>
        static string sendPost(string url, IDictionary<string, string> parameters, string method)
        {
            if (method.ToLower() == "post")
            {
                HttpWebRequest req = null;
                HttpWebResponse rsp = null;
                System.IO.Stream reqStream = null;
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = method;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version10;
                    req.Timeout = 5000;
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                    reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (reqStream != null) reqStream.Close();
                    if (rsp != null) rsp.Close();
                }
            }
            else
            {
                //创建请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));

                //GET请求
                request.Method = "GET";
                request.ReadWriteTimeout = 1000;
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //返回内容
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }


        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }

        private void b_Stop_Click(object sender, EventArgs e)
        {
            t_Time.Stop();
            b_Stop.Enabled = false;
            b_Start.Enabled = true;
            tssl_Status.Text = "整装待发";
            thread_nCoV.Abort();
        }

        private void b_FundNow_Click(object sender, EventArgs e)
        {
            thread_Fund = new Thread(new ThreadStart(RefreshFund));
            thread_Fund.IsBackground = true; // 定义为后台线程，后台线程将会随着主线程的退出而退出。
            thread_Fund.Start();
        }

        private void b_TuringNow_Click(object sender, EventArgs e)
        {
            thread_Turing = new Thread(new ThreadStart(RefreshTuring));
            thread_Turing.IsBackground = true; // 定义为后台线程，后台线程将会随着主线程的退出而退出。
            thread_Turing.Start();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("确认关闭？","警告",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (MessageBox.Show("请再次确认关闭？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (MessageBox.Show("最后一次确认关闭？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void b_MarsNow_Click(object sender, EventArgs e)
        {
            thread_Mars = new Thread(new ThreadStart(RefreshMars));
            thread_Mars.IsBackground = true; // 定义为后台线程，后台线程将会随着主线程的退出而退出。
            thread_Mars.Start();
        }


        private void b_NCOV_Click(object sender, EventArgs e)
        {
            NCOV();
        }

    }
}
