using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LocalChatBase
{
    [JsonObject()]
    public class Config
    {
        [JsonProperty("Notification")]
        public bool Notification { get; set; }
        public Config Clone()
        {
            return new Config() { Notification = this.Notification };
        }
    }

    public static class Configuration
    {
        /// <summary>
        /// イベントハンドラー 設定が変更された時
        /// </summary>
        public static event EventHandler<string> EvConfigChange = (sender, args) => { };
        private static Config s_config = new Config() {Notification=true };
        private static Config s_defaltConfig = new Config() { Notification=true};

        public static string Notification = "Notification";
        const string FILEPATH = "config.json";

        /// <summary>
        /// 現在の設定を設定ファイル(config.json)に書き込む
        /// </summary>
        /// <returns>成功したか</returns>
        public static bool OutputConfigFile()
        {
            try
            {
                var t = JsonConvert.SerializeObject(s_config.Clone());
                File.WriteAllText(FILEPATH, t);

            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 設定ファイル(config.json)から設定を読み込む
        /// </summary>
        /// <returns>読込みに成功したか</returns>
        public static bool LoadConfigFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(FILEPATH, System.Text.Encoding.UTF8))
                {
                    // テキスト取り出し
                    var jsonData = sr.ReadToEnd();

                    // 型嵌め
                    Config conf = JsonConvert.DeserializeObject<Config>(jsonData)??s_config.Clone();

                    // 設定変更する
                    s_config.Notification = conf.Notification;
                }

            }
            catch
            {
                s_config = s_defaltConfig.Clone();
                OutputConfigFile();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 現在の設定をすべて取得
        /// </summary>
        /// <returns>コンフィグ</returns>
        public static Config GetConfig()
        {
            return s_config.Clone();
        }


        /// <summary>
        /// keyの設定を変更する。エラーの場合は差し戻す
        /// </summary>
        /// <param name="key" cref="string">設定の項目</param>
        /// <param name="value">設定の値</param>
        /// <returns>成功したか</returns>
        public static bool ChangeConfig(string key, object value)
        {
            if (key == Notification)
            {
                try
                {
                    var b = System.Convert.ToBoolean(value);
                    s_config.Notification = b;
                    OutputConfigFile();
                }
                catch
                {
                    return false;
                }
                EvConfigChange(null, key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 非推奨 動作確認前未実装
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>特定のコンフィグの変更に成功したか</returns>
        public static bool ChangeConfig(int notification)
        {

            return false;
        }

        /// <summary>
        /// 設定の項目及び項目の変域を出力 未実装
        /// </summary>
        /// <returns>[設定項目 変域]かな？</returns>
        public static dynamic DetailConfig()
        {
            return false;
        }

        /// <summary>
        /// デフォルト設定の項目と値を取得する 未実装
        /// </summary>
        /// <returns>JsonDocument? 辞書かリスト</returns>
        public static dynamic GetDefaultConfig()
        {
            return ("{}");

        }

        /// <summary>
        /// デフォルト設定に変更する 未実装
        /// </summary>
        /// <returns>成功したか</returns>
        public static bool DefaultConfig()
        {
            return false;
        }
    }
}
