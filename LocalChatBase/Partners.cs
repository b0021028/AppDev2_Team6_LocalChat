using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LocalChatBase
{
    /// <summary>
    /// 宛先とiPアドレスを管理するクラス
    /// </summary>
    public class Partners
    {
        /// <summary>
        /// 宛先追加イベント
        /// </summary>
        public static event EventHandler<string> EvAddDestination = (Sender, args) => { };

        public static Dictionary<IPAddress, string> partners=new Dictionary<IPAddress, string>;

        /// <summary>
        /// 宛先の構文が正しいか確認し追加
        /// </summary>
        /// <param name="partner"></param>
        public static void AddPartners(string partner)
        {

            if (Regex.IsMatch(partner, @"[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}"))
            {
                var ip = IPAddress.Parse(partner);
                if (checkip(ip))
                {
                    // ipアドレスを追加登録
                    partners.Add(ip, partner);
                    // イベント発行
                    EvAddDestination(null, partner);
                    return;
                }
            }
        }



        /// <summary>
        /// 宛先取得
        /// </summary>
        public static ICollection<string> GetPartners()
        {
            return partners.Values;
        }

        /// <summary>
        /// 宛先のアドレス取得
        /// </summary>
        public static IPAddress GetAddress(string name)
        {
            foreach(var partner in partners)
            {
                if (partner.Value == name)
                {
                    return partner.Key;
                }
            }
            throw new IndexOutOfRangeException();
;
        }


        /// <summary>
        /// ipアドレスをチェックする
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static bool checkip(IPAddress ip)
        {
            return true;
        }

    }
}
