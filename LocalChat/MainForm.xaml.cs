﻿using System;
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
            Intialize();
        }




        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Intialize()
        {
            DataManager.InitializeData();
            Connectioner.StartListen();
            Partners.EvAddDestination += (sender,e) => { UpdatePartnersList(); };
            EvInitialize(this, true);


        }


        /// <summary>
        /// メッセージ追加
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
            Connectioner.StopListen();
            DataManager.InitializeData();
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
                    var pLabel = new Label();
                    pLabel.Content = messagedata.ip;
                    grid.Children.Add(pLabel);
                }

                var tLabel = new Label();
                tLabel.Content = messagedata.time;
                grid.Children.Add(tLabel);

                var mLabel = new Label();
                mLabel.Content = messagedata.message;
                grid.Children.Add(mLabel);
                

                grid.Width = double.NaN;
                





                this.DisplayMessage.Children.Add(grid);

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


        public new void Close()
        {
            EndLocalChatCore();
        }



    }
}
