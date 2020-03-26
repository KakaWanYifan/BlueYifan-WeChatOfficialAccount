namespace Song
{
    partial class FormMian
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMian));
            this.rtb_Poem = new System.Windows.Forms.RichTextBox();
            this.lb_Status = new System.Windows.Forms.ListBox();
            this.b_Start = new System.Windows.Forms.Button();
            this.t_Tick = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // rtb_Poem
            // 
            this.rtb_Poem.Location = new System.Drawing.Point(272, 12);
            this.rtb_Poem.Name = "rtb_Poem";
            this.rtb_Poem.Size = new System.Drawing.Size(300, 238);
            this.rtb_Poem.TabIndex = 0;
            this.rtb_Poem.Text = "";
            // 
            // lb_Status
            // 
            this.lb_Status.FormattingEnabled = true;
            this.lb_Status.ItemHeight = 12;
            this.lb_Status.Location = new System.Drawing.Point(13, 13);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(253, 208);
            this.lb_Status.TabIndex = 1;
            // 
            // b_Start
            // 
            this.b_Start.Location = new System.Drawing.Point(12, 227);
            this.b_Start.Name = "b_Start";
            this.b_Start.Size = new System.Drawing.Size(254, 23);
            this.b_Start.TabIndex = 2;
            this.b_Start.Text = "开始生成";
            this.b_Start.UseVisualStyleBackColor = true;
            this.b_Start.Click += new System.EventHandler(this.b_Start_Click);
            // 
            // t_Tick
            // 
            this.t_Tick.Interval = 1000;
            this.t_Tick.Tick += new System.EventHandler(this.t_Tick_Tick);
            // 
            // FormMian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.b_Start);
            this.Controls.Add(this.lb_Status);
            this.Controls.Add(this.rtb_Poem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMian";
            this.Text = "Song";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMian_FormClosing);
            this.Load += new System.EventHandler(this.FormMian_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_Poem;
        private System.Windows.Forms.ListBox lb_Status;
        private System.Windows.Forms.Button b_Start;
        private System.Windows.Forms.Timer t_Tick;
    }
}

