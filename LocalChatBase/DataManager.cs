using System;
using System.Net;

namespace LocalChatBase
{
    /// <summary>
    /// データ管理を行うためのクラス
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// データを追加した際に発火するイベント
        /// </summary>
        public event EventHandler <AddData> EvAddData = (Sender, args)  => { };

        /// <summary>
        /// データの追加をします
        /// </summary>
        public void AddData(IPAddress ip, string Reception, string time, string Message)
        {

        }

        /// <summary>
        /// データを初期化します
        /// </summary>
        public void InitializeData()
        {

        }

        /// <summary>
        /// データを取得します
        /// </summary>
        public void GetDatas(IPAddress ip)
        {

        }

    }
}