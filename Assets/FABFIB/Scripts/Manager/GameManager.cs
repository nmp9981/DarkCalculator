using NUnit.Framework;
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
        private int _userCount = 0;
        private int _minUserCount = 3;
        private int _maxUserCount = 10;
        private int _maxPlayerHP = 12;
        private int _startUserIndex;

        public int UserCount { get { return _userCount; } set { _userCount = value; } }
        public int MinUserCount { get { return _minUserCount; } }
        public int MaxUserCount { get {return _maxUserCount; } }
        public int MaxPlayerHP { get { return _maxPlayerHP; } }
        public int StartUserIndex {  get { return _startUserIndex; }set { _startUserIndex = value; } }

        public Stack<NumberCard> restNumberCardList = new Stack<NumberCard>();
        public List<PlayerInfo> playerList = new List<PlayerInfo>();
        public List<string> playerNameList = new List<string>();
    }
}