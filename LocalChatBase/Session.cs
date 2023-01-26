namespace LocalChatBase
{

    /// <summary>
    /// クライアントとのコネクションを一つのセッションとして扱うクラス
    /// </summary>
    public class Session
    {

        // Public
        /// <summary>
        /// セッションが終了したとき発火する
        /// </summary>
        public event EventHandler<System.Net.IPEndPoint> EvEndSession = (sender, args) => { };

        /// <summary>
        /// StartReception() で データを受信したとき発火する
        /// </summary>
        public event EventHandler<string> EvReception = (sender, args) => { };

        /// <summary>
        /// クライアントのエンドポイント
        /// </summary>
        public System.Net.IPEndPoint remoteEndPoint { get; init; }


        // Private
        /// <summary>
        /// 接続している Client
        /// </summary>
        private System.Net.Sockets.TcpClient _client { get; init; }

        /// <summary>
        /// 接続している Client　の NetStream
        /// </summary>
        private System.Net.Sockets.NetworkStream _netStream { get; init; }


        //  Static Private
        /// <summary>
        /// NetStream.Write のタイムアウト
        /// </summary>
        static private int s_timeOut { get; } = 1000;

        /// <summary>
        /// 送受信する時のStringの文字コード
        /// </summary>
        static private System.Text.Encoding s_encode { get; } = System.Text.Encoding.Default;


        /// <summary>
        /// クライアントとのコネクションを一つのセッションとする
        /// </summary>
        /// <param name="client">接続されたTcpClient</param>
        /// <throw>Tia</throw>
        public Session(System.Net.Sockets.TcpClient client)
        {
            _client = client;
            if (_client.Client.RemoteEndPoint != null)
            {
                _netStream = _client.GetStream();
                _netStream.WriteTimeout = s_timeOut;
                remoteEndPoint = (System.Net.IPEndPoint)_client.Client.RemoteEndPoint;
            }
            else
            {
                throw new ArgumentNullException();
            }
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
        /// <param name="data">送信するテキストデータ</param>
        public void SendData(string data)
        {
        }



    }

}