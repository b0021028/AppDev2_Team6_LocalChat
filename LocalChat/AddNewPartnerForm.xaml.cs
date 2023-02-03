using LocalChatBase;
using System.Windows;
using System.Windows.Input;



namespace LocalChat
{
    //
    /// <summary>
    /// AddNewPartnerForm.xaml の相互作用ロジック
    /// </summary>
    public partial class AddNewPartnerForm : Window
    {
        /// <summary>
        /// 設定画面生成
        /// </summary>
        public AddNewPartnerForm()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            this.Title = "新規宛先追加";
            NewAddTextBox.Focus();
        }



        /// <summary>
        /// OKボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // テキストボックスから、ボックス内の値を取得
            string textValue = NewAddTextBox.Text.Trim(' ', '　', '\n');

            bool ret = Partners.AddPartners(textValue);
            if (!ret)
            {
                MessageBox.Show("IPアドレスを読み込めませんでした。もう一度IPアドレスをご確認ください。",
                    "エラー",
                    MessageBoxButton.OK);

            }
            else
            {
                this.Close();
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

        private void NewAddTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OKButton_Click(sender, e);
            }
        }

        private void NewSendToForm_Loaded(object sender, RoutedEventArgs e)
        {
            this.NewAddTextBox.Focus();
        }
    }
}
