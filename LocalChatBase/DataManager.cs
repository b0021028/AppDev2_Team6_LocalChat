using System;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace LocalChatBase
{
    public class DataManager
    {
        /// <summary>
        /// データベースの作成とテーブルの作成
        /// </summary>
        static void Main(string[] args)
        {
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = "temptable" };
            using (var cn = new SQLiteConnection(sqlConnectionSb.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = "CREATE [TEMP | TEMPORARY] TABLE temptable (" +
                "ReceiveFlag NUMERIC NOT NULL," +
                "Recipient TEXT NOT NULL, " +
                "Time NUMERIC NOT NULL," +
                "Message TEXT NOT NULL); ";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// データを追加した際に発火します
        /// </summary>
        public event EventHandler

        /// <summary>
        /// データの追加をします 順番：受信フラグ、受け取り人、時間、メッセージ
        /// </summary>
        public void AddData(string IP, string Reception, string time, string Message)
        {
            var Data = (IP, Reception, time, Message);
        }

        /// <summary>
        /// データを初期化します データベース内のファイルを初期化する(データベース自体の削除)
        /// </summary>
        public void InitializeData(string[] args)
        {
            string FilePath = @"temptable.db";
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

        /// <summary>
        /// データを取得します
        /// </summary>
        public void GetDatas(string IP)
        {

        }

    }
}