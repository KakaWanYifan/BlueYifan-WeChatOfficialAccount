using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockData
{
    public class SimHash
    {
        public double Cosine(DataRow[] drxArr,DataRow[] dryArr)
        {
            int nMin = System.Math.Min(drxArr.Length, dryArr.Length);
            double n = 0;
            double dx = 0;
            double dy = 0;
            for (int i = 0; i < nMin; i++)
            {
                double x = (drxArr[i][0].ToString() == "") ? 0 : Convert.ToDouble(drxArr[i][0].ToString());
                double y = (dryArr[i][0].ToString() == "") ? 0 : Convert.ToDouble(dryArr[i][0].ToString());
                n = n + x * y;
                dx = dx + x * x;
                dy = dy + y * y;
            }
            double d = Math.Sqrt(dx) * Math.Sqrt(dy);
            double rnt = n / d;
            return rnt;
        }
    }
}
