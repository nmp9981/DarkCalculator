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
            ShowUserInfoUI();
        }

        /// <summary>
        /// 유저 정보 UI에 보이기
        /// </summary>
        void ShowUserInfoUI()
        {
            foreach(var user in GameManager.Instance.playerList)
            {
                user.ShowPlayerInfo();
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
