using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.IO;
using System.Threading;

namespace StockData
{
    public partial class FormMain : Form
    {
        private Thread thread_00;//00秒的线程
        private Thread thread_20;//20秒的线程
        private Thread thread_40;//40秒的线程
        private Thread thread_ToDB;//入库的线程
        private Thread thread_300;//沪深300线程
        bool isGetStockList;
        MySQL ms = new MySQL("stock");
        DataTable StockDT = new DataTable();
        DataTable CloseDT = new DataTable();
        string L;

        string Path = AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\";
        string Pic = "C:\\Blue\\300\\";
        WebBrowser wb_Capture = new WebBrowser();  // 创建一个WebBrowser
        string PicName;

        public FormMain()
        {
            InitializeComponent();
        }

        private void b_Start_Click(object sender, EventArgs e)
        {
            b_Start.Enabled = false;
            b_Stop.Enabled = true;
            t_Data.Start();
        }

        private void b_Stop_Click(object sender, EventArgs e)
        {
            b_Start.Enabled = true;
            b_Start.Enabled = false;
            t_Data.Stop();
        }

        public delegate void delegation(string cur);

        public void GApp_Add(string cur)
        {
            lb_Now.Items.Add(DateTime.Now.ToString() + "   " + cur);
            lb_Now.TopIndex = lb_Now.Items.Count - 1;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清除0000表？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ms.I_D_U("delete from stock.0000");
            }
            b_Start.Enabled = true;
            b_Stop.Enabled = false;
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
                Directory.CreateDirectory(Path);
            }
            else
            {
                Directory.CreateDirectory(Path);
            }
            if(!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            //第一次执行的时候，肯定是没有获取StockList
            CloseDT = ms.S("select * from stock.closeday");
            isGetStockList = false;
            lb_Now.Items.Clear();
            thread_00 = new Thread(new ThreadStart(Get00));
            thread_20 = new Thread(new ThreadStart(Get20));
            thread_40 = new Thread(new ThreadStart(Get40));
            thread_ToDB = new Thread(new ThreadStart(ToDB));
            thread_300 = new Thread(new ThreadStart(Quant300));
            t_Main.Start();
        }
        /// <summary>
        /// t_Main的属性有，30秒一个tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t_Main_Tick(object sender, EventArgs e)
        {
            //每天零点，初始化
            if(DateTime.Now.ToString("HH:mm") == "00:00")
            {
                lb_Now.Items.Clear();
                ms.I_D_U("delete from stock.0000");
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\"))
                {
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\", true);
                }
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\");
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\"))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\");
                isGetStockList = false;
                CloseDT = ms.S("select * from stock.closeday");
            }
        }

