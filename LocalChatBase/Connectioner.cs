using System.Net;


namespace LocalChatBase
{
    /// <summary>
    /// セッションを生成するクラス
    /// </summary>
    public static class Connectioner
    {
        /// <summary>
        /// セッションが生まれた時に発火する
        /// </summary>
        public static event EventHandler<Session> EvStartSession = (sender, args) => { };

        /// <summary>
        /// ポート開けて待ち受けるやつ
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
        private static bool s_started = false;

        /// <summary>
        /// クライアント待ち受けを開始する (ip=any, port=6228)
        /// </summary>
        public static void StartListen()
        {
            if (!s_started)
            {
                s_started = true;
                s_listener = new System.Net.Sockets.TcpListener(IPAddress.Any, s_port);
                s_listener.Start();
                new Task(Run).Start();
            }
        }


        /// <summary>
        /// 待ち受け処理
        /// </summary>
        static private void Run()
        {
            while (true)
            {
                s_canceller.Token.ThrowIfCancellationRequested();
                var cl = s_listener.AcceptTcpClient();
                var session = new Session(cl);
                EvStartSession(null, session);
            }

        }




        /// <summary>
        /// クライアント待ち受けを終了しようとする
        /// </summary>
        static public void StopListen()
        {
            if (s_started)
            {
                s_canceller.Cancel();
                s_listener.Stop();
                s_started = false;
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
