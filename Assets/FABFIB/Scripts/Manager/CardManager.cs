using System.Collections.Generic;
using UnityEngine;

namespace FABFIB
{
    public class CardManager : MonoBehaviour
    {
        static CardManager _instance;

        public static CardManager Instance { get { Init(); return _instance; } }

        public List<NumberCard> presentNumber = new List<NumberCard>();
        public InGameMain gameMain;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Transform cardPivot;

        static void Init()
        {
            if (_instance == null)
            {
                GameObject gm = GameObject.Find("CardManager");
                if (gm == null)
                {
                    gm = new GameObject { name = "CardManager" };

                    gm.AddComponent<CardManager>();
                }
                DontDestroyOnLoad(gm);
                _instance = gm.GetComponent<CardManager>();
            }
        }

        private void Start()
        {
            EnrollAllCard();
            ShuffleRestCard();
        }

        /// <summary>
        /// 전체 카드 등록
        /// </summary>
        void EnrollAllCard()
        {
            for (int i = 0; i < 10; i++)//0~9까지
            {
                //각 5장
                for (int j = 0; j < 5; j++)
                {
                    NumberCard card = new NumberCard();
                    if (j == 3)
                    {
                        card.Attack = 2;
                    } else if (j == 4)
                    {
                        card.Attack = 3;
                    }
                    else
                    {
                        card.Attack = 1;
                    }
                    card.Num = i;
                    card.RandomValue = Random.Range(0, int.MaxValue);

                    GameManager.Instance.restNumberCardList.Push(card);
                }
            }
        }

        /// <summary>
        /// 남는 카드 섞기
        /// </summary>
        public void ShuffleRestCard()
        {
            //랜덤시드 재정의
            List<NumberCard> tempcardList = new List<NumberCard>();
            var manager = GameManager.Instance;

            while (manager.restNumberCardList.Count > 0)
            {
                NumberCard card = manager.restNumberCardList.Pop();

                card.RandomValue = Random.Range(0, int.MaxValue);
                tempcardList.Add(card);
            }
            
            tempcardList.Sort((a, b) => a.RandomValue.CompareTo(b.RandomValue));
         
            //다시 등록
            foreach (var card in tempcardList)
            {
                manager.restNumberCardList.Push(card);
            }
        }
        /// <summary>
        /// 카드 다시 뽑기
        /// </summary>
        public void ChangeCard()
        {
            GameManager gm = GameManager.Instance;
            int playerIdx = gm.CurrentUserIndex;

            //횟수 남아있을대만 유효
            if (gm.playerList[playerIdx].changeCount < 1)
            {
                uiManager.ShowMessage("더 이상 교체할 수 없습니다");
                InitChangeState();
                return;
            }

            int changeIndex = -1;
            for (int i = 0; i < presentNumber.Count; i++)
            {
                if (presentNumber[i].isClick)
                {
                    changeIndex = i;
                    break;
                }
            }

            //바닥에 남은 패가 없으면 다시 뽑기
            if (gm.restNumberCardList.Count < 2)
            {
                ChargeCardInFloor();
            }

            //클릭한 카드가 없음
            if(changeIndex == -1)
            {
                uiManager.ShowMessage("클릭한 카드가 없습니다.");
                InitChangeState();
                return;
            }

            NumberCard clickCard = gm.restNumberCardList.Pop();
            //카드 정보 변경
            var card = presentNumber[changeIndex];
            card.Num = clickCard.Num;
            card.Attack = clickCard.Attack;
            card.isClick = false;
            card.RandomValue = clickCard.RandomValue;
            card.GetComponent<NumberCard>().InitClickState();

            //UI및 교체 가능 횟수 감소
            gm.usedCardList.Add(clickCard);
            gm.playerList[playerIdx].changeCount -= 1;
            gm.playerList[playerIdx].ShowPlayerInfo();
            gameMain.ShowRestChangeCardNum();

            SortOrderCard();
        }

        /// <summary>
        /// 카드 오름차순 정렬
        /// </summary>
        public void SortOrderCard()
        {
            for(int i = 0; i < 2; i++)
            {
                for(int j = i + 1; j < 3; j++)
                {
                    if (i == j) continue;

                    Transform aTrans = cardPivot.GetChild(i);
                    Transform bTrans = cardPivot.GetChild(j);
                    int a = aTrans.GetComponent<NumberCard>().Num;
                    int b = bTrans.GetComponent<NumberCard>().Num;

                    //swap
                    if (a < b)
                    {
                        int indexA = aTrans.GetSiblingIndex();
                        int indexB = bTrans.GetSiblingIndex();

                        aTrans.SetSiblingIndex(indexB);
                        bTrans.SetSiblingIndex(indexA);
                    }
                }
            }
        }

        /// <summary>
        /// 교체 표시 초기화
        /// </summary>
        public void InitChangeState()
        {
            for (int i = 0; i < presentNumber.Count; i++)
            {
                presentNumber[i].GetComponent<NumberCard>().InitClickState();
            }
        }

        /// <summary>
        /// 바닥에 있는 패 보충
        /// </summary>
        public void ChargeCardInFloor()
        {
            //사용한 카드 재등록
            foreach (var card in GameManager.Instance.usedCardList)
            {
                GameManager.Instance.restNumberCardList.Push(card);
            }
            GameManager.Instance.usedCardList.Clear();

            //남은 패 섞기
            ShuffleRestCard();
        }
    }
}