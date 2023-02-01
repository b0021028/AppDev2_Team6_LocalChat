
using System;
using System.Data;
using System.Data.SQLite;
using System.Net;


namespace LocalChatBase
{
    public readonly struct Data
    {
        IPAddress ip { get; init; }
        bool receptionFlag { get; init; }
        DateTime time { get; init; }
        string message { get; init; }
        public Data(IPAddress ip, bool receptionFlag, DateTime time, string message)
        {
            this.ip = ip;
            this.time = time;
            this.receptionFlag = receptionFlag;
            this.message = message;
        }
    }
    public class DataManager
    {
        /// <summary>
        /// データを追加した際に発火します
        /// </summary>
        public static event EventHandler<bool> EvAddData = (sender, args) => { };

        private static string s_dataSource { get; } = "Data Source=temptable";

        /// <summary>
        /// データベースの作成とテーブルの作成
        /// </summary>
        static void Main()
        {
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = "temptable" };
            using (var cn = new SQLiteConnection(sqlConnectionSb.ToString()))
            {
                cn.Open();

                using (var cmd = new SQLiteCommand(cn))
                {
                    cmd.CommandText = "CREATE TEMPORARY TABLE temptable (" +
                "ReceiveFlag NUMERIC NOT NULL," +
                "Recipient TEXT NOT NULL, " +
                "Time NUMERIC NOT NULL," +
                "Message TEXT NOT NULL); ";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// データの追加をします 順番：受信フラグ、受け取り人、時間、メッセージ
        /// </summary>
        /// <param name="reception"></param>
        /// <param name="ip"></param>
        /// <param name="time"></param>
        /// <param name="message"></param>
        public static void AddData(bool reception, IPAddress ip, DateTime time, string message)
        {
            using (var conn = new SQLiteConnection(s_dataSource))
            {
                conn.Open();
                // データの追加を試みる
                using (SQLiteTransaction sqlt = conn.BeginTransaction())
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into temptable (RaceiveFlag, Recipient, Time, Message) values (@reception, @ip, @time, @message);";
                    cmd.Parameters.AddWithValue("@reception", reception);
                    cmd.Parameters.AddWithValue("@ip", ip);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.ExecuteNonQuery();

                    EvAddData(null, reception);
                    sqlt.Commit();
                }
                conn.Close();
            }
        }

        /// <summary>
        /// データを初期化します データベース内のファイルを初期化する(データベース自体の削除)
        /// </summary>
        public static void InitializeData()
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
        /// <param name="ip"></param>
        /// <returns></returns>
        public static IEnumerator<string> GetDatas(IPAddress ip)
        {
            SQLiteDataReader reader;

            using (var conn = new SQLiteConnection(s_dataSource))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "SELECT RaceiveFlag, Recipient, Time, Message FROM TEMPTABLE WHERE IP=@ip;";
                command.Parameters.AddWithValue("@ip", ip);
                // データの取得
                reader = command.ExecuteReader();
            }
            foreach (var data in reader)
            {
                new Data(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3))
            }



            return (IEnumerator<string>)reader.GetEnumerator();
        }

    }
}