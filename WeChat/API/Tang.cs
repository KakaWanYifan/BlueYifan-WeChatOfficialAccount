using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace API
{
    public class Tang
    {
        public string RntMsg()
        {
            string url = "http://poem.bosonnlp.com/generate?";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //GET请求
            request.Method = "get";
            request.ContentType = "text/html";
            request.ReadWriteTimeout = 1000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding("utf-8"));
            //返回内容
            string str = sr.ReadToEnd();
            int begin = str.IndexOf("<title>");
            int end = str.IndexOf("</title>");
            str = str.Substring(begin, end - begin).Replace("<title>", "").Replace("：", "：\n").Replace("，", "，\n").Replace("。", "。\n");
            str = "服务器实时生成唐诗\n\n" +  str + "\n作于：" + DateTime.Now.ToString();
            return str;
        }

    }
}
