using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FABFIB
{
    public class PlayerInfo : MonoBehaviour
    {
        public string playerName;//플레이어 명
        public int playerHP;//플레이어 체력
        public int playerIndex;//플레이어 순서
        public int changeCount;//교체 횟수
        public bool isMyTurn;//내턴인가?

        public List<NumberCard> ownCards = new List<NumberCard>();//소유한 카드

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private Image turnSymbol;
        [SerializeField] private Sprite turnSymbolImage;
        [SerializeField] private Sprite deathImage;

        /// <summary>
        /// 플레이어 정보 등록
        /// </summary>
        public void EnrollPlayerInfo(string name, int order, bool isStart)
        {
            playerName = name;
            playerHP = GameManager.Instance.MaxPlayerHP;
            playerIndex = order;
            changeCount = GameManager.maxChangeCount;
            isMyTurn = isStart;
        }

        /// <summary>
        /// UI 보이기
        /// </summary>
        public void ShowPlayerInfo()
        {
            nameText.text = playerName;
            hpText.text = $"HP : {playerHP}";
            turnSymbol.sprite = turnSymbolImage;

            if(isMyTurn) turnSymbol.gameObject.SetActive(true);
            else turnSymbol.gameObject.SetActive(false);
        }

        /// <summary>
        /// HP 감소
        /// </summary>
        public void DecreaseHP()
        {
            int totalDamage = 0;
            foreach(var card in CardManager.Instance.presentNumber)
            {
                totalDamage += card.Attack;
            }
            playerHP = Mathf.Max(0, playerHP - totalDamage);

            //사망 판정
            if (playerHP <= 0) OutPlayer();
        }

        /// <summary>
        /// 플레이어 아웃
        /// </summary>
        public void OutPlayer()
        {
            turnSymbol.sprite = deathImage;
        }
    }
}
