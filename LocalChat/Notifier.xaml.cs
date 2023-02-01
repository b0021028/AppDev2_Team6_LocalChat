using LocalChatBase;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LocalChat
{
    /// <summary>
    /// Notifier.xaml の相互作用ロジック
    /// </summary>
    public partial class Notifier : Window
    {
        public Notifier()
        {
            InitializeComponent();
        }

        public Notifier(string message)
        {
            InitializeComponent();

            NotifierMessage.Content = message;
        }
        
        async public new void Show()
        {
            if (Configuration.GetConfig().Notification)
            {
                this.Focusable = false;
                ShowInTaskbar = false;
                //*
                ResizeMode = ResizeMode.NoResize;
                this.IsEnabled = false;
                var sysWidth = System.Windows.SystemParameters.WorkArea.Width;
                var sysHeight = System.Windows.SystemParameters.WorkArea.Height;
                Width = sysWidth/5;
                Height = sysHeight/4;
                WindowStartupLocation = WindowStartupLocation.Manual;
                Left = sysWidth - Width;
                Top = sysHeight - Height;
                //*/
                base.Show();
                await Task.Delay(3000);
            }
            this.Close();
        }

    }
}
