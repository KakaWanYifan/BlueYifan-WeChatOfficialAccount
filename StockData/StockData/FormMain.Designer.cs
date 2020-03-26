namespace StockData
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
            this.b_Start = new System.Windows.Forms.Button();
            this.b_Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_AM_BeginTime = new System.Windows.Forms.DateTimePicker();
            this.dtp_AM_EndTime = new System.Windows.Forms.DateTimePicker();
            this.t_Main = new System.Windows.Forms.Timer(this.components);
            this.t_Data = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_AM_ToDB = new System.Windows.Forms.DateTimePicker();
            this.lb_Now = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_GetStock = new System.Windows.Forms.DateTimePicker();
            this.dtp_Quant300 = new System.Windows.Forms.DateTimePicker();
            this.dtp_PM_BeginTime = new System.Windows.Forms.DateTimePicker();
            this.dtp_PM_EndTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtp_PM_ToDB = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // b_Start
            // 
            this.b_Start.Location = new System.Drawing.Point(12, 329);
            this.b_Start.Name = "b_Start";
            this.b_Start.Size = new System.Drawing.Size(100, 23);
            this.b_Start.TabIndex = 1;
            this.b_Start.TabStop = false;
            this.b_Start.Text = "开始";
            this.b_Start.UseVisualStyleBackColor = true;
            this.b_Start.Click += new System.EventHandler(this.b_Start_Click);
            // 
            // b_Stop
            // 
            this.b_Stop.Location = new System.Drawing.Point(146, 329);
            this.b_Stop.Name = "b_Stop";
            this.b_Stop.Size = new System.Drawing.Size(100, 23);
            this.b_Stop.TabIndex = 2;
            this.b_Stop.TabStop = false;
            this.b_Stop.Text = "终止";
            this.b_Stop.UseVisualStyleBackColor = true;
            this.b_Stop.Click += new System.EventHandler(this.b_Stop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "AM原始数据文件起始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "AM原始数据文件终止时间";
            // 
            // dtp_AM_BeginTime
            // 
            this.dtp_AM_BeginTime.CustomFormat = "HH:mm";
            this.dtp_AM_BeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_AM_BeginTime.Location = new System.Drawing.Point(186, 39);
            this.dtp_AM_BeginTime.Name = "dtp_AM_BeginTime";
            this.dtp_AM_BeginTime.ShowUpDown = true;
            this.dtp_AM_BeginTime.Size = new System.Drawing.Size(60, 21);
            this.dtp_AM_BeginTime.TabIndex = 5;
            this.dtp_AM_BeginTime.TabStop = false;
            this.dtp_AM_BeginTime.Value = new System.DateTime(2016, 11, 25, 9, 20, 0, 0);
            // 
            // dtp_AM_EndTime
            // 
            this.dtp_AM_EndTime.CustomFormat = "HH:mm";
            this.dtp_AM_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_AM_EndTime.Location = new System.Drawing.Point(186, 66);
            this.dtp_AM_EndTime.Name = "dtp_AM_EndTime";
            this.dtp_AM_EndTime.ShowUpDown = true;
            this.dtp_AM_EndTime.Size = new System.Drawing.Size(60, 21);
            this.dtp_AM_EndTime.TabIndex = 6;
            this.dtp_AM_EndTime.TabStop = false;
            this.dtp_AM_EndTime.Value = new System.DateTime(2016, 11, 25, 11, 40, 0, 0);
            // 
            // t_Main
            // 
            this.t_Main.Interval = 30000;
            this.t_Main.Tick += new System.EventHandler(this.t_Main_Tick);
            // 
            // t_Data
            // 
            this.t_Data.Interval = 500;
            this.t_Data.Tick += new System.EventHandler(this.t_Data_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "AM原始数据文件入库时间";
            // 
            // dtp_AM_ToDB
            // 
            this.dtp_AM_ToDB.CustomFormat = "HH:mm";
            this.dtp_AM_ToDB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_AM_ToDB.Location = new System.Drawing.Point(186, 147);
            this.dtp_AM_ToDB.Name = "dtp_AM_ToDB";
            this.dtp_AM_ToDB.ShowUpDown = true;
            this.dtp_AM_ToDB.Size = new System.Drawing.Size(60, 21);
            this.dtp_AM_ToDB.TabIndex = 8;
            this.dtp_AM_ToDB.TabStop = false;
            this.dtp_AM_ToDB.Value = new System.DateTime(2016, 11, 25, 9, 40, 0, 0);
            // 
            // lb_Now
            // 
            this.lb_Now.FormattingEnabled = true;
            this.lb_Now.ItemHeight = 12;
            this.lb_Now.Location = new System.Drawing.Point(252, 12);
            this.lb_Now.Name = "lb_Now";
            this.lb_Now.Size = new System.Drawing.Size(420, 340);
            this.lb_Now.TabIndex = 0;
            this.lb_Now.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "获取今日交易的股票列表";
            // 
            // dtp_GetStock
            // 
            this.dtp_GetStock.CustomFormat = "HH:mm";
            this.dtp_GetStock.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_GetStock.Location = new System.Drawing.Point(186, 12);
            this.dtp_GetStock.Name = "dtp_GetStock";
            this.dtp_GetStock.ShowUpDown = true;
            this.dtp_GetStock.Size = new System.Drawing.Size(60, 21);
            this.dtp_GetStock.TabIndex = 10;
            this.dtp_GetStock.TabStop = false;
            this.dtp_GetStock.Value = new System.DateTime(2016, 11, 25, 9, 10, 0, 0);
            // 
            // dtp_Quant300
            // 
            this.dtp_Quant300.CustomFormat = "HH:mm";
            this.dtp_Quant300.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Quant300.Location = new System.Drawing.Point(186, 199);
            this.dtp_Quant300.Name = "dtp_Quant300";
            this.dtp_Quant300.ShowUpDown = true;
            this.dtp_Quant300.Size = new System.Drawing.Size(60, 21);
            this.dtp_Quant300.TabIndex = 12;
            this.dtp_Quant300.TabStop = false;
            this.dtp_Quant300.Value = new System.DateTime(2016, 11, 25, 15, 20, 0, 0);
            // 
            // dtp_PM_BeginTime
            // 
            this.dtp_PM_BeginTime.CustomFormat = "HH:mm";
            this.dtp_PM_BeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_PM_BeginTime.Location = new System.Drawing.Point(186, 93);
            this.dtp_PM_BeginTime.Name = "dtp_PM_BeginTime";
            this.dtp_PM_BeginTime.ShowUpDown = true;
            this.dtp_PM_BeginTime.Size = new System.Drawing.Size(60, 21);
            this.dtp_PM_BeginTime.TabIndex = 13;
            this.dtp_PM_BeginTime.TabStop = false;
            this.dtp_PM_BeginTime.Value = new System.DateTime(2016, 11, 25, 12, 50, 0, 0);
            // 
            // dtp_PM_EndTime
            // 
            this.dtp_PM_EndTime.CustomFormat = "HH:mm";
            this.dtp_PM_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_PM_EndTime.Location = new System.Drawing.Point(186, 120);
            this.dtp_PM_EndTime.Name = "dtp_PM_EndTime";
            this.dtp_PM_EndTime.ShowUpDown = true;
            this.dtp_PM_EndTime.Size = new System.Drawing.Size(60, 21);
            this.dtp_PM_EndTime.TabIndex = 14;
            this.dtp_PM_EndTime.TabStop = false;
            this.dtp_PM_EndTime.Value = new System.DateTime(2016, 11, 25, 15, 10, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "PM原始数据文件起始时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "PM原始数据文件终止时间";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "PM原始数据文件入库时间";
            // 
            // dtp_PM_ToDB
            // 
            this.dtp_PM_ToDB.CustomFormat = "HH:mm";
            this.dtp_PM_ToDB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_PM_ToDB.Location = new System.Drawing.Point(186, 172);
            this.dtp_PM_ToDB.Name = "dtp_PM_ToDB";
            this.dtp_PM_ToDB.ShowUpDown = true;
            this.dtp_PM_ToDB.Size = new System.Drawing.Size(60, 21);
            this.dtp_PM_ToDB.TabIndex = 18;
            this.dtp_PM_ToDB.TabStop = false;
            this.dtp_PM_ToDB.Value = new System.DateTime(2016, 11, 25, 13, 10, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "量化分析沪深三百相似性";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 362);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtp_PM_ToDB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtp_PM_EndTime);
            this.Controls.Add(this.dtp_PM_BeginTime);
            this.Controls.Add(this.dtp_Quant300);
            this.Controls.Add(this.dtp_GetStock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtp_AM_ToDB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtp_AM_EndTime);
            this.Controls.Add(this.dtp_AM_BeginTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b_Stop);
            this.Controls.Add(this.b_Start);
            this.Controls.Add(this.lb_Now);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "股票数据";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_Start;
        private System.Windows.Forms.Button b_Stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_AM_BeginTime;
        private System.Windows.Forms.DateTimePicker dtp_AM_EndTime;
        private System.Windows.Forms.Timer t_Main;
        private System.Windows.Forms.Timer t_Data;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_AM_ToDB;
        private System.Windows.Forms.ListBox lb_Now;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_GetStock;
        private System.Windows.Forms.DateTimePicker dtp_Quant300;
        private System.Windows.Forms.DateTimePicker dtp_PM_BeginTime;
        private System.Windows.Forms.DateTimePicker dtp_PM_EndTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtp_PM_ToDB;
        private System.Windows.Forms.Label label5;
    }
}

