using FABFIB;
using NUnit.Framework;
using UnityEngine;

namespace FABFIB
{
    public class InGameMain : MonoBehaviour
    {
        [SerializeField] private UIManager uimanager;
       
        private void OnEnable()
        {
            ShowUserInfo();
        }

        /// <summary>
        /// 유저 정보 보이기
        /// </summary>
        void ShowUserInfo()
        {
            int userCount = GameManager.Instance.UserCount;
            for (int idx = 0; idx < userCount; idx++)
            {
                GameManager.Instance.playerList[idx].gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 다음 플레이어 차례로 넘어감
        /// </summary>
        public void GotoNextPlayer()
        {
            uimanager.uiList[4].gameObject.SetActive(true);
        }
    }
}
