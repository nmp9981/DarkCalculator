using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FABFIB
{
    public class PassUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI doubtText;
        [SerializeField] TextMeshProUGUI numText;
        [SerializeField] UIManager uiManager;
        [SerializeField] InGameMain inGame;

        private void OnEnable()
        {
            ShowNumber();
        }

        /// <summary>
        /// 숫자 보여주기
        /// </summary>
        void ShowNumber()
        {
            var gm = GameManager.Instance;
            string curPlayerName = gm.playerNameList[gm.CurrentUserIndex];
            doubtText.text = $"{curPlayerName}";
            numText.text = inGame.callInput.text;
        }

        /// <summary>
        /// 패스
        /// </summary>
        public void PassButton()
        {
            if (inGame.callInput.text == "1000")
            {
                uiManager.ShowMessage("의심 행동만 가능합니다.");
                return;
            }

            this.gameObject.SetActive(false);
        }
        /// <summary>
        /// 의심
        /// </summary>
        public void DoubtOtherPlayer()
        {
            var gm = GameManager.Instance;
            string resultMsg = string.Empty;
            
            //결과 공개
            if(RealCardNumber() == int.Parse(inGame.callInput.text))
            {
                //의심한 사람이 깍임
                int hpAmount = gm.playerList[gm.CurrentUserIndex].CalDecreaseHP();
                gm.playerList[gm.CurrentUserIndex].DecreaseHP(hpAmount);
                gm.playerList[gm.CurrentUserIndex].ShowPlayerInfo();

                resultMsg = $"{gm.playerList[gm.CurrentUserIndex].playerName} -{hpAmount}";
            }
            else
            {
                //전 사람이 깍임
                int previousUserIdx = PlayerManager.Instance.PreviousPlayerIndex();
                int hpAmount = gm.playerList[previousUserIdx].CalDecreaseHP();
                gm.playerList[previousUserIdx].DecreaseHP(hpAmount);
                gm.playerList[previousUserIdx].ShowPlayerInfo();
                resultMsg = $"{gm.playerList[previousUserIdx].playerName} -{hpAmount}";

                //전사람이 선
                gm.playerList[gm.CurrentUserIndex].isMyTurn = false;
                gm.playerList[gm.CurrentUserIndex].ShowPlayerInfo();
                gm.CurrentUserIndex = previousUserIdx;
            }
            //의심한 사람이 죽은 경우 그 사람의 턴은 오지 않음
            if (gm.playerList[gm.CurrentUserIndex].playerHP <= 0)
            {
                gm.playerList[gm.CurrentUserIndex].isMyTurn = false;
                gm.CurrentUserIndex = PlayerManager.Instance.NextPlayerIndex();
            }

            //선 확정
            gm.playerList[gm.CurrentUserIndex].isMyTurn = true;
            gm.playerList[gm.CurrentUserIndex].ShowPlayerInfo();

            //교환 횟수 초기화
            gm.CurChangeCount = 0;
            inGame.ShowRestChangeCardNum();

            //call 수 초기화
            gm.CallNumber = -1;

            //새카드 뽑기
            inGame.DrawNewCard();

            //카드 숨김창 활성화
            inGame.HideImageActive(true, resultMsg);

            //창 닫기
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// 정답 카드 번호
        /// </summary>
        /// <returns></returns>
        int RealCardNumber()
        {
            var cardManager = CardManager.Instance;
            List<int> numList = new(); 
            foreach (var card in cardManager.presentNumber)
            {
                numList.Add(card.Num);
            }
            numList.Sort();

            int number = numList[2]*100+numList[1]*10+numList[0];

            //000은 1000
            if (number == 0) number = 1000;
            return number;
        }
    }

}