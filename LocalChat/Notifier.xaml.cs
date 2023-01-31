using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            message
        }

        async public new void Show()
        {
            base.Show();
            await Task.Delay(100);
            this.Close();
        }
    }
}