        private void t_Data_Tick(object sender, EventArgs e)
        {
            //12月份，除了周六周末都是交易日，等交易所公布了休市方案，这段代码记得改。
            if(DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                DataRow[] drArr = CloseDT.Select("day = " + DateTime.Now.ToString("yyyyMMdd"));
                if(drArr.Length == 0)
                {
                    //获取当日交易的股票列表
                    if (DateTime.Now.ToString("HH:mm") == dtp_GetStock.Value.ToString("HH:mm") && isGetStockList == false)
                    {
                        isGetStockList = true;
                        GetStockList();
                    }
                    //获取原始数据
                    if (((Convert.ToInt32(DateTime.Now.ToString("HHmm")) > Convert.ToInt32(dtp_AM_BeginTime.Value.ToString("HHmm"))) && (Convert.ToInt32(DateTime.Now.ToString("HHmm")) < Convert.ToInt32(dtp_AM_EndTime.Value.ToString("HHmm")))) || ((Convert.ToInt32(DateTime.Now.ToString("HHmm")) > Convert.ToInt32(dtp_PM_BeginTime.Value.ToString("HHmm"))) && (Convert.ToInt32(DateTime.Now.ToString("HHmm")) < Convert.ToInt32(dtp_PM_EndTime.Value.ToString("HHmm")))))
                    {
                        switch (DateTime.Now.ToString("ss"))
                        {
                            case "00":
                                {
                                    if (thread_00.IsAlive == false)
                                    {
                                        thread_00 = new Thread(new ThreadStart(Get00));
                                        thread_00.IsBackground = true;
                                        thread_00.Start();
                                    }
                                }
                                break;
                            case "20":
                                {
                                    if (thread_20.IsAlive == false)
                                    {
                                        thread_20 = new Thread(new ThreadStart(Get20));
                                        thread_20.IsBackground = true;
                                        thread_20.Start();
                                    }
                                }
                                break;
                            case "40":
                                {
                                    if (thread_40.IsAlive == false)
                                    {
                                        thread_40 = new Thread(new ThreadStart(Get40));
                                        thread_40.IsBackground = true;
                                        thread_40.Start();
                                    }
                                }
                                break;
                        }
                    }
                    //原始数据入库
                    if (DateTime.Now.ToString("HH:mm") == dtp_AM_ToDB.Value.ToString("HH:mm") && thread_ToDB.IsAlive == false && StockDT.Rows.Count > 0)
                    {
                        L = "AM ";
                        thread_ToDB = new Thread(new ThreadStart(ToDB));
                        thread_ToDB.IsBackground = true;
                        thread_ToDB.Start();
                    }
                    //原始数据入库
                    if (DateTime.Now.ToString("HH:mm") == dtp_PM_ToDB.Value.ToString("HH:mm") && thread_ToDB.IsAlive == false && StockDT.Rows.Count > 0)
                    {
                        L = "PM ";
                        thread_ToDB = new Thread(new ThreadStart(ToDB));
                        thread_ToDB.IsBackground = true;
                        thread_ToDB.Start();
                    }
                    //量化分析沪深300
                    if (DateTime.Now.ToString("HH:mm") == dtp_Quant300.Value.ToString("HH:mm") && thread_300.IsAlive == false)
                    {
                        StockDT = ms.S("SELECT * FROM STOCK.TRADE");
                        thread_300 = new Thread(new ThreadStart(Quant300));
                        //thread_300.SetApartmentState(ApartmentState.STA);
                        thread_300.IsBackground = true;
                        thread_300.Start();
                    }
                }
            }
        }

