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
    }
}