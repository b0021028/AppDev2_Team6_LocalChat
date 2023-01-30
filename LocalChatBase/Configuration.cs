using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LocalChatBase
{
    public static class Configuration
    {
        public static string Notification = "Notification";
        /// <summary>
        /// イベントハンドラー 設定が変更された時
        /// </summary>
        public static event EventHandler<string> EvConfigChange = (x, y)=>{};
        public static JsonDocument config = JsonSerializer.SerializeToDocument("{\"Notification}\":true");

        /// <summary>
        /// 現在の設定を設定ファイル(config.json)に書き込む
        /// </summary>
        /// <returns></returns>
        public static bool OutputConfigFile()
        {

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
                using (StreamReader file = File.OpenText("./config.json"))
                {
                    // Json.netのオブジェクトを作成します
                    // デシリアライズ関数に
                    // 読み込んだファイルと、データ用クラスの名称(型)を指定します。
                    // 
                    // デシリアライズされたデータは、自動的にaccountの
                    // メンバ変数に格納されます 
                    //
                    var account = JsonSerializer.Deserialize<JsonDocument>(file.BaseStream);
                    config ??= account;
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
        /// <exception cref="Exception"></exception>
        public static JsonDocument GetConfig()
        {
            if (config != null)
            {
                return config;
            }
            throw new Exception();
        }


        /// <summary>
        /// keyの設定を変更する。エラーの場合は差し戻す
        /// </summary>
        /// <param name="key" cref="string">設定の項目</param>
        /// <param name="value">設定の値</param>
        /// <returns></returns>
        public static bool ChangeConfig(string key, string value)
        {
            var a = Json
            EvConfigChange(null, key);
            return false;
        }

        /// <summary>
        /// 非推奨 動作確認前
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ChangeConfig(int notification)
        {

            return false;
        }

        /// <summary>
        /// 設定の項目及び項目の変域を出力
        /// </summary>
        /// <returns></returns>
        public static bool DetailConfig()
        {
            return false;
        }

        /// <summary>
        /// デフォルト設定の項目と値を取得する
        /// </summary>
        /// <returns>JsonDocument</returns>
        public static JsonDocument GetDefaultConfig()
        {
            return JsonSerializer.SerializeToDocument("{}");

        }

        /// <summary>
        /// デフォルト設定に変更する
        /// </summary>
        /// <returns>成功したか</returns>
        public static bool DefaultConfig()
        {
            return false;
        }
    }
}
