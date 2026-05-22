using FABFIB;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace FABFIB
{
    public class InGameMain : MonoBehaviour
    {
        [SerializeField] private UIManager uimanager;
        [SerializeField] public TextMeshProUGUI restChangeNumText;

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
        /// 의심 or pass
        /// </summary>
        public void GotoNextPlayer()
        {
            uimanager.uiList[4].gameObject.SetActive(true);
        }

        /// <summary>
        /// 새카드 뽑기
        /// </summary>
        public void DrawNewCard()
        {
            GameManager gm = GameManager.Instance;

            for(int i = 0; i < 3; i++)
            {
                NumberCard num = gm.restNumberCardList.Peek();
                CardManager.Instance.presentNumber[i].Num = num.Num;

                gm.restNumberCardList.Pop();
                gm.usedCardList.Add(num);

                num.ShowCard();
            }
        }

        /// <summary>
        /// 남은 카드 교환 횟수 표시
        /// </summary>
        public void ShowRestChangeCardNum()
        {
            restChangeNumText.text = GameManager.Instance.currentPlayer.changeCount.ToString();
        }
    }
}
