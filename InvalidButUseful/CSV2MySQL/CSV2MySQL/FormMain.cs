using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using MySql.Data.MySqlClient;

namespace CSV2MySQL
{
    public partial class FormMain : Form
    {
        List<string> FileList = new List<string>();//文件列表
        List<string> DirList = new List<string>();//文件列表
        string DirTag = "";
        private Thread thread_Sub;

        public FormMain()
        {
            InitializeComponent();
        }

        private void b_Files_Click(object sender, EventArgs e)
        {
            if (ofd_Files.ShowDialog() == DialogResult.OK)
            {
                string[] StrArr = ofd_Files.FileNames;
                if (DirTag.Contains(StrArr[0]))
                {
                    MessageBox.Show("已经选了");
                }
                if (StrArr.Length == 1)
                {
                    MessageBox.Show("只选了一个");
                }
                else
                {
                    DirTag = DirTag + StrArr[0];
                    lb_Now.Items.Add(StrArr[0]);
                    foreach (string str in StrArr)
                    {
                        FileList.Add(str);
                    }
                }
                b_ToDB.Enabled = true;
            }
        }

        private void b_Dirs_Click(object sender, EventArgs e)
        {
            if(fbd_Dir.ShowDialog() == DialogResult.OK)
            {
                string dir = fbd_Dir.SelectedPath;
                DirectoryInfo TheFolder = new DirectoryInfo(dir);
                foreach (DirectoryInfo di in TheFolder.GetDirectories())
                {
                    DirList.Add(di.FullName);
                }

                foreach(string str in DirList)
                {
                    string[] FileArr = System.IO.Directory.GetFiles(str);
                    foreach(string fa in FileArr)
                    {
                        FileList.Add(fa);
                    }
                }
                b_ToDB.Enabled = true;
            }
            MessageBox.Show(FileList.Count.ToString());
        }

        private void b_ToDB_Click(object sender, EventArgs e)
        {
            b_Files.Enabled = false;
            b_Dirs.Enabled = false;
            b_ToDB.Enabled = false;
            lb_Now.Items.Clear();
            thread_Sub = new Thread(new ThreadStart(ToDB));
            thread_Sub.IsBackground = true;
            thread_Sub.Start();
        }

        private void ToDB()
        {
                foreach (string path in FileList)
                {
                    StreamReader sr = new StreamReader(path);
                    string code = path.Substring(path.LastIndexOf('\\') + 1, path.Length - path.LastIndexOf('\\') - 5);
                    string Table = path.Substring(0, path.LastIndexOf('\\'));
                    Table = Table.Substring(Table.LastIndexOf('\\') + 1, 4);
                    if (code.StartsWith("SH6") || code.StartsWith("SZ000") || code.StartsWith("SZ002") || code.StartsWith("SZ300"))
                    {
                        string com = "INSERT INTO stock." + Table + "(code,date,time,open,close) values";
                        string line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] str = line.Split(',');
                            string date = str[0];
                            string time = str[1];
                            string open = str[2];
                            string close = str[5];
                            com = com +  "('" + code + "','" + date + "','" + time + "','" + open + "','" + close + "'),";
                        }
                        com = com.Substring(0, com.Length - 1);
                        MySqlConnection myConn = new MySqlConnection("server=localhost;user id=root; password=MySQL; database=" + "stock" + "; pooling=false;port=3306");
                        MySqlCommand cmd;
                        cmd = myConn.CreateCommand();//sql命令对象，表示要对sql数据库执行一个sql语句
                        cmd.CommandText = com;
                        myConn.Open();//打开连接
                        cmd.ExecuteNonQuery();//执行不是查询的sql语句
                        myConn.Close();
                        delegation d_a = new delegation(GApp_Add);
                        lb_Now.Invoke(d_a, path);
                    }
                    sr.Close();
                    File.Delete(path);
                }
                MessageBox.Show("完成！");
        }

        public delegate void delegation(string cur);

        public void GApp_Add(string cur)
        {
            lb_Now.Items.Add(DateTime.Now.ToString() + "   " + cur);
            lb_Now.TopIndex = lb_Now.Items.Count - 1;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认关闭？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (MessageBox.Show("请再次确认关闭？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (MessageBox.Show("最后一次确认关闭？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            b_Files.Enabled = true;
            b_Dirs.Enabled = true;
            b_ToDB.Enabled = false;
        }
    }
}
