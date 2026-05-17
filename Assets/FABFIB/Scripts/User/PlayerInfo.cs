using System.Collections.Generic;
using UnityEngine;

namespace FABFIB
{
    public class PlayerInfo : MonoBehaviour
    {
        public string playerName;//플레이어 명
        public int playerHP;//플레이어 체력
        public int playerIndex;//플레이어 순서
        public int changeCount;//교체 횟수

        public List<NumberCard> ownCards = new List<NumberCard>();//소유한 카드
    }
}
