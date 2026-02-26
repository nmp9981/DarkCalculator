using System.Collections.Generic;
using UnityEngine;

public class SellResultClass : MonoBehaviour
{
    //각 과일마다 누가 얼마에 팔았는지
    Dictionary<string, List<(string,int)>> allUsersellDic = new Dictionary<string, List<(string, int)>>();

    /// <summary>
    /// 모든 유저 확정했는가?
    /// </summary>
    public void AllUserConfirm()
    {
        int notActiveCount = 0;
        int allButtonCount = FruitGameManager.Instance.PeopleCount * 2;
        foreach (var user in FruitGameManager.Instance.UserInfoList)
        {
            if (!user.fruit1SellButton.interactable) notActiveCount++;
            if (!user.fruit2SellButton.interactable) notActiveCount++;
        }

        //모든 유저가 확정했을 경우 판매 결과 계산
        if (notActiveCount == allButtonCount) CalSellResult();
    }

    /// <summary>
    /// 판매 결과 계산
    /// </summary>
    private void CalSellResult()
    {
        //각 유저별로 검사
        foreach (var user in FruitGameManager.Instance.UserInfoList)
        {
            for(int idx = 0; idx < 2; idx++)
            {
                //이 과일을
                string sellFruitName = user.sellMoneyRound[FruitGameManager.Instance.CurrentRound, idx].name;
                //누가
                string sellUserName = user.userName;
                //얼마에 팔았는지
                int sellPrice = user.sellMoneyRound[FruitGameManager.Instance.CurrentRound, idx].money;


                if (!allUsersellDic.ContainsKey(sellFruitName))
                {
                    allUsersellDic.Add(sellFruitName, new List<(string, int)>());
                }
               
                //정보 추가
                allUsersellDic[sellFruitName].Add((sellUserName, sellPrice));
            }
        }
    }
}
