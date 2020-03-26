using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using HtmlAgilityPack;

namespace API
{
    public class Joke
    {
        public string RntMsg(string Keyword)
        {
            string rnt = "";
            bool fill = false;
            try
            {
                string url = "http://joke.setin.cn/?s=" + Keyword;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                HtmlNode node = doc.GetElementbyId("content");
                if(node.ChildNodes.Count > 7)
                {
                    rnt = "关于" + Keyword + "的段子\n\n";
                    foreach (HtmlNode hn1 in node.ChildNodes)
                    {
                        if (hn1.Attributes.Count == 0)
                            continue;
                        if (hn1.Attributes["class"].Value == "panel")
                        {
                            foreach (HtmlNode hn2 in hn1.ChildNodes)
                            {
                                if (hn2.Attributes.Count == 0)
                                    continue;
                                if (hn2.Attributes["class"].Value == "body")
                                {
                                    string test = hn2.InnerText.Replace("\n","").Replace("\t","").Replace(" ","");
                                    if (System.Text.Encoding.UTF8.GetBytes(rnt + test + "\n\n").Length >= 2048)
                                    {
                                        fill = true;
                                        break;
                                    }
                                    else
                                    {
                                        rnt = rnt + test + "\n\n";
                                    }
                                }
                            }
                        }
                        if (fill == true)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    rnt = "抱歉，没有找到关于" + Keyword + "的段子。";
                }
                return rnt;
            }
            catch
            {
                return "Error";
            }
        }
    }
}
