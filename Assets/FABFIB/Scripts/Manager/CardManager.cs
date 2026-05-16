using UnityEngine;

namespace FABFIB
{
    public class CardManager : MonoBehaviour
    {
        static CardManager _instance;

        public static CardManager Instance { get { Init(); return _instance; } }

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
    }
}