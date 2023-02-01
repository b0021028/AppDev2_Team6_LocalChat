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
        /// 宛先リスト更新 
        /// </summary>
        /// <param name="address"></param>
                // 現在の送信先数
        private Button[] PartnerButtons;
        public void UpdatePartnersList()
        {
            var partnersList = Partners.GetPartners();
            {
                foreach (var partner in partnersList)
                {
                    var Button_num = new Button();
                    //Button_num.Name = "PartnerButton" + ;
                    Button_num.Content = partner;
                    Button_num.Click += (o, e)=> { if (o != null) { DisplayChat(((Button)o).Content); } };
                    PartnersList.Children.Add(Button_num);
                }
            }

        }

        /// <summary>
        /// チャット画面表示
        /// </summary>
        private Label[] MessageLabels;
        public void DisplayChat(string selectedPartner)
        {
            // チャット画面リセット処理
            DisplayMessage.Children.Clear();
            ChatTitle.Content = selectedPartner;

            // var Message_list = DataManager.GetDatas(IP);
            var Message_list = LocalChatBase.Messenger.ReferenceMessage(selectedPartner);

            // メッセージ数分繰り返す
            foreach (var message in Message_list)
            {
                var Label_num = new Label();

                // コントロールのプロパティ
                // Label_num.Name = "MessageLabel" + i;
                Label_num.Content = message;
                

                if (message.receptionFlag)
                //ここに受信、送信側で位置の分岐を作りたい、メッセージラベルを自分が右、相手が左に表示したい
                {
                    this.DisplayMessage.Children.Add(new Label());
                }

            }






            // チャット画面更新処理
            UpdateChat();


        }

        /// <summary>
        /// チャット更新
        /// </summary>
        public void UpdateChat()
        {
            DataManager.GetDatas(IP);
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


        /// <summary>
        /// メッセージ送信ボタンの送信イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var text = MessageText.Text;
            if (text.Length != 0)
            {
                // string massage = TextBox.Text;
                //現在の宛先取得===========================================================================
                var name = "";
                //送信
                Messenger.SendMessage(text, name);

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
