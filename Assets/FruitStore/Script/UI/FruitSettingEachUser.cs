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

    // GC 할당 방지를 위한 캐싱 리스트
    private List<string> _cachedKeys = new List<string>();
    private List<int> _fruitTypeNumList = new List<int>(40);
    private List<int> _randomList = new List<int>(40);
    private List<int> _randomFruitType = new List<int>(40);

    private void OnEnable()
    {
        EnrollUserInfo();//유저 정보 등록
    }

    /// <summary>
    /// 유저 오브젝트 풀링
    /// </summary>
    public void UserObjectFulling()
    {
        var gm = FruitGameManager.Instance;
        //유저 리스트 생성 풀링
        userBlockList = new GameObject[gm.maxPeopleCount];
        for (int i = 0; i < gm.maxPeopleCount; i++)
        {
            userBlockList[i] = Instantiate(userInfoPrefab, scrollTransform, false);
            userBlockList[i].transform.parent = scrollTransform;
            userBlockList[i].SetActive(false);
        }

        divFruitTypeCount = new int[gm.maxFruitTypeCount];
    }

    /// <summary>
    /// 초기 과일 수량 설정
    /// </summary>
    void InitEachFruitCount()
    {
        var gm = FruitGameManager.Instance;
        //총 인원
        int totalNum = gm.PeopleCount;
        for (int i = 0; i < totalNum; i++)
        {
            userBlockList[i].gameObject.SetActive(true);
        }

        //과일 종류 개수 구하기
        gm.FruitTypeCount = (totalNum-1)/3+2;

        int div = (2*totalNum) / gm.FruitTypeCount;//몫
        int rest = (2 * totalNum) % gm.FruitTypeCount;//나머지

        // 기존 리스트를 재활용하여 GC 할당 방지
        _cachedKeys.Clear();
        _cachedKeys.AddRange(gm.fruitTypeCountDic.Keys);

        int idx = 0;
        foreach (var key in _cachedKeys)
        {
            if (idx >= gm.FruitTypeCount) break;//최대 과일 종류 개수 초과

            int targetValue = (idx < rest) ? (div + 1) : div;//각 과일에 최대 수량 설정
            gm.fruitTypeCountDic[key] = (targetValue+2);
            divFruitTypeCount[idx] = targetValue;
            idx++;
        }
    }

    /// <summary>
    /// 유저 정보 등록
    /// </summary>
    void EnrollUserInfo()
    {
        var gm = FruitGameManager.Instance;

        //초기 과일 수량 설정
        InitEachFruitCount();

        //과일 랜덤 배분
        List<int> fruitList = RandomFruit(gm.FruitTypeCount);

        //유저 등록 (뒤에서부터 소비 - 기존 Stack.Pop과 동일 순서)
        int fruitIdx = fruitList.Count - 1;
        for (int i = 0; i < gm.PeopleCount; i++)
        {
            userBlockList[i].SetActive(true);

            UserInfoClass user = userBlockList[i].GetComponent<UserInfoClass>();
            //초기화 및 정보 입력
            user.InitUserInfo();

            user.userName = gm.UserNameList[i];
            user.fruit1Name = gm.fruitImageList[fruitList[fruitIdx--]].fruitName;
            user.fruit2Name = gm.fruitImageList[fruitList[fruitIdx--]].fruitName;

            gm.fruitTypeCountDic[user.fruit1Name] -= 1;
            gm.fruitTypeCountDic[user.fruit2Name] -= 1;

            gm.UserInfoList.Add(user);

            //UI업데이트
            user.ShowUI();
        }
    }
    /// <summary>
    /// 과일 랜덤 배분
    /// </summary>
    /// <returns></returns>
    List<int> RandomFruit(int typeCount)
    {
        int maxNum = FruitGameManager.Instance.PeopleCount*2;

        // 캐싱 리스트 재활용 - Clear는 Capacity를 유지하므로 재할당 없음
        _fruitTypeNumList.Clear();
        _randomList.Clear();
        _randomFruitType.Clear();

        // Enumerable.Range 대신 직접 루프 (Iterator 할당 제거)
        for (int i = 0; i < maxNum; i++) _randomList.Add(i);

        //000011112222와 같이 배치 : 과일별 배치수가 다르거나 부족하면 조정
        for (int i = 0; i < typeCount; i++)
        {
            int eachCount = divFruitTypeCount[i];
            for(int j=0;j<eachCount;j++) _fruitTypeNumList.Add(i);
        }

        // 각 원소 순회하며 셔플 (O(n))
        for (int i = 0; i < maxNum; i++)
        {
            int rnd = Random.Range(i, maxNum);
            (_randomList[i], _randomList[rnd]) = (_randomList[rnd], _randomList[i]);
        }
        //각 과일별로 랜덤 우선 배치
        for (int i = 0; i < _randomList.Count; i++)
        {
            _randomFruitType.Add(_fruitTypeNumList[_randomList[i]]);
        }

        //각 유저의 두번째 과일만 검사(홀수 인덱스)
        for(int idx = 1; idx < maxNum; idx+=2)
        {
            //각 유저의 과일이 서로 달라야함
            if (_randomFruitType[idx] != _randomFruitType[idx - 1]) continue;

            //같은 경우 다음 홀수 인덱스에서 탐색
            for (int idxJ = idx + 2; idxJ < maxNum; idxJ += 2)
            {
                //교환 조건
                if(_randomFruitType[idx]!= _randomFruitType[idxJ])
                {
                    int temp = _randomFruitType[idxJ];
                    _randomFruitType[idxJ] = _randomFruitType[idx];
                    _randomFruitType[idx] = temp;
                    break;
                }
            }
        }
        //마지막 검증 : maxNum-2인덱스
        if (_randomFruitType[maxNum-1] == _randomFruitType[maxNum - 2])
        {
            //같은 경우 처음 홀수 인덱스에서 탐색
            for (int idxJ = 1; idxJ < maxNum-2; idxJ += 2)
            {
                //교환 조건
                if (_randomFruitType[maxNum - 2] != _randomFruitType[idxJ - 1] &&
                    _randomFruitType[maxNum - 1] != _randomFruitType[idxJ])
                {
                    int temp = _randomFruitType[idxJ];
                    _randomFruitType[idxJ] = _randomFruitType[maxNum - 2];
                    _randomFruitType[maxNum - 2] = temp;
                    break;
                }
            }
        }

        return _randomFruitType;
    }
}
