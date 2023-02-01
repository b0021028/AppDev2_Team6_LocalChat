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


        // 現在の送信先数
        private int ip_num = 0;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Intialize()
        {

            var Partners_List = new List<string>();
            DataManager.InitializeData();
            Connectioner.StartListen();
            //Connectioner.EvStartSession += ;
        }

        /// <summary>
        /// メッセージ追加
        /// </summary>
        public void AddMessage()
        {
            Messenger.ReferenceMessage();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void EndLocalChatCore()
        {
            Connectioner.StopListen();
            DataManager.InitializeData();
            Configuration.OutputConfigFile();
            this.Close();
        }


        /// <summary>
        /// チャット更新
        /// </summary>
        public void UpdateChat()
        {
            DataManager.GetDatas(IP);
        }

        private Button[] PartnersButtons;

        /// <summary>
        /// 宛先リスト更新 
        /// </summary>
        /// <param name="address"></param>
        public void UpdatePartnersList(string address)
        {
            var partnersList = Partners.GetAddress(address);

            this.PartnersButtons[ip_num].Name = "PartnerButton" + ip_num;
            this.PartnersButtons[ip_num].Content = address;
            ip_num++;

            // コーユーの
            this.PartnersList.Children.Add(new Button());
        }

        /// <summary>
        /// チャット画面表示
        /// </summary>
        private Label[] MessageLabels;
        public void DisplayChat()
        {

            var Message_list = DataManager.GetDatas(IP);
            // ボタンのインスタンス作成(リスト分)
            this.MessageLabels = new Label[Message_list.Count];
            // メッセージ数分繰り返す
            for (int i = 0; i < Message_list.Count; i++)
                //for (int message_num = 0; i < this.MessageLabels.Length; i++)
                {
                    //ここに受信、送信側で位置の分岐を作りたい

                    this.MessageLabels[i] = new Label();

                    // コントロールのプロパティ
                    this.MessageLabels[i].Name = "MessageLabel" + i;
                    this.MessageLabels[i].Content = "Sample";
                    this.DisplayMessage.Children.Add(new Label());
                }
        }
            /// <summary>
            /// メッセージ送信
            /// </summary>
        public void SendMessage()
        {

        }

        //　イベント
        public event EventHandler<int> EvInitialize = (sender, args) => { };


        public event EventHandler<int> EvEnd = (sender, args) => { };






        //　ボタンイベント
        private void Button_Click(object sender, RoutedEventArgs e)
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
        /// メッセージ送信ボタンの送信イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var text = MessageBox.Text;
            if (text.Length != 0)
            {
                //現在の宛先取得===========================================================================
                var name = "";
                //送信
                Messenger.SendMessage("sampleメッセージ", name);

            }
        }




        // 新規追加画面に移動
        private void OpenAddNewPartnerForm(object sender, RoutedEventArgs e)
        {
            new AddNewPartnerForm().ShowDialog();
        }

        // 設定画面に移動
        private void OpenConfigForm(object sender, RoutedEventArgs e)
        {
            new ConfigForm().ShowDialog();
        }

        private void ScrollPartners(object sender, ContextMenuEventArgs e)
        {

        }
    }
}
