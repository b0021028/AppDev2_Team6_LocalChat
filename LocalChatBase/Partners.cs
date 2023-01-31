using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LocalChat;

namespace LocalChatBase
{
    public class Partners
    {
        // 宛先追加
        public static void AddPartners(string input_address)
        {
            // ipアドレスが入力されているか
            if (Regex.IsMatch(input_address, @"[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}"))
            {
                string address = input_address;
                // リストにipアドレスを追加
                LocalChat.MainForm.UpdatePartnersList(address);
            }
            else
            {
                MessageBox.Show("正しい値を入力してください。");
            }
        }

        // 宛先取得
        public void GetPartners()
        {
            LocalChat.MainForm.UpdatePartnersList();
        }

        // 宛先のアドレス取得
        public void GetAddress()
        {
            DataManager.GetData();
        }

        // 宛先追加イベント
        public event EventHandler<string> EvAddDestination = (Sender, args) => { };
    }
}
