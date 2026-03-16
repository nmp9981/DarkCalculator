using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowDetailResult : MonoBehaviour
{
    UserInfoClass clickUser;

    [Header("각 라운드")]
    [SerializeField] List<TextMeshProUGUI> roundResultList = new();
   
    /// <summary>
    /// 상세정보 표시 텍스트 초기화
    /// </summary>
    public void Init_RoundResultTest()
    {
        for(int idx = 0; idx < FruitGameManager.Instance.TotalRound; idx++)
        {
            roundResultList[idx].text = string.Empty;
        }
    }

    /// <summary>
    /// 결과를 볼 유저 세팅
    /// </summary>
    public void Select_ShowUser(int idx)
    {
        clickUser = FruitGameManager.Instance.UserInfoList[idx];
        ShowDetail_UserInfo(clickUser);
    }

    /// <summary>
    /// 클릭한 유저의 상세정보 공개
    /// </summary>
    private void ShowDetail_UserInfo(UserInfoClass clickUser)
    {
        for(int curRd = 0; curRd < FruitGameManager.Instance.TotalRound; curRd++)
        {
            roundResultList[curRd].text = string.Empty;

            for (int idx = 0; idx < 2; idx++)
            {
                //비밀
                if (clickUser.sellMoneyRound[curRd, idx].isSecret)
                {
                    roundResultList[curRd].text +=
                        $"\n\n{clickUser.sellMoneyRound[curRd, idx].name} 비공개";
                }
                else
                {
                    roundResultList[curRd].text +=
                        $"\n\n{clickUser.sellMoneyRound[curRd, idx].name} {clickUser.sellMoneyRound[curRd, idx].money}";
                }
            }
        }
    }

    /// <summary>
    /// 닫기
    /// </summary>
    public void CloseDetailReultUI()
    {
        this.gameObject.SetActive(false);
    }
}
