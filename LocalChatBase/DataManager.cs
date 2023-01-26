namespace LocalChatBase
{
    public class DataManager
    {
        public event EventHandler <string> EvAddData = (Sender, args)  => { };

        /// <summary>
        /// データの追加をします
        /// </summary>
        public void AddData(string IP, string Reception, string time, string Message)
        {

        }

        /// <summary>
        /// データを初期化します
        /// </summary>
        public void InitializeData()
        {

        }

        /// <summary>
        /// データを取得します
        /// </summary>
        public void GetDatas(string IP)
        {

        }
    }
}