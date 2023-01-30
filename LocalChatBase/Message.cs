using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LocalChatBase
{
    internal class Message
    {
        /// <summary>
        /// イベントハンドラー メッセージ送信され成功した時
        /// </summary>
        public event EventHandler<string> EvSendMessageSuccess = (Sender, args) => { };


        /// <summary>
        /// イベントハンドラー メッセージ受信した後
        /// </summary>
        public event EventHandler<string> EvReceptionMessage = (Sender, args) => { };

        /// <summary>
        /// メッセージを加工し 宛先のIPとともに データ送信へ依頼 受信確認の待機をする 帰ってきたら送信が成功したことにする
        /// </summary>
        public void SendMessage()
        {

        }



        /// <summary>
        /// メッセージ記録から メッセージを 呼び出す
        /// </summary>
        public void ReferenceMessage()
        {
            // オブジェクト作成
            SQLiteConnection con = new SQLiteConnection("Data Source=temp.file.db.sqlite;Version=3;");
            // dbを開いて接続
            con.Open();
            // sql文
            try
            {
                string sql = "select * from temptable where Recipient = 'address'and cnt = num";
                // sql文読み出し
                SQLiteCommand com = new SQLiteCommand(sql, con);
                SQLiteDataReader sdr = com.ExecuteReader();
                while (sdr.Read() == true)
                {
                    textBox1.Text += string.Format("id:{0:d}, code:{1}, name:{2}, price:{3:d}\r\n",
                      (int)sdr["id"], (string)sdr["code"], (string)sdr["name"], (int)sdr["price"]);
                }
                sdr.Close();
            }
            finally
            {
                con.Close();
            }
        }

         /// <summary>
         /// データからメッセージを取り出し 受信確認を返送しセッションを閉じて受信イベントをイベント発行 
         /// </summary>
        public void ReceptionMessage()
        {

        }


    }
}
