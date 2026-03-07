using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitSettingEachUser : MonoBehaviour
{
    [SerializeField]
    GameObject userInfoPrefab;
    [SerializeField]
    Transform scrollTransform;

    public GameObject[] userBlockList;
    private int[] divFruitTypeCount;


    private void OnEnable()
    {
        EnrollUserInfo();//유저 정보 등록
    }

    /// <summary>
    /// 유저 오브젝트 풀링
    /// </summary>
    public void UserObjectFulling()
    {
        //유저 리스트 블록 생성
        userBlockList = new GameObject[FruitGameManager.Instance.maxPeopleCount];
        for (int i = 0; i < FruitGameManager.Instance.maxPeopleCount; i++)
        {
            userBlockList[i] = Instantiate(userInfoPrefab, scrollTransform, false);
            userBlockList[i].transform.parent = scrollTransform;
            userBlockList[i].SetActive(false);
        }

        divFruitTypeCount = new int[FruitGameManager.Instance.maxFruitTypeCount];
    }

    /// <summary>
    /// 초기 과일 개수 설정
    /// </summary>
    void InitEachFruitCount()
    {
        //총 인원
        int totalNum = FruitGameManager.Instance.PeopleCount;
        for (int i = 0; i < totalNum; i++)
        {
            userBlockList[i].gameObject.SetActive(true);
        }

        //과일 종류 개수 정하기
        FruitGameManager.Instance.FruitTypeCount = (totalNum-1)/3+2;

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
            //초기화 후 정보 입력
            user.InitUserInfo();

            user.userName = FruitGameManager.Instance.UserNameList[i];
            user.fruit1Name = FruitGameManager.Instance.fruitImageList[fruitStack.Pop()].fruitName;
            user.fruit2Name = FruitGameManager.Instance.fruitImageList[fruitStack.Pop()].fruitName;

            FruitGameManager.Instance.fruitTypeCountDic[user.fruit1Name] -= 1;
            FruitGameManager.Instance.fruitTypeCountDic[user.fruit2Name] -= 1;

            FruitGameManager.Instance.UserInfoList.Add(user);

            //UI업데이트
            user.ShowUI();
        }
    }
    /// <summary>
    /// 랜덤 과일 설정
    /// </summary>
    /// <returns></returns>
    Stack<int> RandomFruit(int typeCount)
    {
        //과일 분배용 랜덤 해쉬
        //Stack<int> ranStack = new Stack<int>();
        int maxNum = FruitGameManager.Instance.PeopleCount*2;
       
        //while (ranStack.Count < maxNum)
        //{
        //    int ran = Random.Range(0, typeCount);

        //    //남은 과일인가?
        //    if (divFruitTypeCount[ran] <= 0) continue;

        //    if (ranStack.Count % 2 == 1)//같은 유저
        //    {
        //        //다른 종류가 나올때까지 다시 뽑음
        //        //남은 과일이 있어야함, || divFruitTypeCount[ran]<=0
        //        while (ran == ranStack.Peek())
        //        {
        //            ran = Random.Range(0, typeCount);
        //        }
        //        ranStack.Push(ran);
        //        divFruitTypeCount[ran] -= 1;
        //    }
        //    else//각 유저별 첫 과일
        //    {
        //        ranStack.Push(ran);
        //        divFruitTypeCount[ran] -= 1;
        //    }
        //}

        //리스트 셔플 방식
        List<int> fruitTypeNumList = new List<int>();//과일 리스트
        List<int> randomList = Enumerable.Range(0, maxNum).ToList();//랜덤 번호
        List<int> randomFruitType = new List<int>();//배치한 과일 번호
        //000011112222등 과일 배치 : 개수가 넘치거나 모자라지 않음
        for (int i = 0; i < typeCount; i++)
        {
            int eachCount = divFruitTypeCount[i];
            for(int j=0;j<eachCount;j++) fruitTypeNumList.Add(i);
        }
        
        // 한 번만 순회하며 섞기 (O(n))
        for (int i = 0; i < maxNum; i++)
        {
            int rnd = Random.Range(i, maxNum);
            (randomList[i], randomList[rnd]) = (randomList[rnd], randomList[i]);
        }
        //각 유저에게 과일 우선 배치
        foreach (int i in randomList)
        {
            randomFruitType.Add(fruitTypeNumList[i]);
        }

        //각 유저의 두번째 과일만 검사(홀수 인덱스)
        for(int idx = 1; idx < maxNum; idx+=2)
        {
            //각 유저의 과일은 서로 달라야함
            if (randomFruitType[idx] != randomFruitType[idx - 1]) continue;

            //같을 경우 다음 홀수 인덱스부터 탐색
            for (int idxJ = idx + 2; idxJ < maxNum; idxJ += 2)
            {
                //교환 가능
                if(randomFruitType[idx]!= randomFruitType[idxJ])
                {
                    int temp = randomFruitType[idxJ];
                    randomFruitType[idxJ] = randomFruitType[idx];
                    randomFruitType[idx] = temp;
                    break;
                }
            }
        }
        //마지막 과일 : maxNum-2인덱스
        if (randomFruitType[maxNum-1] == randomFruitType[maxNum - 2])
        {
            //같을 경우 처음 홀수 인덱스부터 탐색
            for (int idxJ = 1; idxJ < maxNum-2; idxJ += 2)
            {
                //교환 가능
                if (randomFruitType[maxNum - 2] != randomFruitType[idxJ - 1] &&
                    randomFruitType[maxNum - 1] != randomFruitType[idxJ])
                {
                    int temp = randomFruitType[idxJ];
                    randomFruitType[idxJ] = randomFruitType[maxNum - 2];
                    randomFruitType[maxNum - 2] = temp;
                    break;
                }
            }
        }

        //스택에 등록
        Stack<int> ranStack2 = new Stack<int>();
        foreach (int idx in randomFruitType) ranStack2.Push(idx);

        return ranStack2;
    }
}
