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
using Org.BouncyCastle.Asn1.Ocsp;

namespace LocalChat
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {

        /// <summary>
        /// 初期化された後発火する
        /// </summary>
        public event EventHandler<bool> EvInitialize = (sender, args) => { };

        /// <summary>
        /// 終了処理がされた後発火する
        /// </summary>
        public event EventHandler<int> EvEnd = (sender, args) => { };

        /// <summary>
        /// メイン画面 チャットを見れます
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.Title = "Local Chat (ローカルチャット)";
            Intialize();
        }




        /// <summary>
        /// 初期化処理 元内部コアと外部コア処理
        /// </summary>
        public void Intialize()
        {
            // データ保存管理の初期化
            DataManager.InitializeData(null);

            // コンフィグ読込み
            Configuration.LoadConfigFile();

            // イベント登録 宛先が追加された時 -> GUI宛先リスト更新
            Partners.EvAddDestination += (sender,e) => { UpdatePartnersList(); };

            // イベント登録 通信セッション開始 -> データ受信時 -> メッセージ受け取り
            Connectioner.EvStartSession += (sender, e) => {e.EvReception += Messenger.ReceptionMessage;e.StartReception(); };

            // イベント登録 データ保存処理
            Messenger.EvReceptionMessage += (sender, e) => { DataManager.AddData(e.receptionFlag, e.ip, e.time, e.message); };

            // イベント登録 新規データ取得時チャット画面更新
            DataManager.EvAddData += (sender, e)=> { UpdateChat(); };

            // 受信待ち受け開始
            Connectioner.StartListen();
            EvInitialize(this, true);


        }


        /// <summary>
        /// メッセージ追加 未実装
        /// </summary>
        public void AddMessage()
        {
         //   Messenger.ReferenceMessage();
        }


        /// <summary>
        /// 終了処理
        /// </summary>
        public void EndLocalChatCore()
        {
            // 待ち受け終了
            Connectioner.StopListen();
            // DataManager初期化
            DataManager.InitializeData();
            // コンフィグ保存
            Configuration.OutputConfigFile();
            EvEnd(this, 0);
            base.Close();
        }




        /// <summary>
        /// 宛先リスト更新
        /// </summary>
        /// <param name="address"></param>
        public void UpdatePartnersList()
        {
            PartnersList.Children.Clear();
            var partnersList = Partners.GetPartners();
            {
                foreach (var partners in partnersList)
                {
                    var Button_num = new Button();
                    //Button_num.Name = "PartnerButton" + ;
                    Button_num.Content = partners;
                    Button_num.Click += (o, e) => { if (o != null) { var t = ((Button)o).Content.ToString(); if (t != null) { DisplayChat(t); } } };
                    PartnersList.Children.Add(Button_num);
                }
            }

        }

        /// <summary>
        /// チャット画面表示
        /// </summary>
        public void DisplayChat(string selectedPartner)
        {
            // チャット画面リセット処理
            DisplayMessage.Children.Clear();

            // タイトル変更
            ChatTitle.Content = selectedPartner;

            // メッセージ履歴取得
            // メッセージ数分繰り返す
            foreach (var messagedata in LocalChatBase.Messenger.ReferenceMessage(selectedPartner))
            {
                var grid = new Grid();


                if (messagedata.receptionFlag)
                //ここに受信、送信側で位置の分岐を作りたい、メッセージラベルを自分が右、相手が左に表示したい
                {
                    // 送信者 ipアドレス
                    var pLabel = new Label();
                    pLabel.Content = messagedata.ip;
                    grid.Children.Add(pLabel);
                }
                // タイムスタンプ
                var tLabel = new Label();
                tLabel.Content = messagedata.time;
                grid.Children.Add(tLabel);

                var mLabel = new Label();
                mLabel.Content = messagedata.message;
                grid.Children.Add(mLabel);


                grid.Width = double.NaN;


                DisplayMessage.Children.Add(grid);

            }



        }

        /// <summary>
        /// チャット更新 仮実装 チャット画面消去付き
        /// </summary>
        public void UpdateChat()
        {
            if (ChatTitle.Content != null)
            {
                var title = ChatTitle.Content.ToString();
                if (title != null && title != "")
                {
                    DisplayChat(title);
                }
            }
        }

        /// <summary>
        /// メッセージ送信
        /// </summary>
        public void SendMessage()
        {
            var partner = ChatTitle.Content.ToString();
            string message = MessageText.Text;
            if (partner != null && message.Trim('　',' ','\n')!="")
            {
                LocalChatBase.Messenger.SendMessage(message, partner);

            }

        }



        /// <summary>
        /// メッセージ送信ボタンの送信イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string text = MessageText.Text;
            // 文字数が0ではない
            if (text.Length != 0)
            {
                //現在の宛先取得
                string name = ChatTitle.Content.ToString()??"";
                if (name != "")
                {
                    //送信
                    Messenger.SendMessage(text, name);
                }

            }
        }




        // 新規追加画面に移動
        private void OpenAddNewPartnerForm(object sender, RoutedEventArgs e)
        {
            var child = new AddNewPartnerForm();
            child.Owner = this;
            child.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            child.ShowDialog();
        }

        // 設定画面に移動
        private void OpenConfigForm(object sender, RoutedEventArgs e)
        {
            var child = new ConfigForm();
            child.Owner= this;
            child.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            child.ShowDialog();
        }

        private void ScrollPartners(object sender, ContextMenuEventArgs e)
        {

        }


        public new void Close()
        {
            EndLocalChatCore();
        }

        private void MessageText_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (MessageText.Text == "")
            {
                MessageText.Background = null;
            }
            else
            {

                MessageText.Background = new SolidColorBrush(Colors.White);
            }

        }
    }
}
