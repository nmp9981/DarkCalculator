using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruitSettingUserUI : MonoBehaviour
{
    [SerializeField]
    GameObject exchangeUI;

    [SerializeField]
    Button[] exchangeButtons = new Button[10]; 

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 과일 교환 창 띄우기
    /// </summary>
    /// <param name="user"></param>
    public void ExchangeFruit(UserInfoClass user)
    {
        exchangeUI.SetActive(true);

        var keys = new List<string>(FruitGameManager.Instance.fruitTypeCountDic.Keys);
        for (int i = 0; i < FruitGameManager.Instance.FruitTypeCount; i++)
        {
            exchangeButtons[i].transform.parent.gameObject.SetActive(true);

            //남은 개수 표시
            exchangeButtons[i].transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                $"X {FruitGameManager.Instance.fruitTypeCountDic[keys[i]]}";

            //남은 과일이 없을 경우 비활성화
            if (FruitGameManager.Instance.fruitTypeCountDic[keys[i]] == 0) exchangeButtons[i].interactable = false;
            else exchangeButtons[i].interactable = true;
        }
    }
}
