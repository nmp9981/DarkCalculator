using System.Collections.Generic;
using UnityEngine;

public class FruitSettingEachUser : MonoBehaviour
{
    [SerializeField]
    GameObject userInfoPrefab;
    [SerializeField]
    Transform scrollTransform;

    public GameObject[] userBlockList;

    void Start()
    {
        //유저 리스트 블록 생성
        userBlockList = new GameObject[FruitGameManager.Instance.maxPeopleCount];
        for (int i = 0; i < FruitGameManager.Instance.maxPeopleCount; i++)
        {
            userBlockList[i] = Instantiate(userInfoPrefab);
            userBlockList[i].transform.parent = scrollTransform;
        }
    }

    private void OnEnable()
    {
        EnrollUserInfo();
    }

    /// <summary>
    /// 초기 과일 개수 설정
    /// </summary>
    void InitEachFruitCount()
    {
        //총 인원
        int totalNum = FruitGameManager.Instance.PeopleCount;

        //과일 종류 개수 정하기
        if (totalNum < 10) FruitGameManager.Instance.FruitTypeCount = (totalNum + 1) / 2;
        else FruitGameManager.Instance.FruitTypeCount = totalNum / 2;

        int div = (2*totalNum) / FruitGameManager.Instance.FruitTypeCount;//몫
        int rest = (2 * totalNum) % FruitGameManager.Instance.FruitTypeCount;//나머지

        int idx = 0;
        var keys = new List<string>(FruitGameManager.Instance.fruitTypeCountDic.Keys);//키를 배열로
        foreach (var key in keys)
        {
            int targetValue = (idx < rest) ? (div + 1) : div;//최대 과일 종류 개수
            FruitGameManager.Instance.fruitTypeCountDic[key] = targetValue;
            idx++;
        }
    }

    /// <summary>
    /// 유저 정보 등록
    /// </summary>
    void EnrollUserInfo()
    {
        //초기 과일 개수 설정
        InitEachFruitCount();

        for (int i = 0; i < FruitGameManager.Instance.PeopleCount; i++)
        {
            userBlockList[i].SetActive(true);

            UserInfoClass user = userBlockList[i].GetComponent<UserInfoClass>();
            user.name = FruitGameManager.Instance.UserNameList[i];
            

            FruitGameManager.Instance.UserInfoList.Add(user);
        }
    }
}
