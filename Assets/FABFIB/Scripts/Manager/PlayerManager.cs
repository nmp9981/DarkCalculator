using System.Collections.Generic;
using UnityEngine;

namespace FABFIB
{
    public class PlayerManager : MonoBehaviour
    {
        static PlayerManager _instance;

        public static PlayerManager Instance { get { Init(); return _instance; } }

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
    }
}