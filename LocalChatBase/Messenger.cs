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
        public static event EventHandler<DateTime> EvSendMessageSuccess = (sender, args) => { };

        /// <summary>
        /// イベントハンドラー メッセージ受信した後
        /// </summary>
        public static event EventHandler<Data> EvReceptionMessage = (sender, args) => { };

        /// <summary>
        /// メッセージ送信フラグ
        /// </summary>
        public static bool flg = false;
        /// <summary>
        /// メッセージを送信し 成功したらtrueを返す 仮実装
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="partner">宛先</param>
        async public static Task<bool> SendMessage(string message, string partner)
        {
            var session = Connectioner.CreateSession(Partners.GetAddress(partner), 6228);
            session.EvReception += (sender, args) => { if (sender != null) { ((Session)sender).EndSession();flg = true; } };
            flg = false;
            await session.SendData(textconvate("Message", message)).WaitAsync(TimeSpan.FromSeconds(10));
            session.StartReception();

            EvSendMessageSuccess(null, DateTime.Now);
            return flg;

        }



        /// <summary>
        /// 宛先のメッセージを取得する
        /// </summary>
        /// <param name="partner">宛先</param>
        /// <returns></returns>
        public static List<Data> ReferenceMessage(string partner)
        {
            List<Data> ret = DataManager.GetDatas(Partners.GetAddress(partner));
            return ret;
        }

         /// <summary>
         /// データの受信イベントに登録してください 呼び出さないこと
         /// メッセージを受け取り
         /// 受信イベントをイベント発行
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        public static void ReceptionMessage(object? session, string rawdata)
        {
            if (session != null)
            {
                string data = textdeconvate(rawdata);
                if (data != "")
                {
                    //Messenger.massage = data;
                    //JsonSerializer.Deserialize<Data>(data);
                    var messagedata = new Data(((Session)session).remoteEndPoint.Address, true, DateTime.Now, data);
                    EvReceptionMessage(null, messagedata);

                    ((Session)session).SendData(textconvate("ReMessage", DateTime.Now));
                }
                ((Session)session).EndSession();
            }

        }

        /// <summary>
        /// テキストをを型にはめる
        /// </summary>
        /// <param name="format">データフォーマット</param>
        /// <param name="o"><データ/param>
        /// <returns></returns>
        private static string textconvate(string format, object o)
        {
            return $"{{version{{\"version\":0,\"dataformat\":{{\"name\":\"{format}\",\"version\":1}},\"data\":\"{(o ?? "").ToString()}\"}}";
        }

        /// <summary>
        /// テキストをを型から出す
        /// </summary>
        /// <param name="format">データフォーマット</param>
        /// <param name="o"><データ/param>
        /// <returns></returns>
        private static string textdeconvate(string txt)
        {
            string fixtxt = (JObject.Parse(txt).GetValue("data")?? "").ToString() ;
            return fixtxt;
        }


    }
}
