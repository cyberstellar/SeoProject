namespace Test
{
    partial class Code
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
            this.rtbx_code = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbx_code
            // 
            this.rtbx_code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx_code.Location = new System.Drawing.Point(0, 0);
            this.rtbx_code.Name = "rtbx_code";
            this.rtbx_code.Size = new System.Drawing.Size(1486, 968);
            this.rtbx_code.TabIndex = 0;
            this.rtbx_code.Text = "";
            // 
            // Code
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1486, 968);
            this.Controls.Add(this.rtbx_code);
            this.Name = "Code";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Код страницы";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.RichTextBox rtbx_code;
    }
}