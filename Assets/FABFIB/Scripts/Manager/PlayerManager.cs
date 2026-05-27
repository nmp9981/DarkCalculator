using TMPro;
using UnityEngine;

namespace FABFIB
{
    public class PlayerManager : MonoBehaviour
    {
        static PlayerManager _instance;

        public static PlayerManager Instance { get { Init(); return _instance; } }

        [SerializeField] GameObject _victoryObj;
        [SerializeField] TextMeshProUGUI _winnerText;

        static void Init()
        {
            if (_instance == null)
            {
                GameObject gm = GameObject.Find("PlayerManager");
                if (gm == null)
                {
                    gm = new GameObject { name = "PlayerManager" };

                    gm.AddComponent<PlayerManager>();
                }
                DontDestroyOnLoad(gm);
                _instance = gm.GetComponent<PlayerManager>();
            }
        }

        /// <summary>
        /// 각 플레이어에게 카드 분배
        /// </summary>
        public void DivideCard_EachPlayer()
        {
            var manager = GameManager.Instance;
            for(int i = 0; i < manager.UserCount; i++)
            {
                //3장 뽑기
                for(int j = 0; j < 3; j++)
                {
                    NumberCard card = manager.restNumberCardList.Pop();
                    manager.playerList[i].ownCards.Add(card);
                }
            }
        }

        /// <summary>
        /// 다음 플레이어 인덱스
        /// </summary>
        /// <returns></returns>
        public int NextPlayerIndex()
        {
            var gm = GameManager.Instance;
            int nextIdx = gm.CurrentUserIndex;
            for (int i = 0; i < gm.UserCount; i++)
            {
                nextIdx = (nextIdx+1)%gm.UserCount;
                if (gm.playerList[nextIdx].playerHP > 0) break;
            }
            return nextIdx;
        }
        /// <summary>
        /// 이전 플레이어 인덱스
        /// </summary>
        /// <returns></returns>
        public int PreviousPlayerIndex()
        {
            var gm = GameManager.Instance;
            int prevIdx = gm.CurrentUserIndex;
            for (int i = 0; i < gm.UserCount; i++)
            {
                prevIdx = (prevIdx - 1+gm.UserCount) % gm.UserCount;
                if (gm.playerList[prevIdx].playerHP > 0) break;
            }
            return prevIdx;
        }

        /// <summary>
        /// 최종 승리자
        /// </summary>
        public void WinPlayer()
        {
            //검사
            int saveCount = 0;
            string winner = string.Empty;
            foreach(var player in GameManager.Instance.playerList)
            {
                if (player.playerHP != 0)
                {
                    saveCount += 1;
                    winner = player.playerName;
                }
            }

            //1명만 남음
            if (saveCount == 1)
            {
                _victoryObj.gameObject.SetActive(true);
                _winnerText.text = winner;
            }
        }
    }
}