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
    }
}