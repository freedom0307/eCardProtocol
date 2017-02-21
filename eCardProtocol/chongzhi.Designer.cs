namespace eCardProtocol
{
    partial class chongzhi
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
            this.btnChongzhi = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.JJe = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.userTEL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.stag = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnChongzhi
            // 
            this.btnChongzhi.Location = new System.Drawing.Point(124, 271);
            this.btnChongzhi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChongzhi.Name = "btnChongzhi";
            this.btnChongzhi.Size = new System.Drawing.Size(94, 27);
            this.btnChongzhi.TabIndex = 0;
            this.btnChongzhi.Text = "充值";
            this.btnChongzhi.UseVisualStyleBackColor = true;
            this.btnChongzhi.Click += new System.EventHandler(this.btnChongzhi_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(255, 271);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 27);
            this.button2.TabIndex = 1;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "金额";
            // 
            // JJe
            // 
            this.JJe.Location = new System.Drawing.Point(228, 159);
            this.JJe.Name = "JJe";
            this.JJe.Size = new System.Drawing.Size(157, 29);
            this.JJe.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "用户名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "手机号";
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(228, 54);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(157, 29);
            this.userName.TabIndex = 6;
            // 
            // userTEL
            // 
            this.userTEL.Location = new System.Drawing.Point(228, 103);
            this.userTEL.Name = "userTEL";
            this.userTEL.Size = new System.Drawing.Size(157, 29);
            this.userTEL.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(120, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "标志";
            // 
            // stag
            // 
            this.stag.FormattingEnabled = true;
            this.stag.Items.AddRange(new object[] {
            "00",
            "01",
            "255"});
            this.stag.Location = new System.Drawing.Point(228, 208);
            this.stag.Name = "stag";
            this.stag.Size = new System.Drawing.Size(157, 27);
            this.stag.TabIndex = 9;
            // 
            // chongzhi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 413);
            this.Controls.Add(this.stag);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.userTEL);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.JJe);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnChongzhi);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "chongzhi";
            this.Text = "充值";
            this.Load += new System.EventHandler(this.chongzhi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChongzhi;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox JJe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox userTEL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox stag;
    }
}