using TMPro;
using UnityEngine;

namespace FABFIB
{
    public class PassUI : MonoBehaviour
    {
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
            numText.text = inGame.callInput.text;
        }

        /// <summary>
        /// 패스
        /// </summary>
        public void PassButton()
        {
            this.gameObject.SetActive(false);
        }
        /// <summary>
        /// 의심
        /// </summary>
        public void DoubtOtherPlayer()
        {
            var gm = GameManager.Instance;
            
            //결과 공개
            if(RealCardNumber() == inGame.callInput.text)
            {
                Debug.Log("정답");
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