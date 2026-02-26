using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoClass : MonoBehaviour
{
    //유저 과일 정보
    public string userName;
    public string fruit1Name;
    public string fruit2Name;

    //각 과일의 가격

    //각 라운드별 얻은 돈
    public int[] getMoney = new int[FruitGameManager.Instance.TotalRound];

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
    [SerializeField] Button secretButton;

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
    public void ConfirmPriceConnect()
    {
        FruitGameManager.Instance.Connect_FruitSettingUserUI(this,2);
    }

    /// <summary>
    /// 유저 정보 초기화
    /// </summary>
    public void InitUserInfo()
    {
        useExchangeButton.interactable = true;

        userName = string.Empty;
        fruit1Name = string.Empty;
        fruit2Name = string.Empty;

        for (int i = 0; i < FruitGameManager.Instance.TotalRound; i++) getMoney[i] = 0;

        
    }
}
