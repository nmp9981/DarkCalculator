using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FABFIB
{
    public class NumberCard : MonoBehaviour
    {
        public int RandomValue;//랜덤 넘버(셔플용)
        public int Num;//표시 숫자
        public int Attack;//데미지
       
        [SerializeField] private TextMeshProUGUI numText;
        [SerializeField] private List<GameObject> skullList = new();

        /// <summary>
        /// 카드 UI표시
        /// </summary>
        public void ShowCard()
        {
            //이미지 모두 끄기
            for (int i = 0; i < Attack; i++)
            {
                skullList[i].gameObject.SetActive(false);
            }

            //필요한 만큼 이미지 켜기
            numText.text = Num.ToString();
            for (int i = 0; i < Attack; i++)
            {
                skullList[i].gameObject.SetActive(true);
            }
        }
    }

}