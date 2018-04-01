using System;
using System.Windows.Forms;

namespace Test
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            num_timeout.Value = MainForm.Timeout;
            num_crawl_timeout.Value = MainForm.CrawlTimeout;
            num_crawl_limit.Value = MainForm.CrawlLimit;
            num_crawl_depth.Value = MainForm.CrawlDepth;
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            var config = MainForm.Config;

            if (num_timeout.Value != MainForm.Timeout)
                config.Write("Settings", "timeout", num_timeout.Value + "");

            if (num_crawl_timeout.Value != MainForm.CrawlTimeout)
                config.Write("Settings", "crawl_timeout", num_crawl_timeout.Value + "");

            if (num_crawl_limit.Value != MainForm.CrawlLimit)
                config.Write("Settings", "crawl_limit", num_crawl_limit.Value + "");

            if (num_crawl_depth.Value != MainForm.CrawlDepth)
                config.Write("Settings", "crawl_depth", num_crawl_depth.Value + "");

            DialogResult = DialogResult.OK;
        }
    }
}