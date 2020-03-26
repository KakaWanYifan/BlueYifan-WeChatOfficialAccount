using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySql;
using System.Data;

namespace API
{
    public class MarsCuriosity
    {
        MySQL ms = new MySQL("blue");

        public string ReturnLast()
        {
            string rnt = "";
            string[] StrArr = GetMax().Split('|');
            String Max_Date = StrArr[0];
            String Max_Sol = StrArr[1];
            string Content = "探索火星\n请输入公元日期或火星日。如：2012-08-07或1\n\n";
            Content = Content + "以下为 NASA 好奇号最近所拍摄的部分照片\n公元" + Max_Date + "_" + "火星" + Max_Sol + "\n（点击照片名称查看）\n";
            rnt = GetContent(Content, Max_Date,"earth_date");
            return rnt;
        }

        public string ReturnOrder(String order)
        {
            string rnt = "";
            string[] StrArr = GetMax().Split('|');
            String Max_Date = StrArr[0];
            String Max_Sol = StrArr[1];
            Regex r1 = new Regex("^[0-9]*$");
            Regex r2 = new Regex("(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)");
            string para = "";
            string Content = "";
            if(r1.IsMatch(order))
            {
                if(Convert.ToInt64(order) <= Convert.ToInt32(Max_Sol) && Convert.ToInt64(order) > 0)
                {
                    para = "sol";
                    Content = "";
                    rnt = GetContent(Content, order, para);
                }
                else
                {
                    rnt = "请检查您输入的日期\n目前 NASA 好奇号最近拍摄日期为公元" + Max_Date + "_" + "火星" + Max_Sol + "，最早拍摄日期为公元2012-08-07_火星1";
                }
            }
            else
            {
                if (r2.IsMatch(order) && DateTime.Parse(order) <= DateTime.Parse(Max_Date) && DateTime.Parse(order) >= DateTime.Parse("2012-08-07"))
                {
                    para = "earth_date";
                    Content = "";
                    rnt = GetContent(Content, order, para);
                }
                else
                {
                    rnt = "请检查您输入的日期\n目前 NASA 好奇号最近拍摄日期为公元" + Max_Date + "_" + "火星" + Max_Sol + "，最早拍摄日期为公元2012-08-07_火星1";
                }
            }
            return rnt;
        }

        public string GetContent(string Content,string value,string para)
        {
            string sql = "select earth_date,sol,img_id,img_name,img_src from blue.mars where " + para + "='" + value + "'  order by img_id";
            DataTable dt = ms.S(sql);
            if(dt.Rows.Count == 0)
            {
                Content = Content + "【没有可公开照片】";
            }
            else
            {
                Content = (Content == "") ? Content + "NASA 好奇号于公元" + dt.Rows[0]["earth_date"].ToString() + "_" + "火星" + dt.Rows[0]["sol"].ToString() + "所拍摄的部分照片\n（点击照片名称查看）\n" : Content;
            }
            foreach(DataRow dr in dt.Rows)
            {
                string Append = "";
                Append = Append + "<a href=\"";
                Append = Append + dr["img_src"].ToString();
                Append = Append + "\">";
                Append = Append + dr["img_id"].ToString();
                Append = Append + "-" +  dr["img_name"].ToString();
                Append = Append + "</a>\n";
                if(System.Text.Encoding.UTF8.GetBytes(Content + Append).Length < 2048)
                {
                    Content = Content + Append;
                }
                else
                {
                    break;
                }
            }
            return Content;
        }

        public String GetMax()
        {
            string sql = "select value from blue.marsmax";
            string Max = ms.S(sql).Rows[0][0].ToString();
            return Max;
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
