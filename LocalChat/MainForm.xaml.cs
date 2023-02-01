using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LocalChatBase;

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
            DataManager.InitializeData(true);

            // コンフィグ読込み
            Configuration.LoadConfigFile();

            // イベント登録 宛先が追加された時 -> GUI宛先リスト更新
            Partners.EvAddDestination += (sender,e) => { UpdatePartnersList(); };

            // イベント登録 通信セッション開始 -> データ受信時 -> メッセージ受け取り
            Connectioner.EvStartSession += (sender, e) => {e.EvReception += Messenger.ReceptionMessage;e.StartReception(); };

            // イベント登録 データ保存処理
            Messenger.EvReceptionMessage += (sender, e) => { DataManager.AddData(e.receptionFlag, e.ip, e.time, e.message); };

            // イベント登録 データ保存処理
            Messenger.EvSendMessageSuccess += (sender, e) => { DataManager.AddData(e.receptionFlag, e.ip, e.time, e.message); };

            // イベント登録 新規データ取得時チャット画面更新 動いてない
            DataManager.EvAddData += (sender, e)=> { this.Dispatcher.Invoke(UpdateChat); };

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
            foreach (var messagedata in Messenger.ReferenceMessage(selectedPartner))
            {
                var stack = new StackPanel();


                //ここに受信、送信側で位置の分岐を作りたい、メッセージラベルを自分が右、相手が左に表示したい
                if (messagedata.receptionFlag)
                {
                    // 送信者 ipアドレス
                    var pLabel = new Label();
                    pLabel.Content = messagedata.ip;
                    stack.Children.Add(pLabel);
                }
                // タイムスタンプ
                var tLabel = new Label();
                tLabel.Content = messagedata.time;
                stack.Children.Add(tLabel);

                var mLabel = new Label();
                mLabel.Content = messagedata.message;
                stack.Children.Add(mLabel);


                stack.Width = double.NaN;


                DisplayMessage.Children.Add(stack);

            }



        }

        /// <summary>
        /// チャット更新 仮実装 チャット画面消去付き
        /// </summary>
        public void UpdateChat()
        {
            if (ChatTitle.Content != null)
            {
                string title = ChatTitle.Content.ToString()?? "";
                if (title != null && title != "")
                {
                    DisplayChat(title);
                }
            }
            MessageBox.Show("jgariosgioarhiouhg");
        }

        /// <summary>
        /// メッセージ送信
        /// </summary>
        async public Task SendMessage()
        {
            sendButton.IsEnabled = false;
            //現在の宛先取得
            string name = ChatTitle.Content.ToString()??"";
            string text = MessageText.Text;
            // 文字数が0ではない
            if (name != "" && text.Trim('　',' ','\n','\t').Length != 0)
            {
                //送信
                var a = await Messenger.SendMessage(text, name).WaitAsync(TimeSpan.FromSeconds(10));
                var t = a;
                MessageText.Text = "";
            }
            sendButton.IsEnabled = true;

        }



        /// <summary>
        /// メッセージ送信ボタンの送信イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
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
