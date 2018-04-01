namespace Test
{
    partial class Settings
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.num_timeout = new System.Windows.Forms.NumericUpDown();
            this.num_crawl_timeout = new System.Windows.Forms.NumericUpDown();
            this.btn_accept = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.num_crawl_limit = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.num_crawl_depth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_timeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_crawl_timeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_crawl_limit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_crawl_depth)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Таймаут";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(32, 8, 8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Таймаут краулинга";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // num_timeout
            // 
            this.num_timeout.Location = new System.Drawing.Point(262, 17);
            this.num_timeout.Margin = new System.Windows.Forms.Padding(8);
            this.num_timeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_timeout.Name = "num_timeout";
            this.num_timeout.Size = new System.Drawing.Size(200, 31);
            this.num_timeout.TabIndex = 0;
            this.num_timeout.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // num_crawl_timeout
            // 
            this.num_crawl_timeout.Location = new System.Drawing.Point(262, 64);
            this.num_crawl_timeout.Margin = new System.Windows.Forms.Padding(8);
            this.num_crawl_timeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_crawl_timeout.Name = "num_crawl_timeout";
            this.num_crawl_timeout.Size = new System.Drawing.Size(200, 31);
            this.num_crawl_timeout.TabIndex = 1;
            this.num_crawl_timeout.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // btn_accept
            // 
            this.btn_accept.Location = new System.Drawing.Point(271, 229);
            this.btn_accept.Margin = new System.Windows.Forms.Padding(8, 32, 64, 16);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(150, 44);
            this.btn_accept.TabIndex = 5;
            this.btn_accept.Text = "Принять";
            this.btn_accept.UseVisualStyleBackColor = true;
            this.btn_accept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(73, 229);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(64, 8, 8, 8);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(150, 44);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "Отмена";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // num_crawl_limit
            // 
            this.num_crawl_limit.Location = new System.Drawing.Point(262, 111);
            this.num_crawl_limit.Margin = new System.Windows.Forms.Padding(8);
            this.num_crawl_limit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_crawl_limit.Name = "num_crawl_limit";
            this.num_crawl_limit.Size = new System.Drawing.Size(200, 31);
            this.num_crawl_limit.TabIndex = 2;
            this.num_crawl_limit.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 113);
            this.label3.Margin = new System.Windows.Forms.Padding(32, 8, 8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Лимит краулинга";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // num_crawl_depth
            // 
            this.num_crawl_depth.Location = new System.Drawing.Point(262, 158);
            this.num_crawl_depth.Margin = new System.Windows.Forms.Padding(8);
            this.num_crawl_depth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_crawl_depth.Name = "num_crawl_depth";
            this.num_crawl_depth.Size = new System.Drawing.Size(200, 31);
            this.num_crawl_depth.TabIndex = 3;
            this.num_crawl_depth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 160);
            this.label4.Margin = new System.Windows.Forms.Padding(32, 8, 8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Глубина краулинга";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Settings
            // 
            this.AcceptButton = this.btn_accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(494, 299);
            this.Controls.Add(this.num_crawl_depth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.num_crawl_limit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_accept);
            this.Controls.Add(this.num_crawl_timeout);
            this.Controls.Add(this.num_timeout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            ((System.ComponentModel.ISupportInitialize)(this.num_timeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_crawl_timeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_crawl_limit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_crawl_depth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num_timeout;
        private System.Windows.Forms.NumericUpDown num_crawl_timeout;
        private System.Windows.Forms.Button btn_accept;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.NumericUpDown num_crawl_limit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown num_crawl_depth;
        private System.Windows.Forms.Label label4;
    }
}