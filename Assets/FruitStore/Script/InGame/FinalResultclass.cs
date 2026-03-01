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
        userRanks.Sort();
        //꼴지부터 기록
        int idx = FruitGameManager.Instance.PeopleCount - 1;
        foreach (var rank in userRanks)
        {
            userRankTexts[idx].text = $"{idx+1}위 - {rank.name} {rank.money}원";
            idx--;

            if (idx == -1) break;
        }
    }

    public void GoToMain()
    {
        userRanks.Clear();
        fruitUIManager.ViewToMain();
    }
}
