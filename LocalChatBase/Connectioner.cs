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
        public  static event EventHandler<Session> EvStartSession = (sender, args) => { };

        /// <summary>
        /// 待ち受けるやつ
        /// </summary>
        private static System.Net.Sockets.TcpListener s_listener { get; set; }

        /// <summary>
        /// 待ち受けIPアドレス
        /// </summary>
        private static IPAddress s_ip = IPAddress.Any;

        /// <summary>
        /// 待ち受けポート
        /// </summary>
        private static UInt16 s_port = 6228;

        /// <summary>
        /// キャンセル用
        /// </summary>
        private static CancellationTokenSource s_canceller = new CancellationTokenSource();
        private static bool s_started  = false;

        /// <summary>
        /// クライアント待ち受けを開始する (ip=any, port=6228)
        /// </summary>
        public void StartListen()
        {
            if (!s_started)
            {
                s_started = true;
                s_listener = new System.Net.Sockets.TcpListener(IPAddress.Any, s_port);
                new Task(Run).Start();
            }
        }

        /// <summary>
        /// 待ち受け処理
        /// </summary>
        async private void Run()
        {
            while (true){
                s_canceller.Token.ThrowIfCancellationRequested();
                var cl = await s_listener.AcceptTcpClientAsync();
                var session = new Session(new System.Net.Sockets.TcpClient());
                EvStartSession(null, session);
            }

        }





        /// <summary>
        /// クライアント待ち受けを終了しようとする
        /// </summary>
        async public void StopListen()
        {
            if (s_started)
            {
                s_canceller.Cancel();
                s_listener.Stop();
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


    }
}
