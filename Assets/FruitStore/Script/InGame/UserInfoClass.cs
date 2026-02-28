using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct SellInfo
{
    public string name;
    public int money;
}

public class UserInfoClass : MonoBehaviour
{
    //유저 과일 정보
    public string userName;
    public string fruit1Name;
    public string fruit2Name;

    //각 라운드별 제출한 돈
    [SerializeField] public SellInfo[,] sellMoneyRound;
    
    //각 라운드별 얻은 돈
    public int[] getMoney;

    //교환, 비밀
    public bool isSecret;

    //UI 요소
    [SerializeField] FruitSettingUserUI settingUI;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI fruit1Text;
    [SerializeField] Image fruit1Image;
    [SerializeField] TextMeshProUGUI fruit2Text;
    [SerializeField] Image fruit2Image;

    //버튼
    [SerializeField] Button useExchangeButton;
    [SerializeField] public Button fruit1SellButton;
    [SerializeField] public Button fruit2SellButton;
    [SerializeField] Image fruit1ButtonImage;
    [SerializeField] Image fruit2ButtonImage;

    public UserInfoClass(string name, string fruit1, string fruit2)
    {
        userName = name;
        fruit1Name = fruit1;
        fruit2Name = fruit2;
    }
    
    /// <summary>
    /// 최종 번 금액
    /// </summary>
    /// <returns></returns>
    public int TotalGetMoney()
    {
        int money = 0;
        for(int i = 0; i < getMoney.Length; i++) money+= getMoney[i];
        return money;
    }
    /// <summary>
    /// UI 표시
    /// </summary>
    public void ShowUI()
    {
        nameText.text = userName;
        fruit1Text.text = fruit1Name;
        fruit1Image.sprite = FruitGameManager.Instance.fruitImageDic[fruit1Name];
        fruit2Text.text = fruit2Name;
        fruit2Image.sprite = FruitGameManager.Instance.fruitImageDic[fruit2Name];

        fruit1ButtonImage.sprite = fruit1Image.sprite;
        fruit2ButtonImage.sprite = fruit2Image.sprite;
    }

    /// <summary>
    /// 교환권 사용
    /// </summary>
    public void UseExchange()
    {
        useExchangeButton.interactable = false;
    }
    /// <summary>
    /// 비밀 사용
    /// </summary>
    public void UseSecretPrice()
    {
        isSecret = true;
    }

    /// <summary>
    /// 과일 교환 연결
    /// </summary>
    public void ExchangeFruitConnect()
    {
        FruitGameManager.Instance.Connect_FruitSettingUserUI(this,1);
        ShowUI();
    }

    /// <summary>
    /// 가격 확정 연결
    /// </summary>
    public void ConfirmPrice1Connect()
    {
        FruitGameManager.Instance.Connect_FruitSettingUserUI(this,2);
    }
    /// <summary>
    /// 가격 확정 연결
    /// </summary>
    public void ConfirmPrice2Connect()
    {
        FruitGameManager.Instance.Connect_FruitSettingUserUI(this, 3);
    }

    /// <summary>
    /// 유저 정보 초기화
    /// </summary>
    public void InitUserInfo()
    {
        useExchangeButton.interactable = true;
        isSecret = false;

        userName = string.Empty;
        fruit1Name = string.Empty;
        fruit2Name = string.Empty;

        fruit1SellButton.interactable=true;
        fruit2SellButton.interactable=true;

        sellMoneyRound = new SellInfo[FruitGameManager.Instance.TotalRound, 2];
        getMoney = new int[FruitGameManager.Instance.TotalRound];
        for (int i = 0; i < FruitGameManager.Instance.TotalRound; i++)
        {
            getMoney[i] = 0;
            sellMoneyRound[i,0].name = string.Empty;
            sellMoneyRound[i,0].money = 0;
            sellMoneyRound[i, 1].name = string.Empty;
            sellMoneyRound[i, 1].money = 0;
        }
    }
}
