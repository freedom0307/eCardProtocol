namespace eCardProtocol
{
    partial class NewCardFrm
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.jjjje = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newKeyA = new System.Windows.Forms.ComboBox();
            this.newKeyB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbMaxCount = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(125, 296);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "开卡";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "金额";
            // 
            // jjjje
            // 
            this.jjjje.FormattingEnabled = true;
            this.jjjje.Items.AddRange(new object[] {
            "0.00"});
            this.jjjje.Location = new System.Drawing.Point(250, 57);
            this.jjjje.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.jjjje.Name = "jjjje";
            this.jjjje.Size = new System.Drawing.Size(195, 27);
            this.jjjje.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 113);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "秘钥A";
            // 
            // newKeyA
            // 
            this.newKeyA.FormattingEnabled = true;
            this.newKeyA.Items.AddRange(new object[] {
            "FFFFFFFFFFFF"});
            this.newKeyA.Location = new System.Drawing.Point(250, 113);
            this.newKeyA.Margin = new System.Windows.Forms.Padding(4);
            this.newKeyA.Name = "newKeyA";
            this.newKeyA.Size = new System.Drawing.Size(195, 27);
            this.newKeyA.TabIndex = 4;
            // 
            // newKeyB
            // 
            this.newKeyB.FormattingEnabled = true;
            this.newKeyB.Items.AddRange(new object[] {
            "FFFFFFFFFFFF"});
            this.newKeyB.Location = new System.Drawing.Point(250, 167);
            this.newKeyB.Margin = new System.Windows.Forms.Padding(4);
            this.newKeyB.Name = "newKeyB";
            this.newKeyB.Size = new System.Drawing.Size(195, 27);
            this.newKeyB.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 175);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "秘钥B";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(254, 296);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 27);
            this.button2.TabIndex = 7;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 228);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "最大欠款次数";
            // 
            // cmbMaxCount
            // 
            this.cmbMaxCount.FormattingEnabled = true;
            this.cmbMaxCount.Items.AddRange(new object[] {
            "3",
            "5",
            "8",
            "10"});
            this.cmbMaxCount.Location = new System.Drawing.Point(250, 220);
            this.cmbMaxCount.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMaxCount.Name = "cmbMaxCount";
            this.cmbMaxCount.Size = new System.Drawing.Size(195, 27);
            this.cmbMaxCount.TabIndex = 9;
            // 
            // NewCardFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 397);
            this.Controls.Add(this.cmbMaxCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.newKeyB);
            this.Controls.Add(this.newKeyA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.jjjje);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "NewCardFrm";
            this.Text = "开卡";
            this.Load += new System.EventHandler(this.NewCardFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox jjjje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox newKeyA;
        private System.Windows.Forms.ComboBox newKeyB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbMaxCount;
    }
}