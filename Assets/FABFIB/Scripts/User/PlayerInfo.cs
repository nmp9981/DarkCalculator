using System.Collections.Generic;
using UnityEngine;

namespace FABFIB
{
    public class PlayerInfo : MonoBehaviour
    {
        public string playerName;
        public int playerHP;
        public int playerIndex;

        public List<NumberCard> ownCards = new List<NumberCard>();
    }
}
