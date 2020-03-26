using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Xfrog.Net;
using System.Diagnostics;
using System.Web;
using System.Data;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API
{
    public class BlueRobot
    {
        public string RntMsg(string Content, string CounterID)
        {
            string com = "select * from blue.turing where state = '1' order by robotid";
            MySQL msql = new MySQL("blue");
            DataTable dt = msql.S(com);
            if(dt.Rows.Count == 0)
            {
                return ("好累啊，明天在说吧。");
            }
            else
            {
                string appkey = dt.Rows[0]["APIkey"].ToString();
                string url = "http://www.tuling123.com/openapi/api";
                var parameters = new Dictionary<string, string>();
                if(Content.Length > 30)
                {
                    Content = Content.Substring(0, 29);
                }
                Content = Content.ToLower().Replace("BlueYifan", "图灵机器人");
                parameters.Add("key", appkey);//你申请的key
                parameters.Add("info", Content); //要发送给机器人的内容，不要超过30个字符
                parameters.Add("userid", CounterID); //1~32位，此userid针对您自己的每一个用户，用于上下文的关联
                string str = sendPost(url, parameters, "post");
                JObject jo = JObject.Parse(str);
                string code = jo["code"].ToString();
                string rnt = "";
                switch(code)
                {
                    case "100000":
                        {
                            rnt = jo["text"].ToString();
                        }
                        break;
                    case "200000":
                    {
                        rnt = jo["text"].ToString() + "\n";
                        rnt = rnt + "<a href=\"" + jo["url"].ToString() + "\">" + "点击查看" + "</a> ";
                    }
                    break;
                    case "302000":
                    {
                        rnt = jo["text"].ToString() + "\n";
                        JArray ja = JArray.Parse(jo["list"].ToString());
                        rnt = rnt + "共" + ja.Count.ToString() + "条\n";
                        for (int i = 0; i < ja.Count; i++)
                        {
                            JObject jo_ = JObject.Parse(ja[i].ToString());
                            rnt = rnt + "<a href=\"" + jo_["detailurl"].ToString() + "\">" + (i + 1).ToString() + "、" + jo_["article"].ToString() + "</a> \n";
                        }
                    }
                    break;
                    case "308000":
                    {
                        rnt = jo["text"].ToString() + "\n";
                        JArray ja = JArray.Parse(jo["list"].ToString());
                        rnt = rnt + "共" + ja.Count.ToString() + "条\n";
                        for (int i = 0; i < ja.Count; i++)
                        {
                            JObject jo_ = JObject.Parse(ja[i].ToString());
                            rnt = rnt + "<a href=\"" + jo_["detailurl"].ToString() + "\">" + (i + 1).ToString() + "、" + jo_["name"].ToString() + "</a> \n";
                        }
                    }
                    break;
                    case "40004":
                    {
                        rnt = "抱歉，未能收到您发的信息，可能是因为丢包或其它原因。能再发一遍吗？";
                        msql.I_D_U("UPDATE blue.turing SET state = '0' where APIkey = '" + appkey + "'");
                    }
                    break;
                    case "40001":
                    {
                        rnt = "抱歉，我出故障了，错误代码 1 ，可以把错误代码转告给Wan Yifan吗？";
                    }
                    break;
                    case "40007":
                    {
                        rnt = "抱歉，我出故障了，错误代码 7 ，可以把错误代码转告给Wan Yifan吗？";
                    }
                    break;
                    case "40002":
                    {
                        rnt = "抱歉，我出故障了，错误代码 2 ，可以把错误代码转告给Wan Yifan吗？";
                    }
                    break;
                }
                rnt = rnt.Replace("图灵机器人", "BlueYifan").Replace("亲爱的", "尊敬的用户").Replace("旧服务已下线，请迁移至 http://api.fanyi.baidu.com","我不说了");
                return rnt;
            }
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
                request.ReadWriteTimeout = 5000;
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
    }
}