namespace eCardProtocol
{
    partial class SetSecret
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
            this.labelAkey = new System.Windows.Forms.Label();
            this.btnSetKey = new System.Windows.Forms.Button();
            this.txtAkey = new System.Windows.Forms.TextBox();
            this.labelBkey = new System.Windows.Forms.Label();
            this.txtBkey = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelAkey
            // 
            this.labelAkey.AutoSize = true;
            this.labelAkey.Location = new System.Drawing.Point(70, 118);
            this.labelAkey.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelAkey.Name = "labelAkey";
            this.labelAkey.Size = new System.Drawing.Size(57, 19);
            this.labelAkey.TabIndex = 0;
            this.labelAkey.Text = "秘钥A";
            // 
            // btnSetKey
            // 
            this.btnSetKey.Location = new System.Drawing.Point(214, 256);
            this.btnSetKey.Margin = new System.Windows.Forms.Padding(5);
            this.btnSetKey.Name = "btnSetKey";
            this.btnSetKey.Size = new System.Drawing.Size(118, 36);
            this.btnSetKey.TabIndex = 1;
            this.btnSetKey.Text = "确定";
            this.btnSetKey.UseVisualStyleBackColor = true;
            this.btnSetKey.Click += new System.EventHandler(this.btnSetKey_Click);
            // 
            // txtAkey
            // 
            this.txtAkey.Location = new System.Drawing.Point(214, 118);
            this.txtAkey.Margin = new System.Windows.Forms.Padding(5);
            this.txtAkey.Name = "txtAkey";
            this.txtAkey.Size = new System.Drawing.Size(258, 29);
            this.txtAkey.TabIndex = 2;
            // 
            // labelBkey
            // 
            this.labelBkey.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.labelBkey.AutoSize = true;
            this.labelBkey.Location = new System.Drawing.Point(70, 188);
            this.labelBkey.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelBkey.Name = "labelBkey";
            this.labelBkey.Size = new System.Drawing.Size(57, 19);
            this.labelBkey.TabIndex = 3;
            this.labelBkey.Text = "秘钥B";
            // 
            // txtBkey
            // 
            this.txtBkey.Location = new System.Drawing.Point(214, 178);
            this.txtBkey.Margin = new System.Windows.Forms.Padding(5);
            this.txtBkey.Name = "txtBkey";
            this.txtBkey.Size = new System.Drawing.Size(258, 29);
            this.txtBkey.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(342, 256);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(118, 36);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "返回";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SetSecret
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 439);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtBkey);
            this.Controls.Add(this.labelBkey);
            this.Controls.Add(this.txtAkey);
            this.Controls.Add(this.btnSetKey);
            this.Controls.Add(this.labelAkey);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SetSecret";
            this.Text = "SetSecret";
            this.Load += new System.EventHandler(this.SetSecret_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAkey;
        private System.Windows.Forms.Button btnSetKey;
        private System.Windows.Forms.TextBox txtAkey;
        private System.Windows.Forms.Label labelBkey;
        private System.Windows.Forms.TextBox txtBkey;
        private System.Windows.Forms.Button btnCancel;
    }
}