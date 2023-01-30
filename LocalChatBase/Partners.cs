using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChatBase
{
    public class Partners
    {
        // 宛先追加
        public static void AddPartners(string address)
        {
            string ipaddress = address;
        }

        // 宛先取得
        public void GetPartners()
        {

        }

        // 宛先のアドレス取得
        public void GetAddress()
        {

        }

        // 宛先追加イベント
        public event EventHandler<string> EvAddDestination = (Sender, args) => { };
    }
}
