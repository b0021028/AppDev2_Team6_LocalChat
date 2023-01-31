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

            if (Regex.IsMatch(input_address, @"[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}"))
            {
                string address = input_address;
                // データベースに追加するのか？わからん
                DataManager.AddData(?, address, ?, ?);
                // それともリスト?
                // 宛先はリストっぽいやつに
                LocalChat.MainForm.UpdatePartnersList(address);
            }
            else
            {
                
            }
        }

        // 宛先取得
        public void GetPartners()
        {

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
