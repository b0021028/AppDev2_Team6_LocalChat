using System.Net;
using System.Net.Sockets;
using System.Text;

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
        public event EventHandler<IPEndPoint> EvEndSession = (sender, args) => { };

        /// <summary>
        /// StartReception() の後 データを受信したとき発火する
        /// </summary>
        public event EventHandler<string> EvReception = (sender, args) => { };

        /// <summary>
        /// クライアントのエンドポイント
        /// </summary>
        public IPEndPoint remoteEndPoint { get; init; }


        // Private
        /// <summary>
        /// 接続している Client
        /// </summary>
        private TcpClient _client { get; init; }

        /// <summary>
        /// 接続している Client　の NetStream
        /// </summary>
        private NetworkStream _netStream { get; init; }


        //  Static Private
        /// <summary>
        /// NetStream.Write のタイムアウト
        /// </summary>
        static private int s_timeOut { get; } = 1000;

        /// <summary>
        /// 送受信する時のStringの文字コード
        /// </summary>
        static private Encoding s_encode { get; } = Encoding.Default;

        /// <summary>
        /// キャンセルトークン
        /// </summary>
        private CancellationTokenSource _token { get; set; }
        private bool _started = false;

        /// <summary>
        /// クライアントとのコネクションを一つのセッションとする
        /// </summary>
        /// <param name="client">接続されたTcpClient</param>
        /// <throw>Tia</throw>
        public Session(TcpClient client)
        {
            _client = client;
            if (_client.Client.RemoteEndPoint != null)
            {
                _netStream = _client.GetStream();
                _netStream.WriteTimeout = s_timeOut;
                remoteEndPoint = (IPEndPoint)_client.Client.RemoteEndPoint;
                _token = new CancellationTokenSource();
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
            try
            {
                _token.Cancel();
            }
            catch
            { }
            try
            {
                _netStream.Close();
            }
            catch
            { }
            try
            {
                _client.Close();
            }
            catch
            { }
            EvEndSession(this, remoteEndPoint);


        }


        /// <summary>
        /// 受信を開始します
        /// </summary>
        public void StartReception()
        {
            if (!_started)
            {
                _started = true;
                _ = Task.Run(Run);
            }

        }


        /// <summary>
        /// テキストデータを送信します
        /// </summary>
        /// <param name="data">送信するテキストデータ</param>
        async public Task SendData(string data)
        {
            //データを送信する
            _netStream.WriteTimeout = s_timeOut;

            byte[] sendBytes = s_encode.GetBytes(data);

            await _netStream.WriteAsync(sendBytes, 0, sendBytes.Length);

        }

        /// <summary>
        /// データ受信処理部
        /// </summary>
        async private Task Run()
        {
            try
            {
                while (true)
                {
                    //クライアントから送られたデータを受信する
                    MemoryStream ms = new();
                    byte[] resBytes = new byte[256];
                    int resSize = 0;
                    do
                    {
                        this._token.Token.ThrowIfCancellationRequested();
                        //データの一部を受信する
                        resSize = await _netStream.ReadAsync(resBytes, 0, resBytes.Length);


                        //Readが0を返した時はクライアントが切断したと判断
                        if (resSize == 0)
                        {
                            Console.WriteLine("クライアントが切断しました。");
                            break;
                        }
                        //受信したデータを蓄積する
                        ms.Write(resBytes, 0, resSize);
                        //まだ読み取れるデータがあるか、データの最後が\nでない時は、
                        // 受信を続ける
                    } while (_netStream.DataAvailable);
                    //受信したデータを文字列に変換
                    string resMsg = s_encode.GetString(ms.GetBuffer(), 0, (int)ms.Length);

                    ms.Close();

                    EvReception(this, resMsg);

                }

            }
            catch (OperationCanceledException e) { }
            catch (IOException e) { }


        }


    }

}