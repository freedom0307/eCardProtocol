namespace Test
{
    partial class Form_Test
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
            this.btnNew_card = new System.Windows.Forms.Button();
            this.btnRecharge_card = new System.Windows.Forms.Button();
            this.btnDeductions_card = new System.Windows.Forms.Button();
            this.btnInitialize_card = new System.Windows.Forms.Button();
            this.btnReadblock = new System.Windows.Forms.Button();
            this.btnWriteblock = new System.Windows.Forms.Button();
            this.btnSetPort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNew_card
            // 
            this.btnNew_card.Location = new System.Drawing.Point(65, 81);
            this.btnNew_card.Name = "btnNew_card";
            this.btnNew_card.Size = new System.Drawing.Size(93, 56);
            this.btnNew_card.TabIndex = 0;
            this.btnNew_card.Text = "开卡";
            this.btnNew_card.UseVisualStyleBackColor = true;
            this.btnNew_card.Click += new System.EventHandler(this.btnNew_card_Click);
            // 
            // btnRecharge_card
            // 
            this.btnRecharge_card.Location = new System.Drawing.Point(230, 81);
            this.btnRecharge_card.Name = "btnRecharge_card";
            this.btnRecharge_card.Size = new System.Drawing.Size(93, 56);
            this.btnRecharge_card.TabIndex = 1;
            this.btnRecharge_card.Text = "充值";
            this.btnRecharge_card.UseVisualStyleBackColor = true;
            // 
            // btnDeductions_card
            // 
            this.btnDeductions_card.Location = new System.Drawing.Point(396, 81);
            this.btnDeductions_card.Name = "btnDeductions_card";
            this.btnDeductions_card.Size = new System.Drawing.Size(93, 56);
            this.btnDeductions_card.TabIndex = 2;
            this.btnDeductions_card.Text = "扣款";
            this.btnDeductions_card.UseVisualStyleBackColor = true;
            // 
            // btnInitialize_card
            // 
            this.btnInitialize_card.Location = new System.Drawing.Point(396, 199);
            this.btnInitialize_card.Name = "btnInitialize_card";
            this.btnInitialize_card.Size = new System.Drawing.Size(93, 56);
            this.btnInitialize_card.TabIndex = 3;
            this.btnInitialize_card.Text = "销卡";
            this.btnInitialize_card.UseVisualStyleBackColor = true;
            // 
            // btnReadblock
            // 
            this.btnReadblock.Location = new System.Drawing.Point(65, 199);
            this.btnReadblock.Name = "btnReadblock";
            this.btnReadblock.Size = new System.Drawing.Size(93, 56);
            this.btnReadblock.TabIndex = 4;
            this.btnReadblock.Text = "读块";
            this.btnReadblock.UseVisualStyleBackColor = true;
            // 
            // btnWriteblock
            // 
            this.btnWriteblock.Location = new System.Drawing.Point(230, 199);
            this.btnWriteblock.Name = "btnWriteblock";
            this.btnWriteblock.Size = new System.Drawing.Size(93, 56);
            this.btnWriteblock.TabIndex = 5;
            this.btnWriteblock.Text = "写块";
            this.btnWriteblock.UseVisualStyleBackColor = true;
            // 
            // btnSetPort
            // 
            this.btnSetPort.Location = new System.Drawing.Point(65, 306);
            this.btnSetPort.Name = "btnSetPort";
            this.btnSetPort.Size = new System.Drawing.Size(93, 56);
            this.btnSetPort.TabIndex = 6;
            this.btnSetPort.Text = "设置串口";
            this.btnSetPort.UseVisualStyleBackColor = true;
            this.btnSetPort.Click += new System.EventHandler(this.btnSetPort_Click);
            // 
            // Form_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 452);
            this.Controls.Add(this.btnSetPort);
            this.Controls.Add(this.btnWriteblock);
            this.Controls.Add(this.btnReadblock);
            this.Controls.Add(this.btnInitialize_card);
            this.Controls.Add(this.btnDeductions_card);
            this.Controls.Add(this.btnRecharge_card);
            this.Controls.Add(this.btnNew_card);
            this.Name = "Form_Test";
            this.Text = "测试";
            this.Load += new System.EventHandler(this.Form_Test_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNew_card;
        private System.Windows.Forms.Button btnRecharge_card;
        private System.Windows.Forms.Button btnDeductions_card;
        private System.Windows.Forms.Button btnInitialize_card;
        private System.Windows.Forms.Button btnReadblock;
        private System.Windows.Forms.Button btnWriteblock;
        private System.Windows.Forms.Button btnSetPort;
    }
}

