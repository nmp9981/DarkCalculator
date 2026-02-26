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
    public EnrollUser enrollUser;
    
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

    public void SetToUserCount()
    {
        fruitUIList[2].SetActive(true);
        fruitUIList[1].SetActive(false);
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
}
