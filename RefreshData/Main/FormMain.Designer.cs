namespace FundData
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lb_Status = new System.Windows.Forms.ListBox();
            this.ss_Status = new System.Windows.Forms.StatusStrip();
            this.tssl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.t_Time = new System.Windows.Forms.Timer(this.components);
            this.dtp_Fund = new System.Windows.Forms.DateTimePicker();
            this.dtp_Turing = new System.Windows.Forms.DateTimePicker();
            this.cb_Fund = new System.Windows.Forms.CheckBox();
            this.cb_Truing = new System.Windows.Forms.CheckBox();
            this.b_FundNow = new System.Windows.Forms.Button();
            this.b_TuringNow = new System.Windows.Forms.Button();
            this.b_Start = new System.Windows.Forms.Button();
            this.b_Stop = new System.Windows.Forms.Button();
            this.cb_Mars = new System.Windows.Forms.CheckBox();
            this.dtp_Mars = new System.Windows.Forms.DateTimePicker();
            this.b_MarsNow = new System.Windows.Forms.Button();
            this.cb_nCoV = new System.Windows.Forms.CheckBox();
            this.b_NCOV = new System.Windows.Forms.Button();
            this.ss_Status.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_Status
            // 
            this.lb_Status.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Status.FormattingEnabled = true;
            this.lb_Status.ItemHeight = 17;
            this.lb_Status.Location = new System.Drawing.Point(339, 13);
            this.lb_Status.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(433, 361);
            this.lb_Status.TabIndex = 1;
            // 
            // ss_Status
            // 
            this.ss_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_Status});
            this.ss_Status.Location = new System.Drawing.Point(0, 386);
            this.ss_Status.Name = "ss_Status";
            this.ss_Status.Size = new System.Drawing.Size(784, 26);
            this.ss_Status.TabIndex = 5;
            this.ss_Status.Text = "statusStrip1";
            // 
            // tssl_Status
            // 
            this.tssl_Status.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tssl_Status.Name = "tssl_Status";
            this.tssl_Status.Size = new System.Drawing.Size(74, 21);
            this.tssl_Status.Text = "当前状态";
            // 
            // t_Time
            // 
            this.t_Time.Tick += new System.EventHandler(this.t_Time_Tick);
            // 
            // dtp_Fund
            // 
            this.dtp_Fund.CustomFormat = "HH:mm";
            this.dtp_Fund.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Fund.Location = new System.Drawing.Point(167, 44);
            this.dtp_Fund.Name = "dtp_Fund";
            this.dtp_Fund.ShowUpDown = true;
            this.dtp_Fund.Size = new System.Drawing.Size(60, 23);
            this.dtp_Fund.TabIndex = 11;
            this.dtp_Fund.Value = new System.DateTime(2017, 10, 1, 2, 0, 0, 0);
            // 
            // dtp_Turing
            // 
            this.dtp_Turing.CustomFormat = "HH:mm";
            this.dtp_Turing.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Turing.Location = new System.Drawing.Point(167, 13);
            this.dtp_Turing.Name = "dtp_Turing";
            this.dtp_Turing.ShowUpDown = true;
            this.dtp_Turing.Size = new System.Drawing.Size(60, 23);
            this.dtp_Turing.TabIndex = 13;
            this.dtp_Turing.Value = new System.DateTime(2017, 10, 1, 0, 30, 0, 0);
            // 
            // cb_Fund
            // 
            this.cb_Fund.AutoSize = true;
            this.cb_Fund.Location = new System.Drawing.Point(12, 48);
            this.cb_Fund.Name = "cb_Fund";
            this.cb_Fund.Size = new System.Drawing.Size(123, 21);
            this.cb_Fund.TabIndex = 14;
            this.cb_Fund.Text = "基金数据定时更新";
            this.cb_Fund.UseVisualStyleBackColor = true;
            // 
            // cb_Truing
            // 
            this.cb_Truing.AutoSize = true;
            this.cb_Truing.Location = new System.Drawing.Point(12, 15);
            this.cb_Truing.Name = "cb_Truing";
            this.cb_Truing.Size = new System.Drawing.Size(123, 21);
            this.cb_Truing.TabIndex = 15;
            this.cb_Truing.Text = "图灵数据定时更新";
            this.cb_Truing.UseVisualStyleBackColor = true;
            // 
            // b_FundNow
            // 
            this.b_FundNow.Location = new System.Drawing.Point(258, 44);
            this.b_FundNow.Name = "b_FundNow";
            this.b_FundNow.Size = new System.Drawing.Size(75, 23);
            this.b_FundNow.TabIndex = 16;
            this.b_FundNow.Text = "立即更新";
            this.b_FundNow.UseVisualStyleBackColor = true;
            this.b_FundNow.Click += new System.EventHandler(this.b_FundNow_Click);
            // 
            // b_TuringNow
            // 
            this.b_TuringNow.Location = new System.Drawing.Point(258, 13);
            this.b_TuringNow.Name = "b_TuringNow";
            this.b_TuringNow.Size = new System.Drawing.Size(75, 23);
            this.b_TuringNow.TabIndex = 17;
            this.b_TuringNow.Text = "立即更新";
            this.b_TuringNow.UseVisualStyleBackColor = true;
            this.b_TuringNow.Click += new System.EventHandler(this.b_TuringNow_Click);
            // 
            // b_Start
            // 
            this.b_Start.Location = new System.Drawing.Point(12, 351);
            this.b_Start.Name = "b_Start";
            this.b_Start.Size = new System.Drawing.Size(150, 23);
            this.b_Start.TabIndex = 18;
            this.b_Start.Text = "开始自动";
            this.b_Start.UseVisualStyleBackColor = true;
            this.b_Start.Click += new System.EventHandler(this.b_Start_Click);
            // 
            // b_Stop
            // 
            this.b_Stop.Location = new System.Drawing.Point(183, 351);
            this.b_Stop.Name = "b_Stop";
            this.b_Stop.Size = new System.Drawing.Size(150, 23);
            this.b_Stop.TabIndex = 19;
            this.b_Stop.Text = "终止自动";
            this.b_Stop.UseVisualStyleBackColor = true;
            this.b_Stop.Click += new System.EventHandler(this.b_Stop_Click);
            // 
            // cb_Mars
            // 
            this.cb_Mars.AutoSize = true;
            this.cb_Mars.Location = new System.Drawing.Point(12, 83);
            this.cb_Mars.Name = "cb_Mars";
            this.cb_Mars.Size = new System.Drawing.Size(123, 21);
            this.cb_Mars.TabIndex = 20;
            this.cb_Mars.Text = "火星数据定时更新";
            this.cb_Mars.UseVisualStyleBackColor = true;
            // 
            // dtp_Mars
            // 
            this.dtp_Mars.CustomFormat = "HH:mm";
            this.dtp_Mars.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Mars.Location = new System.Drawing.Point(167, 83);
            this.dtp_Mars.Name = "dtp_Mars";
            this.dtp_Mars.ShowUpDown = true;
            this.dtp_Mars.Size = new System.Drawing.Size(60, 23);
            this.dtp_Mars.TabIndex = 21;
            this.dtp_Mars.Value = new System.DateTime(2017, 10, 1, 9, 30, 0, 0);
            // 
            // b_MarsNow
            // 
            this.b_MarsNow.Location = new System.Drawing.Point(258, 81);
            this.b_MarsNow.Name = "b_MarsNow";
            this.b_MarsNow.Size = new System.Drawing.Size(75, 23);
            this.b_MarsNow.TabIndex = 22;
            this.b_MarsNow.Text = "立即更新";
            this.b_MarsNow.UseVisualStyleBackColor = true;
            this.b_MarsNow.Click += new System.EventHandler(this.b_MarsNow_Click);
            // 
            // cb_nCoV
            // 
            this.cb_nCoV.AutoSize = true;
            this.cb_nCoV.Location = new System.Drawing.Point(12, 120);
            this.cb_nCoV.Name = "cb_nCoV";
            this.cb_nCoV.Size = new System.Drawing.Size(123, 21);
            this.cb_nCoV.TabIndex = 23;
            this.cb_nCoV.Text = "肺炎数据随机更新";
            this.cb_nCoV.UseVisualStyleBackColor = true;
            // 
            // b_NCOV
            // 
            this.b_NCOV.Location = new System.Drawing.Point(258, 120);
            this.b_NCOV.Name = "b_NCOV";
            this.b_NCOV.Size = new System.Drawing.Size(75, 23);
            this.b_NCOV.TabIndex = 24;
            this.b_NCOV.Text = "立即更新";
            this.b_NCOV.UseVisualStyleBackColor = true;
            this.b_NCOV.Click += new System.EventHandler(this.b_NCOV_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 412);
            this.Controls.Add(this.b_NCOV);
            this.Controls.Add(this.cb_nCoV);
            this.Controls.Add(this.b_MarsNow);
            this.Controls.Add(this.dtp_Mars);
            this.Controls.Add(this.cb_Mars);
            this.Controls.Add(this.b_Stop);
            this.Controls.Add(this.b_Start);
            this.Controls.Add(this.b_TuringNow);
            this.Controls.Add(this.b_FundNow);
            this.Controls.Add(this.cb_Truing);
            this.Controls.Add(this.cb_Fund);
            this.Controls.Add(this.dtp_Turing);
            this.Controls.Add(this.dtp_Fund);
            this.Controls.Add(this.ss_Status);
            this.Controls.Add(this.lb_Status);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据定时更新";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ss_Status.ResumeLayout(false);
            this.ss_Status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_Status;
        private System.Windows.Forms.StatusStrip ss_Status;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Status;
        private System.Windows.Forms.Timer t_Time;
        private System.Windows.Forms.DateTimePicker dtp_Fund;
        private System.Windows.Forms.DateTimePicker dtp_Turing;
        private System.Windows.Forms.CheckBox cb_Fund;
        private System.Windows.Forms.CheckBox cb_Truing;
        private System.Windows.Forms.Button b_FundNow;
        private System.Windows.Forms.Button b_TuringNow;
        private System.Windows.Forms.Button b_Start;
        private System.Windows.Forms.Button b_Stop;
        private System.Windows.Forms.CheckBox cb_Mars;
        private System.Windows.Forms.DateTimePicker dtp_Mars;
        private System.Windows.Forms.Button b_MarsNow;
        private System.Windows.Forms.CheckBox cb_nCoV;
        private System.Windows.Forms.Button b_NCOV;
    }
}

