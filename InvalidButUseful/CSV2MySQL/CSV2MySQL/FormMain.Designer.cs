namespace CSV2MySQL
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lb_Now = new System.Windows.Forms.ListBox();
            this.b_Files = new System.Windows.Forms.Button();
            this.b_Dirs = new System.Windows.Forms.Button();
            this.b_ToDB = new System.Windows.Forms.Button();
            this.ofd_Files = new System.Windows.Forms.OpenFileDialog();
            this.fbd_Dir = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // lb_Now
            // 
            this.lb_Now.FormattingEnabled = true;
            this.lb_Now.ItemHeight = 12;
            this.lb_Now.Location = new System.Drawing.Point(13, 13);
            this.lb_Now.Name = "lb_Now";
            this.lb_Now.Size = new System.Drawing.Size(459, 400);
            this.lb_Now.TabIndex = 0;
            // 
            // b_Files
            // 
            this.b_Files.Location = new System.Drawing.Point(12, 427);
            this.b_Files.Name = "b_Files";
            this.b_Files.Size = new System.Drawing.Size(100, 23);
            this.b_Files.TabIndex = 1;
            this.b_Files.Text = "选择文件";
            this.b_Files.UseVisualStyleBackColor = true;
            this.b_Files.Click += new System.EventHandler(this.b_Files_Click);
            // 
            // b_Dirs
            // 
            this.b_Dirs.Location = new System.Drawing.Point(132, 427);
            this.b_Dirs.Name = "b_Dirs";
            this.b_Dirs.Size = new System.Drawing.Size(100, 23);
            this.b_Dirs.TabIndex = 2;
            this.b_Dirs.Text = "选择目录";
            this.b_Dirs.UseVisualStyleBackColor = true;
            this.b_Dirs.Click += new System.EventHandler(this.b_Dirs_Click);
            // 
            // b_ToDB
            // 
            this.b_ToDB.Location = new System.Drawing.Point(252, 427);
            this.b_ToDB.Name = "b_ToDB";
            this.b_ToDB.Size = new System.Drawing.Size(100, 23);
            this.b_ToDB.TabIndex = 3;
            this.b_ToDB.Text = "导入MySQL";
            this.b_ToDB.UseVisualStyleBackColor = true;
            this.b_ToDB.Click += new System.EventHandler(this.b_ToDB_Click);
            // 
            // ofd_Files
            // 
            this.ofd_Files.FileName = "openFileDialog1";
            this.ofd_Files.Multiselect = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.b_ToDB);
            this.Controls.Add(this.b_Dirs);
            this.Controls.Add(this.b_Files);
            this.Controls.Add(this.lb_Now);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "CSV2MySQL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_Now;
        private System.Windows.Forms.Button b_Files;
        private System.Windows.Forms.Button b_Dirs;
        private System.Windows.Forms.Button b_ToDB;
        private System.Windows.Forms.OpenFileDialog ofd_Files;
        private System.Windows.Forms.FolderBrowserDialog fbd_Dir;
    }
}

