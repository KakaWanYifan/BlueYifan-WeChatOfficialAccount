using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;

namespace Main
{
    public class AccessTokenBox
    {
        public string AppId { get; set; }
        public AccessToken Token { get; set; }
        private static List<AccessTokenBox> _boxs;

        public static string GetTokenValue(string appid, string appSecret)
        {
            _boxs = (_boxs == null ? new List<AccessTokenBox>() : _boxs.Where(b => b.Token.ExpirationTime > DateTime.Now).ToList());
            var tempat = _boxs.FirstOrDefault(b => b.AppId == appid);
            if (tempat != null)
            {
                return tempat.Token.access_token;
            }
            var newAT = GetAccessToken(appid, appSecret);
            if (!string.IsNullOrEmpty(newAT.access_token))
            {
                _boxs.Add(new AccessTokenBox
                {
                    AppId = appid,
                    Token = newAT
                });
                return newAT.access_token;
            }
            else
            {
                //此处可以写日志，将错误信息保存。
                return "";
            }
        }

        public static AccessToken GetAccessToken(string appid, string appSecret)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, appSecret);
            return GetResult<AccessToken>(url);
        }

        public static T GetResult<T>(string url)
        {
            var retdata = HttpGet(url);
            return JsonConvert.DeserializeObject<T>(retdata);
        }
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";//设置请求的方法
            request.Accept = "*/*";//设置Accept标头的值
            string responseStr = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())//获取响应
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = reader.ReadToEnd();
                }
            }
            return responseStr;
        }
    }
}