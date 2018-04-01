using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using HtmlAgilityPack;
using LemmaSharp;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Test
{
    public partial class MainForm : Form
    {
        internal static List<Link> Links;
        internal static List<Link> LinksToSave;

        public static Factors Factors = new Factors();

        internal static int Index;
        internal static int Level;

        internal static readonly Ini Config = new Ini("config.ini");
        internal static int Timeout = 5000;
        internal static int CrawlTimeout = 3000;
        internal static int CrawlLimit = 500;
        internal static int CrawlDepth = 5;

        private readonly HashSet<KeywordsToUrl> _keywordsToUrls = new HashSet<KeywordsToUrl>();
        private readonly Factors _metrics = new Factors();
        private readonly List<QueriesToKeyword> _queriesToKeywords = new List<QueriesToKeyword>();
        private bool _isCancelRequired;

        private bool _isChangesSaved = true;
        private List<string> _keywordsToUse;
        private bool _metricsReceived;

        private List<Problem> _problems;
        private List<object> _summary;

        private List<string> _trvLinksState;

        public MainForm()
        {
            InitializeComponent();

            // ComboBox
            cbx_url.DataSource = GetRecent();
            cbx_url.SelectedIndex = -1;

            // DataGridView
            dgv_links.DoubleBuffered(true);
            dgv_results.DoubleBuffered(true);

            // TabControl
            tab_control.TabPages.Remove(tab_crawler);

            // Settings
            if (Config.KeyExists("timeout", "Settings"))
                int.TryParse(Config.Read("Settings", "timeout"), out Timeout);

            if (Config.KeyExists("crawl_timeout", "Settings"))
                int.TryParse(Config.Read("Settings", "crawl_timeout"), out CrawlTimeout);

            if (Config.KeyExists("crawl_limit", "Settings"))
                int.TryParse(Config.Read("Settings", "crawl_limit"), out CrawlLimit);

            if (Config.KeyExists("crawl_depth", "Settings"))
                int.TryParse(Config.Read("Settings", "crawl_depth"), out CrawlDepth);
        }

        #region События

        private async void BtnStart_ClickAsync(object sender, EventArgs e)
        {
            var url = cbx_url.Text;
            var content = string.Empty;

            switch (IsFile(url))
            {
                case 0:
                    MessageBox.Show(@"Неверный формат адреса", @"Ошибка");
                    return;
                case 1:
                    content = await Task.Factory.StartNew(() => GetFileContent(url));
                    break;
                case 2:
                    if (!IsPageExists(url = GetFixedUrl(url)))
                    {
                        MessageBox.Show(@"Введенный адрес не существует или истекло время ожидания", @"Ошибка");
                        return;
                    }

                    cbx_url.Text = url;
                    content = await Task.Factory.StartNew(() => GetPageContent(url));
                    break;
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show(@"Содержимое страницы пусто", @"Ошибка");
                return;
            }

            // Заполнение ComboBox
            WriteRecent(url);
            var recent = GetRecent();
            var index = recent.FindIndex(x => x == url);
            cbx_url.DataSource = recent;
            cbx_url.SelectedIndex = index;

            var words = new List<Word>();
            var tags = new List<Tag>();

            _summary = new List<object>();
            LinksToSave = new List<Link>();
            _isChangesSaved = true;

            var stopwatch = Stopwatch.StartNew();

            var cts = new CancellationTokenSource();
            var token = cts.Token;
            if (_isCancelRequired)
            {
                cts.Cancel();
                _isCancelRequired = false;
            }


            label_score.Text = @"...";
            tbx_advice.Text = @"Идет анализ...";
            lbx_summary.DataSource = null;
            trv_problems.Nodes.Clear();

            lbx_keywords.Items.Clear();
            dgv_results.DataSource = null;
            tab_control.TabPages.Remove(tab_crawler);

            btn_start.Enabled = false;
            btn_save_links.Enabled = false;
            cbx_url.Enabled = false;
            cbx_keywords.Enabled = false;
            menu_file.Enabled = false;
            menu_settings.Enabled = false;
            menu_parameters.Enabled = false;
            ctx_menu.Items["ctx_edit_link"].Enabled = false;


            await Task.Factory.StartNew(() =>
                Parallel.Invoke(
                    () =>
                    {
                        Links = GetLinksFromContent(url, content);
                        Links = Links.OrderBy(x => x.Address)
                            .ThenBy(x => x.Location)
                            .ThenBy(x => x.Anchor)
                            .ToList();

                        _summary.Insert(0, $"Исходящих ссылок: {Links.Count}");

                        Invoke((MethodInvoker) delegate
                        {
                            tab_links.Text = $@"Ссылки ({Links.Count})";
                            _trvLinksState = new List<string>();
                            trv_links.TreeViewNodeSorter = new NodeSorter();
                            BuildLinksTree(Links, true);
                        });
                    },
                    () =>
                    {
                        words = GetWordsFromContent(content);
                        words = words.OrderBy(x => x.Count).ToList();

                        Invoke((MethodInvoker) delegate
                        {
                            tab_words.Text = $@"Словарь ({words.Count})";
                            BuildWordsTree(words);
                        });
                    },
                    () =>
                    {
                        tags = GetTagsFromContent(content);

                        Invoke((MethodInvoker) delegate { BuildTagsTree(tags); });
                    }
                ), token);


            // Заполнение ListBox
            var current = _keywordsToUrls.Where(x => x.Url == url).ToList();
            if (current.Count != 0)
            {
                _keywordsToUse = new List<string>();

                switch (cbx_keywords.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        _keywordsToUse = current.First().Keywords.ToList()
                            .GetRange(2, current.First().Keywords.Count - 2);
                        break;
                    default:
                        if (cbx_keywords.SelectedIndex != -1)
                            _keywordsToUse.Add(current.First().Keywords
                                .ToList()[cbx_keywords.SelectedIndex]);
                        break;
                }

                await Task.Factory.StartNew(() => GetQueriesToKeywordsPairs(_keywordsToUse), token);

                var worst = 0;
                var best = 101;
                foreach (var keyword in _keywordsToUse)
                {
                    var str = "#0";
                    try
                    {
                        var pos = _queriesToKeywords
                            .FirstOrDefault(x => x.Keyword == keyword).Queries
                            .FirstOrDefault(x => x.Address
                                .Contains(new Uri(url).Host)).Position;

                        if (pos > worst)
                            worst = pos;

                        if (pos < best)
                            best = pos;

                        str = $"#{pos}";
                    }
                    catch
                    {
                        // ignored
                    }

                    lbx_keywords.Items.Add($"{keyword.ReduceIfTooLong(9),-12} {str}");
                }

                if (best != 101)
                    _summary.Add($"Лучшая позиция: #{best}");
                if (worst != 0)
                    _summary.Add($"Худшая позиция: #{worst}");
            }

            var canUpdate = false;
            await Task.Factory.StartNew(() =>
                Parallel.Invoke(
                    () =>
                    {
                        // Получение статуса ссылок
                        GetLinksStatus(Links.GroupBy(x => x.Address)
                            .Select(g => g.First()).ToList());

                        foreach (var link in Links.GroupBy(x => x.Address).SelectMany(g => g))
                            link.Status = Links.First(x => x.Address == link.Address).Status;

                        // Вычисление оценки сайта
                        var score = CalculateScore(Links, words, tags, _keywordsToUse, content);
                        var advice = AdviceBuilder(_problems);
                        canUpdate = true;
                        stopwatch.Stop();

                        Invoke((MethodInvoker) delegate
                        {
                            label_score.Text = $@"{score}/100";
                            tbx_advice.Text = advice + $@"Этот анализ занял {stopwatch.ElapsedMilliseconds} ms";

                            BuildProblemsTree(_problems);
                            ShowSummary();

                            if (score >= 0 && score < 30)
                                label_score.ForeColor = Color.Red;
                            else if (score >= 30 && score < 60)
                                label_score.ForeColor = Color.Orange;
                            else if (score >= 60 && score < 90)
                                label_score.ForeColor = Color.LightGreen;
                            else if (score >= 90)
                                label_score.ForeColor = Color.Green;

                            btn_start.Enabled = true;
                            cbx_url.Enabled = true;
                            cbx_keywords.Enabled = true;
                            menu_file.Enabled = true;
                            menu_settings.Enabled = true;
                            menu_parameters.Enabled = true;
                            ctx_menu.Items["ctx_edit_link"].Enabled = true;
                        });

                        SystemSounds.Beep.Play();
                    },
                    () =>
                    {
                        if (cts.IsCancellationRequested)
                            return;

                        _isCancelRequired = true;

                        var megaLinks = new List<Link>(Links);

                        var sw = Stopwatch.StartNew();
                        GetLinksFromCrawler(megaLinks, url, CrawlDepth);
                        sw.Stop();

                        if (Config.Read("Debug", "show_info") == "true")
                        {
                            var megaLinksCopy = megaLinks;
                            Task.Run(() => MessageBox.Show(
                                $@"Crawl Depth:\t{CrawlDepth}\r\n" +
                                $@"Crawl Limit:\t{CrawlLimit}\r\n" +
                                $@"Crawl Timeout:\t{CrawlTimeout}\r\n" +
                                $@"Elapsed Time:\t{sw.ElapsedMilliseconds}\r\n" +
                                $@"Founded Links:\t{megaLinksCopy.Count}",
                                @"Debug Info",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information), token);
                        }

                        try
                        {
                            var megaLinksCopy = megaLinks;
                            megaLinks = megaLinks
                                .GroupBy(x => x.Address)
                                .SelectMany(g => g)
                                .Select(x => new Link
                                {
                                    Address = x.Address,
                                    Location = x.Location,
                                    Inbound = megaLinksCopy.Count(y => y.Address == x.Address),
                                    Outbound = megaLinksCopy.Count(y => y.Location == x.Location)
                                })
                                .GroupBy(x => x.Address)
                                .Select(g => g.First())
                                .OrderByDescending(x => x.Inbound)
                                .ThenByDescending(x => x.Outbound)
                                .ThenBy(x => x.Address)
                                .ToList();
                        }
                        catch
                        {
                            MessageBox.Show(
                                @"Возникла проблема при добавлении списка ссылок.\r\nПовторите попытку.",
                                @"Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }

                        Invoke((MethodInvoker) delegate
                        {
                            if (megaLinks.FirstOrDefault(x => x.Address == url) != null)
                            {
                                _summary.Insert(0,
                                    $"Входящих ссылок: {megaLinks.First(x => x.Address == url).Inbound}");
                                if (canUpdate)
                                    ShowSummary();
                            }
                            else
                            {
                                _summary.Insert(0, "! Нет входящих ссылок !");
                            }

                            tab_control.TabPages.Add(tab_crawler);
                            tab_crawler.Text = $@"Краулер ({megaLinks.Select(x => x.Inbound).Sum()})";
                            dgv_links.DataSource =
                                megaLinks.Select(x => new {x.Address, x.Inbound, x.Outbound}).ToList();
                        });

                        _isCancelRequired = false;

                        SystemSounds.Hand.Play();
                    }
                ), token);
        }

        private void BtnSaveLinks_Click(object sender, EventArgs e)
        {
            try
            {
                if (LinksToSave == null || LinksToSave.Count == 0) return;
                if (folder_browser_dialog.ShowDialog() != DialogResult.OK) return;
                var path = folder_browser_dialog.SelectedPath;

                foreach (var link in LinksToSave.GroupBy(x => x.Location).Select(x => x.First()))
                {
                    var isFile = IsFile(link.Location) == 1;
                    var content = isFile ? GetFileContent(link.Location) : GetPageContent(link.Location);

                    content = Regex.Replace(content, @"[ ]{2,}", @" ");

                    var document = new HtmlDocument();
                    document.LoadHtml(content);

                    foreach (var link1 in LinksToSave.Where(x => x.Location == link.Location))
                    {
                        var address = new Uri(link1.Address).AbsoluteUri
                            .Contains($"{new Uri(link1.Location).Scheme}://{new Uri(link1.Location).Host}")
                            ? new Uri(link1.Address).AbsolutePath
                            : new Uri(link1.Address).AbsoluteUri;

                        var nodes = document.DocumentNode.SelectNodes(".//a[@href]");

                        if (nodes == null)
                            break;

                        try
                        {
                            var node = nodes.Where(x => x.GetAttributeValue("href", "") == link1.Original)
                                .ToList()[link1.Inbound];

                            var oldText = node.OuterHtml;

                            node.InnerHtml = !string.IsNullOrWhiteSpace(node.InnerHtml)
                                ? node.InnerHtml.Replace(node.InnerText, link1.Anchor)
                                : link1.Anchor;

                            node.Attributes["href"].Value = address;

                            var newText = node.OuterHtml;
                            content = content.Replace(oldText, newText);
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    if (isFile)
                    {
                        File.WriteAllText(link.Location, content, Encoding.UTF8);
                    }
                    else
                    {
                        var location = new Uri(link.Location);

                        var name = location.Segments.Last().Contains('.')
                            ? location.Segments.Last()
                            : "index.html";
                        var folders = location.Segments.Last().Contains('.')
                            ? location.AbsoluteUri.Substring(0, location.AbsoluteUri.LastIndexOf('/'))
                            : location.AbsoluteUri;
                        folders = folders.Replace($"{location.Scheme}://", "").Replace("/", "\\");

                        Directory.CreateDirectory($"{path}\\{folders}");
                        File.WriteAllText($"{path}\\{folders}\\{name}", content, Encoding.UTF8);
                    }
                }

                foreach (var node in trv_links.Nodes.Cast<TreeNode>()
                    .Where(x => x.Text.StartsWith("● ")))
                    node.Text = node.Text.Replace("● ", "");

                tab_links.Text = tab_links.Text.Replace("● ", "");

                _isChangesSaved = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        // Строит TreeView для заданного списка
        private void BuildLinksTree(List<Link> links, bool clear = false)
        {
            trv_links.BeginUpdate();

            if (clear)
                trv_links.Nodes.Clear();

            links = links.GroupBy(x => x.Address).SelectMany(x => x).ToList();

            var linksUnited = links.GroupBy(x => new {x.Address, x.Location, x.Anchor})
                .Select(g => new Link
                {
                    Address = g.Key.Address,
                    Location = g.Key.Location,
                    Anchor = g.Key.Anchor,
                    Inbound = g.Count()
                })
                .GroupBy(x => x.Address)
                .SelectMany(g => g)
                .ToList();

            var linksUnique = links.GroupBy(x => x.Address)
                .Select(g => new Link
                {
                    Address = g.Key,
                    Status = g.Select(x => x.Status).First(),
                    Inbound = g.Count()
                })
                .ToList();

            foreach (var link in linksUnique)
            {
                var node = new TreeNode();

                var linksUnitedFiltered = linksUnited.Where(x => x.Address == link.Address).ToList();
                var anchor = linksUnitedFiltered.GroupBy(x => x.Anchor).First().Count() != linksUnitedFiltered.Count
                    ? "..."
                    : linksUnitedFiltered.GroupBy(x => x.Anchor).Select(g => g.Key).First();

                foreach (var link1 in linksUnitedFiltered)
                {
                    var node1 = new TreeNode();
                    var text =
                        $"LOCATION = {link1.Location.ReduceIfTooLong(),-50} ANCHOR = {link1.Anchor.ReduceIfTooLong()}";

                    if (link1.Inbound > 1)
                    {
                        text = $"{"(" + link1.Inbound + ")",-4} " + text;

                        var linksOrderedFiltered = links.Where(x => x.Address == link1.Address
                                                                    && x.Anchor == link1.Anchor &&
                                                                    x.Location == link1.Location);

                        foreach (var link2 in linksOrderedFiltered)
                        {
                            var key = $"{link2.Address}#{link2.Location}#{link2.Anchor}#_";
                            node1.Nodes.Add(key, $"ANCHOR = {link2.Anchor.ReduceIfTooLong()}");
                        }
                    }

                    node1.Text = text;
                    node1.Name = $"{link1.Address}#{link1.Location}#{link1.Anchor}";
                    node.Nodes.Add(node1);
                }

                node.Text =
                    $@"{"(" + link.Inbound + ")",-4} {link.Address.ReduceIfTooLong(46),-50} ANCHOR = {anchor.ReduceIfTooLong(36),-40} STATUS = {link.Status}";
                node.Name = link.Address;
                trv_links.Nodes.Add(node);
            }

            trv_links.Sort();
            trv_links.Nodes.SetExpansionState(_trvLinksState);
            trv_links.EndUpdate();
        }

        private void RebuildLinksTree()
        {
            _trvLinksState = trv_links.Nodes.GetExpansionState();

            var inOldOrder = Links[Index];

            Links = Links.OrderBy(x => x.Address)
                .ThenBy(x => x.Location)
                .ThenBy(x => x.Anchor)
                .ToList();

            // Сначала удалить выбранный узел
            switch (Level)
            {
                case 0:
                    trv_links.SelectedNode.Remove();
                    break;
                case 1:
                    if (trv_links.SelectedNode.Parent.Nodes.Count == 1)
                        trv_links.SelectedNode.Parent.Remove();
                    else
                        trv_links.SelectedNode.Remove();
                    break;
                case 2:
                    if (trv_links.SelectedNode.Parent.Nodes.Count == 1)
                        if (trv_links.SelectedNode.Parent.Parent.Nodes.Count == 1)
                            trv_links.SelectedNode.Parent.Parent.Remove();
                        else
                            trv_links.SelectedNode.Parent.Remove();
                    else
                        trv_links.SelectedNode.Remove();
                    break;
            }

            // Затем удалить узлы с таким же адресом как у выбранного
            var nodes = trv_links.Nodes
                .Cast<TreeNode>()
                .Where(x => x.Name == inOldOrder.Address)
                .ToArray();

            foreach (var node in nodes)
                node.Remove();

            // И построить узел заново
            BuildLinksTree(Links.Where(x => x.Address == inOldOrder.Address).ToList());

            SetLinkColorAndStatus(Links.First(x => x.Address == inOldOrder.Address));

            // Выделить и перейти к этому узлу
            trv_links.SelectedNode = trv_links.Nodes.Find(inOldOrder.Address, false).First();
            trv_links.SelectedNode.EnsureVisible();

            try
            {
                trv_links.TopNode = trv_links.Nodes[trv_links.SelectedNode.Index - 5];
            }
            catch
            {
                // ignored
            }

            // Косметические изменения для TreeView
            trv_links.SelectedNode.Text = $@"● {trv_links.SelectedNode.Text}";
        }

        private void BuildWordsTree(List<Word> words)
        {
            trv_words.BeginUpdate();
            trv_words.Nodes.Clear();

            ILemmatizer lemmatizer = new LemmatizerPrebuiltCompact(LanguagePrebuilt.Russian);

            var wordsUnited = words.GroupBy(x => lemmatizer.Lemmatize(x.Term))
                .Select(g => new Word
                {
                    Term = g.Key,
                    Count = g.Count(),
                    Density = Math.Round((decimal) g.Count() / words.Count() * 100, 2)
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            foreach (var word in wordsUnited)
            {
                var node = new TreeNode();

                var wordsUnitedFiltered =
                    words.Where(x => lemmatizer.Lemmatize(x.Term) == lemmatizer.Lemmatize(word.Term)).ToList();
                var text = words.Count(x => x.Term == word.Term) != wordsUnitedFiltered.Count
                    ? $"{lemmatizer.Lemmatize(word.Term).ReduceIfTooLong(26),-30} COUNT = {word.Count,-10} DENSITY = {word.Density}%"
                    : $"{word.Term.ReduceIfTooLong(26),-30} COUNT = {word.Count,-10} DENSITY = {word.Density}%";

                if (word.Count > 1)
                    foreach (var word1 in wordsUnitedFiltered)
                        node.Nodes.Add(word1.Term);

                node.Text = text;
                trv_words.Nodes.Add(node);
            }

            trv_words.EndUpdate();
        }

        private void BuildTagsTree(List<Tag> tags)
        {
            trv_tags.BeginUpdate();
            trv_tags.Nodes.Clear();

            var tagsUnique = tags.GroupBy(x => x.Name)
                .Select(g => new Tag
                {
                    Name = g.Key,
                    Content = g.Select(x => x.Content).First(),
                    Length = g.Select(x => x.Length).First(),
                    Count = g.Count()
                })
                .ToList();

            foreach (var tag in tagsUnique.Where(x => !x.Name.StartsWith("img") && !x.Name.StartsWith("hidden")))
            {
                var node = new TreeNode();
                var text = $"{tag.Name,-20} COUNT = {tag.Count,-10}";

                foreach (var tag1 in tags.Where(x => x.Name == tag.Name))
                    node.Nodes.Add($"{tag1.Name,-20} LENGTH = {tag1.Length,-10} CONTENT = {tag1.Content}");

                node.Text = text;
                trv_tags.Nodes.Add(node);
            }

            if (tags.Count(x => x.Name.StartsWith("img")) != 0)
            {
                var img = new TreeNode("img");
                foreach (var tag in tags.Where(x => x.Name.StartsWith("img")))
                {
                    var src = tag.Name.Substring(4, tag.Name.Length - 4);
                    img.Nodes.Add($"SRC = {src.ReduceIfTooLong(28),-34} ALT = {tag.Content}");
                }

                trv_tags.Nodes.Add(img);
            }

            if (tags.Count(x => x.Name.StartsWith("hidden")) != 0)
            {
                var hidden = new TreeNode("hidden");
                foreach (var tag in tags.Where(x => x.Name.StartsWith("hidden")))
                    hidden.Nodes.Add($"{tag.Name,-20} CONTENT = {tag.Content}");
                trv_tags.Nodes.Add(hidden);
            }

            trv_tags.ExpandAll();
            trv_tags.EndUpdate();
        }

        private void BuildProblemsTree(List<Problem> problems)
        {
            trv_problems.BeginUpdate();
            trv_problems.Nodes.Clear();

            var high = problems.Where(x =>
                x.Type == "Not Found" || x.Type == "Redirect" || x.Type == "Timeout" || x.Type == "Hidden Tag" ||
                x.Type == "No Keywords").ToList();
            var mid = problems.Where(x => x.Type == "Empty Anchor" || x.Type == "Empty Alt" ||
                                          x.Type == "High Academic Vomit" || x.Type == "Low Academic Vomit"
                                          || x.Type == "High Classic Vomit" || x.Type == "Low Classic Vomit" ||
                                          x.Type == "More Than One Tag").ToList();
            var low = problems.Where(x => x.Type == "Low Code Ratio" || x.Type == "Bad Length").ToList();

            if (high.Count != 0)
            {
                var node = new TreeNode("High Level");

                foreach (var h in high.GroupBy(x => x.Type).OrderBy(g => g.Key))
                    node.Nodes.Add($"{h.Key} ({h.Count()})");
                trv_problems.Nodes.Add(node);
            }

            if (mid.Count != 0)
            {
                var node = new TreeNode("Middle Level");
                foreach (var m in mid.GroupBy(x => x.Type).OrderBy(g => g.Key))
                    node.Nodes.Add($"{m.Key} ({m.Count()})");
                trv_problems.Nodes.Add(node);
            }

            if (low.Count != 0)
            {
                var node = new TreeNode("Low Level");
                foreach (var l in low.GroupBy(x => x.Type).OrderBy(g => g.Key))
                    node.Nodes.Add($"{l.Key} ({l.Count()})");
                trv_problems.Nodes.Add(node);
            }

            trv_problems.ExpandAll();
            trv_problems.EndUpdate();
        }

        private void ShowSummary()
        {
            lbx_summary.DataSource = null;
            lbx_summary.DataSource = _summary;
        }


        private void CbxUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            e.Handled = e.SuppressKeyPress = true;
            BtnStart_ClickAsync(sender, e);
        }

        private void CbxUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_keywordsToUrls == null) return;
            var current = _keywordsToUrls.Where(x => x.Url == cbx_url.Text).ToList();
            if (!current.Any())
            {
                _keywordsToUrls.Add(new KeywordsToUrl(cbx_url.Text));
                cbx_keywords.DataSource = current.First().Keywords.ToList();
            }
            else
            {
                cbx_keywords.DataSource = current.First().Keywords.ToList();
                cbx_keywords.SelectedIndex = current.First().Keywords.Count > 2 ? 1 : 0;
            }
        }

        private void CbxKeywords_KeyDown(object sender, KeyEventArgs e)
        {
            if (_keywordsToUrls == null) return;
            var current = _keywordsToUrls.Where(x => x.Url == cbx_url.Text).ToList();
            if (!current.Any()) return;
            {
                switch (e.KeyData)
                {
                    case Keys.Enter:
                        if (!string.IsNullOrWhiteSpace(cbx_keywords.Text)
                            && !string.IsNullOrWhiteSpace(cbx_url.Text))
                        {
                            e.Handled = e.SuppressKeyPress = true;
                            var keyword = Regex.Replace(Regex.Replace(cbx_keywords.Text, @"[^\w\s]+", " "), @"\s+",
                                " ").Trim();
                            current.First().Keywords.Add(keyword);
                            cbx_keywords.DataSource = current.First().Keywords.ToList();
                            cbx_keywords.SelectedIndex = 1;
                            cbx_keywords.SelectAll();
                        }

                        break;
                    case Keys.Right:
                        var index = current.First().Keywords.ToList().FindIndex(x => x == cbx_keywords.Text);
                        if (index != 0 && index != 1)
                        {
                            current.First().Keywords.Remove(cbx_keywords.Text);
                            cbx_keywords.DataSource = current.First().Keywords.ToList();
                            cbx_keywords.SelectAll();
                        }

                        break;
                }
            }
        }

        private void LbxKeywords_Click(object sender, EventArgs e)
        {
            if (lbx_keywords.SelectedItem == null) return;
            var keyword = _keywordsToUse[lbx_keywords.SelectedIndex];
            var rowsData = new Dictionary<int, string>();
            foreach (var query in _queriesToKeywords
                .FirstOrDefault(x => x.Keyword == keyword).Queries)
                rowsData.Add(query.Position,
                    $"{query.Title}\n\n" +
                    $"{query.Address}\n\n" +
                    $"{query.Snippet}");
            dgv_results.AutoGenerateColumns = false;
            dgv_results.DataSource = new BindingSource {DataSource = rowsData};
        }


        private void MenuOpenFile_Click(object sender, EventArgs e)
        {
            if (open_file_dialog.ShowDialog() == DialogResult.OK) cbx_url.Text = open_file_dialog.FileName;
        }

        private void MenuSettings_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            if (settings.ShowDialog() != DialogResult.OK) return;
            if (Config.KeyExists("timeout", "Settings"))
                int.TryParse(Config.Read("Settings", "timeout"), out Timeout);

            if (Config.KeyExists("crawl_timeout", "Settings"))
                int.TryParse(Config.Read("Settings", "crawl_timeout"), out CrawlTimeout);

            if (Config.KeyExists("crawl_limit", "Settings"))
                int.TryParse(Config.Read("Settings", "crawl_limit"), out CrawlLimit);

            if (Config.KeyExists("crawl_depth", "Settings"))
                int.TryParse(Config.Read("Settings", "crawl_depth"), out CrawlDepth);
        }

        private void MenuParameters_Click(object sender, EventArgs e)
        {
            var parameters = new Parameters();
            if (parameters.ShowDialog() != DialogResult.OK) return;
            if (!_metricsReceived) return;
            var score = _metrics.A * Factors.A
                        + _metrics.B * Factors.B
                        + _metrics.C * Factors.C
                        + _metrics.D * Factors.D
                        + _metrics.E * Factors.E
                        + _metrics.F * Factors.F
                        + _metrics.G * Factors.G
                        + _metrics.H * Factors.H
                        + _metrics.I * Factors.I
                        + _metrics.J * Factors.J
                        - _metrics.K * Factors.K
                        - _metrics.L * Factors.L;

            label_score.Text = $@"{Math.Round(score, 1)}/100";

            if (score >= 0 && score < 30)
                label_score.ForeColor = Color.Red;
            else if (score >= 30 && score < 60)
                label_score.ForeColor = Color.Orange;
            else if (score >= 60 && score < 90)
                label_score.ForeColor = Color.LightGreen;
            else if (score >= 90)
                label_score.ForeColor = Color.Green;
        }

        private void MenuAbout_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }


        private void TrvLinks_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            trv_links.SelectedNode = e.Node;
            Level = e.Node.Level;
            Index = 0;

            try
            {
                var stop = false;
                foreach (TreeNode node in trv_links.Nodes)
                {
                    if (node == e.Node || stop)
                        break;

                    foreach (TreeNode node1 in node.Nodes)
                    {
                        if (node1 == e.Node || stop)
                        {
                            stop = true;
                            break;
                        }

                        if (node1.Nodes.Count == 0)
                            Index += 1;
                        else
                            foreach (TreeNode node2 in node1.Nodes)
                            {
                                if (node2 == e.Node)
                                {
                                    stop = true;
                                    break;
                                }

                                Index += 1;
                            }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private void CtxMenu_Opening(object sender, CancelEventArgs e)
        {
            if (trv_links.SelectedNode == null)
                e.Cancel = true;
        }

        private void CtxEditLink_Click(object sender, EventArgs e)
        {
            var edit = new Edit
            {
                Owner = this,
                tbx_address = {Text = Links[Index].Address},
                tbx_anchor = {Text = Links[Index].Anchor},
                tbx_status = {Text = Links[Index].Status}
            };


            if (Regex.Match(Links[Index].Anchor, @"<\w+?(.|\n)*?>").Success)
            {
                edit.tbx_anchor.ReadOnly = true;
                edit.cbx_autoload.Enabled = false;
            }

            if (edit.ShowDialog() != DialogResult.OK) return;
            _isChangesSaved = false;
            btn_save_links.Enabled = true;
            RebuildLinksTree();
            tab_links.Text = $@"● Ссылки ({Links.Count})";
        }

        private void CtxShowCode_Click(object sender, EventArgs e)
        {
            try
            {
                var code = new Code();
                code.Owner = this;
                code.Text = Links[Index].Address;

                var rtbx = code.rtbx_code;
                rtbx.Text = GetPageContent(Links[Index].Address);

                code.ShowDialog();
            }
            catch
            {
                // ignored
            }
        }

        private void CtxCopyAddress_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(Links[Index].Address);
            }
            catch
            {
                // ignored
            }
        }

        private void CtxOpenInBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Links[Index].Address);
            }
            catch
            {
                // ignored
            }
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isChangesSaved) return;
            var result = MessageBox.Show(
                @"Не все изменения сохранены.\r\nВы уверены, что хотите выйти?",
                @"Внимание",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                e.Cancel = true;
        }

        #endregion События

        #region Методы

        // Сохраняет последние введенные адреса
        private static void WriteRecent(string value)
        {
            var keys = new[] {"URL0", "URL1", "URL2", "URL3", "URL4", "URL5", "URL6", "URL7", "URL8", "URL9"};
            var values = new string[keys.Length];

            var count = 0;
            foreach (var key in keys)
            {
                if (Config.Read("Addresses", key) == value)
                    break;

                if (!Config.KeyExists(key, "Addresses"))
                {
                    Config.Write("Addresses", key, value);
                    break;
                }

                if (!Config.KeyExists(key, "Addresses")) continue;
                values[count] = Config.Read("Addresses", key);
                count++;
            }

            if (count != keys.Length) return;
            var temp = new string[count];
            temp[0] = value;

            for (var i = 0; i < count - 1; i++) temp[i + 1] = values[i];

            for (var i = 0; i < count; i++) Config.Write("Addresses", keys[i], temp[i]);
        }

        // Получает список последних введенных адресов
        private static List<string> GetRecent()
        {
            var keys = new[] {"URL0", "URL1", "URL2", "URL3", "URL4", "URL5", "URL6", "URL7", "URL8", "URL9"};

            return (from key in keys where Config.KeyExists(key, "Addresses") select Config.Read("Addresses", key))
                .ToList();
        }


        // Проверяет к какому типу относится ссылка
        private static int IsFile(string url)
        {
            try
            {
                return new Uri(GetFixedUrl(url)).IsFile ? 1 : 2;
            }
            catch
            {
                return 0;
            }
        }

        private static int IsFile(Uri uri)
        {
            try
            {
                return new Uri(GetFixedUrl(uri)).IsFile ? 1 : 2;
            }
            catch
            {
                return 0;
            }
        }


        // Преобразует Url к нормализованному виду
        public static string GetFixedUrl(string url)
        {
            return new UriBuilder(url).Uri.ToString();
        }

        private static string GetFixedUrl(Uri uri)
        {
            return new UriBuilder(uri).Uri.ToString();
        }

        // Преобразует относительный Url в абсолютный
        private static string GetAbsoluteUrl(string abs, string rel)
        {
            var uri = new Uri(rel, UriKind.RelativeOrAbsolute);
            uri = new Uri(new Uri(abs), uri);

            return IsFile(uri) == 1 ? uri.OriginalString : uri.ToString();
        }


        // Проверяет существует ли данная страница
        public static bool IsPageExists(string url)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                //request.AllowAutoRedirect = false;
                request.Timeout = Timeout;

                var response = (HttpWebResponse) request.GetResponse();
                request.Abort();

                if (response.StatusCode == HttpStatusCode.OK)
                    return true;

                response.Close();
            }
            catch
            {
                // ignored
            }

            return false;
        }

        // Получает код ответа сервера
        public static string GetStatusCode(string url)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                // request.MaximumAutomaticRedirections = 4;
                request.AllowAutoRedirect = false;
                request.Timeout = Timeout;

                var response = (HttpWebResponse) request.GetResponse();
                request.Abort();

                response.Close();

                return response.StatusCode.ToString();
            }
            catch (WebException ex)
            {
                return ex.Response != null
                    ? ((int) ((HttpWebResponse) ex.Response).StatusCode).ToString()
                    : ex.Status.ToString();
            }
        }

        // Пытается создать запрос к серверу
        private static bool TryCreateRequest(string url)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Abort();
                return true;
            }
            catch
            {
                return false;
            }
        }


        // Получает содержимое страницы
        public static string GetPageContent(string url, int timeout = 10000)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Timeout = timeout;

                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    var content = string.Empty;
                    if (response.StatusCode != HttpStatusCode.OK) return content;
                    var characterSet = response.CharacterSet;
                    if (characterSet == "\"utf-8\"") characterSet = "utf-8"; //HOTFIX 1
                    if (characterSet == "") characterSet = "utf-8"; //HOTFIX 2

                    using (var stream = new StreamReader(response.GetResponseStream(),
                        Encoding.GetEncoding(characterSet)))
                    {
                        content = stream.ReadToEnd();
                        var c = content;

                        if (response.CharacterSet == "ISO-8859-1")
                        {
                            content = Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(content));

                            if (content.Contains("�")) //HOTFIX 3
                                content = Encoding.GetEncoding(1251)
                                    .GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(c));
                        }

                        content = HttpUtility.HtmlDecode(content);
                    }

                    return content;
                }
            }
            catch
            {
                return null;
            }
        }

        // Получает содержимое файла
        private static string GetFileContent(string url)
        {
            try
            {
                using (var reader = new StreamReader(url, Encoding.Default, true))
                {
                    return HttpUtility.HtmlDecode(reader.ReadToEnd());
                }
            }
            catch
            {
                return null;
            }
        }


        // Получает коды ответа сервера для указанных ссылок
        private void GetLinksStatus(List<Link> links)
        {
            if (links != null)
                Parallel.ForEach(links, new ParallelOptions {MaxDegreeOfParallelism = 3},
                    link =>
                    {
                        switch (IsFile(link.Address))
                        {
                            case 1:
                                link.Status = "Локальный файл";
                                break;
                            case 2:
                                link.Status = GetStatusCode(link.Address);
                                break;
                        }

                        try
                        {
                            Invoke((MethodInvoker) delegate { SetLinkColorAndStatus(link); });
                        }
                        catch
                        {
                            // ignored
                        }
                    });
        }

        // Подсвечивает указанную ссылку в TreeView
        private void SetLinkColorAndStatus(Link link)
        {
            try
            {
                var node = trv_links.Nodes
                    .Cast<TreeNode>().First(x => x.Name == link.Address);

                node.Text = node.Text.Substring(0, node.Text.LastIndexOf("=", StringComparison.Ordinal) + 2) +
                            link.Status;

                if (link.Status == HttpStatusCode.OK.ToString())
                {
                    node.BackColor = Color.FromArgb(128, 255, 128);
                    foreach (TreeNode node1 in node.Nodes)
                    {
                        node1.BackColor = Color.FromArgb(128, 245, 128);
                        foreach (TreeNode node2 in node1.Nodes)
                            node2.BackColor = Color.FromArgb(128, 235, 128);
                    }
                }
                else if (link.Status == HttpStatusCode.Redirect.ToString() ||
                         link.Status == HttpStatusCode.MovedPermanently.ToString())
                {
                    node.BackColor = Color.FromArgb(255, 255, 128);
                    foreach (TreeNode node1 in node.Nodes)
                    {
                        node1.BackColor = Color.FromArgb(245, 245, 128);
                        foreach (TreeNode node2 in node1.Nodes)
                            node2.BackColor = Color.FromArgb(235, 235, 128);
                    }
                }
                else if (link.Status == string.Empty)
                {
                    node.BackColor = Color.FromArgb(224, 224, 224);
                    foreach (TreeNode node1 in node.Nodes)
                    {
                        node1.BackColor = Color.FromArgb(214, 214, 214);
                        foreach (TreeNode node2 in node1.Nodes)
                            node2.BackColor = Color.FromArgb(200, 200, 200);
                    }
                }
                else
                {
                    node.BackColor = Color.FromArgb(255, 128, 128);
                    foreach (TreeNode node1 in node.Nodes)
                    {
                        node1.BackColor = Color.FromArgb(245, 128, 128);
                        foreach (TreeNode node2 in node1.Nodes)
                            node2.BackColor = Color.FromArgb(235, 128, 128);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }


        // Получает список ссылок со всех найденных страниц как есть, включая дубликаты
        private List<Link> GetLinksFromCrawler(List<Link> links, string url, int depth = 1)
        {
            var linksFound = links;
            var linksVisited = new List<Link> {new Link(links.First().Location)};

            // Удалить из найденных ссылок посещенные
            // Удалить дубликаты
            // Оставить только внутренние ссылки
            var linksRemained = linksFound
                .Where(x => !linksVisited.Select(y => y.Address).Contains(x.Address))
                .GroupBy(x => x.Address).Select(g => g.First()).ToList()
                .FindAll(x => new Uri(x.Address).Host == new Uri(url).Host);

            // Удалить все ссылки на файлы, кроме ссылок на популярные динамические форматы страниц
            linksRemained = linksRemained.Where(x => !new Uri(x.Address).Segments.Last().Contains(".")
                                                     || Regex.IsMatch(new Uri(x.Address).Segments.Last(),
                                                         @"\.(html?|php|aspx?|jsp)")).ToList();

            {
                // Привести якоря к нормальному виду ссылок...
                var anchors = linksRemained.Where(x => x.Address.Contains("#"))
                    .Select(x => new Link
                        {Address = x.Address.Substring(0, x.Address.IndexOf("#", StringComparison.Ordinal))})
                    .ToList();

                // Чтобы добавить их в общий список
                foreach (var _ in anchors.Where(anchor =>
                    !linksVisited.Select(y => y.Address).Contains(anchor.Address)))
                    linksRemained.AddRange(anchors);
            }

            // Удалить все якоря обычного вида
            linksRemained = linksRemained.Where(x => !x.Address.Contains("#")).ToList();

            // Удалить дубликаты
            linksRemained = linksRemained.GroupBy(x => x.Address).Select(g => g.First()).ToList();

            // Счетчик глубины рекурсии
            var depthCounter = 0;

            CrawlingMechanism(linksRemained);

            void CrawlingMechanism(List<Link> links1)
            {
                try
                {
                    if (depthCounter >= depth) return;
                    if (links1.Count <= 0 || links1.Count > CrawlLimit) return;
                    depthCounter += 1;

                    // Добавить найденные ссылки в список
                    linksFound.AddRange(GetLinksFromSpecifiedPages(links1));

                    // Добавить посещенные ссылки в список
                    linksVisited.AddRange(links1);

                    links1 = linksFound
                        .Where(x => !linksVisited.Select(y => y.Address).Contains(x.Address))
                        .GroupBy(x => x.Address).Select(g => g.First()).ToList()
                        .FindAll(x => new Uri(x.Address).Host == new Uri(url).Host);

                    // Удалить все ссылки на файлы, кроме ссылок на популярные динамические форматы страниц
                    links1 = links1.Where(x => !new Uri(x.Address).Segments.Last().Contains(".")
                                               || Regex.IsMatch(new Uri(x.Address).Segments.Last(),
                                                   @"\.(html?|php|aspx?|jsp)")).ToList();

                    {
                        // Привести якоря к нормальному виду ссылок...
                        var anchors = links1.Where(x => x.Address.Contains("#"))
                            .Select(x => new Link
                                {Address = x.Address.Substring(0, x.Address.IndexOf("#", StringComparison.Ordinal))})
                            .ToList();

                        // Чтобы добавить их в общий список
                        foreach (var _ in anchors.Where(anchor =>
                            !linksVisited.Select(y => y.Address).Contains(anchor.Address)))
                            links1.AddRange(anchors);
                    }

                    // Удалить все якоря обычного вида
                    links1 = links1.Where(x => !x.Address.Contains("#")).ToList();

                    // Удалить дубликаты
                    links1 = links1.GroupBy(x => x.Address).Select(g => g.First()).ToList();

                    CrawlingMechanism(links1);
                }
                catch
                {
                    // ignored
                }
            }

            return linksFound;
        }

        // Получает список ссылок с указанных страниц как есть, включая дубликаты
        private List<Link> GetLinksFromSpecifiedPages(List<Link> links)
        {
            try
            {
                // Удалить дубликаты
                links = links.GroupBy(x => x.Address).Select(g => g.First()).ToList();

                var links1 = new List<Link>();

                Parallel.ForEach(links, new ParallelOptions {MaxDegreeOfParallelism = 4},
                    link =>
                    {
                        try
                        {
                            var url = GetFixedUrl(link.Address);
                            var content = GetPageContent(url, CrawlTimeout);

                            if (!string.IsNullOrWhiteSpace(content))
                                links1.AddRange(GetLinksFromContent(url, content));
                        }
                        catch
                        {
                            // ignored
                        }
                    });

                return links1;
            }
            catch
            {
                return null;
            }
        }

        // Получает список ссылок из кода страницы как есть, включая дубликаты
        private List<Link> GetLinksFromContent(string url, string content)
        {
            try
            {
                var links = new List<Link>();

                var document = new HtmlDocument();
                document.LoadHtml(content);

                var nodes = document.DocumentNode.SelectNodes(".//a[@href]");

                if (nodes == null) return links;
                foreach (var node in nodes)
                    if (!node.GetAttributeValue("href", "").StartsWith("#"))
                    {
                        var original = node.GetAttributeValue("href", "");
                        var absolute = GetAbsoluteUrl(url, original);

                        if (!TryCreateRequest(absolute))
                            continue;

                        var anchor = string.IsNullOrWhiteSpace(node.InnerText)
                            ? node.InnerHtml
                            : node.InnerText;

                        anchor = Regex.Replace(anchor, @"\s+", " ").Trim();

                        links.Add(new Link(absolute, url, anchor));
                        links.Last().Original = original;
                    }

                return links;
            }
            catch
            {
                return null;
            }
        }


        // Получает список слов из кода страницы
        private static List<Word> GetWordsFromContent(string content)
        {
            try
            {
                var words = new List<Word>();

                var document = new HtmlDocument();
                document.LoadHtml(content);

                var body = document.DocumentNode.SelectSingleNode(".//body");

                if (body == null)
                    return words;

                var scripts = document.DocumentNode.SelectNodes(".//script");
                if (scripts != null)
                    foreach (var script in scripts)
                        script.ParentNode.RemoveChild(script);

                var text = body.InnerText.ToLower();
                text = Regex.Replace(text, @"[^\w\s]+", " ");
                text = Regex.Replace(text, @"\d+", " ");
                text = Regex.Replace(text, @"_+", " ");
                text = $" {text} ";
                text = Regex.Replace(text, @"\s+", @"  ");
                text = Regex.Replace(text, @"\s+?\w{1,3}\s+?", "");
                text = Regex.Replace(text, @"\s+", " ").Trim();

                var textArray = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                words.AddRange(textArray.Select(word => new Word(word)));

                return words;
            }
            catch
            {
                return null;
            }
        }

        // Получает список тегов из кода страницы
        private List<Tag> GetTagsFromContent(string content)
        {
            try
            {
                var tags = new List<Tag>();

                var document = new HtmlDocument();
                document.LoadHtml(content);

                var nodes = document.DocumentNode.SelectNodes(".//title");
                if (nodes != null)
                    foreach (var node in nodes)
                    {
                        const string name = "title";
                        var text = Regex.Replace(node.InnerText, @"\s+", " ").Trim();
                        var length = text.Length;

                        tags.Add(new Tag(name, text, length));
                    }

                nodes = document.DocumentNode.SelectNodes(".//meta[@name]");
                if (nodes != null)
                    foreach (var node in nodes)
                        if (node.GetAttributeValue("name", "") == "description"
                            || node.GetAttributeValue("name", "") == "title")
                        {
                            var name = "meta-" + node.GetAttributeValue("name", "");
                            var text = Regex.Replace(node.GetAttributeValue("content", ""), @"\s+", " ").Trim();
                            var length = text.Length;

                            tags.Add(new Tag(name, text, length));
                        }

                var hs = new[] {"h1", "h2", "h3", "h4", "h5", "h6"};
                foreach (var h in hs)
                {
                    nodes = document.DocumentNode.SelectNodes(".//" + h);
                    if (nodes == null) continue;
                    foreach (var node in nodes)
                    {
                        var name = h;
                        var text = Regex.Replace(node.InnerText, @"\s+", " ").Trim();
                        var length = text.Length;

                        tags.Add(new Tag(name, text, length));
                    }
                }

                nodes = document.DocumentNode.SelectNodes(".//img");
                if (nodes != null)
                    foreach (var node in nodes)
                    {
                        var src = string.IsNullOrWhiteSpace(node.GetAttributeValue("src", ""))
                            ? node.GetAttributeValue("srcset", "")
                            : node.GetAttributeValue("src", "");

                        var name = "img " + src;
                        var text = Regex.Replace(node.GetAttributeValue("alt", ""), @"\s+", " ").Trim();
                        var length = text.Length;

                        tags.Add(new Tag(name, text, length));
                    }

                var hidden = new List<HtmlNode>();

                if (document.DocumentNode.SelectNodes(".//*[@hidden]") != null)
                    hidden.AddRange(document.DocumentNode.SelectNodes(".//*[@hidden]"));
                if (document.DocumentNode.SelectNodes(".//*[@aria-hidden]") != null)
                    hidden.AddRange(document.DocumentNode.SelectNodes(".//*[@aria-hidden]"));

                foreach (var node in hidden)
                {
                    var name = "hidden " + node.Name;
                    var text = Regex.Replace(node.InnerText, @"[\s|\t|\n]+", " ").Trim();
                    var length = text.Length;

                    tags.Add(new Tag(name, text, length));
                }

                return tags;
            }
            catch
            {
                return null;
            }
        }


        // Задает список пар слово-запрос
        private void GetQueriesToKeywordsPairs(List<string> keywordsToUse)
        {
            foreach (var keyword in keywordsToUse)
                if (_queriesToKeywords.Count(x => x.Keyword == keyword) == 0)
                {
                    var queries = GetRelatedQueries(keyword) ?? GetRelatedQueriesViaApi(keyword);
                    if (queries != null)
                        _queriesToKeywords.Add(new QueriesToKeyword(keyword, queries));
                    else
                        _queriesToKeywords.Add(new QueriesToKeyword(keyword,
                            new List<Query> {new Query(503, "Данные не получены", "", "")}));
                }
        }

        // Получает список связанных запросов
        private static List<Query> GetRelatedQueries(string query)
        {
            try
            {
                var queries = new List<Query>();

                var url = new UriBuilder($"https://google.com/search?q={query}&num=100").ToString();
                var content = GetPageContent(url);

                var document = new HtmlDocument();
                document.LoadHtml(content);

                var nodes = document.DocumentNode.SelectNodes(".//div[@class='g']");

                if (nodes == null) return queries;
                var i = 0;
                foreach (var node in nodes)
                    try
                    {
                        var address = node.SelectNodes("//cite")[i].InnerText;
                        var title = node.SelectNodes("//h3[@class='r']")[i].InnerText;
                        var snippet = node.SelectNodes("//span[@class='st']")[i].InnerText;

                        queries.Add(new Query(i + 1, address, title, snippet));
                        i++;
                    }
                    catch
                    {
                        // ignored
                    }

                return queries;
            }
            catch
            {
                return null;
            }
        }

        private static List<Query> GetRelatedQueriesViaApi(string query)
        {
            try
            {
                var queries = new List<Query>();

                const string apiKey = "/*past here*/";
                const string cseId = "/*past here*/";

                var customSearch = new CustomsearchService(new BaseClientService.Initializer {ApiKey = apiKey});
                var request = customSearch.Cse.List(query);
                request.Cx = cseId;
                request.Gl = "ru";

                for (var page = 1; page <= 10; page++)
                {
                    request.Start = page;
                    var result = request.Execute().Items;

                    if (result == null) continue;
                    queries.AddRange(result.Select((t, i) =>
                        new Query(i + 1 + (page - 1) * 10, t.Title, t.Link, t.Snippet)));
                }

                return queries;
            }
            catch
            {
                return null;
            }
        }


        // Рассчитывает оценку на основе данных
        private double CalculateScore(List<Link> links, List<Word> words, List<Tag> tags, List<string> keywords,
            string code)
        {
            if (words.Count < 5)
                return 0;

            _problems = new List<Problem>();

            var lemmatizer = new LemmatizerPrebuiltCompact(LanguagePrebuilt.Russian);

            words = words.GroupBy(x => lemmatizer.Lemmatize(x.Term))
                .Select(g => new Word
                {
                    Term = g.Key,
                    Count = g.Count(),
                    Density = Math.Round((decimal) g.Count() / words.Count * 100, 2)
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            // Если ключевые слова не указаны, вместо них выбрать 5 самых популярных слов
            if (keywords.Count == 0)
                keywords.AddRange(words.GetRange(0, 5).Select(x => x.Term));
            else
                keywords = keywords.Select(x => lemmatizer.Lemmatize(x)).ToList();


            var mLinksStatus = 0.0m; // Статус ссылок
            var mLinksAnchor = 0.0m; // Текст ссылок
            var mLinksKeywords = 0.0m; // Ключевые слова в тексте ссылок
            foreach (var link in links)
            {
                if (link.Status == HttpStatusCode.OK.ToString())
                {
                    mLinksStatus += 1.0m;
                }
                else if (link.Status == HttpStatusCode.Redirect.ToString()
                         || link.Status == HttpStatusCode.MovedPermanently.ToString())
                {
                    mLinksStatus += 0.5m;
                    _problems.Add(new Problem(link, "Redirect"));
                }
                else if (link.Status == "Timeout")
                {
                    mLinksStatus += 0.2m;
                    _problems.Add(new Problem(link, "Timeout"));
                }
                else
                {
                    _problems.Add(new Problem(link, "Not Found"));
                }

                if (!string.IsNullOrWhiteSpace(link.Anchor))
                {
                    mLinksAnchor += 1.0m;

                    var anchor = string.Join(" ", link.Anchor.Split(' ').Select(x => lemmatizer.Lemmatize(x)));
                    if (keywords.Any(keyword => anchor.Contains(keyword))) mLinksKeywords += 1.0m;
                }
                else
                {
                    _problems.Add(new Problem(link, "Empty Anchor"));
                }
            }

            mLinksStatus /= links.Count;
            mLinksAnchor /= links.Count;

            if (mLinksKeywords / links.Count >= 0.3m && mLinksKeywords / links.Count <= 0.7m)
                mLinksKeywords = 1.0m;
            else
                mLinksKeywords = 0.0m;


            Word topKeyword = null;
            foreach (var keyword in keywords)
                try
                {
                    if (topKeyword == null)
                        topKeyword = words.First(x => x.Term == keyword);
                    else if (topKeyword.Count < words.First(x => x.Term == keyword).Count)
                        topKeyword = words.First(x => x.Term == keyword);
                }
                catch
                {
                    // ignored
                }

            var mAcademicVomit = 0.0m; // Академическая тошнота
            var mClassicVomit = 0.0m; // Классическая тошнота
            if (topKeyword != null)
            {
                _summary.Add($@"Для слова «{topKeyword.Term}»:");
                _summary.Add($@"- академич. тошнота: {(double) topKeyword.Density}%");
                _summary.Add($@"- классич.  тошнота: {Math.Round(Math.Sqrt(topKeyword.Count), 2)}%");

                if (topKeyword.Density >= 3 && topKeyword.Density <= 8)
                {
                    mAcademicVomit += 1.0m;
                }
                else
                {
                    if (topKeyword.Density > 8)
                    {
                        mAcademicVomit += 0.5m;
                        _problems.Add(new Problem(topKeyword, "High Academic Vomit"));
                    }
                    else if (topKeyword.Density < 3)
                    {
                        _problems.Add(new Problem(topKeyword, "Low Academic Vomit"));
                    }
                }

                if (Math.Sqrt(topKeyword.Count) >= 2 && Math.Sqrt(topKeyword.Count) <= 7)
                {
                    mClassicVomit += 1.0m;
                }
                else
                {
                    if (Math.Sqrt(topKeyword.Count) > 7)
                    {
                        mClassicVomit += 0.5m;
                        _problems.Add(new Problem(topKeyword, "High Classic Vomit"));
                    }
                    else if (Math.Sqrt(topKeyword.Count) < 2)
                    {
                        _problems.Add(new Problem(topKeyword, "Low Classic Vomit"));
                    }
                }
            }
            else
            {
                _summary.Add("! Нет ключевого слова !");
                _problems.Add(new Problem(keywords, "No Keywords"));
            }


            tags = tags.GroupBy(x => x.Name)
                .SelectMany(g => g)
                .Select(x => new Tag
                {
                    Name = x.Name,
                    Content = x.Content,
                    Length = x.Length,
                    Count = tags.Count(y => y.Name == x.Name)
                })
                .ToList();

            var mTagsPresence = 0.0m; // Наличие важных тегов
            var mTagsLength = 0.0m; // Длина важных тегов
            var mTagsKeywords = 0.0m; // Ключевые слова в важных тегах
            var mTagsImgAlt = 0.0m; // Текст у картинок
            var mTagsHidden = 0.0m; // Скрытые теги
            var mTagsDuplication = 0.0m; // Дубликаты тегов
            foreach (var tag in tags)
                if (tag.Name == "title" || tag.Name == "meta-title"
                                        || tag.Name == "meta-description" || tag.Name == "h1")
                {
                    if (tag.Count == 1)
                    {
                        mTagsPresence += 1.0m;

                        if ((tag.Name == "title" || tag.Name == "meta-title") && tag.Length > 10 && tag.Length < 70
                            || tag.Name == "meta-description" && tag.Length > 50 && tag.Length < 320
                            || tag.Name == "h1" && tag.Length > 10 && tag.Length < 50)
                            mTagsLength += 1.0m;
                        else
                            _problems.Add(new Problem(tag, "Bad Length"));

                        string.Join(" ", tag.Content.Split(' ').Select(x => lemmatizer.Lemmatize(x)));
                        if (keywords.Any(keyword => tag.Content.Contains(keyword))) mTagsKeywords += 1.0m;
                    }
                    else
                    {
                        if (tag.Name == "h1")
                            mTagsDuplication += 0.5m;
                        else
                            mTagsDuplication += 1.0m;

                        _problems.Add(new Problem(tag, "More Than One Tag"));
                    }
                }
                else if (tag.Name.StartsWith("img"))
                {
                    if (!string.IsNullOrWhiteSpace(tag.Content))
                        mTagsImgAlt += 1.0m;
                    else
                        _problems.Add(new Problem(tag, "Empty Alt"));
                }
                else if (tag.Name.StartsWith("hidden"))
                {
                    if (string.IsNullOrWhiteSpace(tag.Content)) continue;
                    var content = string.Join(" ", tag.Content.Split(' ').Select(x => lemmatizer.Lemmatize(x)));
                    if (!keywords.Any(keyword => content.Contains(keyword))) continue;
                    mTagsHidden += 1.0m;
                    _problems.Add(new Problem(tag, "Hidden Tag"));
                }

            mTagsPresence /= 4;
            mTagsLength /= 4;
            mTagsKeywords /= 4;
            mTagsDuplication /= 4;
            if (mTagsImgAlt != 0)
                mTagsImgAlt /= tags.Count(x => x.Name.StartsWith("img"));
            if (mTagsHidden != 0)
                mTagsHidden /= tags.Count(x => x.Name.StartsWith("hidden"));


            var mTextToCode = 0.0m; // Соотношение текста к коду

            var document = new HtmlDocument();
            document.LoadHtml(code);
            var text = document.DocumentNode.SelectSingleNode(".//body").InnerText;

            if ((double) text.Length / code.Length > 0.1)
                mTextToCode += 1.0m;
            else
                _problems.Add(new Problem(Math.Round((double) text.Length / code.Length, 2), "Low Code Ratio"));

            _metrics.A = mLinksStatus;
            _metrics.B = mLinksAnchor;
            _metrics.C = mLinksKeywords;
            _metrics.D = mAcademicVomit;
            _metrics.E = mClassicVomit;
            _metrics.F = mTagsPresence;
            _metrics.G = mTagsLength;
            _metrics.H = mTagsKeywords;
            _metrics.I = mTagsImgAlt;
            _metrics.J = mTextToCode;
            _metrics.K = mTagsHidden;
            _metrics.L = mTagsDuplication;
            _metricsReceived = true;

            var score = mLinksStatus * Factors.A // A    Сильное влияние
                        + mLinksAnchor * Factors.B // B    Среднее влияние
                        + mLinksKeywords * Factors.C // C    Слабое влияние
                        + mAcademicVomit * Factors.D // D    Среднее влияние
                        + mClassicVomit * Factors.E // E    Слабое влияние
                        + mTagsPresence * Factors.F // F    Сильное влияние
                        + mTagsLength * Factors.G // G    Среднее влияние
                        + mTagsKeywords * Factors.H // H    Среднее влияние
                        + mTagsImgAlt * Factors.I // I    Среднее влияние
                        + mTextToCode * Factors.J // J    Слабое влияние
                        - mTagsHidden * Factors.K // K    Сильное влияние // Негативный
                        - mTagsDuplication * Factors.L; // L    Сильное влияние // Негативный

            return Math.Round((double) score, 1);
        }

        // Сопоставляет советы для списка проблем
        private static string AdviceBuilder(List<Problem> problems)
        {
            var advice = "Похоже, все хорошо.";

            if (problems.Count == 0) return advice;

            advice = string.Empty;
            foreach (var problem in problems.GroupBy(x => x.Type).Select(g => g.First()))
                switch (problem.Type)
                {
                    case "Not Found":
                        advice += "Эти ссылки ведут на несуществующие страницы:\r\n";
                        advice += string.Join("", problems.Where(x => x.Type == problem.Type)
                            .Select(x => x.Source as Link).GroupBy(x => x.Address).Select(x => x.First())
                            .Select(x => $"\t{x.Address.ReduceIfTooLong(60)}\r\n"));
                        advice += Environment.NewLine;
                        break;
                    case "Redirect":
                        advice += "Поисковые системы по-разному относятся к редиректам. " +
                                  "Все же, лучше будет избавиться от них на этих страницах:\r\n";
                        advice += string.Join("", problems.Where(x => x.Type == problem.Type)
                            .Select(x => x.Source as Link).GroupBy(x => x.Address).Select(x => x.First())
                            .Select(x => $"\t{x.Address.ReduceIfTooLong(60)}\r\n"));
                        advice += Environment.NewLine;
                        break;
                    case "Timeout":
                        advice += "Чрезвычайно долгая загрузка на этих страницах:\r\n";
                        advice += string.Join("", problems.Where(x => x.Type == problem.Type)
                            .Select(x => x.Source as Link).GroupBy(x => x.Address).Select(x => x.First())
                            .Select(x => $"\t{x.Address.ReduceIfTooLong(60)}\r\n"));
                        advice += Environment.NewLine;
                        break;
                    case "Empty Anchor":
                        advice += "Эти ссылки не содержат ни текста, ни других элементов:\r\n";
                        advice += string.Join("", problems.Where(x => x.Type == problem.Type)
                            .Select(x => $"\t{((Link) x.Source).Address.ReduceIfTooLong(60)}\r\n"));
                        advice += Environment.NewLine;
                        break;
                    case "Empty Alt":
                        advice += "Эти теги содержат пустой атрибут alt или не содержат вовсе:\r\n";
                        advice += string.Join("",
                            problems.Where(x => x.Type == problem.Type)
                                .Select(x => $"\t{((Tag) x.Source).Name}\r\n"));
                        advice += Environment.NewLine;
                        break;
                    case "High Academic Vomit":
                        advice +=
                            $"Значение академической тошноты для ключевого слова «{((Word) problem.Source).Term}» " +
                            $"выше рекомендованного (3-8) и составляет {((Word) problem.Source).Density}%.\r\n";
                        advice += Environment.NewLine;
                        break;
                    case "Low Academic Vomit":
                        advice +=
                            $"Значение академической тошноты для ключевого слова «{((Word) problem.Source).Term}» " +
                            $"ниже рекомендованного (3-8) и составляет {((Word) problem.Source).Density}%.\r\n";
                        advice += Environment.NewLine;
                        break;
                    case "High Classic Vomit":
                        advice +=
                            $"Возможно, ключевое слово «‎{((Word) problem.Source).Term}» встречается слишком часто: {((Word) problem.Source).Count}.\r\n";
                        advice += Environment.NewLine;
                        break;
                    case "Low Classic Vomit":
                        advice +=
                            $"Возможно, ключевое слово «{((Word) problem.Source).Term}» встречается слишком редко: {((Word) problem.Source).Count}.\r\n";
                        advice += Environment.NewLine;
                        break;
                    case "Bad Length":
                        advice +=
                            "Длина этих тегов выходит за рекомендованные границы (title: 10-70, desciption: 50-320, h1: 10-50):\r\n";
                        advice += string.Join("", problems.Where(x => x.Type == problem.Type)
                            .Select(x => x.Source as Tag).Select(x =>
                                $"\t{x.Name}\t{x.Content.ReduceIfTooLong()} [{x.Length}]\r\n"));
                        advice += Environment.NewLine;
                        break;
                    case "More Than One Tag":
                        advice += "Эти теги встречаются более одного раза:\r\n";
                        advice += string.Join("", problems.Where(x => x.Type == problem.Type)
                            .Select(x => x.Source as Tag)
                            .Select(x => $"\t{x.Name}\t{x.Content.ReduceIfTooLong()}\r\n"));
                        advice += Environment.NewLine;
                        break;
                    case "Hidden Tag":
                        advice +=
                            "Не исользуйте запрещенные методы оптимизации. Избавьтесь от атрибута hidden в следующих тегах:\r\n";
                        advice += string.Join("", problems.Where(x => x.Type == problem.Type)
                            .Select(x => x.Source as Tag)
                            .Select(x => $"\t{x.Name}\t{x.Content.ReduceIfTooLong()}\r\n"));
                        advice += Environment.NewLine;
                        break;
                    case "Low Code Ratio":
                        advice +=
                            $"Соотношение текста к коду составляет {problem.Source} %. На этой странице очень мало текста и очень много кода.\r\n";
                        advice += Environment.NewLine;
                        break;
                    case "No Keywords":
                        advice +=
                            $"На этой странице нет ни одного ключевого слова из списка:\r\n\t{string.Join("\r\n\t", problem.Source as List<string>)}\r\n";
                        advice += Environment.NewLine;
                        break;
                }

            return advice;
        }

        #endregion
    }

    public class Link
    {
        public Link()
        {
        }

        public Link(string address)
        {
            Address = address;
        }

        public Link(string address, string location, string anchor)
        {
            Address = address;
            Location = location;
            Anchor = anchor;
        }

        public string Original { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Anchor { get; set; }
        public string Status { get; set; }
        public int Inbound { get; set; }
        public int Outbound { get; set; }
    }

    public class KeywordsToUrl
    {
        public KeywordsToUrl(string url)
        {
            Url = url;
        }

        public string Url { get; }
        public HashSet<string> Keywords { get; } = new HashSet<string> {"none", "each"};
    }

    public class Word
    {
        public Word()
        {
        }

        public Word(string term)
        {
            Term = term;
        }

        public string Term { get; set; }
        public int Count { get; set; }
        public decimal Density { get; set; }
    }

    public class Query
    {
        public Query(int position, string address, string title, string snippet)
        {
            Position = position;
            Address = address;
            Title = title;
            Snippet = snippet;
        }

        public int Position { get; }
        public string Address { get; }
        public string Title { get; }
        public string Snippet { get; }
    }

    public class QueriesToKeyword
    {
        public QueriesToKeyword(string keyword, List<Query> queries)
        {
            Keyword = keyword;
            Queries = queries;
        }

        public string Keyword { get; }
        public List<Query> Queries { get; }
    }

    public class Tag
    {
        public Tag()
        {
        }

        public Tag(string name, string content, int length)
        {
            Name = name;
            Content = content;
            Length = length;
        }

        public string Name { get; set; }
        public string Content { get; set; }
        public int Length { get; set; }
        public int Count { get; set; }
    }

    public class Problem
    {
        public Problem(object source, string type)
        {
            Source = source;
            Type = type;
        }

        public object Source { get; }
        public string Type { get; }
    }

    public class Factors
    {
        public decimal A { get; set; } = 35;
        public decimal B { get; set; } = 7;
        public decimal C { get; set; } = 3;
        public decimal D { get; set; } = 12;
        public decimal E { get; set; } = 2;
        public decimal F { get; set; } = 12;
        public decimal G { get; set; } = 5;
        public decimal H { get; set; } = 15;
        public decimal I { get; set; } = 7;
        public decimal J { get; set; } = 2;

        public decimal K { get; set; } = 5;
        public decimal L { get; set; } = 5;
    }


    public class NodeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var tx = x as TreeNode;
            var ty = y as TreeNode;

            return string.CompareOrdinal(tx.Name, ty.Name);
        }
    }

    public static class TreeViewExtensions
    {
        public static List<string> GetExpansionState(this TreeNodeCollection nodes)
        {
            return nodes.Descendants()
                .Where(n => n.IsExpanded)
                .Select(n => n.Name)
                .ToList();
        }

        public static void SetExpansionState(this TreeNodeCollection nodes, List<string> savedExpansionState)
        {
            foreach (var node in nodes.Descendants()
                .Where(n => savedExpansionState.Contains(n.Name)))
                node.Expand();
        }

        private static IEnumerable<TreeNode> Descendants(this TreeNodeCollection c)
        {
            foreach (var node in c.OfType<TreeNode>())
            {
                yield return node;

                foreach (var child in node.Nodes.Descendants()) yield return child;
            }
        }
    }

    public static class DataGridViewExtension
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            var dgvType = dgv.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }

    public static class StringExtension
    {
        public static string ReduceIfTooLong(this string text, int max = 40)
        {
            return text != null && text.Length > max ? text.Substring(0, max) + "..." : text;
        }
    }
}