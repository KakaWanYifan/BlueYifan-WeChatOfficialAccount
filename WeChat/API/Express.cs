using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace API
{
    public class Express
    {
        //快递鸟API帐号
        private string EBusinessID = "1268663";
        //快递鸟API密码
        private string AppKey = "a61558e2-7424-4400-bb6b-20322e56f205";
        //快递鸟API接口
        private string ReqURL = "http://api.kdniao.cc/Ebusiness/EbusinessOrderHandle.aspx";

        private JArray jaShipperCode = null;

        private JArray GetShipperCode(string LogisticCode)
        {
            string requestData = "{'LogisticCode':'" + LogisticCode + "'}";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("RequestData", HttpUtility.UrlEncode(requestData, Encoding.UTF8));
            param.Add("EBusinessID", EBusinessID);
            param.Add("RequestType", "2002");
            string dataSign = encrypt(requestData, AppKey, "UTF-8");
            param.Add("DataSign", HttpUtility.UrlEncode(dataSign, Encoding.UTF8));
            param.Add("DataType", "2");
            string result = sendPost(ReqURL, param);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            JArray ja = JArray.Parse(jo["Shippers"].ToString());
            return ja;
        }

        private string GetTrace(string LogisticCode)
        {
            string ShipperCode = "";
            string ShipperName = "";
            string Trace = "";
            //返回的快递公司代码的数量为0，理论上应该是单号错了，但是有一家快递公司需要特别处理。
            //京东
            if (jaShipperCode.Count == 0)
            {
                ShipperCode = "JD";
                ShipperName = "京东快递";
                Trace = Search(ShipperCode, LogisticCode);
                if (Trace == "暂时没有物流信息。")
                    return Trace;
                else
                    return (LogisticCode + "\n" + ShipperName + " " + Trace);
            }
            else
            {
                for (int i = 0; i < jaShipperCode.Count; i++)
                {
                    JObject jo = JObject.Parse(jaShipperCode[i].ToString());
                    ShipperCode = jo["ShipperCode"].ToString();
                    ShipperName = jo["ShipperName"].ToString();
                    Trace = Search(ShipperCode, LogisticCode);
                    //如风达快递需要特殊处理
                    if (Trace != "暂时没有物流信息。" && ShipperCode != "RFD")
                    {
                        break;
                    }
                    if (Trace == "暂时没有物流信息。" && ShipperCode == "RFD")
                    {
                        ShipperCode = "AMAZON";
                        ShipperName = "亚马逊物流";
                        Trace = Search(ShipperCode, LogisticCode);
                        if (Trace != "暂时没有物流信息。")
                        {
                            break;
                        }
                    }
                }
                return (LogisticCode + "\n" + ShipperName + " " + Trace);
            }
        }

        public string RntExpressInfo(string LogisticCode)
        {
            jaShipperCode = GetShipperCode(LogisticCode);
            string rnt = GetTrace(LogisticCode);
            if (System.Text.Encoding.UTF8.GetBytes(rnt).Length >= 2048)
            {
                rnt = "抱歉，当前无法返回信息。因为腾讯的限制，所返回的消息不能超过2048个字节。";
            }
            return rnt;
        }

        public string Search(string ShipperCode, string LogisticCode)
        {
            string requestData = "{'OrderCode':'','ShipperCode':'" + ShipperCode + "','LogisticCode':'" + LogisticCode + "'}";

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("RequestData", HttpUtility.UrlEncode(requestData, Encoding.UTF8));
            param.Add("EBusinessID", EBusinessID);
            param.Add("RequestType", "1002");
            string dataSign = encrypt(requestData, AppKey, "UTF-8");
            param.Add("DataSign", HttpUtility.UrlEncode(dataSign, Encoding.UTF8));
            param.Add("DataType", "2");

            string result = sendPost(ReqURL, param);

            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            string rnt = "";
            if (jo["Traces"].ToString() == "[]")
            {
                rnt = "暂时没有物流信息。";
                return rnt;
            }
            else
            {
                if (jo["State"].ToString() == "2")
                {
                    rnt = rnt + "在途中\n";
                }
                if (jo["State"].ToString() == "3")
                {
                    rnt = rnt + "已签收\n";
                }
                if (jo["State"].ToString() == "4")
                {
                    rnt = rnt + "问题件\n";
                }
                JArray ja = JArray.Parse(jo["Traces"].ToString());
                for (int i = 0; i < ja.Count; i++)
                {
                    JObject _jo = JObject.Parse(ja[i].ToString());
                    string _AcceptTime = _jo["AcceptTime"].ToString();
                    string _AcceptStation = _jo["AcceptStation"].ToString();
                    rnt = rnt + _AcceptTime + "\n" + _AcceptStation + "\n";
                }
                return rnt;
            }
        }

        /// <summary>
        /// Post方式提交数据，返回网页的源代码
        /// </summary>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="param">请求的参数集合</param>
        /// <returns>远程资源的响应结果</returns>
        private string sendPost(string url, Dictionary<string, string> param)
        {
            string result = "";
            StringBuilder postData = new StringBuilder();
            if (param != null && param.Count > 0)
            {
                foreach (var p in param)
                {
                    if (postData.Length > 0)
                    {
                        postData.Append("&");
                    }
                    postData.Append(p.Key);
                    postData.Append("=");
                    postData.Append(p.Value);
                }
            }
            byte[] byteData = Encoding.GetEncoding("UTF-8").GetBytes(postData.ToString());
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = url;
                request.Accept = "*/*";
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Method = "POST";
                request.ContentLength = byteData.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private string encrypt(String content, String keyValue, String charset)
        {
            if (keyValue != null)
            {
                return base64(MD5(content + keyValue, charset), charset);
            }
            return base64(MD5(content, charset), charset);
        }

        ///<summary>
        /// 字符串MD5加密
        ///</summary>
        ///<param name="str">要加密的字符串</param>
        ///<param name="charset">编码方式</param>
        ///<returns>密文</returns>
        private string MD5(string str, string charset)
        {
            byte[] buffer = System.Text.Encoding.GetEncoding(charset).GetBytes(str);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider check;
                check = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] somme = check.ComputeHash(buffer);
                string ret = "";
                foreach (byte a in somme)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("X");
                    else
                        ret += a.ToString("X");
                }
                return ret.ToLower();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="charset">编码方式</param>
        /// <returns></returns>
        private string base64(String str, String charset)
        {
            return Convert.ToBase64String(System.Text.Encoding.GetEncoding(charset).GetBytes(str));
        }
    }
}