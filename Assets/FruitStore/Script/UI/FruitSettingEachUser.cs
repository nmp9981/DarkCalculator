using System.Collections.Generic;
using UnityEngine;

public class FruitSettingEachUser : MonoBehaviour
{
    [SerializeField]
    GameObject userInfoPrefab;
    [SerializeField]
    Transform scrollTransform;

    public GameObject[] userBlockList;

    private int[] divFruitTypeCount;

    void Start()
    {
        //유저 리스트 블록 생성
        userBlockList = new GameObject[FruitGameManager.Instance.maxPeopleCount];
        for (int i = 0; i < FruitGameManager.Instance.maxPeopleCount; i++)
        {
            userBlockList[i] = Instantiate(userInfoPrefab);
            userBlockList[i].transform.parent = scrollTransform;
        }

        divFruitTypeCount = new int[FruitGameManager.Instance.maxPeopleCount];
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
            if (idx >= FruitGameManager.Instance.FruitTypeCount) break;//최대 과일 종류 개수 초과

            int targetValue = (idx < rest) ? (div + 1) : div;//각 종류별 최대 과일 개수
            FruitGameManager.Instance.fruitTypeCountDic[key] = (targetValue+2);
            divFruitTypeCount[idx] = targetValue;
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

        //랜덤 과일 순서
        Stack<int> fruitStack = RandomFruit(FruitGameManager.Instance.FruitTypeCount);

        //과일 배분
        for (int i = 0; i < FruitGameManager.Instance.PeopleCount; i++)
        {
            userBlockList[i].SetActive(true);

            UserInfoClass user = userBlockList[i].GetComponent<UserInfoClass>();
            user.name = FruitGameManager.Instance.UserNameList[i];
            user.fruit1Name = FruitGameManager.Instance.fruitImageList[fruitStack.Pop()].fruitName;
            user.fruit2Name = FruitGameManager.Instance.fruitImageList[fruitStack.Pop()].fruitName;

            FruitGameManager.Instance.fruitTypeCountDic[user.fruit1Name] -= 1;
            FruitGameManager.Instance.fruitTypeCountDic[user.fruit2Name] -= 1;

            FruitGameManager.Instance.UserInfoList.Add(user);
        }
    }
    /// <summary>
    /// 랜덤 과일 설정
    /// </summary>
    /// <returns></returns>
    Stack<int> RandomFruit(int typeCount)
    {
        //과일 분배용 랜덤 해쉬
        Stack<int> ranStack = new Stack<int>();
        int maxNum = FruitGameManager.Instance.PeopleCount*2;
        
        while (ranStack.Count < maxNum)
        {
            int ran = Random.Range(0, typeCount);

            //남은 과일인가?
            if (divFruitTypeCount[ran] == 0) continue;

            if (ranStack.Count % 2 == 1)//같은 유저
            {
                while (ran == ranStack.Peek())//다른 종류가 나올때까지 다시 뽑음
                {
                    ran = Random.Range(0, typeCount);
                }
                ranStack.Push(ran);
                divFruitTypeCount[ran] -= 1;
            }
            else//각 유저별 첫 과일
            {
                ranStack.Push(ran);
                divFruitTypeCount[ran] -= 1;
            }
        }
        return ranStack;
    }
}
