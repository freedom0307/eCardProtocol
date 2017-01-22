namespace eCardProtocol
{
    partial class Frm_portSet
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
            this.portname = new System.Windows.Forms.Label();
            this.portControl = new System.Windows.Forms.Button();
            this.cmbPortname = new System.Windows.Forms.ComboBox();
            this.串口设置 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.portstate = new System.Windows.Forms.Label();
            this.cmbStopbit = new System.Windows.Forms.ComboBox();
            this.stopbit = new System.Windows.Forms.Label();
            this.cmbCheckbit = new System.Windows.Forms.ComboBox();
            this.checkbit = new System.Windows.Forms.Label();
            this.cmbDatabit = new System.Windows.Forms.ComboBox();
            this.databit = new System.Windows.Forms.Label();
            this.cmbBaudrate = new System.Windows.Forms.ComboBox();
            this.Exit = new System.Windows.Forms.Label();
            this.串口设置.SuspendLayout();
            this.SuspendLayout();
            // 
            // portname
            // 
            this.portname.AutoSize = true;
            this.portname.Location = new System.Drawing.Point(30, 48);
            this.portname.Name = "portname";
            this.portname.Size = new System.Drawing.Size(40, 16);
            this.portname.TabIndex = 0;
            this.portname.Text = "串口";
            // 
            // portControl
            // 
            this.portControl.Location = new System.Drawing.Point(111, 261);
            this.portControl.Name = "portControl";
            this.portControl.Size = new System.Drawing.Size(108, 32);
            this.portControl.TabIndex = 1;
            this.portControl.Text = "打开串口";
            this.portControl.UseVisualStyleBackColor = true;
            this.portControl.Click += new System.EventHandler(this.portControl_Click);
            // 
            // cmbPortname
            // 
            this.cmbPortname.FormattingEnabled = true;
            this.cmbPortname.Location = new System.Drawing.Point(111, 45);
            this.cmbPortname.Name = "cmbPortname";
            this.cmbPortname.Size = new System.Drawing.Size(121, 24);
            this.cmbPortname.TabIndex = 2;
            this.cmbPortname.SelectedIndexChanged += new System.EventHandler(this.cmbPortname_SelectedIndexChanged);
            // 
            // 串口设置
            // 
            this.串口设置.Controls.Add(this.btnExit);
            this.串口设置.Controls.Add(this.portstate);
            this.串口设置.Controls.Add(this.cmbStopbit);
            this.串口设置.Controls.Add(this.portControl);
            this.串口设置.Controls.Add(this.stopbit);
            this.串口设置.Controls.Add(this.cmbCheckbit);
            this.串口设置.Controls.Add(this.checkbit);
            this.串口设置.Controls.Add(this.cmbDatabit);
            this.串口设置.Controls.Add(this.databit);
            this.串口设置.Controls.Add(this.cmbBaudrate);
            this.串口设置.Controls.Add(this.Exit);
            this.串口设置.Controls.Add(this.portname);
            this.串口设置.Controls.Add(this.cmbPortname);
            this.串口设置.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.串口设置.Location = new System.Drawing.Point(22, 28);
            this.串口设置.Name = "串口设置";
            this.串口设置.Size = new System.Drawing.Size(294, 365);
            this.串口设置.TabIndex = 3;
            this.串口设置.TabStop = false;
            this.串口设置.Text = "串口设置";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(111, 316);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(108, 32);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // portstate
            // 
            this.portstate.AutoSize = true;
            this.portstate.Location = new System.Drawing.Point(21, 269);
            this.portstate.Name = "portstate";
            this.portstate.Size = new System.Drawing.Size(88, 16);
            this.portstate.TabIndex = 11;
            this.portstate.Text = "串口已关闭";
            // 
            // cmbStopbit
            // 
            this.cmbStopbit.FormattingEnabled = true;
            this.cmbStopbit.Location = new System.Drawing.Point(111, 205);
            this.cmbStopbit.Name = "cmbStopbit";
            this.cmbStopbit.Size = new System.Drawing.Size(121, 24);
            this.cmbStopbit.TabIndex = 10;
            // 
            // stopbit
            // 
            this.stopbit.AutoSize = true;
            this.stopbit.Location = new System.Drawing.Point(30, 208);
            this.stopbit.Name = "stopbit";
            this.stopbit.Size = new System.Drawing.Size(56, 16);
            this.stopbit.TabIndex = 9;
            this.stopbit.Text = "结束位";
            // 
            // cmbCheckbit
            // 
            this.cmbCheckbit.FormattingEnabled = true;
            this.cmbCheckbit.Location = new System.Drawing.Point(111, 165);
            this.cmbCheckbit.Name = "cmbCheckbit";
            this.cmbCheckbit.Size = new System.Drawing.Size(121, 24);
            this.cmbCheckbit.TabIndex = 8;
            // 
            // checkbit
            // 
            this.checkbit.AutoSize = true;
            this.checkbit.Location = new System.Drawing.Point(30, 168);
            this.checkbit.Name = "checkbit";
            this.checkbit.Size = new System.Drawing.Size(56, 16);
            this.checkbit.TabIndex = 7;
            this.checkbit.Text = "校验位";
            // 
            // cmbDatabit
            // 
            this.cmbDatabit.FormattingEnabled = true;
            this.cmbDatabit.Location = new System.Drawing.Point(111, 125);
            this.cmbDatabit.Name = "cmbDatabit";
            this.cmbDatabit.Size = new System.Drawing.Size(121, 24);
            this.cmbDatabit.TabIndex = 6;
            // 
            // databit
            // 
            this.databit.AutoSize = true;
            this.databit.Location = new System.Drawing.Point(30, 128);
            this.databit.Name = "databit";
            this.databit.Size = new System.Drawing.Size(56, 16);
            this.databit.TabIndex = 5;
            this.databit.Text = "数据位";
            // 
            // cmbBaudrate
            // 
            this.cmbBaudrate.FormattingEnabled = true;
            this.cmbBaudrate.Location = new System.Drawing.Point(111, 85);
            this.cmbBaudrate.Name = "cmbBaudrate";
            this.cmbBaudrate.Size = new System.Drawing.Size(121, 24);
            this.cmbBaudrate.TabIndex = 4;
            // 
            // Exit
            // 
            this.Exit.AutoSize = true;
            this.Exit.Location = new System.Drawing.Point(30, 88);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(56, 16);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "波特率";
            // 
            // Frm_portSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 417);
            this.Controls.Add(this.串口设置);
            this.Name = "Frm_portSet";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.串口设置.ResumeLayout(false);
            this.串口设置.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label portname;
        private System.Windows.Forms.Button portControl;
        private System.Windows.Forms.ComboBox cmbPortname;
        private System.Windows.Forms.GroupBox 串口设置;
        private System.Windows.Forms.Label portstate;
        private System.Windows.Forms.ComboBox cmbStopbit;
        private System.Windows.Forms.Label stopbit;
        private System.Windows.Forms.ComboBox cmbCheckbit;
        private System.Windows.Forms.Label checkbit;
        private System.Windows.Forms.ComboBox cmbDatabit;
        private System.Windows.Forms.Label databit;
        private System.Windows.Forms.ComboBox cmbBaudrate;
        private System.Windows.Forms.Label Exit;
        private System.Windows.Forms.Button btnExit;
    }
}

