using UnityEngine;

public class FruitSettingEachUser : MonoBehaviour
{
    [SerializeField]
    GameObject userInfoPrefab;
    [SerializeField]
    Transform scrollTransform;

    public GameObject[] userBlockList = new GameObject[FruitGameManager.Instance.maxPeopleCount];

    void Start()
    {
        //유저 리스트 블록 생성
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
    /// 유저 정보 등록
    /// </summary>
    void EnrollUserInfo()
    {
        for (int i = 0; i < FruitGameManager.Instance.PeopleCount; i++)
        {
            userBlockList[i].SetActive(true);

            UserInfoClass user = userBlockList[i].GetComponent<UserInfoClass>();

            

            FruitGameManager.Instance.UserInfoList.Add(user);
        }
    }
}
