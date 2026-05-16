using System.Collections.Generic;
using UnityEngine;

namespace FABFIB
{
    public class CardManager : MonoBehaviour
    {
        static CardManager _instance;

        public static CardManager Instance { get { Init(); return _instance; } }

        public List<NumberCard> presentNumber=new List<NumberCard>();

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
        }

        /// <summary>
        /// 전체 카드 등록
        /// </summary>
        void EnrollAllCard()
        {
            for(int i = 0; i < 10; i++)//0~9까지
            {
                //각 5장
                for(int j = 0; j < 5; j++)
                {
                    NumberCard card = new NumberCard();
                    if (j == 3)
                    {
                        card.Attack = 2;
                    }else if (j==4)
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

            tempcardList.Sort();

            //다시 등록
            foreach(var card in tempcardList)
            {
                manager.restNumberCardList.Push(card);
            }
        }
    }
}