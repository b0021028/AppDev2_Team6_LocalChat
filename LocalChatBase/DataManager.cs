using System;
using System.Net;

namespace LocalChatBase
{
    /// <summary>
    /// �f�[�^�Ǘ����s�����߂̃N���X
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// �f�[�^��ǉ������ۂɔ��΂���C�x���g
        /// </summary>
        public event EventHandler <AddData> EvAddData = (Sender, args)  => { };

        /// <summary>
        /// �f�[�^�̒ǉ������܂�
        /// </summary>
        public void AddData(IPAddress ip, string Reception, string time, string Message)
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
        public void GetDatas(IPAddress ip)
        {

        }

    }
}