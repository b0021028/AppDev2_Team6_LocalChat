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
            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = System.Windows.SystemParameters.WorkArea.Width - Width;
            Top = System.Windows.SystemParameters.WorkArea.Height - Height;
            base.Show();
            await Task.Delay(3000);
            this.Close();
        }

    }
}
