using LocalChatBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

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
            CleanupContent();
        }


        /// <summary>
        /// 仮実装 実際の動作とは異なります
        /// 設定項目の作成
        /// </summary>
        private void CleanupContent()
        {
            ConfigerContents.Children.Clear();
            var g = AddItemBase();

            var lbl = new Label();
            lbl.Content = "通知";


            var radio = new RadioButton();
            radio.Content = "On";

            var radio2 = new RadioButton();
            radio.Content = "Off";
            if (LocalChatBase.Configuration.GetConfig().Notification)
            {
                radio.IsChecked = true;
                radio2.IsChecked = false;
            }
            else
            {
                radio.IsChecked = false;
                radio2.IsChecked = true;
            }

            var grp = new WrapPanel();
            grp.Children.Add(radio);
            grp.Children.Add(radio2);

            // 登録
            g.Children.Add(lbl);
            g.Children.Add(grp);

            // コンフィグデータとして登録
            Func<object> a = ()=> radio.IsChecked;
            config.Add(LocalChatBase.Configuration.Notification, a);



        }

        /// <summary>
        /// 設定項目を表示するパネルを作成
        /// </summary>
        /// <returns>設定項目用グリッド 1行2列</returns>
        private Grid AddItemBase()
        {
            var g = new Grid();
            g.VerticalAlignment = VerticalAlignment.Top;
            g.HorizontalAlignment = HorizontalAlignment.Left;
            g.ColumnDefinitions.Add(new ColumnDefinition());
            g.ColumnDefinitions.Add(new ColumnDefinition());
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
                LocalChatBase.Configuration.ChangeConfig(conf.Key, conf.Value());

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
