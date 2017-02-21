namespace eCardProtocol
{
    partial class frm_Test
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSetPort = new System.Windows.Forms.Button();
            this.btnWriteblock = new System.Windows.Forms.Button();
            this.btnReadblock = new System.Windows.Forms.Button();
            this.btnInitialize_card = new System.Windows.Forms.Button();
            this.btnDeductions_card = new System.Windows.Forms.Button();
            this.btnRecharge_card = new System.Windows.Forms.Button();
            this.btnNew_card = new System.Windows.Forms.Button();
            this.btnVirtualTest = new System.Windows.Forms.Button();
            this.btnLoadkey = new System.Windows.Forms.Button();
            this.btnQuery_card = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnSetPort
            // 
            this.btnSetPort.Location = new System.Drawing.Point(64, 315);
            this.btnSetPort.Name = "btnSetPort";
            this.btnSetPort.Size = new System.Drawing.Size(93, 56);
            this.btnSetPort.TabIndex = 13;
            this.btnSetPort.Text = "设置串口";
            this.btnSetPort.UseVisualStyleBackColor = true;
            this.btnSetPort.Click += new System.EventHandler(this.btnSetPort_Click);
            // 
            // btnWriteblock
            // 
            this.btnWriteblock.Location = new System.Drawing.Point(177, 208);
            this.btnWriteblock.Name = "btnWriteblock";
            this.btnWriteblock.Size = new System.Drawing.Size(93, 56);
            this.btnWriteblock.TabIndex = 12;
            this.btnWriteblock.Text = "写块";
            this.btnWriteblock.UseVisualStyleBackColor = true;
            this.btnWriteblock.Click += new System.EventHandler(this.btnWriteblock_Click);
            // 
            // btnReadblock
            // 
            this.btnReadblock.Location = new System.Drawing.Point(64, 208);
            this.btnReadblock.Name = "btnReadblock";
            this.btnReadblock.Size = new System.Drawing.Size(93, 56);
            this.btnReadblock.TabIndex = 11;
            this.btnReadblock.Text = "读块";
            this.btnReadblock.UseVisualStyleBackColor = true;
            this.btnReadblock.Click += new System.EventHandler(this.btnReadblock_Click);
            // 
            // btnInitialize_card
            // 
            this.btnInitialize_card.Location = new System.Drawing.Point(290, 208);
            this.btnInitialize_card.Name = "btnInitialize_card";
            this.btnInitialize_card.Size = new System.Drawing.Size(93, 56);
            this.btnInitialize_card.TabIndex = 10;
            this.btnInitialize_card.Text = "销卡";
            this.btnInitialize_card.UseVisualStyleBackColor = true;
            this.btnInitialize_card.Click += new System.EventHandler(this.btnInitialize_card_Click);
            // 
            // btnDeductions_card
            // 
            this.btnDeductions_card.Location = new System.Drawing.Point(290, 92);
            this.btnDeductions_card.Name = "btnDeductions_card";
            this.btnDeductions_card.Size = new System.Drawing.Size(93, 56);
            this.btnDeductions_card.TabIndex = 9;
            this.btnDeductions_card.Text = "扣款";
            this.btnDeductions_card.UseVisualStyleBackColor = true;
            this.btnDeductions_card.Click += new System.EventHandler(this.btnDeductions_card_Click);
            // 
            // btnRecharge_card
            // 
            this.btnRecharge_card.Location = new System.Drawing.Point(177, 92);
            this.btnRecharge_card.Name = "btnRecharge_card";
            this.btnRecharge_card.Size = new System.Drawing.Size(93, 56);
            this.btnRecharge_card.TabIndex = 8;
            this.btnRecharge_card.Text = "充值";
            this.btnRecharge_card.UseVisualStyleBackColor = true;
            this.btnRecharge_card.Click += new System.EventHandler(this.btnRecharge_card_Click);
            // 
            // btnNew_card
            // 
            this.btnNew_card.Location = new System.Drawing.Point(64, 92);
            this.btnNew_card.Name = "btnNew_card";
            this.btnNew_card.Size = new System.Drawing.Size(93, 56);
            this.btnNew_card.TabIndex = 7;
            this.btnNew_card.Text = "开卡";
            this.btnNew_card.UseVisualStyleBackColor = true;
            this.btnNew_card.Click += new System.EventHandler(this.btnNew_card_Click);
            // 
            // btnVirtualTest
            // 
            this.btnVirtualTest.Location = new System.Drawing.Point(64, 423);
            this.btnVirtualTest.Name = "btnVirtualTest";
            this.btnVirtualTest.Size = new System.Drawing.Size(93, 56);
            this.btnVirtualTest.TabIndex = 14;
            this.btnVirtualTest.Text = "虚拟测试";
            this.btnVirtualTest.UseVisualStyleBackColor = true;
            this.btnVirtualTest.Visible = false;
            this.btnVirtualTest.Click += new System.EventHandler(this.btnVirtualTest_Click);
            // 
            // btnLoadkey
            // 
            this.btnLoadkey.Location = new System.Drawing.Point(290, 315);
            this.btnLoadkey.Name = "btnLoadkey";
            this.btnLoadkey.Size = new System.Drawing.Size(93, 56);
            this.btnLoadkey.TabIndex = 15;
            this.btnLoadkey.Text = "装载秘钥";
            this.btnLoadkey.UseVisualStyleBackColor = true;
            this.btnLoadkey.Click += new System.EventHandler(this.btnLoadkey_Click);
            // 
            // btnQuery_card
            // 
            this.btnQuery_card.Location = new System.Drawing.Point(177, 315);
            this.btnQuery_card.Name = "btnQuery_card";
            this.btnQuery_card.Size = new System.Drawing.Size(93, 56);
            this.btnQuery_card.TabIndex = 16;
            this.btnQuery_card.Text = "查询";
            this.btnQuery_card.UseVisualStyleBackColor = true;
            this.btnQuery_card.Click += new System.EventHandler(this.btnQuery_card_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "订单信息";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(425, 92);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(443, 387);
            this.richTextBox1.TabIndex = 19;
            this.richTextBox1.Text = "";
            // 
            // frm_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 502);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnQuery_card);
            this.Controls.Add(this.btnLoadkey);
            this.Controls.Add(this.btnVirtualTest);
            this.Controls.Add(this.btnSetPort);
            this.Controls.Add(this.btnWriteblock);
            this.Controls.Add(this.btnReadblock);
            this.Controls.Add(this.btnInitialize_card);
            this.Controls.Add(this.btnDeductions_card);
            this.Controls.Add(this.btnRecharge_card);
            this.Controls.Add(this.btnNew_card);
            this.Name = "frm_Test";
            this.Text = "测试";
            this.Load += new System.EventHandler(this.frm_Test_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetPort;
        private System.Windows.Forms.Button btnWriteblock;
        private System.Windows.Forms.Button btnReadblock;
        private System.Windows.Forms.Button btnInitialize_card;
        private System.Windows.Forms.Button btnDeductions_card;
        private System.Windows.Forms.Button btnRecharge_card;
        private System.Windows.Forms.Button btnNew_card;
        private System.Windows.Forms.Button btnVirtualTest;
        private System.Windows.Forms.Button btnLoadkey;
        private System.Windows.Forms.Button btnQuery_card;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}