using Org.BouncyCastle.Tls;
using System;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace LocalChatBase
{
    public class DataManager
    {
        /// <summary>
        /// �f�[�^�x�[�X�̍쐬�ƃe�[�u���̍쐬
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
        /// �f�[�^��ǉ������ۂɔ��΂��܂�
        /// </summary>
        public static event EventHandler<string> EvAddData = (sender, args) => { };

        /// <summary>
        /// �f�[�^�̒ǉ������܂� ���ԁF��M�t���O�A�󂯎��l�A���ԁA���b�Z�[�W
        /// </summary>
        
        public static void AddData(object sender, EventArgs e, string IP, string Reception, string time, string Message)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=temptable;");

            con.Open();
            // �f�[�^�̒ǉ������݂�
            try
            {
                string sql = $"insert into temptable (RaceiveFlag, Recipient, Time, Message) values ({IP}, {Reception}, {time}, {Message});";
                SQLiteCommand com = new SQLiteCommand(sql, con);
                com.ExecuteNonQuery();

                EvAddData(this, Reception);
            }
            catch
            {

            }

            finally
            {
                con.Close();
            }
            



        }

        /// <summary>
        /// �f�[�^�����������܂� �f�[�^�x�[�X���̃t�@�C��������������(�f�[�^�x�[�X���̂̍폜)
        /// </summary>
        public static void InitializeData(string[] args)
        {
            string FilePath = @"temptable.db";
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

        /// <summary>
        /// �f�[�^���擾���܂�
        /// </summary>
        public static void GetDatas(string IP)
        {

        }

    }
}