using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Select.Messaging;
using Select.Models;
using Select.Presentation.Dialogs;
using Select.Presentation.Utils;
using Select.Rtti;

namespace Select.Presentation.Views;

public enum ViewType
{
    General,
    Resource,
}

public partial class BrowserView : View
{
    private const string GoogleUrl = "https://www.google.com/";

    private readonly ChromiumWebBrowser _browser = new();

    public BrowserView(ViewType viewType)
    {
        ViewType = viewType;

        InitializeComponent();
        InitializeToolStripLayout();
        InitializeBrowser();
        InitializeTheme();
        InitializeView();
    }

    private ViewType ViewType { get; }

    #region event handlers

    private void GoButtonClick(object sender, EventArgs e) => LoadUrl(urlTextBox.Text);
    private void BackButtonClick(object sender, EventArgs e) => _browser.Back();
    private void ForwardButtonClick(object sender, EventArgs e) => _browser.Forward();
    private void SchemaButton_Click(object sender, EventArgs e) => LoadUrl(Project.SchemaPath);
    private void ShowTablesButton_Click(object sender, EventArgs e) => LoadUrl(Project.Schema2Path);
    private void SearchButton_Click(object sender, EventArgs e) => LoadUrl(GoogleUrl);

    private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            LoadUrl(urlTextBox.Text);
        }
    }

    private void btnBookmarks_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
        var url = e.ClickedItem.Tag as string;
        if (string.IsNullOrEmpty(url)) return;

        btnBookmark.Image = Properties.Resources.star_full;
        LoadUrl(url!);
    }

    private void DocMenuItemClick(object sender, EventArgs e)
    {
        if (sender is not ToolStripMenuItem o) return;

        var url = o.Tag as string;
        if (string.IsNullOrEmpty(url)) return;

        if (url!.StartsWith("R:"))
        {
            url = "file://" + Path.Combine(Project.ResourcesPath, url.Substring(2)).Replace("\\", "/");
        }

        LoadUrl(url);
    }

    #endregion

    #region private methods

    public void LoadUrl(string url)
    {
        if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute) || url.StartsWith("file"))
        {
            Activate();
            _browser.Load(url);
        }
    }

    private void CheckIfBookmarked(string url)
    {
        var exists = Project.Bookmarks.Any(b => b.Url == url);
        btnBookmark.Image = exists ? Properties.Resources.star_full : Properties.Resources.star_empty;
    }

    private void btnBookmark_Click(object sender, EventArgs e)
    {
        var url = urlTextBox.Text.Trim();

        var bookmark = Project.Bookmarks.FirstOrDefault(b => b.Url == url);

        if (bookmark is null)
        {
            AddBookmark(url);
        }
        else
        {
            RemoveBookmark(bookmark);
        }
    }

    private void AddBookmark(string url)
    {
        var (ok, value) = TextPromptDialog.ShowDialog("Add Bookmark", "Enter a name for the bookmark");
        if (!ok) return;

        var bookmark = new Bookmark
        {
            Name = value,
            Url = url,
        };

        Project.Bookmarks.Add(bookmark);

        LoadBookmarks();

        btnBookmark.Image = Properties.Resources.star_full;
    }

    private void RemoveBookmark(Bookmark bookmark)
    {
        var msg = $"Are you sure you want to remove the bookmark '{bookmark.Name}'?";
        var result = OkCancelDialog.ShowDialog("Remove Bookmark", msg);

        if (!result) return;

        Project.Bookmarks.Remove(bookmark);
        btnBookmarks.DropDownItems.RemoveByKey(bookmark.Name);

        btnBookmark.Image = Properties.Resources.star_empty;
    }
    
    private void SetCanGoBack(bool canGoBack)
        => this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);

    private void SetCanGoForward(bool canGoForward)
        => this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);

    private void SetIsLoading(bool isLoading)
    {
        goButton.Text = isLoading ?
            "Stop" :
            "Go";
        
        goButton.Image = isLoading ?
            Properties.Resources.nav_plain_red :
            Properties.Resources.nav_plain_green;

        _browser.Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
        
        InitializeToolStripLayout();
    }

    private void LoadBookmarks()
    {
        btnBookmarks.DropDownItems.Clear();

        List<ToolStripItem> items = [];

        var size = new Size(180, 22);

        foreach (var bookmark in Project.Bookmarks.OrderBy(b => b.Name))
        {
            var item = new ToolStripMenuItem
            {
                Text = bookmark.Name,
                Name = bookmark.Name,
                Size = size,
                Tag = bookmark.Url,
            };

            items.Add(item);
        }

        btnBookmarks.DropDownItems.AddRange(items.ToArray());
    }

    #endregion

    #region message handlers

    private void OnOpenProject(OpenProjectMessage obj) => LoadBookmarks();
    private void OnShowUrl(ShowUrlMessage msg) => LoadUrl(msg.Url);
    private void OnResizePage(ResizePageMessage msg) => InitializeToolStripLayout();

    private void OnStartup(StartupMessage msg)
    {
        if (ViewType == ViewType.General) return;
        Task.Delay(500).ContinueWith(_ => _browser.GetDevToolsClient().Emulation.SetAutoDarkModeOverrideAsync(true));
    }

    private void OnShutdown(ShutdownMessage msg)
    {
        _browser.Dispose();
        Cef.Shutdown();
    }

    #endregion

    #region browser event handlers

    private void OnBrowserPage_Resize(object sender, EventArgs e)
        => InitializeToolStripLayout();

    private void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs args)
    {
        if (args.Frame.IsMain)
        {
            args.Browser.MainFrame.ExecuteJavaScriptAsync("document.body.style.overflow = 'visible'");
        }
    }

    private void OnBrowserLoadError(object sender, LoadErrorEventArgs e)
    {
        const string error = "<html><body><h2>Failed to load URL {0} with error {1} ({2}).</h2></body></html>";
        
        if (e.ErrorCode == CefErrorCode.Aborted) return;

        var msg = string.Format(error, e.FailedUrl, e.ErrorText, e.ErrorCode);
        _ = e.Browser.SetMainFrameDocumentContentAsync(msg);

        this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = e.FailedUrl);
    }

    private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
    {
        var browser = (ChromiumWebBrowser)sender;
        this.InvokeOnUiThreadIfRequired(() => browser.Focus());
    }

    private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
    {
        SetCanGoBack(args.CanGoBack);
        SetCanGoForward(args.CanGoForward);

        this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
    }

    private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        => this.InvokeOnUiThreadIfRequired(() => Text = args.Title);

    private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
    {
        this.InvokeOnUiThreadIfRequired(() =>
        {
            urlTextBox.Text = args.Address;
            CheckIfBookmarked(urlTextBox.Text);
        });
    }

    #endregion

    #region initializers

    private void InitializeView()
    {
        if (!Context.Ready) return;

        Subscribe<ShowUrlMessage>(OnShowUrl);
        Subscribe<ShutdownMessage>(OnShutdown);
        Subscribe<OpenProjectMessage>(OnOpenProject);
        Subscribe<StartupMessage>(OnStartup);
        Subscribe<ResizePageMessage>(OnResizePage);
        
        if (ViewType == ViewType.General)
        {
            ResourceButton.Visible = false;
            LoadUrl(Project.Schema2Path);
        }
        else
        {
            SchemaButton.Visible = false;
            ShowTablesButton.Visible = false;
            FirstSeparator.Visible = false;
            btnBookmark.Visible = false;
            btnBookmarks.Visible = false;
            ResourceButton.Visible = true;
            LoadUrl(GoogleUrl);
        }
    }

    private void InitializeBrowser()
    {
        panel1.Controls.Add(_browser);

        _browser.Dock = DockStyle.Fill;
        _browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
        _browser.LoadingStateChanged += OnLoadingStateChanged;
        _browser.TitleChanged += OnBrowserTitleChanged;
        _browser.AddressChanged += OnBrowserAddressChanged;
        _browser.LoadError += OnBrowserLoadError;
        _browser.BrowserSettings = new BrowserSettings();
        _browser.FrameLoadEnd += OnBrowserFrameLoadEnd;
    }

    private void InitializeTheme()
    {
        WindowThemer.SetDarkModeByForce(Handle);
        ToolStripRender.Configure(tsWeb);

        _browser.BrowserSettings.BackgroundColor = Cef.ColorSetARGB(0, 100, 100, 100);
        _browser.BackColor = Color.FromArgb(100, 100, 100);
        urlTextBox.BackColor = Color.FromArgb(100, 100, 100);
    }

    private void InitializeToolStripLayout()
    {
        var width = tsWeb.Width;

        foreach (ToolStripItem item in tsWeb.Items)
        {
            if (item != urlTextBox)
            {
                width -= item.Width - item.Margin.Horizontal;
            }
        }

        urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 30);
    }

    #endregion
}
