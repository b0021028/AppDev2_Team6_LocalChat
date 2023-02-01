using System;
using System.Data;
using System.Data.SQLite;
using System.Net;
using System.Linq;


namespace LocalChatBase
{
    public struct Data
    {
        public IPAddress ip { get; init; }
        public bool receptionFlag { get; init; }
        public DateTime time { get; init; }
        public string message { get; init; }
        public Data(IPAddress ip, bool receptionFlag, DateTime time, string message)
        {
            this.ip = ip;
            this.time = time;
            this.receptionFlag = receptionFlag;
            this.message = message;
        }
        public Data(string ip, string receptionFlag, string time, string message)
        {
            this.ip = IPAddress.Parse(ip);
            this.time = DateTime.Parse(time);
            this.receptionFlag = bool.Parse(receptionFlag);
            this.message = message;
        }
    }
    public class DataManager
    {
        /// <summary>
        /// データを追加した際に発火します
        /// </summary>
        public static event EventHandler<bool> EvAddData = (sender, args) => { };

        private static string s_datapath { get; } = "temptable.sql";
        private static string s_dataSource { get; } = $"Data Source={s_datapath};Version=3;";


        private static List<Data> NearDatabase = new();
        private static bool IsDatabaseMode = false;

        /// <summary>
        /// データベースの作成とテーブルの作成
        /// </summary>
        static void Main()
        {
            if (IsDatabaseMode)
            {
                using (var connect = new SQLiteConnection(s_dataSource))
                {
                    connect.Open();

                    string sql =
                        "CREATE TABLE MESSAGES ("+
                        "RECEIVEFLAG NUMERIC NOT NULL, "+
                        "RECIPIENT TEXT NOT NULL, "+"" +
                        "TIME NUMERIC NOT NULL, "+
                        "MESSAGE TEXT NOT NULL"+
                        "); ";
                    try
                    {
                        var cmd = new SQLiteCommand(sql, connect);
                        cmd.ExecuteNonQuery();


                        cmd = connect.CreateCommand();
                        cmd.CommandText = "INSERT INTO MESSAGES (RECEIVEFLAG, RECIPIENT, TIME, MESSAGE) VALUES (@flag, @ip, @time, @message);";
                        cmd.Parameters.AddWithValue("@flag", true);
                        cmd.Parameters.AddWithValue("@ip", IPAddress.Parse("127.255.255.255"));
                        cmd.Parameters.AddWithValue("@time", DateTime.Now);
                        cmd.Parameters.AddWithValue("@message", "testtestesttesttest");
                        cmd.ExecuteNonQuery();

                        sql = "SELECT RECIPIENT, RECEIVEFLAG, TIME, MESSAGE FROM MESSAGES WHERE RECIPIENT='10.146.221.28';";
                        cmd = new SQLiteCommand(sql, connect);
                        cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        connect.Close();
                    }
                }

            }
            else
            {

            }
        }

        /// <summary>
        /// データの追加をします 順番：受信フラグ、受け取り人、時間、メッセージ
        /// </summary>
        /// <param name="receptionflag"></param>
        /// <param name="ip"></param>
        /// <param name="time"></param>
        /// <param name="message"></param>
        public static void AddData(bool receptionflag, IPAddress ip, DateTime time, string message)
        {
            if (IsDatabaseMode)
            {
                using (var connect = new SQLiteConnection(s_dataSource))
                {
                    connect.Open();
                    try
                    {
                        // データの追加を試みる
                        var cmd = connect.CreateCommand();
                        cmd.CommandText = "INSERT INTO MESSAGES (RECEIVEFLAG, RECIPIENT, TIME, MESSAGE) VALUES (@flag, @ip, @time, @message);";
                        cmd.Parameters.AddWithValue("@flag", receptionflag);
                        cmd.Parameters.AddWithValue("@ip", ip);
                        cmd.Parameters.AddWithValue("@time", time);
                        cmd.Parameters.AddWithValue("@message", message);
                        cmd.ExecuteNonQuery(); // デッドロック
                    }
                    finally { connect.Close(); }
                }
                EvAddData(null, receptionflag);

            }
            else
            {
                NearDatabase.Add(new Data(ip, receptionflag, time, message));
                EvAddData(null, receptionflag);
            }
        }


        /// <summary>
        /// データを初期化します データベース内のファイルを初期化する(データベース自体の削除)
        /// </summary>
        public static void InitializeData()
        {
            if (IsDatabaseMode)
            {
                if (File.Exists(s_datapath))
                {
                    try
                    {
                        File.Delete(s_datapath);
                    }
                    catch
                    {
                    }
                }
            }
            else
            {

            }
        }
        public static void InitializeData(bool? t)
        {
            InitializeData();
            Main();
        }

        /// <summary>
        /// データを取得します
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static List<Data> GetDatas(IPAddress ip)
        {
            if (IsDatabaseMode)
            {
                SQLiteDataReader reader;
                int count = 0;
                using (var conntect = new SQLiteConnection(s_dataSource))
                {
                    conntect.Open();
                    var command = conntect.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM MESSAGES;";
                    try
                    {
                        count = (int)command.ExecuteReader().GetValue(0);

                    }
                    catch (InvalidOperationException e)
                    {
                        count = 0;
                    }
                    command = conntect.CreateCommand();
                    command.CommandText = "SELECT RECIPIENT, RECEIVEFLAG, TIME, MESSAGE FROM MESSAGES WHERE RECIPIENT=@ip;";
                    command.Parameters.AddWithValue("@ip", ip);


                    // データの取得
                    ////
                    try // =========================================================================================================================================================debug
                    {
                        reader = command.ExecuteReader();
                    }
                    catch { reader = null; }
                }
                // =========================================================================================================================================================debug
                List<Data> ret = new() {new Data("10.146.221.28","true",DateTime.Now.ToString(),"testbuck") };

                if (reader == null)// =========================================================================================================================================================debug
                {
                    return ret;
                }


                for (int i=0; i< count; i++)
                {
                    ret.Add(new Data(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    reader.NextResult();
                }



                return ret;

            }
            else
            {
                var x = NearDatabase.Where(x => x.ip == ip);
                return (List<Data>)x;
            }
        }

    }
}