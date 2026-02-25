using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruitSettingUserUI : MonoBehaviour
{
    [SerializeField]
    GameObject exchangeUI;
    [SerializeField]
    GameObject exchangeCheckUI;

    [SerializeField]
    Button[] exchangeButtons = new Button[10];

    [SerializeField]
    UserInfoClass targetUserInfo;
    [SerializeField]
    string targetExchangeFruit;//바꿀 과일(결과)
    int exchangeIdx;

    //교환 UI 관련
    [SerializeField] Button fruit1ImageButton;
    [SerializeField] Button fruit2ImageButton;
    [SerializeField] GameObject deduplicationText;

    /// <summary>
    /// 과일 교환 창 띄우기
    /// </summary>
    /// <param name="user"></param>
    public void ExchangeFruitShow(UserInfoClass user)
    {
        exchangeUI.SetActive(true);

        targetUserInfo = user;

        var keys = new List<string>(FruitGameManager.Instance.fruitTypeCountDic.Keys);
        for (int i = 0; i < FruitGameManager.Instance.maxFruitTypeCount; i++)
        {
            //비활성화
            if(i >= FruitGameManager.Instance.FruitTypeCount)
            {
                exchangeButtons[i].transform.parent.gameObject.SetActive(false);
                continue;
            }

            exchangeButtons[i].transform.parent.gameObject.SetActive(true);

            //남은 개수 표시
            exchangeButtons[i].transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                $"X {FruitGameManager.Instance.fruitTypeCountDic[keys[i]]}";

            //남은 과일이 없을 경우 비활성화
            if (FruitGameManager.Instance.fruitTypeCountDic[keys[i]] == 0) exchangeButtons[i].interactable = false;
            else exchangeButtons[i].interactable = true;
        }
    }
    /// <summary>
    /// 과일 교환 찬 닫기
    /// </summary>
    public void ExitExchangeFruitUI()
    {
        exchangeUI.SetActive(false);
    }
    /// <summary>
    /// 과일 교환
    /// </summary>
    /// <param name="nextFruit"></param>
    public void ExchangeFruitCheckUI(string nextFruit)
    {
        exchangeCheckUI.SetActive(true);
        targetExchangeFruit = nextFruit;

        //과일 버튼 
        fruit1ImageButton.image.sprite = FruitGameManager.Instance.fruitImageDic[targetUserInfo.fruit1Name];
        fruit2ImageButton.image.sprite = FruitGameManager.Instance.fruitImageDic[targetUserInfo.fruit2Name];
    }
    /// <summary>
    /// 과일1 교환
    /// </summary>
    public void ExchangeFruit1()
    {
        exchangeIdx = 1;
    }
    /// <summary>
    /// 과일2 교환
    /// </summary>
    public void ExchangeFruit2()
    {
        exchangeIdx = 2;
    }
    /// <summary>
    /// 교환 취소
    /// </summary>
    public void CencelExchange()
    {
        exchangeCheckUI.SetActive(false);
    }
    /// <summary>
    /// 교환 확정
    /// </summary>
    public void ExcahngeFruitReal()
    {
        //한 유저가 중복과일을 가질 수 없음 -> 중복 검사
        bool isDeduplication = false;
        if (exchangeIdx == 1)
        {
            if(targetUserInfo.fruit2Name == targetExchangeFruit) isDeduplication=true;
        }
        if (exchangeIdx == 2)
        {
            if (targetUserInfo.fruit1Name == targetExchangeFruit) isDeduplication=true;
        }

        //과일을 중복으로 가지려함
        if (isDeduplication)
        {
            deduplicationText.SetActive(true);
            Invoke("ShowDeduplicationText", 0.5f);
            return;
        }
        
        //남은 아이템 개수 조절
        if (exchangeIdx==1) FruitGameManager.Instance.fruitTypeCountDic[targetUserInfo.fruit1Name] += 1;
        else FruitGameManager.Instance.fruitTypeCountDic[targetUserInfo.fruit2Name] += 1;
        FruitGameManager.Instance.fruitTypeCountDic[targetExchangeFruit] -= 1;

        //UI갱신
        if(exchangeIdx==1) targetUserInfo.fruit1Name = targetExchangeFruit;
        else targetUserInfo.fruit2Name = targetExchangeFruit;

        targetUserInfo.ShowUI();
        exchangeCheckUI.SetActive(false);

        exchangeCheckUI.SetActive(false);
    }

    /// <summary>
    /// 중복 방지 글자 삭제
    /// </summary>
    void ShowDeduplicationText()
    {
        deduplicationText.SetActive(false);
    }
}
