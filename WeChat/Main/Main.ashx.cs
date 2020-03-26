using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Xml.Linq;
using System.Text;
using System.Net;
using System.Data;
using System.Text.RegularExpressions;
using API;

namespace Main
{
    /// <summary>
    /// Main 的摘要说明
    /// </summary>
    public class Main : IHttpHandler
    {
        private string Token = "token";

        DataTable dt = new DataTable();
        MySQL msql = new MySQL("blue");
        string OurID = "";
        string CounterID = "";

        public void ProcessRequest(HttpContext context)
        {
            if(context.Request.HttpMethod == "GET")
            {
                BaseServices.ValidUrl(Token);
            }
            if(context.Request.HttpMethod == "POST")
            {
                using (StreamReader sr = new StreamReader(context.Request.InputStream))
                {
                    string str = sr.ReadToEnd();
                    //这时候其实应该新建工具类的，暂时放一放吧。先快速的把功能实现。
                    XElement xe = XElement.Parse(str);
                    string MsgType = xe.Element("MsgType").Value;
                    OurID = xe.Element("ToUserName").Value;
                    CounterID = xe.Element("FromUserName").Value;

                    if (MsgType != "text" && MsgType != "event" && MsgType != "voice" && MsgType != "location" && MsgType != "image")
                    {
                        Msg.RetrurnTextMsg(CounterID, OurID, "抱歉，暂不支持此类消息。");
                    }

                    if(MsgType == "event")
                    {
                        string Event = xe.Element("Event").Value.Trim();
                        if (Event == "CLICK" || Event == "VIEW" || Event == "pic_photo_or_album")
                        {
                            #region CLICK
                            string EventKey = xe.Element("EventKey").Value.Trim();
                            switch(EventKey)
                            {
                                case "查快递状态":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        Msg.RetrurnTextMsg(CounterID, OurID, "查快递状态\n请输入快递单号，如： 883000768272760447");
                                    }
                                    break;
                                case "查基金":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        Msg.RetrurnTextMsg(CounterID, OurID, "查基金\n请输入基金代码或基金名称。如：000001或华夏成长\n（支持模糊查询）\n注：服务器每天凌晨自动更新前一天的基金数据。");
                                        //string[] titles = new string[] { "1", "2", "3"};
                                        //string[] dess = new string[] { "one", "two", "three"};
                                        //string[] pics = new string[] {"https://mmbiz.qpic.cn/mmbiz_jpg/Cf9GRFAWRNJZ29SDMydqYQ0dBkficAP0Aj6nGRHEbMAhtvDRozQLBRsVskDrb4kyPd6JJqZBwSMCjjd6Jo9hu6w/0?wx_fmt=jpeg","https://mmbiz.qpic.cn/mmbiz_jpg/Cf9GRFAWRNJZ29SDMydqYQ0dBkficAP0Aj6nGRHEbMAhtvDRozQLBRsVskDrb4kyPd6JJqZBwSMCjjd6Jo9hu6w/0?wx_fmt=jpeg","https://mmbiz.qpic.cn/mmbiz_jpg/Cf9GRFAWRNJZ29SDMydqYQ0dBkficAP0Aj6nGRHEbMAhtvDRozQLBRsVskDrb4kyPd6JJqZBwSMCjjd6Jo9hu6w/0?wx_fmt=jpeg"};
                                        //string[] urls = new string[] {"","https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=6&sn=989b0faf398ee1ebdbe7e5303a142fe6","https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=6&sn=989b0faf398ee1ebdbe7e5303a142fe6"};
                                        //Msg.ReturnNewsMsg(CounterID,OurID,3,titles,dess,pics,urls);
                                    }
                                    break;
                                case "数学的笔记与练习":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        com = "SELECT * FROM blue.articles where valid = '1' order by articleId";
                                        dt = msql.S(com);
                                        string[] titles = new string[dt.Rows.Count];
                                        string[] dess = new string[dt.Rows.Count];
                                        string[] pics = new string[dt.Rows.Count];
                                        string[] urls = new string[dt.Rows.Count];
                                        int i = 0;
                                        foreach(DataRow dr in dt.Rows)
                                        {
                                            titles[i] = dr["title"].ToString();
                                            dess[i] = dr["des"].ToString();
                                            pics[i] = dr["pic"].ToString();
                                            urls[i] = dr["url"].ToString();
                                            i = i + 1;
                                        }
                                        //string[] titles = Convert.ToString(dt.Columns["title"]);
                                        //Msg.RetrurnTextMsg(CounterID, OurID, "查基金\n请输入基金代码或基金名称。如：000001或华夏成长\n（支持模糊查询）\n注：服务器每天凌晨自动更新前一天的基金数据。");
                                        //string[] titles = new string[] { "1", "2", "3"};
                                        //string[] dess = new string[] { "one", "two", "three"};
                                        //string[] pics = new string[] {"https://mmbiz.qpic.cn/mmbiz_jpg/Cf9GRFAWRNJZ29SDMydqYQ0dBkficAP0Aj6nGRHEbMAhtvDRozQLBRsVskDrb4kyPd6JJqZBwSMCjjd6Jo9hu6w/0?wx_fmt=jpeg","https://mmbiz.qpic.cn/mmbiz_jpg/Cf9GRFAWRNJZ29SDMydqYQ0dBkficAP0Aj6nGRHEbMAhtvDRozQLBRsVskDrb4kyPd6JJqZBwSMCjjd6Jo9hu6w/0?wx_fmt=jpeg","https://mmbiz.qpic.cn/mmbiz_jpg/Cf9GRFAWRNJZ29SDMydqYQ0dBkficAP0Aj6nGRHEbMAhtvDRozQLBRsVskDrb4kyPd6JJqZBwSMCjjd6Jo9hu6w/0?wx_fmt=jpeg"};
                                        //string[] urls = new string[] {"","https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=6&sn=989b0faf398ee1ebdbe7e5303a142fe6","https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=6&sn=989b0faf398ee1ebdbe7e5303a142fe6"};
                                        Msg.ReturnNewsMsg(CounterID,OurID,dt.Rows.Count,titles,dess,pics,urls);
                                    }
                                    break;
                                case "计量金融学的Python实现":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        com = "SELECT * FROM blue.articles where valid = '3' order by articleId";
                                        dt = msql.S(com);
                                        string[] titles = new string[dt.Rows.Count];
                                        string[] dess = new string[dt.Rows.Count];
                                        string[] pics = new string[dt.Rows.Count];
                                        string[] urls = new string[dt.Rows.Count];
                                        int i = 0;
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            titles[i] = dr["title"].ToString();
                                            dess[i] = dr["des"].ToString();
                                            pics[i] = dr["pic"].ToString();
                                            urls[i] = dr["url"].ToString();
                                            i = i + 1;
                                        }
                                        //string[] titles = Convert.ToString(dt.Columns["title"]);
                                        //Msg.RetrurnTextMsg(CounterID, OurID, "查基金\n请输入基金代码或基金名称。如：000001或华夏成长\n（支持模糊查询）\n注：服务器每天凌晨自动更新前一天的基金数据。");
                                        //string[] titles = new string[] { "1", "2", "3"};
                                        //string[] dess = new string[] { "one", "two", "three"};
                                        //string[] pics = new string[] {"https://mmbiz.qpic.cn/mmbiz_jpg/Cf9GRFAWRNJZ29SDMydqYQ0dBkficAP0Aj6nGRHEbMAhtvDRozQLBRsVskDrb4kyPd6JJqZBwSMCjjd6Jo9hu6w/0?wx_fmt=jpeg","https://mmbiz.qpic.cn/mmbiz_jpg/Cf9GRFAWRNJZ29SDMydqYQ0dBkficAP0Aj6nGRHEbMAhtvDRozQLBRsVskDrb4kyPd6JJqZBwSMCjjd6Jo9hu6w/0?wx_fmt=jpeg","https://mmbiz.qpic.cn/mmbiz_jpg/Cf9GRFAWRNJZ29SDMydqYQ0dBkficAP0Aj6nGRHEbMAhtvDRozQLBRsVskDrb4kyPd6JJqZBwSMCjjd6Jo9hu6w/0?wx_fmt=jpeg"};
                                        //string[] urls = new string[] {"","https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=6&sn=989b0faf398ee1ebdbe7e5303a142fe6","https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=6&sn=989b0faf398ee1ebdbe7e5303a142fe6"};
                                        Msg.ReturnNewsMsg(CounterID, OurID, dt.Rows.Count, titles, dess, pics, urls);
                                    }
                                    break;
                                case "蓝色机器人":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        Msg.RetrurnTextMsg(CounterID, OurID, "您好，我是人工智能机器人。请问需要什么帮助？\n\n【类似Siri】");
                                    }
                                    break;
                                case "量化分析沪深300成份股":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        Msg.RetrurnTextMsg(CounterID, OurID, "量化分析沪深300成份股\n1、该功能将最近一个交易日的沪深300成份股与最近一个交易日的所有沪深A股进行量化分析，求涨跌幅的相似度。\n2、<a href=\"http://www.csindex.com.cn/zh-CN/indices/index-detail/000300\">点击查看关于沪深300指数</a>\n\n请回复沪深300成份股代码或名称，如000001或平安银行\n支持模糊查询");
                                    }
                                    break;
                                case "人工智能写宋词":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        dt = msql.S("SELECT * from song.rnt");
                                        string song = dt.Rows[0][0].ToString();
                                        Msg.RetrurnTextMsg(CounterID, OurID, "人工智能写宋词\n服务器每五分钟自动酝酿一篇宋词。\n\n" + song + "\n\n倘若这首词写得不太好，还请谅解。	\ue41d");
                                    }
                                    break;
                                case "测海拔高度":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        Msg.RetrurnTextMsg(CounterID, OurID, "测海拔高度\n请发送微信位置");
                                    }
                                    break;
                                case "人工智能写唐诗":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        Tang t = new Tang();
                                        string msg = t.RntMsg();
                                        Msg.RetrurnTextMsg(CounterID, OurID, msg);
                                    }
                                    break;
                                case "探索火星":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        MarsCuriosity mc = new MarsCuriosity();
                                        String msg = mc.ReturnLast();
                                        Msg.RetrurnTextMsg(CounterID, OurID, msg);
                                    }
                                    break;
                                case "花是什么花":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                        Msg.RetrurnTextMsg(CounterID, OurID, "花是什么花\n请发送特写照片\n\n不只可以识别花哦");
                                    }
                                    break;
                                case "色觉辨认助手":
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + EventKey + "')";
                                        msql.I_D_U(com);
                                    }
                                    break;
                                case "C# ⇋ Java":
                                    {
                                        string com = "select count(*) FROM blue.ch where cid = '" + CounterID + "' and handle = 'C# To Java'";
                                        DataTable dt = msql.S(com);
                                        if(dt.Rows[0][0].ToString() == "0")
                                        {
                                            com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                            com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + "C# To Java" + "')";
                                            msql.I_D_U(com);
                                            Msg.RetrurnTextMsg(CounterID, OurID, "当前操作为C# To Java\n如需Java To C#，请再次点击菜单。\n\n请输入代码");
                                        }
                                        else
                                        {
                                            com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                            com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + "Java To C#" + "')";
                                            msql.I_D_U(com);
                                            Msg.RetrurnTextMsg(CounterID, OurID, "当前操作为Java To C#\n如需C# To Java，请再次点击菜单。\n\n请输入代码");
                                        }
                                    }
                                    break;
                                default:
                                    {
                                        string com = "DELETE from blue.ch where cid = '" + CounterID + "';";
                                        com = com + "INSERT INTO blue.ch (cid,handle) values ('" + CounterID + "','" + "公众号文章" + "')";
                                        msql.I_D_U(com);
                                    }
                                    break;
                            }
                            #endregion
                        }
                        else
                        {
                            #region subscribe
                            if (Event == "subscribe")
                            {
                                Msg.RetrurnTextMsg(CounterID, OurID, "BlueYifan\n\n数学、便捷与更多。");
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        #region text voice
                        if (MsgType == "text" || MsgType == "voice")
                        {
                            
                            //传进来的时候，就把空格去掉。
                            string Content = "";
                            if (MsgType == "text")
                                Content = xe.Element("Content").Value.Trim();
                            else
                                Content = xe.Element("Recognition").Value.Trim();
                            dt = msql.S("SELECT handle from blue.ch where cid = '" + CounterID + "'");
                            if (dt.Rows.Count == 0)
                            {
                                BlueRobot br = new BlueRobot();
                                string msg = br.RntMsg(Content, CounterID);
                                Msg.RetrurnTextMsg(CounterID, OurID, msg);
                                return;
                            }
                            switch (dt.Rows[0][0].ToString())
                            {
                                case "查快递状态":
                                    {
                                        Regex r = new Regex(@"[\u4e00-\u9fa5]");
                                        if (r.IsMatch(Content))
                                        {
                                            Msg.RetrurnTextMsg(CounterID, OurID, "请输入正确的快递单号。");
                                        }
                                        else
                                        {
                                            BlueRobot br = new BlueRobot();
                                            string msg = br.RntMsg("查快递 " + Content, CounterID);
                                            string[] a = msg.Split('\n');
                                            string rnt = "";
                                            foreach(string astr in a)
                                            {
                                                rnt = astr + "\n" + rnt;
                                            }
                                            Msg.RetrurnTextMsg(CounterID, OurID, rnt);
                                        }
                                    }
                                    break;
                                case "查基金":
                                    {
                                        Fund f = new Fund();
                                        string msg = f.FundNet(Content);
                                        Msg.RetrurnTextMsg(CounterID, OurID, msg);
                                    }
                                    break;
                                case "蓝色机器人":
                                    {
                                        BlueRobot br = new BlueRobot();
                                        if(Content == "【收到不支持的消息类型，暂无法显示】")
                                        {
                                            Msg.RetrurnTextMsg(CounterID, OurID, "我还理解不了表情包这种复杂的人类情感。");
                                        }
                                        else
                                        {
                                            string msg = br.RntMsg(Content, CounterID);
                                            Msg.RetrurnTextMsg(CounterID, OurID, msg);
                                        }
                                    }
                                    break;
                                case "量化分析沪深300成份股":
                                    {
                                        Quant300 q3 = new Quant300();
                                        string msg = q3.Rnt(Content);
                                        Msg.RetrurnTextMsg(CounterID, OurID, msg);
                                    }
                                    break;
                                case "探索火星":
                                    {
                                        MarsCuriosity mc = new MarsCuriosity();
                                        string msg = mc.ReturnOrder(Content);
                                        Msg.RetrurnTextMsg(CounterID, OurID, msg);
                                    }
                                    break;
                                case "C# To Java":
                                case "Java To C#":
                                    {
                                        CodeConvert c = new CodeConvert();
                                        string msg = c.RntMsg(dt.Rows[0][0].ToString(), Content);
                                        Msg.RetrurnTextMsg(CounterID, OurID, msg);
                                    }
                                    break;
                                default:
                                    {
                                        BlueRobot br = new BlueRobot();
                                        if (Content == "【收到不支持的消息类型，暂无法显示】")
                                        {
                                            Msg.RetrurnTextMsg(CounterID, OurID, "我还理解不了表情包这种复杂的人类情感。");
                                        }
                                        else
                                        {
                                            string msg = br.RntMsg(Content, CounterID);
                                            Msg.RetrurnTextMsg(CounterID, OurID, msg);
                                        }
                                    }
                                    break;
                            }
                        }
                        #endregion

                        if(MsgType == "location")
                        {
                            string Label = xe.Element("Label").Value;
                            string Location_X = xe.Element("Location_X").Value;
                            string Location_Y = xe.Element("Location_Y").Value;
                            Elevation e = new Elevation();
                            string msg = e.RntMsg(Location_X, Location_Y);
                            msg = "所在地：" + Label + "\n纬度：" + Location_X + "\n经度：" + Location_Y + "\n海拔：" + msg;
                            Msg.RetrurnTextMsg(CounterID, OurID, msg);
                        }

                        if(MsgType == "image")
                        {
                            dt = msql.S("SELECT handle from blue.ch where cid = '" + CounterID + "'");
                            if(dt.Rows[0][0].ToString() == "色觉辨认助手")
                            {
                                string PicUrl = xe.Element("PicUrl").Value;
                                Color c = new Color();
                                string msg = c.RntMsg(PicUrl);
                                Msg.RetrurnTextMsg(CounterID, OurID, msg);
                            }
                            else
                            {
                                string PicUrl = xe.Element("PicUrl").Value;
                                Flower f = new Flower();
                                string msg = f.RntMsg(PicUrl);
                                Msg.RetrurnTextMsg(CounterID, OurID, msg);
                            }
                        }
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}