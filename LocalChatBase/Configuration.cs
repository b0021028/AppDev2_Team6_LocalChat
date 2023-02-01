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
        public static event EventHandler<string> EvConfigChange = (x, y)=>{};
        private static Config s_config = new Config() {Notification=true };
        private static Config s_defaltConfig = new Config() { Notification=true};

        public static string Notification = "Notification";
        const string FILEPATH = "config.json";

        /// <summary>
        /// 現在の設定を設定ファイル(config.json)に書き込む
        /// </summary>
        /// <returns></returns>
        public static bool OutputConfigFile()
        {
            var t = JsonConvert.SerializeObject(s_config.Clone());
            File.WriteAllText(FILEPATH, t);
            return false;
        }
        /// <summary>
        /// 設定ファイル(config.json)から設定を読み込む
        /// </summary>
        /// <returns></returns>
        public static bool LoadConfigFile()
        {
            try
            {
                using (StreamReader file = File.OpenText(FILEPATH))
                {
                    var serializer = new JsonSerializer();
                    // Json.netのオブジェクトを作成します
                    // デシリアライズ関数に
                    // 読み込んだファイルと、データ用クラスの名称(型)を指定します。
                    // 
                    // デシリアライズされたデータは、自動的にaccountの
                    // メンバ変数に格納されます 
                    //
                    string reader = new JsonTextReader(file).ToString()??"{}";
                    Config jsonData = JsonConvert.DeserializeObject<Config>(reader)??s_config.Clone();

                    s_config.Notification = jsonData.Notification;
                }

            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 現在の設定をすべて取得
        /// </summary>
        /// <returns></returns>
        public static Config GetConfig()
        {
            return s_config.Clone();
        }


        /// <summary>
        /// keyの設定を変更する。エラーの場合は差し戻す
        /// </summary>
        /// <param name="key" cref="string">設定の項目</param>
        /// <param name="value">設定の値</param>
        /// <returns></returns>
        public static bool ChangeConfig(string key, object value)
        {
            if (key == Notification)
            {
                try
                {
                    var b = System.Convert.ToBoolean(value);
                    s_config.Notification = b;
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
        /// <returns></returns>
        public static bool ChangeConfig(int notification)
        {

            return false;
        }

        /// <summary>
        /// 設定の項目及び項目の変域を出力 未実装
        /// </summary>
        /// <returns></returns>
        public static bool DetailConfig()
        {
            return false;
        }

        /// <summary>
        /// デフォルト設定の項目と値を取得する 未実装
        /// </summary>
        /// <returns>JsonDocument</returns>
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
