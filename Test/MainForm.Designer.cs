namespace Test
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_start = new System.Windows.Forms.Button();
            this.tab_control = new System.Windows.Forms.TabControl();
            this.tab_audit = new System.Windows.Forms.TabPage();
            this.label_score = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_advice = new System.Windows.Forms.TextBox();
            this.trv_problems = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.lbx_summary = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tab_links = new System.Windows.Forms.TabPage();
            this.btn_save_links = new System.Windows.Forms.Button();
            this.trv_links = new System.Windows.Forms.TreeView();
            this.ctx_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctx_edit_link = new System.Windows.Forms.ToolStripMenuItem();
            this.ctx_copy_address = new System.Windows.Forms.ToolStripMenuItem();
            this.ctx_show_code = new System.Windows.Forms.ToolStripMenuItem();
            this.ctx_open_in_browser = new System.Windows.Forms.ToolStripMenuItem();
            this.tab_words = new System.Windows.Forms.TabPage();
            this.trv_words = new System.Windows.Forms.TreeView();
            this.tab_tags = new System.Windows.Forms.TabPage();
            this.trv_tags = new System.Windows.Forms.TreeView();
            this.tab_position = new System.Windows.Forms.TabPage();
            this.lbx_keywords = new System.Windows.Forms.ListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dgv_results = new System.Windows.Forms.DataGridView();
            this.position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.query = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_crawler = new System.Windows.Forms.TabPage();
            this.dgv_links = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu_strip = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_parameters = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about = new System.Windows.Forms.ToolStripMenuItem();
            this.open_file_dialog = new System.Windows.Forms.OpenFileDialog();
            this.cbx_url = new System.Windows.Forms.ComboBox();
            this.folder_browser_dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cbx_keywords = new System.Windows.Forms.ComboBox();
            this.tab_control.SuspendLayout();
            this.tab_audit.SuspendLayout();
            this.tab_links.SuspendLayout();
            this.ctx_menu.SuspendLayout();
            this.tab_words.SuspendLayout();
            this.tab_tags.SuspendLayout();
            this.tab_position.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).BeginInit();
            this.tab_crawler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_links)).BeginInit();
            this.menu_strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(833, 50);
            this.btn_start.Margin = new System.Windows.Forms.Padding(8);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(150, 44);
            this.btn_start.TabIndex = 1;
            this.btn_start.Text = "Начать";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.BtnStart_ClickAsync);
            // 
            // tab_control
            // 
            this.tab_control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab_control.Controls.Add(this.tab_audit);
            this.tab_control.Controls.Add(this.tab_links);
            this.tab_control.Controls.Add(this.tab_words);
            this.tab_control.Controls.Add(this.tab_tags);
            this.tab_control.Controls.Add(this.tab_position);
            this.tab_control.Controls.Add(this.tab_crawler);
            this.tab_control.Location = new System.Drawing.Point(17, 101);
            this.tab_control.Margin = new System.Windows.Forms.Padding(8);
            this.tab_control.Name = "tab_control";
            this.tab_control.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tab_control.SelectedIndex = 0;
            this.tab_control.Size = new System.Drawing.Size(1242, 658);
            this.tab_control.TabIndex = 2;
            // 
            // tab_audit
            // 
            this.tab_audit.Controls.Add(this.label_score);
            this.tab_audit.Controls.Add(this.label4);
            this.tab_audit.Controls.Add(this.tbx_advice);
            this.tab_audit.Controls.Add(this.trv_problems);
            this.tab_audit.Controls.Add(this.label3);
            this.tab_audit.Controls.Add(this.lbx_summary);
            this.tab_audit.Controls.Add(this.label2);
            this.tab_audit.Location = new System.Drawing.Point(8, 39);
            this.tab_audit.Name = "tab_audit";
            this.tab_audit.Padding = new System.Windows.Forms.Padding(3);
            this.tab_audit.Size = new System.Drawing.Size(1226, 611);
            this.tab_audit.TabIndex = 6;
            this.tab_audit.Text = "Обзор";
            this.tab_audit.UseVisualStyleBackColor = true;
            // 
            // label_score
            // 
            this.label_score.Font = new System.Drawing.Font("Arial Black", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_score.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label_score.Location = new System.Drawing.Point(11, 465);
            this.label_score.Margin = new System.Windows.Forms.Padding(8);
            this.label_score.Name = "label_score";
            this.label_score.Size = new System.Drawing.Size(400, 135);
            this.label_score.TabIndex = 0;
            this.label_score.Text = "...";
            this.label_score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(865, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(350, 45);
            this.label4.TabIndex = 0;
            this.label4.Text = "Рекомендации";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_advice
            // 
            this.tbx_advice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_advice.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbx_advice.Location = new System.Drawing.Point(873, 72);
            this.tbx_advice.Margin = new System.Windows.Forms.Padding(8);
            this.tbx_advice.Multiline = true;
            this.tbx_advice.Name = "tbx_advice";
            this.tbx_advice.ReadOnly = true;
            this.tbx_advice.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbx_advice.Size = new System.Drawing.Size(342, 528);
            this.tbx_advice.TabIndex = 2;
            this.tbx_advice.Text = "Нажмите \"Начать\", чтобы продолжить.\r\n";
            // 
            // trv_problems
            // 
            this.trv_problems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trv_problems.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trv_problems.FullRowSelect = true;
            this.trv_problems.Location = new System.Drawing.Point(427, 72);
            this.trv_problems.Margin = new System.Windows.Forms.Padding(8);
            this.trv_problems.Name = "trv_problems";
            this.trv_problems.ShowLines = false;
            this.trv_problems.Size = new System.Drawing.Size(430, 528);
            this.trv_problems.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(427, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(430, 45);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ошибки";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbx_summary
            // 
            this.lbx_summary.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbx_summary.FormattingEnabled = true;
            this.lbx_summary.ItemHeight = 28;
            this.lbx_summary.Location = new System.Drawing.Point(11, 72);
            this.lbx_summary.Margin = new System.Windows.Forms.Padding(8);
            this.lbx_summary.Name = "lbx_summary";
            this.lbx_summary.Size = new System.Drawing.Size(400, 368);
            this.lbx_summary.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(11, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 45);
            this.label2.TabIndex = 0;
            this.label2.Text = "Информация";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab_links
            // 
            this.tab_links.Controls.Add(this.btn_save_links);
            this.tab_links.Controls.Add(this.trv_links);
            this.tab_links.Location = new System.Drawing.Point(8, 39);
            this.tab_links.Name = "tab_links";
            this.tab_links.Padding = new System.Windows.Forms.Padding(3);
            this.tab_links.Size = new System.Drawing.Size(1226, 611);
            this.tab_links.TabIndex = 5;
            this.tab_links.Text = "Ссылки";
            this.tab_links.UseVisualStyleBackColor = true;
            // 
            // btn_save_links
            // 
            this.btn_save_links.Enabled = false;
            this.btn_save_links.Location = new System.Drawing.Point(11, 11);
            this.btn_save_links.Margin = new System.Windows.Forms.Padding(8);
            this.btn_save_links.Name = "btn_save_links";
            this.btn_save_links.Size = new System.Drawing.Size(150, 44);
            this.btn_save_links.TabIndex = 4;
            this.btn_save_links.Text = "Сохранить";
            this.btn_save_links.UseVisualStyleBackColor = true;
            this.btn_save_links.Click += new System.EventHandler(this.BtnSaveLinks_Click);
            // 
            // trv_links
            // 
            this.trv_links.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trv_links.ContextMenuStrip = this.ctx_menu;
            this.trv_links.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trv_links.FullRowSelect = true;
            this.trv_links.Location = new System.Drawing.Point(11, 67);
            this.trv_links.Margin = new System.Windows.Forms.Padding(8);
            this.trv_links.Name = "trv_links";
            this.trv_links.ShowLines = false;
            this.trv_links.Size = new System.Drawing.Size(1204, 533);
            this.trv_links.TabIndex = 5;
            this.trv_links.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvLinks_NodeMouseClick);
            // 
            // ctx_menu
            // 
            this.ctx_menu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctx_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctx_edit_link,
            this.ctx_copy_address,
            this.ctx_show_code,
            this.ctx_open_in_browser});
            this.ctx_menu.Name = "contextMenuStrip1";
            this.ctx_menu.Size = new System.Drawing.Size(350, 148);
            this.ctx_menu.Opening += new System.ComponentModel.CancelEventHandler(this.CtxMenu_Opening);
            // 
            // ctx_edit_link
            // 
            this.ctx_edit_link.Name = "ctx_edit_link";
            this.ctx_edit_link.Size = new System.Drawing.Size(349, 36);
            this.ctx_edit_link.Text = "Редактировать";
            this.ctx_edit_link.Click += new System.EventHandler(this.CtxEditLink_Click);
            // 
            // ctx_copy_address
            // 
            this.ctx_copy_address.Name = "ctx_copy_address";
            this.ctx_copy_address.Size = new System.Drawing.Size(349, 36);
            this.ctx_copy_address.Text = "Копировать адрес";
            this.ctx_copy_address.Click += new System.EventHandler(this.CtxCopyAddress_Click);
            // 
            // ctx_show_code
            // 
            this.ctx_show_code.Name = "ctx_show_code";
            this.ctx_show_code.Size = new System.Drawing.Size(349, 36);
            this.ctx_show_code.Text = "Показать код страницы";
            this.ctx_show_code.Click += new System.EventHandler(this.CtxShowCode_Click);
            // 
            // ctx_open_in_browser
            // 
            this.ctx_open_in_browser.Name = "ctx_open_in_browser";
            this.ctx_open_in_browser.Size = new System.Drawing.Size(349, 36);
            this.ctx_open_in_browser.Text = "Открыть в браузере";
            this.ctx_open_in_browser.Click += new System.EventHandler(this.CtxOpenInBrowser_Click);
            // 
            // tab_words
            // 
            this.tab_words.Controls.Add(this.trv_words);
            this.tab_words.Location = new System.Drawing.Point(8, 39);
            this.tab_words.Name = "tab_words";
            this.tab_words.Padding = new System.Windows.Forms.Padding(3);
            this.tab_words.Size = new System.Drawing.Size(1226, 611);
            this.tab_words.TabIndex = 7;
            this.tab_words.Text = "Словарь";
            this.tab_words.UseVisualStyleBackColor = true;
            // 
            // trv_words
            // 
            this.trv_words.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trv_words.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trv_words.FullRowSelect = true;
            this.trv_words.Location = new System.Drawing.Point(11, 11);
            this.trv_words.Margin = new System.Windows.Forms.Padding(8);
            this.trv_words.Name = "trv_words";
            this.trv_words.ShowLines = false;
            this.trv_words.Size = new System.Drawing.Size(1204, 589);
            this.trv_words.TabIndex = 0;
            // 
            // tab_tags
            // 
            this.tab_tags.Controls.Add(this.trv_tags);
            this.tab_tags.Location = new System.Drawing.Point(8, 39);
            this.tab_tags.Name = "tab_tags";
            this.tab_tags.Padding = new System.Windows.Forms.Padding(3);
            this.tab_tags.Size = new System.Drawing.Size(1226, 611);
            this.tab_tags.TabIndex = 8;
            this.tab_tags.Text = "Теги";
            this.tab_tags.UseVisualStyleBackColor = true;
            // 
            // trv_tags
            // 
            this.trv_tags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trv_tags.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trv_tags.FullRowSelect = true;
            this.trv_tags.Location = new System.Drawing.Point(11, 11);
            this.trv_tags.Margin = new System.Windows.Forms.Padding(8);
            this.trv_tags.Name = "trv_tags";
            this.trv_tags.ShowLines = false;
            this.trv_tags.Size = new System.Drawing.Size(1204, 589);
            this.trv_tags.TabIndex = 0;
            // 
            // tab_position
            // 
            this.tab_position.Controls.Add(this.lbx_keywords);
            this.tab_position.Controls.Add(this.label15);
            this.tab_position.Controls.Add(this.dgv_results);
            this.tab_position.Location = new System.Drawing.Point(8, 39);
            this.tab_position.Name = "tab_position";
            this.tab_position.Padding = new System.Windows.Forms.Padding(3);
            this.tab_position.Size = new System.Drawing.Size(1226, 611);
            this.tab_position.TabIndex = 9;
            this.tab_position.Text = "Позиция";
            this.tab_position.UseVisualStyleBackColor = true;
            // 
            // lbx_keywords
            // 
            this.lbx_keywords.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbx_keywords.FormattingEnabled = true;
            this.lbx_keywords.ItemHeight = 28;
            this.lbx_keywords.Location = new System.Drawing.Point(19, 72);
            this.lbx_keywords.Margin = new System.Windows.Forms.Padding(8);
            this.lbx_keywords.MultiColumn = true;
            this.lbx_keywords.Name = "lbx_keywords";
            this.lbx_keywords.Size = new System.Drawing.Size(242, 508);
            this.lbx_keywords.TabIndex = 0;
            this.lbx_keywords.Click += new System.EventHandler(this.LbxKeywords_Click);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(11, 11);
            this.label15.Margin = new System.Windows.Forms.Padding(8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(250, 45);
            this.label15.TabIndex = 4;
            this.label15.Text = "Запросы";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgv_results
            // 
            this.dgv_results.AllowUserToAddRows = false;
            this.dgv_results.AllowUserToDeleteRows = false;
            this.dgv_results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_results.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_results.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_results.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.position,
            this.query});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_results.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_results.Location = new System.Drawing.Point(285, 11);
            this.dgv_results.Margin = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this.dgv_results.MultiSelect = false;
            this.dgv_results.Name = "dgv_results";
            this.dgv_results.ReadOnly = true;
            this.dgv_results.RowHeadersVisible = false;
            this.dgv_results.RowTemplate.Height = 33;
            this.dgv_results.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_results.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_results.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_results.Size = new System.Drawing.Size(933, 592);
            this.dgv_results.TabIndex = 2;
            // 
            // position
            // 
            this.position.DataPropertyName = "Key";
            this.position.HeaderText = "#";
            this.position.Name = "position";
            this.position.ReadOnly = true;
            this.position.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.position.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.position.Width = 40;
            // 
            // query
            // 
            this.query.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.query.DataPropertyName = "Value";
            this.query.HeaderText = "Google Search Results";
            this.query.Name = "query";
            this.query.ReadOnly = true;
            this.query.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tab_crawler
            // 
            this.tab_crawler.Controls.Add(this.dgv_links);
            this.tab_crawler.Location = new System.Drawing.Point(8, 39);
            this.tab_crawler.Name = "tab_crawler";
            this.tab_crawler.Size = new System.Drawing.Size(1226, 611);
            this.tab_crawler.TabIndex = 10;
            this.tab_crawler.Text = "Краулер";
            this.tab_crawler.UseVisualStyleBackColor = true;
            // 
            // dgv_links
            // 
            this.dgv_links.AllowUserToAddRows = false;
            this.dgv_links.AllowUserToDeleteRows = false;
            this.dgv_links.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_links.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_links.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_links.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_links.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_links.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_links.Location = new System.Drawing.Point(0, 0);
            this.dgv_links.Margin = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this.dgv_links.MultiSelect = false;
            this.dgv_links.Name = "dgv_links";
            this.dgv_links.ReadOnly = true;
            this.dgv_links.RowHeadersVisible = false;
            this.dgv_links.RowTemplate.Height = 33;
            this.dgv_links.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_links.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_links.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_links.Size = new System.Drawing.Size(1226, 611);
            this.dgv_links.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn1.FillWeight = 60F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Address";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Inbound";
            this.dataGridViewTextBoxColumn3.FillWeight = 20F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Inbound";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Outbound";
            this.dataGridViewTextBoxColumn4.FillWeight = 20F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Outbound";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // menu_strip
            // 
            this.menu_strip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menu_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.menu_parameters,
            this.menu_settings,
            this.menu_about});
            this.menu_strip.Location = new System.Drawing.Point(0, 0);
            this.menu_strip.Name = "menu_strip";
            this.menu_strip.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menu_strip.Size = new System.Drawing.Size(1274, 44);
            this.menu_strip.TabIndex = 5;
            this.menu_strip.Text = "menuStrip1";
            // 
            // menu_file
            // 
            this.menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_open});
            this.menu_file.Name = "menu_file";
            this.menu_file.Size = new System.Drawing.Size(83, 36);
            this.menu_file.Text = "Файл";
            // 
            // menu_open
            // 
            this.menu_open.Name = "menu_open";
            this.menu_open.Size = new System.Drawing.Size(207, 38);
            this.menu_open.Text = "Открыть";
            this.menu_open.Click += new System.EventHandler(this.MenuOpenFile_Click);
            // 
            // menu_parameters
            // 
            this.menu_parameters.Name = "menu_parameters";
            this.menu_parameters.Size = new System.Drawing.Size(153, 36);
            this.menu_parameters.Text = "Параметры";
            this.menu_parameters.Click += new System.EventHandler(this.MenuParameters_Click);
            // 
            // menu_settings
            // 
            this.menu_settings.Name = "menu_settings";
            this.menu_settings.Size = new System.Drawing.Size(145, 36);
            this.menu_settings.Text = "Настройки";
            this.menu_settings.Click += new System.EventHandler(this.MenuSettings_Click);
            // 
            // menu_about
            // 
            this.menu_about.Name = "menu_about";
            this.menu_about.Size = new System.Drawing.Size(176, 36);
            this.menu_about.Text = "О программе";
            this.menu_about.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // open_file_dialog
            // 
            this.open_file_dialog.Filter = "Веб-страницы|*.html|Все файлы|*.*";
            // 
            // cbx_url
            // 
            this.cbx_url.FormattingEnabled = true;
            this.cbx_url.Location = new System.Drawing.Point(17, 52);
            this.cbx_url.Margin = new System.Windows.Forms.Padding(8, 8, 0, 8);
            this.cbx_url.Name = "cbx_url";
            this.cbx_url.Size = new System.Drawing.Size(550, 33);
            this.cbx_url.TabIndex = 0;
            this.cbx_url.SelectedIndexChanged += new System.EventHandler(this.CbxUrl_SelectedIndexChanged);
            this.cbx_url.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CbxUrl_KeyDown);
            // 
            // cbx_keywords
            // 
            this.cbx_keywords.FormattingEnabled = true;
            this.cbx_keywords.Location = new System.Drawing.Point(567, 52);
            this.cbx_keywords.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.cbx_keywords.Name = "cbx_keywords";
            this.cbx_keywords.Size = new System.Drawing.Size(250, 33);
            this.cbx_keywords.TabIndex = 1;
            this.cbx_keywords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CbxKeywords_KeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 774);
            this.Controls.Add(this.cbx_keywords);
            this.Controls.Add(this.menu_strip);
            this.Controls.Add(this.cbx_url);
            this.Controls.Add(this.tab_control);
            this.Controls.Add(this.btn_start);
            this.MainMenuStrip = this.menu_strip;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(1300, 800);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seo Project";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tab_control.ResumeLayout(false);
            this.tab_audit.ResumeLayout(false);
            this.tab_audit.PerformLayout();
            this.tab_links.ResumeLayout(false);
            this.ctx_menu.ResumeLayout(false);
            this.tab_words.ResumeLayout(false);
            this.tab_tags.ResumeLayout(false);
            this.tab_position.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_results)).EndInit();
            this.tab_crawler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_links)).EndInit();
            this.menu_strip.ResumeLayout(false);
            this.menu_strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_start;

        private System.Windows.Forms.TabControl tab_control;

        private System.Windows.Forms.ComboBox cbx_url;
        private System.Windows.Forms.MenuStrip menu_strip;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_open;
        private System.Windows.Forms.OpenFileDialog open_file_dialog;
        private System.Windows.Forms.ContextMenuStrip ctx_menu;
        private System.Windows.Forms.ToolStripMenuItem ctx_copy_address;
        private System.Windows.Forms.ToolStripMenuItem ctx_open_in_browser;
       
        private System.Windows.Forms.ToolStripMenuItem menu_about;
        private System.Windows.Forms.TabPage tab_links;
        private System.Windows.Forms.TreeView trv_links;
        private System.Windows.Forms.TabPage tab_audit;
        private System.Windows.Forms.ToolStripMenuItem menu_parameters;
        private System.Windows.Forms.ToolStripMenuItem ctx_edit_link;
        private System.Windows.Forms.Button btn_save_links;
        private System.Windows.Forms.ToolStripMenuItem ctx_show_code;
        private System.Windows.Forms.TabPage tab_words;
        private System.Windows.Forms.TreeView trv_words;
        private System.Windows.Forms.TabPage tab_tags;
        private System.Windows.Forms.TreeView trv_tags;
        private System.Windows.Forms.FolderBrowserDialog folder_browser_dialog;
        private System.Windows.Forms.ComboBox cbx_keywords;
        private System.Windows.Forms.TabPage tab_position;
        private System.Windows.Forms.DataGridView dgv_results;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn position;
        private System.Windows.Forms.DataGridViewTextBoxColumn query;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbx_keywords;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbx_advice;
        private System.Windows.Forms.TreeView trv_problems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbx_summary;
        private System.Windows.Forms.Label label_score;
        private System.Windows.Forms.TabPage tab_crawler;
        private System.Windows.Forms.DataGridView dgv_links;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ToolStripMenuItem menu_settings;
    }
}