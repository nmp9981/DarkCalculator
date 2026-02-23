using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class EnrollUser : MonoBehaviour
{
    [SerializeField]
    TMP_InputField[] enrollUserArray = new TMP_InputField[20];
    [SerializeField]
    TextMeshProUGUI notifyText;//¾Ë¸²

    private void Awake()
    {
        notifyText.text = string.Empty;

        int idx = 0;
        foreach (var tmp in this.gameObject.GetComponentsInChildren<TMP_InputField>(true))
        {
            enrollUserArray[idx] = tmp;
            idx++;
        }
    }

    private void OnEnable()
    {
        notifyText.text = string.Empty;
        for(int i = 0; i < FruitGameManager.Instance.maxPeopleCount; i++)
        {
            if(i>=FruitGameManager.Instance.PeopleCount) enrollUserArray[i].gameObject.SetActive(false);
            else enrollUserArray[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ¸ðµç À¯ÀúµéÀÇ ÀÌ¸§ÀÌ ±â·ÏµÇ¾ú´ÂÁö °Ë»ç
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
                //ÀÌ¸§ À¯È¿¼º °Ë»ç
                if (CheckEffectiveness_UserName(name)) curCount++;
            }
        }
        return curCount == FruitGameManager.Instance.PeopleCount;
    }

    /// <summary>
    /// À¯Àú ³×ÀÓ À¯È¿¼º °Ë»ç
    /// </summary>
    /// <returns></returns>
    public bool CheckEffectiveness_UserName(string name)
    {
        //Null °Ë»ç
        if (name == null) return false;

        //±ÛÀÚ ¼ö °Ë»ç
        if(name.Length >= 6 || name.Length<=1) return false;
        
        //Æ¯¼ö ¹®ÀÚ °Ë»ç
        if(Regex.IsMatch(name, @"[^a-zA-Z0-9°¡-ÆR¤¡-¤¾¤¿-¤Ó\s]"))
        {
            return false;
        }
        return true;
    }

    public void ShowNotFullMassage()
    {
        notifyText.text = $"¸ðµç À¯ÀúÀÇ ´Ð³×ÀÓÀ»\n ÀÔ·ÂÇØ¾ß ÇÕ´Ï´Ù.";
        Invoke("Off_ShowNotFullMassage",0.5f);
    }
    void Off_ShowNotFullMassage()
    {
        notifyText.text = string.Empty; 
    }
}
