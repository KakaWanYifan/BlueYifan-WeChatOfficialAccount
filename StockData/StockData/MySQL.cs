using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace StockData
{
    public class MySQL
    {
        private string database;
        public MySQL(string _database)
        {
            database = _database;
        }

        public DataTable S(string com)
        {
            MySqlConnection myConn = new MySqlConnection("server=localhost;user id=root; password=MySQL; database=" + database + "; pooling=false;port=3306;default command timeout=0;Allow User Variables=True");
            myConn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(com, myConn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            myConn.Close();
            return dt;
        }

        public int I_D_U(string com)
        {
            MySqlConnection myConn = new MySqlConnection("server=localhost;user id=root; password=MySQL; database=" + database + "; pooling=false;port=3306;default command timeout=0;Allow User Variables=True");
            MySqlCommand cmd;
            cmd = myConn.CreateCommand();//sql命令对象，表示要对sql数据库执行一个sql语句
            cmd.CommandText = com;
            myConn.Open();//打开连接
            int i = cmd.ExecuteNonQuery();//执行不是查询的sql语句
            myConn.Close();
            return i;
        }
    }
}
