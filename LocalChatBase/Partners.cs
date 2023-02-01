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
        public static event EventHandler<string> EvAddDestination = (sender, args) => { };

        /// <summary>
        /// 宛先を登録している辞書
        /// </summary>
        private static Dictionary<IPAddress, string> partners = new Dictionary<IPAddress, string>();

        /// <summary>
        /// 宛先の追加登録 とりあえずそのままIPアドレスに変換する
        /// </summary>
        /// <param name="partner">とりあえずstring のIPv4アドレス</param>
        public static void AddPartners(string partner)
        {
            // sintax チェック
            if (Regex.IsMatch(partner, @"[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}"))
            {
                var ip = IPAddress.Parse(partner);
                // 許可されたIPか確認
                if (checkip(ip))
                {
                    // ipアドレスを追加登録
                    if (partners.TryAdd(ip, partner))
                    {
                        // イベント発行
                        EvAddDestination(null, partner);
                    }
                    return;
                }
            }
        }


        /// <summary>
        /// 宛先をすべて取得する
        /// </summary>
        /// <returns>宛先が入った ICollectionで返す</returns>
        public static ICollection<string> GetPartners()
        {
            return partners.Values;
        }


        /// <summary>
        /// 宛先のアドレス取得
        /// </summary>
        /// <param name="name">宛先の名前</param>
        /// <returns>IPアドレス</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
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
        /// <param name="ip">チェックするipアドレス</param>
        /// <returns>とりあえずtrue</returns>
        private static bool checkip(IPAddress ip)
        {
            return true;
        }

    }
}
