﻿using LocalChatBase;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LocalChat
{
    /// <summary>
    /// ConfigForm.xaml の相互作用ロジック
    /// 設定画面
    /// </summary>
    public partial class ConfigForm : Window
    {
        /// <summary>
        /// コンフィグデータ
        /// </summary>
        private Dictionary<string, Func<object>> config = new Dictionary<string, Func<object>>();


        /// <summary>
        /// 設定画面生成
        /// </summary>
        public ConfigForm()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            this.Title = "設定画面";
            CleanupContent();
        }


        /// <summary>
        /// 仮実装 実際の動作とは異なります
        /// 設定項目の作成
        /// </summary>
        private void CleanupContent()
        {
            ConfigerContents.Children.Clear();

            // Notificationテスト実装 本来はfor などでやろうとした
            if (true)
            {


                // 説明欄設定
                var lbl = new Label();
                lbl.Content = "通知";


                // Radioボタン設定
                var radio = new RadioButton();
                radio.Content = "On";

                var radio2 = new RadioButton();
                radio2.Content = "Off";
                if (Configuration.GetConfig().Notification)
                {
                    radio.IsChecked = true;
                    radio2.IsChecked = false;
                }
                else
                {
                    radio.IsChecked = false;
                    radio2.IsChecked = true;
                }
                var grp = new StackPanel();
                grp.Orientation = Orientation.Horizontal;
                grp.Children.Add(radio);
                grp.Children.Add(radio2);

                // 画面に登録
                var g = AddItemBase();
                g.Children.Add(lbl);
                g.Children.Add(grp);

                // コンフィグデータとして登録
                Func<object> a = () => radio.IsChecked;
                config.Add(Configuration.Notification, a);

            }

        }

        /// <summary>
        /// 設定項目を表示するパネルを作成
        /// </summary>
        /// <returns>設定項目用グリッド 1行2列</returns>
        private StackPanel AddItemBase()
        {
            var g = new StackPanel();
            g.Width = double.NaN;
            //g.Background = new SolidColorBrush(Colors.Black);
            g.VerticalAlignment = VerticalAlignment.Top;
            /*var col = new ColumnDefinition();
            col.Width = GridLength.Auto;
            g.ColumnDefinitions.Add(col);
            col = new ColumnDefinition();
            col.Width = GridLength.Auto;
            g.ColumnDefinitions.Add(col);//*/
            g.Orientation = Orientation.Horizontal;
            ConfigerContents.Children.Add(g);
            return g;
        }


        /// <summary>
        /// 各項目の設定を適用する
        /// </summary>
        public void ApplyConfig()
        {
            foreach (var conf in config)
            {
                Configuration.ChangeConfig(conf.Key, conf.Value());

            }
        }

        /// <summary>
        /// オッケーが押された時 ApplyConfigする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyConfig();
            this.Close();
        }

        /// <summary>
        /// キャンセルが押された時 Closeする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
