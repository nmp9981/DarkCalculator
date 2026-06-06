using System.Collections.Generic;
using UnityEngine;

namespace FABFIB
{
    /// <summary>
    /// 게임 모드
    /// </summary>
    public enum FABFIBMode
    {
        General,
        Turn,
        Count
    }

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

        public const int maxChangeCount = 3;
        public const int totalTurnCount = 7;

        //인원 수
        private int _userCount = 0;
        private int _minUserCount = 3;
        private int _maxUserCount = 10;
        private int _maxPlayerHP = 12;
        private int _startUserIndex;
        private int _curChangeCount = 0;//현재 교환한 카드 개수
        [SerializeField] private int _currentPlayerIdx;//현재 플레이어
        private int _callNumber = -1;//부른 번호
        private FABFIBMode _fabfibMode;//게임 모드
        private int _curTurnCount;//현재 턴 수

        public int UserCount { get { return _userCount; } set { _userCount = value; } }
        public int MinUserCount { get { return _minUserCount; } }
        public int MaxUserCount { get {return _maxUserCount; } }
        public int MaxPlayerHP { get { return _maxPlayerHP; } }
        public int StartUserIndex {  get { return _startUserIndex; }set { _startUserIndex = value; } }
        public int CurChangeCount { get { return _curChangeCount; } set { _curChangeCount = value; } }
        public int CurrentUserIndex { get { return _currentPlayerIdx; } set { _currentPlayerIdx = value; } }
        public int CallNumber { get { return _callNumber; } set { _callNumber = value; } }
        public FABFIBMode FABFIBGameMode { get { return _fabfibMode; } set{ _fabfibMode = value; } }
        public int CurTurnCount { get { return _curTurnCount; } set { _curTurnCount = value; } }

        public Stack<NumberCard> restNumberCardList = new Stack<NumberCard>();
        public List<NumberCard> usedCardList = new List<NumberCard>();//사용한 카드
        public List<PlayerInfo> playerList = new List<PlayerInfo>();
        public List<string> playerNameList = new List<string>();
    }
}