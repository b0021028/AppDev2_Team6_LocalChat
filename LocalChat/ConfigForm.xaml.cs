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
        private ObservableCollection<int> testData { get; set; } = new ObservableCollection<int>();

        // 設定画面生成
        public ConfigForm()
        {
            InitializeComponent();
            
        }


        private void AddItemRadioButton(dynamic? args)
        {
            testData.Add(args);

        }
        private void AddItemSlider(dynamic? args)
        {
            testData.Add(args);

        }
        private void AddItemLabel(dynamic? args)
        {
            testData.Add(args);

        }




        public void ApplyConfig()
        {

            LocalChatBase.Configuration.ChangeConfig(LocalChatBase.Configuration.Notification, "true" );

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyConfig();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
