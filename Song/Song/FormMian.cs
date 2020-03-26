using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Song
{
    public partial class FormMian : Form
    {
        DataTable dt2;
        DataTable dt3;
        DataTable dt4;
        DataTable dtP;
        bool Is0r = true;
        bool Is5r = true;
        public FormMian()
        {
            InitializeComponent();
        }

        private void b_Start_Click(object sender, EventArgs e)
        {
            t_Tick.Start();
            b_Start.Enabled = false;
        }

        private void FormMian_Load(object sender, EventArgs e)
        {
            MySQL ms = new MySQL("song");
            string str = "";
            str = "SELECT w FROM song.words where len = 2";
            dt2 = ms.S(str);
            str = "SELECT w FROM song.words where len = 3";
            dt3 = ms.S(str);
            str = "SELECT w FROM song.words where len = 4";
            dt4 = ms.S(str);
            str = "SELECT * FROM song.poems";
            dtP = ms.S(str);
        }

        private bool IsOK(string s,string temp)
        {
            bool rnt = false;
            foreach(char c in temp)
            {
                if(s.Contains(c.ToString()))
                {
                    rnt = true;
                    break;
                }
            }
            return rnt;
        }

        private void t_Tick_Tick(object sender, EventArgs e)
        {
            string t = DateTime.Now.ToString("HHmm");
            if (t == "0000")
            {
                lb_Status.Items.Clear();
            }
            t = t.Substring(3, 1);
            if(t == "0")
            {
                if(Is0r == true)
                {
                    CreatePoem();
                    Is0r = false;
                    Is5r = true;
                }
            }
            if(t == "5")
            {
                if (Is5r == true)
                {
                    CreatePoem();
                    Is5r = false;
                    Is0r = true;
                }
            }
        }

        private void CreatePoem()
        {
            Random rd = new Random();
            int p = rd.Next(dtP.Rows.Count);

            string s = dtP.Rows[p]["struct"].ToString();
            string r = dtP.Rows[p]["name"].ToString() + "\n";
            foreach (char c in s)
            {
                string temp = c.ToString();
                switch (temp)
                {
                    case "2":
                        {
                            do
                            {
                                int i = rd.Next(dt2.Rows.Count);
                                temp = dt2.Rows[i][0].ToString();
                            } while (IsOK(r, temp));

                        }
                        break;
                    case "3":
                        {
                            do
                            {
                                int i = rd.Next(dt3.Rows.Count);
                                temp = dt3.Rows[i][0].ToString();
                            } while (IsOK(r, temp));
                        }
                        break;
                    case "4":
                        {
                            do
                            {
                                int i = rd.Next(dt4.Rows.Count);
                                temp = dt4.Rows[i][0].ToString();
                            } while (IsOK(r, temp));
                        }
                        break;
                }
                r = r + temp;
            }
            r = r.Replace("。", "。\n");
            r = r + "作于：" + DateTime.Now.ToString();
            rtb_Poem.Text = r;
            lb_Status.Items.Add(DateTime.Now.ToString() + "   写宋词成功。");
            lb_Status.TopIndex = lb_Status.Items.Count - 1;
            string com = "delete from song.rnt;";
            com = com + "insert into song.rnt (rnt) values ('" + r + "')";
            MySQL ms = new MySQL("song");
            ms.I_D_U(com);
        }

        private void FormMian_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
