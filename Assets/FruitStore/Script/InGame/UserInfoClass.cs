using UnityEngine;

public class UserInfoClass : MonoBehaviour
{
    //유저 과일 정보
    public string userName;
    public string fruit1Name;
    public string fruit2Name;

    //각 과일의 가격

    //각 라운드별 얻은 돈
    public int[] getMoney = new int[FruitGameManager.Instance.TotalRound];

    public UserInfoClass(string name, string fruit1, string fruit2)
    {
        userName = name;
        fruit1Name = fruit1;
        fruit2Name = fruit2;
    }
    
    /// <summary>
    /// 최종 번 금액
    /// </summary>
    /// <returns></returns>
    public int TotalGetMoney()
    {
        int money = 0;
        for(int i = 0; i < getMoney.Length; i++) money+= getMoney[i];
        return money;
    }
}
