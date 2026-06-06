using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FABFIB
{
    public class NumberCard : MonoBehaviour
    {
        public int RandomValue;//랜덤 넘버(셔플용)
        public int Num;//표시 숫자
        public int Attack;//데미지
        public bool isClick;//클릭 여부

        [SerializeField] private Image cardImage;
        [SerializeField] private TextMeshProUGUI numText;
        [SerializeField] private List<GameObject> skullList = new();
        [SerializeField] private InGameMain gameMain;

        /// <summary>
        /// 카드 UI표시
        /// </summary>
        public void ShowCard()
        {
            //이미지 모두 끄기
            for (int i = 0; i < 3; i++)
            {
                skullList[i].SetActive(false);
            }

            //필요한 만큼 이미지 켜기
            numText.text = Num.ToString();
            for (int i = 0; i < Attack; i++)
            {
                skullList[i].SetActive(true);
            }
        }

        /// <summary>
        /// 카드 클릭
        /// </summary>
        public void ClickCard()
        {
            isClick = !isClick;
            if (isClick)
            {
                GameManager.Instance.CurChangeCount = Mathf.Min(GameManager.Instance.CurChangeCount+ 1, GameManager.maxChangeCount);
                cardImage.color = Color.yellow;
            }
            else
            {
                GameManager.Instance.CurChangeCount = Mathf.Max(GameManager.Instance.CurChangeCount - 1, 0);
                cardImage.color = Color.white;
            }
            gameMain.ShowRestChangeCardNum();
        }

        /// <summary>
        /// 클릭 상태 초기화
        /// </summary>
        public void InitClickState()
        {
            cardImage.color = Color.white;
            GameManager.Instance.CurChangeCount = Mathf.Max(GameManager.Instance.CurChangeCount - 1, 0);
            ShowCard();
        }
    }
}