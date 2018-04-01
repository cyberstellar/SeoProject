using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Test
{
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            var links = MainForm.Links;
            var linksToSave = MainForm.LinksToSave;
            var level = MainForm.Level;
            var index = MainForm.Index;

            var address = links[index].Address;
            var anchor = links[index].Anchor;

            if (!cbx_autoload.Checked)
            {
                if (address == tbx_address.Text)
                {
                    if (anchor == tbx_anchor.Text)
                    {
                        DialogResult = DialogResult.Cancel;
                        return;
                    }
                }
                else if (anchor == tbx_anchor.Text)
                {
                    if (address == tbx_address.Text)
                    {
                        DialogResult = DialogResult.Cancel;
                        return;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(tbx_address.Text))
            {
                MessageBox.Show(@"Пустой адрес", @"Ошибка");
                return;
            }

            var url = MainForm.GetFixedUrl(tbx_address.Text);
            if (MainForm.IsPageExists(url))
            {
                tbx_address.Text = url;
                tbx_status.Text = MainForm.GetStatusCode(url);

                if (cbx_autoload.Checked)
                    tbx_anchor.Text = GetPageTitle(url);
            }
            else
            {
                MessageBox.Show(@"Введенный адрес не существует или истекло время ожидания", @"Ошибка");
                return;
            }

            int i;
            switch (level)
            {
                case 0:
                    var linksFiltered = links.Where(x => x.Address == address);
                    foreach (var link in linksFiltered)
                    {
                        i = links.Where(x => x.Address == address)
                            .ToList().FindIndex(x => x == link);

                        link.Address = tbx_address.Text;
                        link.Anchor = tbx_anchor.Text;
                        link.Status = tbx_status.Text;

                        linksToSave.Add(link);
                        linksToSave.Last().Inbound = i;
                    }

                    break;
                case 1:
                    linksFiltered = links.Where(x => x.Address == address &&
                                             x.Anchor == anchor && x.Location == links[index].Location);
                    foreach (var link in linksFiltered)
                    {
                        i = links.Where(x => x.Address == address)
                            .ToList().FindIndex(x => x == link);

                        link.Address = tbx_address.Text;
                        link.Anchor = tbx_anchor.Text;
                        link.Status = tbx_status.Text;

                        linksToSave.Add(link);
                        linksToSave.Last().Inbound = i;
                    }

                    break;
                case 2:
                    i = links.Where(x => x.Address == address)
                        .ToList().FindIndex(x => x == links[index]);

                    links[index].Address = tbx_address.Text;
                    links[index].Anchor = tbx_anchor.Text;
                    links[index].Status = tbx_status.Text;

                    linksToSave.Add(links[index]);
                    linksToSave.Last().Inbound = i;
                    break;
            }

            linksToSave = linksToSave.GroupBy(x => new {x.Address, x.Anchor, x.Inbound})
                .Select(g => g.First()).ToList();

            DialogResult = DialogResult.OK;
        }

        private void CbxAutoload_CheckedChanged(object sender, EventArgs e)
        {
            tbx_anchor.ReadOnly = ((CheckBox) sender).Checked;
        }

        private static string GetPageTitle(string url)
        {
            var title = string.Empty;
            var content = MainForm.GetPageContent(url);

            var document = new HtmlDocument();
            document.LoadHtml(content);

            var node = document.DocumentNode.SelectSingleNode(".//title");
            if (node != null) title = Regex.Replace(node.InnerText, @"\s+", " ").Trim();

            return title;
        }
    }
}