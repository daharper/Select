using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Select.Messaging;
using Select.Models;
using Select.Presentation.Utils;
using Select.Presentation.Views;
using Select.Rtti;
using WeifenLuo.WinFormsUI.Docking;

namespace Select.Presentation
{
    using View = Views.View;

    public partial class MainForm : Form
    {
        #region private

        private static Project Project => Context.Get<Project>();

        #endregion

        public MainForm()
        {
            InitializeCef();
            InitializeComponent();
            InitializeTheme();
        }

        #region private methods

        private void DockView<T>(DockPanel? panel = null, DockState state = DockState.Document) where T : View, new()
            => DockView(new T(), panel, state);

        private void DockView(View view, DockPanel? panel = null, DockState state = DockState.Document) 
        {
            panel ??= MainPanel;

            if (panel.Panes.Count > 0)
            {
                panel.Panes.First().Activate();
            }

            view.Show(panel, state);
        }

        #endregion

        #region event handlers

        private void MainForm_ResizeEnd(object sender, EventArgs e)
            => AppMessageBus.Send<ResizePageMessage>();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppMessageBus.Send<CloseProjectMessage>();
            Thread.Sleep(1000);
            
            AppMessageBus.Send<ShutdownMessage>();
            Project.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RightHostPanel.Width = Width / 2 - 100;

            var browser = new BrowserView(ViewType.General);
            var browser2 = new BrowserView(ViewType.Resource);

            DockView<QueryView>(RightPanel);
            DockView(browser, MainPanel);
            DockView(browser2, MainPanel);
            
            browser.Activate();

            AppMessageBus.Subscribe<NewQueryMessage>(_ => DockView<QueryView>(RightPanel));
            AppMessageBus.Subscribe<NewWebMessage>(_ => DockView(new BrowserView(ViewType.Resource), MainPanel));
            
            AppMessageBus.Send<StartupMessage>();
        }

        #endregion

        #region initializers

        private void InitializeTheme() 
            => WindowThemer.SetDarkModeByForce(Handle);

        private void InitializeCef()
        {
            var settings = new CefSettings
            {
                CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 /CefSharp Browser" + Cef.CefSharpVersion,
                IgnoreCertificateErrors = true,
                Locale = "en-US",
                PersistSessionCookies = true,
            };
            settings.CefCommandLineArgs.Add("persist-user-preferences");
                        
            Cef.Initialize(settings);
        }

        #endregion
    }
}