        /// <summary>
        /// 量化分析沪深300
        /// </summary>
        private void Quant300()
        {
            delegation d_a = new delegation(GApp_Add);
            lb_Now.Invoke(d_a, "开始量化分析");
            string com = "";
            //Step 1 整理数据
            // SQL 去头去尾
            #region 整理数据
            lb_Now.Invoke(d_a, "开始整理数据");
            com = "delete from stock.0000 where time <= '09:30:00' or time >= '15:00:00'";
            ms.I_D_U(com);
            lb_Now.Invoke(d_a, "20%");
            com = "delete from stock.0000 where time >= '11:30:00' and time <= '13:00:00'";
            ms.I_D_U(com);
            lb_Now.Invoke(d_a, "40%");
            com = "update stock.0000 set fluctuation = close/open";
            ms.I_D_U(com);
            lb_Now.Invoke(d_a, "60%");
            com = "delete from stock.0000 where code in (select code from(select code,sum(fluctuation) as s from stock.0000 group by code) a where a.s = 720) ";
            ms.I_D_U(com);
            lb_Now.Invoke(d_a, "80%");
            com = "delete from stock.result ";
            ms.I_D_U(com);
            lb_Now.Invoke(d_a, "100%");
            lb_Now.Invoke(d_a, "整理数据完毕");
            #endregion
            //Step 2 计算拟合度
            #region 计算拟合度
            com = "select * from stock.300";
            DataTable dt_300 = new DataTable();
            dt_300 = ms.S(com);
            SimHash sh = new SimHash();
            DataTable dt_0000 = new DataTable();
            com = "select fluctuation,code from stock.0000 order by time";
            dt_0000 = ms.S(com);
            foreach (DataRow dr_300 in dt_300.Rows)
            {
                DataRow[] drxArr = dt_0000.Select("code = '" + dr_300[1] + "'");
                if(drxArr.Length == 0)
                {
                    continue;
                }
                string str_Ins = "Insert into stock.result (code,object,sim) values";
                foreach (DataRow dr_Trade in StockDT.Rows)
                {
                    DataTable dt_y = new DataTable();
                    DataRow[] dryArr = dt_0000.Select("code = '" + dr_Trade[0] + "'");
                    double f = sh.Cosine(drxArr, dryArr);
                    string temp = "('" + dr_300[1] + "','" + dr_Trade[0] + "','" + f + "'),";
                    temp = temp.Replace("非数字", "0");
                    str_Ins = str_Ins + temp;
                }
                str_Ins = str_Ins.Substring(0, str_Ins.Length - 1);
                try
                {
                    ms.I_D_U(str_Ins);
                }
                catch
                {
                    Thread.Sleep(5000);
                    try
                    {
                        Thread.Sleep(5000);
                        ms.I_D_U(str_Ins);
                    }
                    catch
                    {
                        Thread.Sleep(5000);
                        try
                        {
                            ms.I_D_U(str_Ins);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                lb_Now.Invoke(d_a, dr_300[1] + "量化完成");
            }
            lb_Now.Invoke(d_a, "计算拟合度完毕");
            #endregion
            //Step 2.5 下载图片
            #region 下载图片
            string StockPicPath = "C:\\Blue\\StockPic\\";
            if (Directory.Exists(StockPicPath))
            {
                Directory.Delete(StockPicPath, true);
                Directory.CreateDirectory(StockPicPath);
            }
            else
            {
                Directory.CreateDirectory(StockPicPath);
            }
            if (!Directory.Exists(StockPicPath))
                Directory.CreateDirectory(StockPicPath);
            foreach (DataRow drDown in StockDT.Rows)
            {
                try
                {
                    WebRequest request = WebRequest.Create("http://image.sinajs.cn/newchart/min/n/" + drDown[0] + ".gif");
                    WebResponse response = request.GetResponse();
                    Stream reader = response.GetResponseStream();
                    FileStream writer = new FileStream(StockPicPath + drDown[0] + ".gif", FileMode.OpenOrCreate, FileAccess.Write);
                    byte[] buff = new byte[512];
                    int c = 0; //实际读取的字节数
                    while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                    {
                        writer.Write(buff, 0, c);
                    }
                    writer.Close();
                    writer.Dispose();
                    reader.Close();
                    reader.Dispose();
                    response.Close();
                    lb_Now.Invoke(d_a, drDown[0] + "下载完成");
                }
                catch
                {

                }
            }
            lb_Now.Invoke(d_a, "下载完毕");
            #endregion
            //Step 3 HTML
            #region HTML

            if (!Directory.Exists(Pic))
            {
                Directory.CreateDirectory(Pic);
            }

            DataTable dt_result = new DataTable();
            foreach (DataRow dr_300 in dt_300.Rows)
            {
                try
                {
                    PicName = dr_300[1].ToString();
                    dt_result = ms.S("select object from stock.result where code = '" + dr_300[1] + "' and object != '" + dr_300[1] + "' order by sim desc LIMIT 10");
                    string html = "";
                    html = html + "<html xmlns=http://www.w3.org/1999/xhtml>";
                    html = html + "<head>";
                    html = html + "<meta http-equiv=Content-Type content=text/html; charset=utf-8 />";
                    html = html + "<title>" + dr_300[1].ToString().Substring(2,6) + "  " + dr_300[2] + "</title>";
                    html = html + "</head>";
                    html = html + "<body>";
                    html = html + "<h3>（如未显示图片，请点击页面底部的【原网页】）<h3>";
                    html = html + "<h2>更新时间为：" + DateTime.Now.ToString("yyyy-MM-dd") + "</h2>";
                    html = html + "<h1>请求的股票为：</h1>";
                    html = html + "<img " + ImageToBase64("C:\\Blue\\StockPic\\" + dr_300[1] + ".gif") + " width=100% height=600>";
                    if (dt_result.Rows.Count != 10)
                    {
                        html = html + "<h1>该股票为停牌股</h1>";
                    }
                    else
                    {
                        html = html + "<h1>涨跌幅相似度从大到小排序的前十只依次为：</h1>";
                        html = html + "<h2>（大家看蓝色线条，是不是长得挺像呢。）</h2>";
                        html = html + "<img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[0][0] + ".gif") + " width=100% height=600>";
                        html = html + "<br /><img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[1][0] + ".gif") + " width=100% height=600>";
                        html = html + "<br /><img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[2][0] + ".gif") + " width=100% height=600>";
                        html = html + "<br /><img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[3][0] + ".gif") + " width=100% height=600>";
                        html = html + "<br /><img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[4][0] + ".gif") + " width=100% height=600>";
                        html = html + "<br /><img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[5][0] + ".gif") + " width=100% height=600>";
                        html = html + "<br /><img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[6][0] + ".gif") + " width=100% height=600>";
                        html = html + "<br /><img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[7][0] + ".gif") + " width=100% height=600>";
                        html = html + "<br /><img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[8][0] + ".gif") + " width=100% height=600>";
                        html = html + "<br /><img " + ImageToBase64("C:\\Blue\\StockPic\\" + dt_result.Rows[9][0] + ".gif") + " width=100% height=600>";
                    }
                    html = html + "<br /><h2>本功能所载的资料、工具及材料只提供给阁下作查照之用，并非作为或被视为出售或购买或认购证券或其它金融票据的建议或研究观点。所采用的信息均来源于已公开的资料，我本人对这些信息的准确性及完整性不作任何保证。市场有风险，投资需谨慎。</h2>";
                    html = html + "</body>";
                    html = html + "</html>";
                    StreamWriter sw = new StreamWriter(Pic + PicName + ".html",false, Encoding.UTF8);
                    sw.WriteLine(html);
                    sw.Close();
                    lb_Now.Invoke(d_a, PicName + "HTML完成");
                }
                catch
                {

                }
            }
            lb_Now.Invoke(d_a, "HTML完毕");
            #endregion
            lb_Now.Invoke(d_a, "量化分析结束");
        }

        private string ImageToBase64(string filepath)
        {
            Bitmap bmp = new Bitmap(filepath);
            MemoryStream mStream = new MemoryStream();
            bmp.Save(mStream, System.Drawing.Imaging.ImageFormat.Gif);
            byte[] arr = new byte[mStream.Length];
            mStream.Position = 0;
            mStream.Read(arr, 0, (int)mStream.Length);
            mStream.Close();
            string strbase64 = Convert.ToBase64String(arr);
            strbase64 = "src=data:image/gif;base64," + strbase64;
            return strbase64;
        }

        /// <summary>
        /// 把文件里的数据读入数据库
        /// </summary>
        private void ToDB()
        {
            delegation d_a = new delegation(GApp_Add);
            long FileTime = 0;
            string[] FileArr = System.IO.Directory.GetFiles(Path);
            while(FileArr.Length > 0)
            {
                //对FileArr 重新赋值
                FileArr = System.IO.Directory.GetFiles(Path);
                //给一次机会
                if(FileArr.Length == 0)
                {
                    Thread.Sleep(120000);
                    continue;
                }
                Array.Sort(FileArr);
                //如果第一个文件的创建时间小于两分钟，那么有可能还没写好，等待两分钟
                FileInfo fi = new FileInfo(FileArr[0]);
                if (DateTime.Now.Subtract(fi.CreationTime).TotalSeconds < 120)
                    Thread.Sleep(120000);
                //如果时间小于10秒
                if (fi.CreationTime.Ticks - FileTime < 100000000)
                {
                    FileTime = fi.CreationTime.Ticks;
                }
                else
                {
                    FileTime = fi.CreationTime.Ticks;
                    //用 ; 对股票进行拆分
                    StreamReader sr = new StreamReader(FileArr[0]);
                    string[] FileContent = sr.ReadToEnd().Split(';');
                    string com = "INSERT INTO stock.0000 (code,date,time,open,close) values";
                    foreach(string FileLine in FileContent)
                    {
                        //用 , 对股票字段进行拆分
                        string[] StockContent = FileLine.Split(',');
                        if(StockContent.Length == 33)
                        {
                            string code = StockContent[0].Substring(StockContent[0].LastIndexOf("_") + 1, 8);
                            string date = StockContent[30];
                            string time = StockContent[31];
                            string open = StockContent[2];
                            string close = StockContent[3];
                            com = com + "('" + code + "','" + date + "','" + time + "','" + open + "','" + close + "'),";
                        }
                    }
                    com = com.Substring(0, com.Length - 1);
                    ms.I_D_U(com);
                    sr.Close();
                }
                File.Delete(FileArr[0]);
                string filename_p = FileArr[0].Substring(FileArr[0].LastIndexOf('\\') + 1, FileArr[0].Length - FileArr[0].LastIndexOf('\\') - 1);
                lb_Now.Invoke(d_a, filename_p + "读取成功。");
                //对FileArr 重新赋值
                FileArr = System.IO.Directory.GetFiles(Path);
            }
            lb_Now.Invoke(d_a, L + "入库完成。");
        }

        /// <summary>
        /// 获取00秒的原始数据
        /// </summary>
        private void Get00()
        {
            int i = 0;
            int j = 0;
            string str = "";
            for(j = 0;j<StockDT.Rows.Count + 600;j += 600)
            {
                string url = "http://hq.sinajs.cn/list=";
                if (j > StockDT.Rows.Count)
                {
                    j = StockDT.Rows.Count;
                }
                if(j >0)
                {
                    for (; i < j; i++)
                    {
                        url = url + StockDT.Rows[i][0] + ",";
                    }
                    url = url.Substring(0, url.Length - 1);
                    //创建请求
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    //GET请求
                    request.Method = "get";
                    request.ContentType = "text/html";
                    request.ReadWriteTimeout = 1000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = response.GetResponseStream();
                    StreamReader sr = new StreamReader(s, Encoding.GetEncoding("gb2312"));
                    //返回内容
                    str = str + sr.ReadToEnd();
                }
            }
            string filename_p = DateTime.Now.Ticks + "_00.txt";
            StreamWriter sw = new StreamWriter(Path + DateTime.Now.Ticks  + "_00.txt", false);
            sw.WriteLine(str);
            sw.Close();
            delegation d_a = new delegation(GApp_Add);
            lb_Now.Invoke(d_a, filename_p + "写入成功。");
        }
        /// <summary>
        /// 获取20秒的原始数据
        /// </summary>
        private void Get20()
        {
            int i = 0;
            int j = 0;
            string str = "";
            for (j = 0; j < StockDT.Rows.Count + 600; j += 600)
            {
                string url = "http://hq.sinajs.cn/list=";
                if (j > StockDT.Rows.Count)
                {
                    j = StockDT.Rows.Count;
                }
                if (j > 0)
                {
                    for (; i < j; i++)
                    {
                        url = url + StockDT.Rows[i][0] + ",";
                    }
                    url = url.Substring(0, url.Length - 1);
                    //创建请求
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    //GET请求
                    request.Method = "get";
                    request.ContentType = "text/html";
                    request.ReadWriteTimeout = 1000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = response.GetResponseStream();
                    StreamReader sr = new StreamReader(s, Encoding.GetEncoding("gb2312"));
                    //返回内容
                    str = str + sr.ReadToEnd();
                }
            }
            string filename_p = DateTime.Now.Ticks + "_20.txt";
            StreamWriter sw = new StreamWriter(Path + filename_p, false);
            sw.WriteLine(str);
            sw.Close();
            delegation d_a = new delegation(GApp_Add);
            lb_Now.Invoke(d_a, filename_p + "写入成功。");
        }
        /// <summary>
        /// 获取20秒的原始数据
        /// </summary>
        private void Get40()
        {
            int i = 0;
            int j = 0;
            string str = "";
            for (j = 0; j < StockDT.Rows.Count + 600; j += 600)
            {
                string url = "http://hq.sinajs.cn/list=";
                if (j > StockDT.Rows.Count)
                {
                    j = StockDT.Rows.Count;
                }
                if (j > 0)
                {
                    for (; i < j; i++)
                    {
                        url = url + StockDT.Rows[i][0] + ",";
                    }
                    url = url.Substring(0, url.Length - 1);
                    //创建请求
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    //GET请求
                    request.Method = "get";
                    request.ContentType = "text/html";
                    request.ReadWriteTimeout = 1000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = response.GetResponseStream();
                    StreamReader sr = new StreamReader(s, Encoding.GetEncoding("gb2312"));
                    //返回内容
                    str = str + sr.ReadToEnd();
                }
            }
            string filename_p = DateTime.Now.Ticks + "_40.txt";
            StreamWriter sw = new StreamWriter(Path + filename_p, false);
            sw.WriteLine(str);
            sw.Close();
            delegation d_a = new delegation(GApp_Add);
            lb_Now.Invoke(d_a, filename_p + "写入成功。");
        }

        public void StockListFunc()
        {
            List<string> StockList = new List<string>();//股市列表
            string url = "http://www.shdjt.com/js/lib/astock.js";
            //创建请求
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            //GET请求
            hwr.Method = "GET";
            hwr.ReadWriteTimeout = 1000;
            hwr.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse hwp = (HttpWebResponse)hwr.GetResponse();
            Stream s = hwp.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding("utf-8"));
            //返回内容
            string str = sr.ReadToEnd();
            str = str.Substring(str.IndexOf("\"") + 2, str.Length - str.IndexOf("\"") - 2);
            string[] StockArr = str.Split('~');
            int Stock_Index = 0;
            foreach (string code in StockArr)
            {
                string Stock_code = code.Substring(0, 6);
                Stock_Index++;
                if (Stock_code.StartsWith("000"))
                    Stock_code = "sz" + Stock_code;
                if (Stock_code.StartsWith("001"))
                    Stock_code = "sz" + Stock_code;
                if (Stock_code.StartsWith("002"))
                    Stock_code = "sz" + Stock_code;
                if (Stock_code.StartsWith("300"))
                    Stock_code = "sz" + Stock_code;
                if (Stock_code.StartsWith("6"))
                    Stock_code = "sh" + Stock_code;
                if (StockList.Contains(Stock_code))
                {
                    continue;
                }
                else
                {
                    if(Stock_code.StartsWith("sh") || Stock_code.StartsWith("sz"))
                        StockList.Add(Stock_code);
                }
            }
            if (StockList.Count > 0)    
            {
                string com = "DELETE FROM stock.trade;INSERT INTO STOCK.TRADE (CODE) VALUES ";
                foreach (string code in StockList)
                {
                    com = com + "('" + code + "'),";
                }
                com = com.Substring(0, com.Length - 1);
                ms.I_D_U(com);
                GApp_Add("今日交易的股票列表获取成功。");
            }
            StockDT = ms.S("select * from stock.trade");
        }

        /// <summary>
        /// 获取今日交易的股票列表
        /// </summary>
        private void GetStockList()
        {
            try
            {
                StockListFunc();
            }
            catch
            {
                try
                {
                    Thread.Sleep(60000);
                    StockListFunc();
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(60000);
                        StockListFunc();
                    }
                    catch
                    {
                        StockDT = ms.S("select * from stock.trade");
                    }
                }

            }
           
        }
        /// <summary>
        /// 关闭的时候再三确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认关闭？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
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
    }
}
