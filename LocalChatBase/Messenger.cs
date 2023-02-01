using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public string massage = "";
        /// <summary>
        /// メッセージを送信し 成功したらtrueを返す 仮実装
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="partner">宛先</param>
        public bool SendMessage(string message, string partner)
        {
            try
            {
                var session = Connectioner.CreateSession(Partners.GetAddress(partner), 6228);
                session.EvReception += (e,r) => { if (e != null) { ((Session)e).EndSession(); } };
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
        /// 宛先のメッセージを取得する
        /// </summary>
        /// <param name="partner">宛先</param>
        /// <returns></returns>
        public List<Data> ReferenceMessage(string partner)
        {
            var ret = DataManager.GetDatas(Partners.GetAddress(partner));
            return ret;
        }

         /// <summary>
         /// データの受信イベントに登録してください 呼び出すな
         /// メッセージを受け取り
         /// 受信イベントをイベント発行
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        public void ReceptionMessage(Session session, string data)
        {
            string message = textdeconvate(data);
            if (message != "")
            {
                this.massage = message;
                //JsonSerializer.Deserialize<Data>(data);
                EvReceptionMessage(this, DateTime.Now);

                session.SendData(textconvate("ReMessage", DateTime.Now));
            }
            session.EndSession();

        }

        /// <summary>
        /// テキストをを型にはめる
        /// </summary>
        /// <param name="format">データフォーマット</param>
        /// <param name="o"><データ/param>
        /// <returns></returns>
        private static string textconvate(string format, object o)
        {
            return $"version{{\"version\":0,\"dataformat\":{{\"name\":\"{format}\",\"version\":1}},\"data\":\"{(o ?? "").ToString()}";
        }

        /// <summary>
        /// テキストをを型から出す
        /// </summary>
        /// <param name="format">データフォーマット</param>
        /// <param name="o"><データ/param>
        /// <returns></returns>
        private static string textdeconvate(string txt)
        {
            return txt ;
        }


    }
}
