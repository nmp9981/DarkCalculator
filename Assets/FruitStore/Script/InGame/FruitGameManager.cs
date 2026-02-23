using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct FruitImageData
{
    public string fruitName;
    public Image fruitImage;
    public int restCount;
}
public class FruitGameManager : MonoBehaviour
{ 
    static FruitGameManager _instance;
    public static FruitGameManager Instance { get { Init(); return _instance; } }

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

    #region µ•¿Ã≈Õ
    private int totalRound =4;
    private int peopleCount;
    private int fruitTypeCount;

    public int maxPeopleCount = 20;
    public int TotalRound { get { return totalRound; } }
    public int PeopleCount { get { return peopleCount; } set { peopleCount = value; } }
    public int FruitTypeCount { get { return fruitTypeCount; } set { fruitTypeCount = value; } }

    public List<string> UserNameList = new List<string>();
    public List<UserInfoClass> UserInfoList  = new List<UserInfoClass>();
    public List<FruitImageData> fruitImageList = new List<FruitImageData>();
    public Dictionary<string, Image> fruitImageDic = new Dictionary<string, Image>();
    public Dictionary<string, int> fruitTypeCountDic = new Dictionary<string, int>();
    #endregion
}
