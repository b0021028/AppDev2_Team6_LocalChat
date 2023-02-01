using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
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
using LocalChatBase;


namespace LocalChat
{
    /// <summary>
    /// AddNewPartnerForm.xaml の相互作用ロジック
    /// </summary>
    public partial class AddNewPartnerForm : Window
    {
        
        /// <summary>
        /// OKボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // テキストボックスから、ボックス内の値を取得
            string textValue = @"NewAddTextBox.Text";

            IPAddress ipaddr;

            bool ret = IPAddress.TryParse(textValue, out ipaddr);
            if(!ret) {
                MessageBox.Show("IPアドレスを読み込めませんでした。もう一度IPアドレスをご確認ください。",
                    "エラー",
                    MessageBoxButton.OK);

            }

            /// <summary>
            /// データベースの欄にこのIPアドレスを追加する(データベースの方にアドレスの値を渡す)
            /// </summary>
            var sqlConnection = new SQLiteConnectionStringBuilder { DataSource = "temptable.db" };

            using (var cn = new SQLiteConnection(sqlConnection.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    string.Format("", ipaddr);
                    cmd.Inserttemptable(string.Format("{}, NULL, NULL, NULL", ipaddr));
                }
            }

        }

        /// <summary>
        /// キャンセルボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
