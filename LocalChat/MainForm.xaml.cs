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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalChat
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        /// <summary>
        /// 初期化処理
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 宛先、メッセージの追加
        /// </summary>
        public void Intialize()
        {

        }

        /// <summary>
        /// メッセージ追加
        /// </summary>
        public void AddMessage()
        {

        }

        public void EndLocalChatCore()
        {

        }

        public void UpdateChat()
        {

        }

        public void UpdatePartnersList()
        {

        }

        public void DisplayChat()
        {

        }

        public void SendMessage()
        {

        }

        //　イベント
        public event EventHandler<int> EvInitialize = dummy;


        public event EventHandler<int> EvEnd = dummy;


        //　ボタンイベント
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserChange(object sender, RoutedEventArgs e)
        {

        }

        private void UserChange2(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// メッセージ送信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text
        }

        private void AddNewPartnerForm(object sender, RoutedEventArgs e)
        {

        }

        private void OpenAddNewPartnerForm(object sender, RoutedEventArgs e)
        {

        }
    }
}
