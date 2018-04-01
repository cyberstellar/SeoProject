namespace Test
{
    partial class Edit
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
            this.tbx_address = new System.Windows.Forms.TextBox();
            this.tbx_anchor = new System.Windows.Forms.TextBox();
            this.tbx_status = new System.Windows.Forms.TextBox();
            this.cbx_autoload = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_accept = new System.Windows.Forms.Button();
            this.btn_cancell = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbx_address
            // 
            this.tbx_address.Location = new System.Drawing.Point(149, 17);
            this.tbx_address.Margin = new System.Windows.Forms.Padding(8, 8, 16, 8);
            this.tbx_address.Name = "tbx_address";
            this.tbx_address.Size = new System.Drawing.Size(600, 31);
            this.tbx_address.TabIndex = 0;
            // 
            // tbx_anchor
            // 
            this.tbx_anchor.Location = new System.Drawing.Point(149, 64);
            this.tbx_anchor.Margin = new System.Windows.Forms.Padding(8, 8, 16, 8);
            this.tbx_anchor.Name = "tbx_anchor";
            this.tbx_anchor.Size = new System.Drawing.Size(600, 31);
            this.tbx_anchor.TabIndex = 1;
            // 
            // tbx_status
            // 
            this.tbx_status.Location = new System.Drawing.Point(149, 170);
            this.tbx_status.Margin = new System.Windows.Forms.Padding(8, 32, 16, 8);
            this.tbx_status.Name = "tbx_status";
            this.tbx_status.ReadOnly = true;
            this.tbx_status.Size = new System.Drawing.Size(240, 31);
            this.tbx_status.TabIndex = 3;
            // 
            // cbx_autoload
            // 
            this.cbx_autoload.AutoSize = true;
            this.cbx_autoload.Location = new System.Drawing.Point(149, 106);
            this.cbx_autoload.Name = "cbx_autoload";
            this.cbx_autoload.Size = new System.Drawing.Size(333, 29);
            this.cbx_autoload.TabIndex = 2;
            this.cbx_autoload.Text = "Использовать Title страницы";
            this.cbx_autoload.UseVisualStyleBackColor = true;
            this.cbx_autoload.CheckedChanged += new System.EventHandler(this.CbxAutoload_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Anchor:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Status:";
            // 
            // btn_accept
            // 
            this.btn_accept.Location = new System.Drawing.Point(599, 166);
            this.btn_accept.Margin = new System.Windows.Forms.Padding(8, 8, 16, 8);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(150, 44);
            this.btn_accept.TabIndex = 4;
            this.btn_accept.Text = "Принять";
            this.btn_accept.UseVisualStyleBackColor = true;
            this.btn_accept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // btn_cancell
            // 
            this.btn_cancell.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancell.Location = new System.Drawing.Point(438, 166);
            this.btn_cancell.Name = "btn_cancell";
            this.btn_cancell.Size = new System.Drawing.Size(150, 44);
            this.btn_cancell.TabIndex = 5;
            this.btn_cancell.Text = "Отмена";
            this.btn_cancell.UseVisualStyleBackColor = true;
            // 
            // Edit
            // 
            this.AcceptButton = this.btn_accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancell;
            this.ClientSize = new System.Drawing.Size(774, 233);
            this.Controls.Add(this.btn_cancell);
            this.Controls.Add(this.btn_accept);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbx_autoload);
            this.Controls.Add(this.tbx_status);
            this.Controls.Add(this.tbx_anchor);
            this.Controls.Add(this.tbx_address);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Edit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbx_address;
        public System.Windows.Forms.TextBox tbx_anchor;
        public System.Windows.Forms.TextBox tbx_status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_cancell;
        private System.Windows.Forms.Button btn_accept;
        public System.Windows.Forms.CheckBox cbx_autoload;
    }
}