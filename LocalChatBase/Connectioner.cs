using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LocalChatBase
{
    /// <summary>
    /// セッションを生成するクラス
    /// </summary>
    public class Connectioner
    {
        /// <summary>
        /// セッションが生まれた時に発火する
        /// </summary>
        public static event EventHandler<Session> EvStartSession = (sender, args) => { };


        /// 止めるフラグ
        private static bool s_run = false;

        /// <summary>
        /// クライアント待ち受けを開始する (ip=any, port=6228)
        /// </summary>
        public static void StartListen()
        {
            s_run = true;
            while (s_run)
            {
                var session = new Session(new System.Net.Sockets.TcpClient());
                EvStartSession(null, session);
            }
        }


        /// <summary>
        /// サーバとのコネクションを行う
        /// </summary>
        /// <param name="ip">相手のIPアドレス</param>
        /// <param name="port">相手のポート番号</param>
        /// <returns>Sessionのインスタンス</returns>
        public static Session CreateSession(IPAddress ip, int port)
        {
            return new Session(new System.Net.Sockets.TcpClient(ip.ToString(), port));
        }


        /// <summary>
        /// クライアント待ち受けを終了しようとする
        /// </summary>
        async public static void StopListen()
        {
            s_run = false;
        }

    }
}
