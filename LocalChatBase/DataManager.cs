namespace LocalChatBase
{
    public class DataManager
    {
        /// <summary>
        /// �f�[�^��ǉ������ۂɔ��΂��܂�
        /// </summary>
        public event EventHandler <string> EvAddData = (Sender, args)  => { };

        /// <summary>
        /// �f�[�^�̒ǉ������܂�
        /// </summary>
        public void AddData(string IP, string Reception, string time, string Message)
        {

        }

        /// <summary>
        /// �f�[�^�����������܂�
        /// </summary>
        public void InitializeData()
        {

        }

        /// <summary>
        /// �f�[�^���擾���܂�
        /// </summary>
        public void GetDatas(string IP)
        {

        }

    }
}