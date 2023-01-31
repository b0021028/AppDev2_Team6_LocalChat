using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat
{
    public partial class FormMain : FormMain
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "おしらせ";
            notifyIcon1.BalloonTipText = "おしらせのメッセージ";
            notifyIcon1.ShowBalloonTip(3000);
        }
        class Notifer
        {
        }
    }
}
