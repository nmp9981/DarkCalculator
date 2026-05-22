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

            //횟수 남아있을대만 유효
            if (gm.currentPlayer.changeCount < 1) return;

            int changeIndex = 0;
            for (int i = 0; i < presentNumber.Count; i++)
            {
                if (presentNumber[i].isClick)
                {
                    changeIndex = i;
                    break;
                }
            }

            NumberCard clickCard = gm.restNumberCardList.Pop();
            presentNumber[changeIndex] = clickCard;
            gm.usedCardList.Add(clickCard);

            presentNumber[changeIndex].GetComponent<NumberCard>().ShowCard();
            gm.currentPlayer.changeCount -= 1;
            gm.currentPlayer.ShowPlayerInfo();
            gameMain.ShowRestChangeCardNum();

            SortOrderCard();
        }

        /// <summary>
        /// 카드 오름차순 정렬
        /// </summary>
        public void SortOrderCard()
        {

        }
    }
}