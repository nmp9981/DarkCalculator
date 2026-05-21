using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FABFIB
{
    public class PlayerInfo : MonoBehaviour
    {
        const int totalChangeCount = 3;//총 교체 회수

        public string playerName;//플레이어 명
        public int playerHP;//플레이어 체력
        public int playerIndex;//플레이어 순서
        public int changeCount;//교체 횟수
        public bool isMyTurn;//내턴인가?

        public List<NumberCard> ownCards = new List<NumberCard>();//소유한 카드

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private Image turnSymbol;

        /// <summary>
        /// 플레이어 정보 등록
        /// </summary>
        public void EnrollPlayerInfo(string name, int order)
        {
            playerName = name;
            playerHP = GameManager.Instance.MaxPlayerHP;
            playerIndex = order;
            changeCount = totalChangeCount;
            isMyTurn = false;
        }

        /// <summary>
        /// UI 보이기
        /// </summary>
        public void ShowPlayerInfo()
        {
            nameText.text = playerName;
            hpText.text = $"HP : {playerHP}";
            if(isMyTurn) turnSymbol.gameObject.SetActive(true);
            else turnSymbol.gameObject.SetActive(false);
        }
    }
}
