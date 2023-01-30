﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

         /// <summary>
         /// データからメッセージを取り出し 受信確認を返送しセッションを閉じて受信イベントをイベント発行 
         /// </summary>
        public void ReceptionMessage()
        {

        }


    }
}
