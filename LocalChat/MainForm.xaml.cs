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
            var ip_num = 0;
            DataManager.InitializeData();
            Connectioner.StartListen();
            Connectioner.EvStartSession += ;
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
            DataManager.IntializeData();
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
            {
                InitializeComponent();
                PartnersList = Partners.GetAddress();
            }
            /*public Form1()
            {
                InitializeComponent();
            }
            */
            /*
            private void button1_Click(object sender, EventArgs e)
            {
                if (this.manyButtons != null)
                {
                    MessageBox.Show("フォームはすでに表示されています");
                    return;
                }
            */
            // ボタンのインスタンス作成()
            /*
            this.manyButtons = new Button[5];
            for (int i = 0; i < this.manyButtons.Length; i++)
            {
                this.manyButtons[i] = new Button();

                // コントロールのプロパティ
                this.manyButtons[i].Name = "OriginalButton" + i;
                this.manyButtons[i].Text = "ボタン" + i;
                this.manyButtons[i].Location = new Point(10, 10 + i * 22);
                this.manyButtons[i].Size = new Size(80, 20);

                // フォームへの追加
                this.Controls.Add(this.manyButtons[i]);
            }
        }
            */

            this.PartnersButtons[ip_num].Name = "PartnersButton" + ip_num;
            this.PartnersButtons[ip_num].Text = address;
            this.PartnersButtons[ip_num].Location = new Point(10, 10 + ip_num * 22);
            this.PartnersButtons[ip_num].Size = new Size(80, 20);
            ip_num += 1;
        }


        private Label[] MessageLabels;
        public void DisplayChat(Message)
        {
            {
                InitializeComponent();
                this.MessageLabels = null;
            }
            /*public Form1()
            {
                InitializeComponent();
            }
            */
            /*
            private void button1_Click(object sender, EventArgs e)
            {
                if (this.MessageLabels != null)
                {
                    MessageBox.Show("フォームはすでに表示されています");
                    return;
                }
            */
                // ボタンのインスタンス作成(5つ)
                this.MessageLabels = new Label[5];
                for (int i = 0; i < this.MessageLabels.Length; i++)
                {
                    this.MessageLabels[i] = new Label();

                    // コントロールのプロパティ
                    this.MessageLabels[i].Name = "MessageLabel" + i;
                    this.MessageLabels[i].Text = ;
                    this.MessageLabels[i].Location = new Point(10, 10 + i * 22);
                    this.MessageLabels[i].Size = new Size(80, 20);

                    // フォームへの追加
                    this.Controls.Add(this.MessageLabels[i]);
                }
            }
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
                Messenger.SendMessage();

            }
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
