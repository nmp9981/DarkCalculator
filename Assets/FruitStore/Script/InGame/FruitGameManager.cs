using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct FruitImageData
{
    public string fruitName;
    public Sprite fruitImage;
    public int restCount;
}
public class FruitGameManager : MonoBehaviour
{ 
    static FruitGameManager _instance;
    public static FruitGameManager Instance { get { Init(); return _instance; } }
    public FruitSettingUserUI setUserUI;

    static void Init()
    {
        if (_instance == null)
        {
            GameObject gm = GameObject.Find("FruitGameManager");
            if (gm == null)
            {
                gm = new GameObject { name = "FruitGameManager" };

                gm.AddComponent<FruitGameManager>();
            }
            DontDestroyOnLoad(gm);
            _instance = gm.GetComponent<FruitGameManager>();
        }
    }

    private void Awake()
    {
        foreach (var data in fruitImageList)
        {
            if (!fruitImageDic.ContainsKey(data.fruitName))
            {
                fruitImageDic.Add(data.fruitName, data.fruitImage);
                fruitTypeCountDic.Add(data.fruitName, 0);
            }
        }
    }

    /// <summary>
    /// 교환, 비밀, 확정 버튼 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="buttonNum"></param>
    public void Connect_FruitSettingUserUI(UserInfoClass user, int buttonNum)
    {
        switch (buttonNum)
        {
            case 1://교환
                setUserUI.ExchangeFruitShow(user);
                break;
            case 2://비밀
                break;
            case 3://확정
                break;
        }
    }

    #region 데이터
    private int totalRound =4;
    private int peopleCount;
    private int fruitTypeCount;

    public int maxFruitTypeCount = 10;
    public int maxPeopleCount = 20;
    public int TotalRound { get { return totalRound; } }
    public int PeopleCount { get { return peopleCount; } set { peopleCount = value; } }
    public int FruitTypeCount { get { return fruitTypeCount; } set { fruitTypeCount = value; } }

    public List<string> UserNameList = new List<string>();
    public List<UserInfoClass> UserInfoList  = new List<UserInfoClass>();
    public List<FruitImageData> fruitImageList = new List<FruitImageData>();
    public Dictionary<string, Sprite> fruitImageDic = new Dictionary<string, Sprite>();
    public Dictionary<string, int> fruitTypeCountDic = new Dictionary<string, int>();
    #endregion
}
