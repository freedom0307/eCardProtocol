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
            this.SuspendLayout();
            // 
            // btnSetPort
            // 
            this.btnSetPort.Location = new System.Drawing.Point(64, 363);
            this.btnSetPort.Name = "btnSetPort";
            this.btnSetPort.Size = new System.Drawing.Size(93, 56);
            this.btnSetPort.TabIndex = 13;
            this.btnSetPort.Text = "设置串口";
            this.btnSetPort.UseVisualStyleBackColor = true;
            this.btnSetPort.Click += new System.EventHandler(this.btnSetPort_Click);
            // 
            // btnWriteblock
            // 
            this.btnWriteblock.Location = new System.Drawing.Point(229, 256);
            this.btnWriteblock.Name = "btnWriteblock";
            this.btnWriteblock.Size = new System.Drawing.Size(93, 56);
            this.btnWriteblock.TabIndex = 12;
            this.btnWriteblock.Text = "写块";
            this.btnWriteblock.UseVisualStyleBackColor = true;
            // 
            // btnReadblock
            // 
            this.btnReadblock.Location = new System.Drawing.Point(64, 256);
            this.btnReadblock.Name = "btnReadblock";
            this.btnReadblock.Size = new System.Drawing.Size(93, 56);
            this.btnReadblock.TabIndex = 11;
            this.btnReadblock.Text = "读块";
            this.btnReadblock.UseVisualStyleBackColor = true;
            this.btnReadblock.Click += new System.EventHandler(this.btnReadblock_Click);
            // 
            // btnInitialize_card
            // 
            this.btnInitialize_card.Location = new System.Drawing.Point(395, 256);
            this.btnInitialize_card.Name = "btnInitialize_card";
            this.btnInitialize_card.Size = new System.Drawing.Size(93, 56);
            this.btnInitialize_card.TabIndex = 10;
            this.btnInitialize_card.Text = "销卡";
            this.btnInitialize_card.UseVisualStyleBackColor = true;
            this.btnInitialize_card.Click += new System.EventHandler(this.btnInitialize_card_Click);
            // 
            // btnDeductions_card
            // 
            this.btnDeductions_card.Location = new System.Drawing.Point(395, 138);
            this.btnDeductions_card.Name = "btnDeductions_card";
            this.btnDeductions_card.Size = new System.Drawing.Size(93, 56);
            this.btnDeductions_card.TabIndex = 9;
            this.btnDeductions_card.Text = "扣款";
            this.btnDeductions_card.UseVisualStyleBackColor = true;
            this.btnDeductions_card.Click += new System.EventHandler(this.btnDeductions_card_Click);
            // 
            // btnRecharge_card
            // 
            this.btnRecharge_card.Location = new System.Drawing.Point(229, 138);
            this.btnRecharge_card.Name = "btnRecharge_card";
            this.btnRecharge_card.Size = new System.Drawing.Size(93, 56);
            this.btnRecharge_card.TabIndex = 8;
            this.btnRecharge_card.Text = "充值";
            this.btnRecharge_card.UseVisualStyleBackColor = true;
            this.btnRecharge_card.Click += new System.EventHandler(this.btnRecharge_card_Click);
            // 
            // btnNew_card
            // 
            this.btnNew_card.Location = new System.Drawing.Point(64, 138);
            this.btnNew_card.Name = "btnNew_card";
            this.btnNew_card.Size = new System.Drawing.Size(93, 56);
            this.btnNew_card.TabIndex = 7;
            this.btnNew_card.Text = "开卡";
            this.btnNew_card.UseVisualStyleBackColor = true;
            this.btnNew_card.Click += new System.EventHandler(this.btnNew_card_Click);
            // 
            // btnVirtualTest
            // 
            this.btnVirtualTest.Location = new System.Drawing.Point(229, 363);
            this.btnVirtualTest.Name = "btnVirtualTest";
            this.btnVirtualTest.Size = new System.Drawing.Size(93, 56);
            this.btnVirtualTest.TabIndex = 14;
            this.btnVirtualTest.Text = "虚拟测试";
            this.btnVirtualTest.UseVisualStyleBackColor = true;
            this.btnVirtualTest.Click += new System.EventHandler(this.btnVirtualTest_Click);
            // 
            // btnLoadkey
            // 
            this.btnLoadkey.Location = new System.Drawing.Point(395, 363);
            this.btnLoadkey.Name = "btnLoadkey";
            this.btnLoadkey.Size = new System.Drawing.Size(93, 56);
            this.btnLoadkey.TabIndex = 15;
            this.btnLoadkey.Text = "装载秘钥";
            this.btnLoadkey.UseVisualStyleBackColor = true;
            this.btnLoadkey.Click += new System.EventHandler(this.btnLoadkey_Click);
            // 
            // btnQuery_card
            // 
            this.btnQuery_card.Location = new System.Drawing.Point(229, 28);
            this.btnQuery_card.Name = "btnQuery_card";
            this.btnQuery_card.Size = new System.Drawing.Size(93, 56);
            this.btnQuery_card.TabIndex = 16;
            this.btnQuery_card.Text = "查询";
            this.btnQuery_card.UseVisualStyleBackColor = true;
            this.btnQuery_card.Click += new System.EventHandler(this.btnQuery_card_Click);
            // 
            // frm_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 502);
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
    }
}