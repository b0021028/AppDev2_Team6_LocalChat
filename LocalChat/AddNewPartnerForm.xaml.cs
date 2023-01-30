using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using LocalChatBase;

namespace LocalChat
{
    /// <summary>
    /// AddNewPartnerForm.xaml の相互作用ロジック
    /// </summary>
    public partial class AddNewPartnerForm : Window
    {
        public AddNewPartnerForm()
        {
            InitializeComponent();
        }

        private void NewAddTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /// <summary>
        /// OKボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var input_address = NewAddTextBox.Text;
            LocalChatBase.Partners.AddPartners(input_address);
        }

        /// <summary>
        /// キャンセルボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
