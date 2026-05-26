using FABFIB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FABFIB
{
    public class InGameMain : MonoBehaviour
    {
        [SerializeField] private UIManager uimanager;
        [SerializeField] public TextMeshProUGUI restChangeNumText;
        [SerializeField] private Image hideCardInage;

        [SerializeField] Button newCardDrawButton;
        [SerializeField] public TMP_InputField callInput;

        private void OnEnable()
        {
            ShowUserInfoUI();
            HideImageActive(true);
            DrawNewCard();
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
            //입력해야만 넘어갈 수 있음
            if(callInput.text.Length != 3)
            {
                uimanager.ShowMessage("숫자 3자리를 입력해야 합니다.");
                return;
            }
            //더 큰수를 입력해야함
            var gm = GameManager.Instance;
            if (int.Parse(callInput.text) <= gm.CallNumber)
            {
                uimanager.ShowMessage("더 큰 수를 입력해야 합니다.");
                return;
            }
            //숫자는 반드시 내림차순으로 입력해야함
            if (!FABFIB_Utility.IsDescendingOrderInput(callInput.text))
            {
                uimanager.ShowMessage("내림차순으로\n 입력해야 합니다.");
                return;
            }

            //다음 사람으로 넘어감
            gm.playerList[gm.CurrentUserIndex].isMyTurn = false;
            gm.playerList[gm.CurrentUserIndex].ShowPlayerInfo();

            //다음 사람으로 넘어감
            gm.CurrentUserIndex = PlayerManager.Instance.NextPlayerIndex();
            gm.playerList[gm.CurrentUserIndex].isMyTurn = true;
            gm.playerList[gm.CurrentUserIndex].ShowPlayerInfo();

            //수 갱신
            gm.CallNumber = int.Parse(callInput.text);

            uimanager.uiList[4].gameObject.SetActive(true);
        }

        /// <summary>
        /// 새카드 뽑기
        /// </summary>
        public void DrawNewCard()
        {
            GameManager gm = GameManager.Instance;
            HideImageActive(false);

            //바닥에 남은 패 개수가 5개 미만이면 다시 뽑기
            if (gm.restNumberCardList.Count < 5)
            {
                CardManager.Instance.ChargeCardInFloor();
            }

            for(int i = 0; i < 3; i++)
            {
                NumberCard num = gm.restNumberCardList.Pop();

                var card = CardManager.Instance.presentNumber[i];
                card.Num = num.Num;
                card.Attack = num.Attack;
                card.isClick = false;
                card.GetComponent<NumberCard>().ShowCard();
                gm.usedCardList.Add(num);
            }
            //카드 정렬
            CardManager.Instance.SortOrderCard();
        }

        /// <summary>
        /// 남은 카드 교환 횟수 표시
        /// </summary>
        public void ShowRestChangeCardNum()
        {
            int idx = GameManager.Instance.CurrentUserIndex;
            restChangeNumText.text = "남은 횟수 : "+ GameManager.Instance.playerList[idx].changeCount.ToString();
        }

        /// <summary>
        /// 카드 숨기기 이미지 활성화
        /// </summary>
        public void HideImageActive(bool isHide)
        {
            hideCardInage.gameObject.SetActive(isHide);
        }
    }
}
