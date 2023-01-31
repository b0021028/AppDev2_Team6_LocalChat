using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LocalChatBase
{
    public class Messenger
    {

        /// <summary>
        /// イベントハンドラー メッセージ送信され成功した時
        /// </summary>
        public event EventHandler<System.DateTime> EvSendMessageSuccess = (Sender, args) => { };


        /// <summary>
        /// イベントハンドラー メッセージ受信した後
        /// </summary>
        public event EventHandler<string> EvReceptionMessage = (Sender, args) => { };


        /// <summary>
        /// メッセージを送信し 成功したらtrueを返す
        /// </summary>
        /// <param name="message"></param>
        /// <param name="partner"></param>
        public bool SendMessage(string message, string partner)
        {

            if (false)
            {
                EvSendMessageSuccess(this, DateTime.Now);
                return true;
            }
            else
            {
            return false;

            }
        }



        /// <summary>
        /// 宛先のメッセージを取得する 未完成
        /// </summary>
        /// <param name="partner">宛先</param>
        /// <returns></returns>
        public dynamic ReferenceMessage(string partner)
        {
            DataManager.GetDatas(Partners.GetAddress(partner).ToString());
            return new List<string>() { };
        }

         /// <summary>
         /// データからメッセージを取り出し 受信確認を返送しセッションを閉じて受信イベントをイベント発行 
         /// </summary>
        public void ReceptionMessage(Session session, string data)
        {

            session.EndSession();

            EvReceptionMessage();
        }


    }
}
