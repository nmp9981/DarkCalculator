using System.Collections.Generic;
using UnityEngine;

namespace FABFIB
{
    public class GameManager : MonoBehaviour
    {
        static GameManager _instance;

        public static GameManager Instance { get { Init(); return _instance; } }

        static public Dictionary<string, int> mapDictoinaty;
        static public List<Vector3> startPosList = new List<Vector3>();
        static void Init()
        {
            if (_instance == null)
            {
                GameObject gm = GameObject.Find("FABFIBGameManager");
                if (gm == null)
                {
                    gm = new GameObject { name = "FABFIBGameManager" };

                    gm.AddComponent<GameManager>();
                }
                DontDestroyOnLoad(gm);
                _instance = gm.GetComponent<GameManager>();
            }
        }

        void Awake()
        {
            Init();
        }

        //ÀÎ¿ø ¼ö
        private int userCount = 0;
        private int minUserCount = 3;
        private int maxUserCount = 10;


        public int UserCount { get { return userCount; } set { userCount = value; } }
        public int MinUserCount { get { return minUserCount; } }
        public int MaxUserCount { get {return maxUserCount; } }
    }
}