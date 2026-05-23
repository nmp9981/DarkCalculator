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
            //다음 사람으로 넘어감
            this.gameObject.SetActive(false);
        }
        /// <summary>
        /// 의심
        /// </summary>
        public void DoubtOtherPlayer()
        {
            if(inputNum.text.Length!=3)
            {
                uiManager.ShowMessage("숫자 3자리를 \n입력해야 합니다.");
                return;
            }

            //결과 공개
            if(RealCardNumber() == inputNum.text)
            {
                //의심한 사람이 깍임


            }
            else
            {
                //전 사람이 깍임
            }
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
            return number;
        }
    }

}