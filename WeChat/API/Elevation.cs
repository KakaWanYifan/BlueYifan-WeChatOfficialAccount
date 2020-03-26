using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API
{
    public class Elevation
    {
        public string RntMsg(string X, string Y)
        {
            string url = "https://google.cn/maps/api/elevation/json?locations=";
            url = url + X + "," + Y;
            url = url + "&key=AIzaSyByQUxG-W6QgZVLgpn9ReRtxKH8-lQc7ME";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //GET请求
            request.Method = "get";
            request.ContentType = "text/html";
            request.ReadWriteTimeout = 1000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding("gb2312"));
            //返回内容
            string str = sr.ReadToEnd();
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            JArray ja = JArray.Parse(jo["results"].ToString());
            if(ja.Count == 0)
            {
                str = "【系统错误，请再发送一次。】";
            }
            else
            {
                jo = JObject.Parse(ja[0].ToString());
                str = jo["elevation"].ToString();
                str = Convert.ToDouble(str).ToString("0.00") + "米";
            }
            return str;
        }
       
    }
}
