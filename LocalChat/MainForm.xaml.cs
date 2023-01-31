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
using LocalChatBase;

namespace LocalChat
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Intialize()
        {

            var Partners_List = new List<string>();
            var Button_num = 0;
            LocalChatBase.DataManager.InitializeData();
            
        }

        /// <summary>
        /// メッセージ追加
        /// </summary>
        public void AddMessage()
        {
            LocalChatBase.Message.ReferenceMessage();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void EndLocalChatCore()
        {

        }


        /// <summary>
        /// チャット更新
        /// </summary>
        public void UpdateChat()
        {
            LocalChatBase.DataManager.GetDatas(IP);
        }

        private Button[] manyButtons;

        /// <summary>
        /// 宛先リスト更新 
        /// </summary>
        /// <param name="address"></param>
        public void UpdatePartnersList(string address)
        {

            Partners_List.Add(address);
            this.manyButtons[Button_num].Name = "PartnersButton" + i;
            this.manyButtons[Button_num].Text = Partners_List[i];
            this.manyButtons[Button_num].Location = new Point(10, 10 + i * 22);
            this.manyButtons[Button_num].Size = new Size(80, 20);
            Button_num += 1;
        }

        public void DisplayChat()
        {

        }

        /// <summary>
        /// メッセージ送信
        /// </summary>
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
            LocalChatBase.Message.SendMessage();
        }




        // 新規追加画面に移動
        private void OpenAddNewPartnerForm(object sender, RoutedEventArgs e)
        {
            AddNewPartnerForm.ShowDialog(this);
        }

        // 設定画面に移動
        private void OpenConfigForm(object sender, RoutedEventArgs e)
        {
            ConfigForm.ShowDialog(this);
        }

        private void ScrollPartners(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
