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
        notifyText.gameObject.SetActive(false);

        for (int i = 0; i < FruitGameManager.Instance.maxPeopleCount; i++)
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
        for (int i = 0; i < FruitGameManager.Instance.PeopleCount; i++)
        {
            string name = enrollUserArray[i].text;
            //ÀÌ¸§ À¯È¿¼º °Ë»ç
            if (CheckEffectiveness_UserName(name)) curCount++;
        }

        //ÀüºÎ À¯È¿
        if(curCount == FruitGameManager.Instance.PeopleCount)
        {
            for (int i = 0; i < curCount; i++)
            {
                string name = enrollUserArray[i].text;
                FruitGameManager.Instance.UserNameList.Add(name);
            }
            return true;
        }

        return false;//À¯È¿ÇÏÁö ¾ÊÀº ÀÌ¸§µéÀÌ ÀÖÀ½
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
        if (name.Length >= 6 || name.Length <= 1)
        {
            ShowNotLengthMassage();
            return false;
        }
        
        //Æ¯¼ö ¹®ÀÚ °Ë»ç
        if(Regex.IsMatch(name, @"[^a-zA-Z0-9°¡-ÆR¤¡-¤¾¤¿-¤Ó\s]"))
        {
            return false;
        }
        return true;
    }

    public void ShowNotFullMassage()
    {
        notifyText.gameObject.SetActive(true);
        notifyText.text = $"¸ðµç À¯ÀúÀÇ ´Ð³×ÀÓÀ»\n ÀÔ·ÂÇØ¾ß ÇÕ´Ï´Ù.\nÀÌ¸§¿¡ Æ¯¼ö¹®ÀÚ°¡ ¼¯ÀÌÁö´Â\n¾Ê¾Ò´ÂÁö È®ÀÎÇØ ÁÖ¼¼¿ä.";
        Invoke("Off_ShowNotFullMassage",0.5f);
    }
    public void ShowNotLengthMassage()
    {
        notifyText.gameObject.SetActive(true);
        notifyText.text = $"´Ð³×ÀÓÀÇ ±ÛÀÚ ¼ö´Â\n 2~5ÀÚ¸¸ °¡´ÉÇÕ´Ï´Ù.";
        Invoke("Off_ShowNotFullMassage", 0.5f);
    }
    void Off_ShowNotFullMassage()
    {
        notifyText.text = string.Empty;
        notifyText.gameObject.SetActive(false);
    }
}
