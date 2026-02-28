using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruitPriceConfirmUI : MonoBehaviour
{
    [SerializeField]
    GameObject priceCheckUI;

    UserInfoClass userInfo;

    [SerializeField]
    List<Toggle> priceToggles;
    [SerializeField]
    Toggle secretToggle;
    [SerializeField]
    TextMeshProUGUI notiText;

    private int fruitIdx;
    private int selectPrice;
    private string sellFruitName;

    /// <summary>
    /// 과일 가격 결정 UI보이기
    /// </summary>
    public void FruitPriceDecideUIShow(UserInfoClass user, string fruitName, int idx)
    {
        priceCheckUI.SetActive(true);

        //맨 처음엔 모두 비활성화
        foreach (Toggle toggle in priceToggles)
        {
            toggle.isOn = false;
        }

        userInfo = user;
        sellFruitName = fruitName;
        fruitIdx = idx;
    }
    /// <summary>
    /// 과일 가격 선택
    /// </summary>
    /// <param name="price"></param>
    public void SelectPrice(int price)
    {
        selectPrice = price;
    }
    /// <summary>
    /// 과일 가격 선택
    /// </summary>
    /// <param name="price"></param>
    public void SelectSecret()
    {
        userInfo.isSecret = true;
    }
    /// <summary>
    /// 과일 가격 확정
    /// </summary>
    public void FruitPriceConfirms()
    {
        if (!CheckSinglePrice())//가격 중복 체크
        {
            notiText.gameObject.SetActive(true);
            Invoke("ShowNoti", 0.5f);
        }
        else
        {
            //가격 확정
            userInfo.sellMoneyRound[FruitGameManager.Instance.CurrentRound - 1, fruitIdx]
                = new SellInfo { name = sellFruitName, money = selectPrice };
           
            //버튼 비활성화
            if(fruitIdx==0) userInfo.fruit1SellButton.interactable = false;
            else userInfo.fruit2SellButton.interactable = false;

            secretToggle.interactable = !userInfo.isSecret;
            priceCheckUI.SetActive(false);
        }
    }
    /// <summary>
    /// 가격 UI창 닫기
    /// </summary>
    public void ClosePriceUI()
    {
        priceCheckUI.SetActive(false);
    }

    /// <summary>
    /// 알림 메세지 지우기
    /// </summary>
    void ShowNoti()
    {
        notiText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 중복 가격을 체크했는지 체크
    /// </summary>
    /// <returns></returns>
    bool CheckSinglePrice()
    {
        int checkCount = 0;
        foreach (var tog in priceToggles)
        {
            if (tog.isOn) checkCount++;
        }

        return checkCount == 1;
    }
}
