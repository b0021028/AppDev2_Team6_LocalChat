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
        public event EventHandler<DateTime> EvSendMessageSuccess = (sender, args) => { };

        /// <summary>
        /// イベントハンドラー メッセージ受信した後
        /// </summary>
        public event EventHandler<DateTime> EvReceptionMessage = (sender, args) => { };


        /// <summary>
        /// メッセージを送信し 成功したらtrueを返す
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="partner">宛先</param>
        public bool SendMessage(string message, string partner)
        {
            try
            {
                var session = Connectioner.CreateSession(Partners.GetAddress(partner), 6228);
                session.EvReception += (e,r) => {((Session)e).EndSession(); };
                session.SendData(textconvate("Message", message));
                session.StartReception();

            }
            catch
            {
                return false;
            }

            EvSendMessageSuccess(this, DateTime.Now);

            return true;
        }



        /// <summary>
        /// 宛先のメッセージを取得する 未完成 DataManager完成待ち
        /// </summary>
        /// <param name="partner">宛先</param>
        /// <returns></returns>
        public dynamic ReferenceMessage(string partner)
        {
            // 保留
            //DataManager.GetDatas(Partners.GetAddress(partner).ToString());
            return new List<string>() { };
        }

         /// <summary>
         /// データからメッセージを取り出し
         /// 受信確認を返送し
         /// セッションを閉じて
         /// 受信イベントをイベント発行
         /// </summary>
        public void ReceptionMessage(Session session, string data)
        {
            if (data != "")
            {
                session.SendData(textconvate("ReMessage", DateTime.Now));
            }
            session.EndSession();

            EvReceptionMessage(this, DateTime.Now);
        }

        /// <summary>
        /// テキストをを型にはめる
        /// </summary>
        /// <param name="format">データフォーマット</param>
        /// <param name="o"><データ/param>
        /// <returns></returns>
        private string textconvate(string format, object o)
        {
            return $"version{{\"version\":0,\"dataformat\":{{\"name\":\"{format}\",\"version\":1}},\"data\":\"{(o??"").ToString()}";
        }


    }
}
