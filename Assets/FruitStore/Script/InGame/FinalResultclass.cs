using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalResultclass : MonoBehaviour
{
    [SerializeField]
    Button goToMainButton;
    [SerializeField]
    TextMeshProUGUI[] userRankTexts = new TextMeshProUGUI[20];
    [SerializeField]
    FruitUIManager fruitUIManager;

    //유저 순위
    List<(int money, string name)> userRanks = new List<(int money, string name)> ();

    private void OnEnable()
    {
        goToMainButton.gameObject.SetActive(true);
        ShowRankUser();
    }

    /// <summary>
    /// 유저 순위 공개
    /// </summary>
    void ShowRankUser()
    {
        //초기화
        foreach (var rank in userRankTexts)
        {
            rank.text = string.Empty;
        }
        
        //각 유저들의 최종 금액 계산
        foreach (var user in FruitGameManager.Instance.UserInfoList)
        {
            string userName = user.userName;
            int totalMoney = user.TotalGetMoney();
            userRanks.Add((totalMoney, userName));
        }
        //내림차순 정렬
        userRanks.Sort((a, b) => b.money.CompareTo(a.money));
        //1위부터 기록
        int idx = 0;
        int lastMoney = -1;
        int currentRank = 1;//실제 표시 순위
        foreach (var rank in userRanks)
        {
            //인덱스 아웃 방지
            if (idx >= FruitGameManager.Instance.PeopleCount) break;

            //더 낮은 순위 발생
            if (lastMoney != rank.money)
            {
                currentRank = idx + 1;
                lastMoney = rank.money;
            }

            //기록
            userRankTexts[idx].text = $"{currentRank}위 - {rank.name} {rank.money}원";

            //다음 칸
            idx++;
        }
    }

    public void GoToMain()
    {
        userRanks.Clear();
        fruitUIManager.ViewToMain();
    }
}
