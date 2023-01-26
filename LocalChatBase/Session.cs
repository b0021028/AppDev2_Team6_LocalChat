namespace LocalChatBase
{
    public class Session
    {
        public event EventHandler<System.Net.IPEndPoint> EvEndSession = (sender, args) => { };
        public event EventHandler<string> EvReception = (sender, args) => { };


        public System.Net.IPEndPoint remoteEndPoint { get; init; }


        // インスタンス作成
        public Session(System.Net.Sockets.TcpClient client)
        {

        }

        /// <summary>
        /// セッションを切断します
        /// </summary>
        public void EndSession()
        {

        }

            /// <summary>
            /// 受信を開始します
            /// </summary>
            public void StartReception()
        {
        }

        /// <summary>
        /// テキストデータを送信します
        /// </summary>
        /// <param name="data"></param>
        public void SendData(string data)
        {
        }



    }

}