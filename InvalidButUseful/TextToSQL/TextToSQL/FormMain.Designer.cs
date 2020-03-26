namespace TextToSQL
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
            this.tb_Path = new System.Windows.Forms.TextBox();
            this.b_Path = new System.Windows.Forms.Button();
            this.b_Handle = new System.Windows.Forms.Button();
            this.lb_Now = new System.Windows.Forms.ListBox();
            this.ofd_Path = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // tb_Path
            // 
            this.tb_Path.Location = new System.Drawing.Point(13, 13);
            this.tb_Path.Name = "tb_Path";
            this.tb_Path.Size = new System.Drawing.Size(278, 21);
            this.tb_Path.TabIndex = 0;
            // 
            // b_Path
            // 
            this.b_Path.Location = new System.Drawing.Point(297, 13);
            this.b_Path.Name = "b_Path";
            this.b_Path.Size = new System.Drawing.Size(75, 23);
            this.b_Path.TabIndex = 1;
            this.b_Path.Text = "选择路径";
            this.b_Path.UseVisualStyleBackColor = true;
            this.b_Path.Click += new System.EventHandler(this.b_Path_Click);
            // 
            // b_Handle
            // 
            this.b_Handle.Location = new System.Drawing.Point(12, 40);
            this.b_Handle.Name = "b_Handle";
            this.b_Handle.Size = new System.Drawing.Size(360, 23);
            this.b_Handle.TabIndex = 2;
            this.b_Handle.Text = "处理数据";
            this.b_Handle.UseVisualStyleBackColor = true;
            this.b_Handle.Click += new System.EventHandler(this.b_Handle_Click);
            // 
            // lb_Now
            // 
            this.lb_Now.FormattingEnabled = true;
            this.lb_Now.ItemHeight = 12;
            this.lb_Now.Location = new System.Drawing.Point(13, 70);
            this.lb_Now.Name = "lb_Now";
            this.lb_Now.Size = new System.Drawing.Size(359, 112);
            this.lb_Now.TabIndex = 3;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 187);
            this.Controls.Add(this.lb_Now);
            this.Controls.Add(this.b_Handle);
            this.Controls.Add(this.b_Path);
            this.Controls.Add(this.tb_Path);
            this.Name = "FormMain";
            this.Text = "TextToSQL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_Path;
        private System.Windows.Forms.Button b_Path;
        private System.Windows.Forms.Button b_Handle;
        private System.Windows.Forms.ListBox lb_Now;
        private System.Windows.Forms.OpenFileDialog ofd_Path;
    }
}

