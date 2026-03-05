using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FruitUIManager : MonoBehaviour
{
    [Header("UI")]
    public List<GameObject> fruitUIList = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI roundText;

    [Header("Class")]
    public SettingPopulation settingPopulation;
    public EnrollUser enrollUser;
    public SellResultClass sellResultClass;
    
    private void Awake()
    {
        SetButtonBinding();
        fruitUIList[3].GetComponent<FruitSettingEachUser>().UserObjectFulling();
    }

    void SetButtonBinding()
    {
        foreach (var btn in this.gameObject.GetComponentsInChildren<Button>(true))
        {
            string btnName = btn.name;
            switch (btnName)
            {
                case "GameStartButton":
                    btn.onClick.AddListener(GoToGameStart);
                    break;
                case "EnrollToUser":
                    btn.onClick.AddListener(SetToUserCount);
                    break;
                case "ViewToUserListButton":
                    btn.onClick.AddListener(ViewToUserList);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 게임 시작
    /// </summary>
    public void GoToGameStart()
    {
        fruitUIList[1].SetActive(true);
        fruitUIList[0].SetActive(false);
    }
    public void SetToUserCount()
    {
        if (settingPopulation.InspectPopulation())
        {
            fruitUIList[2].SetActive(true);
            fruitUIList[1].SetActive(false);
        }
    }
    /// <summary>
    /// Main 으로 돌아가기
    /// </summary>
    public void ReturnToMain()
    {
        fruitUIList[0].SetActive(true);
        fruitUIList[1].SetActive(false);
    }
    /// <summary>
    /// 유저 수 입력으로 돌아가기
    /// </summary>
    public void ReturnToUserCount()
    {
        fruitUIList[1].SetActive(true);
        fruitUIList[2].SetActive(false);
    }

    public void ViewToUserList()
    {
        //인원 검사
        if (enrollUser.CheckEnroll_AllUserNick()){
            fruitUIList[3].SetActive(true);
            fruitUIList[2].SetActive(false);
        }
        else
        {
            enrollUser.ShowNotFullMassage();
        }
    }
    public void ViewToSellResult()
    {
        if (sellResultClass.AllUserConfirm()){
            fruitUIList[4].SetActive(true);
            sellResultClass.SellResultFlow();
        }
    }
    /// <summary>
    /// 메인으로 돌아가기
    /// </summary>
    public void ViewToMain()
    {
        fruitUIList[4].SetActive(false);
        fruitUIList[3].SetActive(false);
        fruitUIList[0].SetActive(true);
    }
}
