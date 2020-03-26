using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing;
using System.Data;

namespace API
{
    public class Color
    {
        List<Statistics.MajorColor> MC;
        public string RntMsg(string PicUrl)
        {
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(PicUrl);
            MemoryStream ms = new MemoryStream(bytes);
            Image img = Image.FromStream(ms);
            MC = Statistics.PrincipalColorAnalysis((Bitmap)img, 3, 24);

            MySQL msql = new MySQL("blue");
            DataTable dtColor = new DataTable();
            dtColor = msql.S("SELECT * FROM blue.color;");

            DataTable dtRnt = new DataTable();
            dtRnt.Columns.Add("name");
            dtRnt.Columns.Add("amount");


            for (int i = 0; i < MC.Count; i++)
            {
                string cn_name = "";
                string en_name = "";
                string name = "";
                long sim = long.MaxValue;

                int X1 = MC[i].Color & 255;
                int X2 = (MC[i].Color & 65280) / 256;
                int X3 = (MC[i].Color & 16711680) / 65536;
                foreach (DataRow drColor in dtColor.Rows)
                {
                    int Y1 = Convert.ToInt32(drColor["R"].ToString());
                    int Y2 = Convert.ToInt32(drColor["G"].ToString());
                    int Y3 = Convert.ToInt32(drColor["B"].ToString());
                    long result = (X1 - Y1) * (X1 - Y1) + (X2 - Y2) * (X2 - Y2) + (X3 - Y3) * (X3 - Y3);
                    //double X = (X1 * Y1 + X2 * Y2 + X3 * Y3);
                    //double Y = Math.Sqrt(X1 * X1 + X2 * X2 + X3 * X3) * Math.Sqrt(Y1 * Y1 + Y2 * Y2 + Y3 * Y3);
                    //double result = X / Y;
                    if (result <= sim)
                    {
                        cn_name = drColor["CN_NAME"].ToString();
                        en_name = drColor["EN_NAME"].ToString();
                        name = cn_name + "(" + en_name + ")";
                        sim = result;
                    }
                }
                DataRow drNew = dtRnt.NewRow();
                drNew["name"] = name;
                drNew["amount"] = MC[i].Amount.ToString();
                dtRnt.Rows.Add(drNew);
            }


            Dictionary<string, DataRow> dict = new Dictionary<string, DataRow>(); //这个字典用于查找第一列相同的项目    
            List<DataRow> removeRows = new List<DataRow>(); //这个List 存储需要删除的重复行          
            foreach (DataRow row in dtRnt.Rows)
            {
                string key = row["name"].ToString();
                DataRow dr_;
                if (dict.TryGetValue(key, out dr_))
                {
                    dr_["amount"] = int.Parse(dr_["amount"].ToString()) + int.Parse(row["amount"].ToString());
                    removeRows.Add(row); //将这一行加入要删除的行列表中               
                }
                else
                {
                    dict.Add(key, row);
                }
            }
            //删除重复的行          
            foreach (DataRow row in removeRows)
            {
                dtRnt.Rows.Remove(row);
            }

            string rnt = "";
            foreach(DataRow dr in dtRnt.Rows)
            {
                rnt = rnt + "颜色名称：" + dr["name"].ToString() + "\n" + "像素个数：" + dr["amount"].ToString() + "\n\n";
            }
            return rnt;
        }
    }
}
