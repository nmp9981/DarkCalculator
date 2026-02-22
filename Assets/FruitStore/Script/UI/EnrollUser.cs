using TMPro;
using UnityEngine;

public class EnrollUser : MonoBehaviour
{
    [SerializeField]
    TMP_InputField[] enrollUserArray = new TMP_InputField[20];

    private void Awake()
    {
        int idx = 0;
        foreach (var tmp in this.gameObject.GetComponentsInChildren<TMP_InputField>(true))
        {
            enrollUserArray[idx] = tmp;
            idx++;
        }
    }

    private void OnEnable()
    {
        for(int i = 0; i < FruitGameManager.Instance.maxPeopleCount; i++)
        {
            if(i>=FruitGameManager.Instance.PeopleCount) enrollUserArray[i].gameObject.SetActive(false);
            else enrollUserArray[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 모든 유저들의 이름이 기록되었는지 검사
    /// </summary>
    /// <returns></returns>
    public bool CheckEnroll_AllUserNick()
    {
        int curCount = 0;
        for (int i = 0; i < FruitGameManager.Instance.maxPeopleCount; i++)
        {
            if (i >= FruitGameManager.Instance.PeopleCount) continue;
            else
            {
                string name = enrollUserArray[i].text;
                if (CheckEffectiveness_UserName(name)) curCount++;
            }
        }
        return curCount == FruitGameManager.Instance.PeopleCount;
    }

    /// <summary>
    /// 유저 네임 유효성 검사
    /// </summary>
    /// <returns></returns>
    public bool CheckEffectiveness_UserName(string name)
    {
        //Null 검사
        if (name == null) return false;

        //글자 수 검사
        if(name.Length >= 6 || name.Length<=1) return false;

        return true;
    }
}
