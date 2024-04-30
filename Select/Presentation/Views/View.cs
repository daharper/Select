using System;
using System.Drawing;
using Select.Messaging;
using Select.Models;
using Select.Rtti;

namespace Select.Presentation.Views
{
    public partial class View : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public View()
        {
            InitializeComponent();

            HideOnClose = true;
        }

        protected Color OriginalBackColor { get; } = Color.FromArgb(26, 26, 26);
        
        protected Project Project => Context.Get<Project>();

        protected void Send<T>() where T : AppMessage, new()
            => AppMessageBus.Send(new T());
        
        protected void Send<T>(T message) where T : AppMessage
            => AppMessageBus.Send(message);

        protected void Subscribe<T>(Action<T> handler) where T : AppMessage
            => AppMessageBus.Subscribe(handler);

        protected void Unsubscribe<T>(Action<T> handler) where T : AppMessage
            => AppMessageBus.Unsubscribe(handler);
    }
}
