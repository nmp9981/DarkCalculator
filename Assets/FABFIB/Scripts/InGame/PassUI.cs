using TMPro;
using UnityEngine;

namespace FABFIB
{
    public class PassUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI numText;
        [SerializeField] TMP_InputField inputNum;
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
            numText.text = inGame.callInput.text;
        }

        /// <summary>
        /// 패스
        /// </summary>
        public void PassButton()
        {
            //이전 사람 패스
            var gm = GameManager.Instance;
            gm.playerList[gm.CurrentUserIndex].isMyTurn = false;
            gm.playerList[gm.CurrentUserIndex].ShowPlayerInfo();

            //다음 사람으로 넘어감
            gm.CurrentUserIndex = (gm.CurrentUserIndex+1)%gm.UserCount;
            gm.playerList[gm.CurrentUserIndex].isMyTurn = true;
            gm.playerList[gm.CurrentUserIndex].ShowPlayerInfo();

            this.gameObject.SetActive(false);
        }
        /// <summary>
        /// 의심
        /// </summary>
        public void DoubtOtherPlayer()
        {
            var gm = GameManager.Instance;
            if (inputNum.text.Length!=3)
            {
                uiManager.ShowMessage("숫자 3자리를 \n입력해야 합니다.");
                return;
            }

            //결과 공개
            if(RealCardNumber() == inputNum.text)
            {
                //의심한 사람이 깍임
                gm.playerList[gm.CurrentUserIndex].DecreaseHP();
                gm.playerList[gm.CurrentUserIndex].ShowPlayerInfo();
                
            }
            else
            {
                //전 사람이 깍임
                int previousUserIdx = (gm.CurrentUserIndex - 1+gm.UserCount) % gm.UserCount;
                gm.playerList[previousUserIdx].DecreaseHP();
                gm.playerList[previousUserIdx].ShowPlayerInfo();
            }
            //창 닫기
            PassButton();
        }

        /// <summary>
        /// 정답 카드 번호
        /// </summary>
        /// <returns></returns>
        string RealCardNumber()
        {
            var cardManager = CardManager.Instance;
            string number = string.Empty;
            foreach (var card in cardManager.presentNumber)
            {
                number += card.Num.ToString();
            }
            Debug.Log("실제 카드 "+number);
            return number;
        }
    }

}