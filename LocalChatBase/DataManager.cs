
using System;
using System.Data;
using System.Data.SQLite;
using System.Net;


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
        /// �f�[�^��ǉ������ۂɔ��΂��܂�
        /// </summary>
        public static event EventHandler<bool> EvAddData = (sender, args) => { };

        private static string s_datapath { get; } = "temptable.sql";
        private static string s_dataSource { get; } = $"Data Source={s_datapath};Version=3;";

        /// <summary>
        /// �f�[�^�x�[�X�̍쐬�ƃe�[�u���̍쐬
        /// </summary>
        static void Main()
        {
            using (var connect = new SQLiteConnection(s_dataSource))
            {
                connect.Open();

                string sql = "CREATE TABLE MESSAGES (RECEIVEFLAG NUMERIC NOT NULL, RECIPIENT TEXT NOT NULL, TIME NUMERIC NOT NULL, MESSAGE TEXT NOT NULL ); ";
                try
                {
                    var cmd = new SQLiteCommand(sql, cn);
                    cmd.ExecuteNonQuery();

                }
                finally
                {
                    connect.Close();
                }
            }
        }

        /// <summary>
        /// �f�[�^�̒ǉ������܂� ���ԁF��M�t���O�A�󂯎��l�A���ԁA���b�Z�[�W
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
                // �f�[�^�̒ǉ������݂�
                using (SQLiteTransaction sqlt = conn.BeginTransaction())
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO MESSAGES (RACEIVEFLAG, RECIPIENT, TIME, MESSAGE) VALUES (@reception, @ip, @time, @message);";
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
        /// �f�[�^�����������܂� �f�[�^�x�[�X���̃t�@�C��������������(�f�[�^�x�[�X���̂̍폜)
        /// </summary>
        public static void InitializeData()
        {
            if(File.Exists(s_datapath))
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
        public static void InitializeData(bool? t)
        {
            InitializeData();
            Main();
        }

        /// <summary>
        /// �f�[�^���擾���܂�
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static List<Data> GetDatas(IPAddress ip)
        {
            SQLiteDataReader reader;

            using (var conn = new SQLiteConnection(s_dataSource))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "SELECT RECIPIENT, RECEIVEFLAG, TIME, MESSAGE FROM TEMPTABLE WHERE IP=@ip;";
                command.Parameters.AddWithValue("@ip", ip);
                // �f�[�^�̎擾
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


            for (int i=0; i< reader.StepCount; i++)
            {
                ret.Add(new Data(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                reader.NextResult();
            }



            return ret;
        }

    }
}