using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SellResultClass : MonoBehaviour
{
    // 각 과일마다 누가 얼마에 팔았는지
    Dictionary<string, Stack<(string name,int money)>> allUsersellDic = new Dictionary<string, Stack<(string, int)>>();
    //각 과일별 적어낸 인원 수
    Dictionary<string, int> sellEachFruitCountDic = new Dictionary<string, int>();
    //각 과일별 최종 낙찰 가격
    Dictionary<string,int> resultSellDic = new Dictionary<string, int>();
    //각 과일별 비공개 여부
    Dictionary<string, bool> isNotPublicDic = new Dictionary<string, bool>();

    //라운드 텍스트
    [SerializeField] TextMeshProUGUI roundText;
    //페이지 오브젝트
    [SerializeField] GameObject[] pageObject = new GameObject[4];
    //최종 결과 페이지
    [SerializeField] GameObject finalResultUI;
    int currentPageIdx;//현재 페이지

    //과일 세팅 UI
    [SerializeField] FruitSettingUserUI fruitSettingUserUI;
    //닫기 버튼 따로 관리
    [SerializeField] Button closeResulUIButton;


    private void OnEnable()
    {
        //최종 결과 오브젝트 비활성화
        finalResultUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// 모든 유저 확정했는가?
    /// </summary>
    public bool AllUserConfirm()
    {
        int notActiveCount = 0;
        int allButtonCount = FruitGameManager.Instance.PeopleCount * 2;
        foreach (var user in FruitGameManager.Instance.UserInfoList)
        {
            if (!user.fruit1SellButton.interactable) notActiveCount++;
            if (!user.fruit2SellButton.interactable) notActiveCount++;
        }

        //모든 유저가 확정했을 경우 판매 결과 계산
        if (notActiveCount == allButtonCount) return true;
        return false;
    }
    /// <summary>
    /// 판매 결과 플로우
    /// </summary>
    public void SellResultFlow()
    {
        CalSellResult();//판매 결과 계산
        ResultSell();//판매 결과
        ShowResultUI();//UI로 공개
    }

    /// <summary>
    /// 판매 결과 계산
    /// </summary>
    private void CalSellResult()
    {
        //각 유저별로 검사
        foreach (var user in FruitGameManager.Instance.UserInfoList)
        {
            for(int idx = 0; idx < 2; idx++)
            {
                //이 과일을
                string sellFruitName = user.sellMoneyRound[FruitGameManager.Instance.CurrentRound-1, idx].name;
                //누가
                string sellUserName = user.userName;
                //얼마에 팔았는지
                int sellPrice = user.sellMoneyRound[FruitGameManager.Instance.CurrentRound-1, idx].money;
               
                //new Stack 추가
                if (!allUsersellDic.ContainsKey(sellFruitName))
                {
                    allUsersellDic.Add(sellFruitName, new Stack<(string, int)>());
                }
                //인원 정보 Key 추가
                if (!sellEachFruitCountDic.ContainsKey(sellFruitName))
                {
                    sellEachFruitCountDic.Add(sellFruitName, 0);
                }
                sellEachFruitCountDic[sellFruitName] += 1;
                //비공개 여부 정보 추가
                if (!isNotPublicDic.ContainsKey(sellFruitName))
                {
                    isNotPublicDic.Add(sellFruitName, false);
                }
                //비공개 과일 표시
                if (user.isSecret && user.secretFruitName == sellFruitName)
                {
                    isNotPublicDic[sellFruitName] = true;
                }

                //정보 추가
                if (allUsersellDic[sellFruitName].Count == 0)//빈 스택
                {
                    allUsersellDic[sellFruitName].Push((sellUserName, sellPrice));
                }
                else
                {
                    //더 낮은 가격
                    if (allUsersellDic[sellFruitName].Peek().money > sellPrice)
                    {
                        //스택을 비운다.
                        allUsersellDic[sellFruitName].Clear();
                        allUsersellDic[sellFruitName].Push((sellUserName, sellPrice));
                    }else if(allUsersellDic[sellFruitName].Peek().money == sellPrice)
                    {
                        //같은 가격일때도 푸시 진행
                        allUsersellDic[sellFruitName].Push((sellUserName, sellPrice));
                    }
                }
            }
        }
    }

    /// <summary>
    /// 판매 결과
    /// </summary>
    private void ResultSell()
    {
        foreach (var finalFruitName in allUsersellDic.Keys)
        {
            int finalEachPrice = allUsersellDic[finalFruitName].Peek().money;//다 동일한 가격
            int originSellCount = sellEachFruitCountDic[finalFruitName];//총 판매 인원
            int finalFruitSellCount = allUsersellDic[finalFruitName].Count;//최종 판매 인원
            int finalPrice = finalEachPrice * originSellCount;//총 금액
            
            //결과 종류 추가
            if (!resultSellDic.ContainsKey(finalFruitName))
            {
                resultSellDic.Add(finalFruitName,0);
            }
 
            //최종 가격 기록
            resultSellDic[finalFruitName] = finalEachPrice;

            //스택을 비우면서 최종 판매자 기록
            while (allUsersellDic[finalFruitName].Count != 0)
            {
                foreach (var user in FruitGameManager.Instance.UserInfoList)
                {
                    //승리 유저
                    if (user.userName == allUsersellDic[finalFruitName].Peek().name)
                    {
                        user.getMoney[FruitGameManager.Instance.CurrentRound - 1] += (finalPrice / finalFruitSellCount);
                    }
                }
                allUsersellDic[finalFruitName].Pop();
            }
        }
    } 

    /// <summary>
    /// 결과 공개
    /// 각 과일별로 얼마에 낙찰되었는지 공개
    /// </summary>
    public void ShowResultUI()
    {
        //전부 끈 상태에서 시작
        for(int objIdx = 0; objIdx < FruitGameManager.Instance.TotalRound; objIdx++)
        {
            pageObject[objIdx].SetActive(false);
        }

        //현재 라운드 오브젝트 켜기
        GameObject curPageObject = pageObject[FruitGameManager.Instance.CurrentRound - 1];
        curPageObject.SetActive(true);

        //라운드 텍스트
        roundText.text = $"{FruitGameManager.Instance.CurrentRound} Round";

        //각 과일별 결과 띄우기
        int idx = 0;
        foreach(var result in resultSellDic)
        {
            Transform curFruit = curPageObject.transform.GetChild(idx);
            string frutName = result.Key;
            int price = result.Value;
           
            Sprite sp = FruitGameManager.Instance.fruitImageDic[frutName]; 
            curFruit.transform.GetChild(0).GetComponent<Image>().sprite = FruitGameManager.Instance.fruitImageDic[frutName];
            if (isNotPublicDic[frutName])
            {
                curFruit.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"비공개";
            }
            else curFruit.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{price}원";
            idx++;
        }
        //나머지 비활성화
        for (int i = idx; i < FruitGameManager.Instance.maxFruitTypeCount; i++) {
            curPageObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 다음 라운드로
    /// </summary>
    public void GoNextRound()
    {
        //최종 라운드인 경우 각 유저별 최종 정산 금액 공개
        if(FruitGameManager.Instance.CurrentRound == FruitGameManager.Instance.TotalRound)
        {
            ShowFinalResult();
        }
        else
        {
            //닫기 버튼 비활성화
            closeResulUIButton.gameObject.SetActive(false);

            //결과 페이지 열기
            GameObject curPageObject = pageObject[FruitGameManager.Instance.CurrentRound - 1];
            curPageObject.SetActive(false);
            FruitGameManager.Instance.CurrentRound += 1;

            //다음 라운드 텍스트 표시
            fruitSettingUserUI.ShowRoundText();
            //초기화
            InitSelectButton();
            //결과 UI페이지 닫기
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 현재 라운드 결과 보기
    /// </summary>
    public void ShowCurrentRoundResult()
    {
        if (FruitGameManager.Instance.CurrentRound == 1) return;

        this.gameObject.SetActive(true);
        closeResulUIButton.gameObject.SetActive(true);//닫기 버튼 활성화

        //페이지 열기
        currentPageIdx = FruitGameManager.Instance.CurrentRound - 2;
        GameObject curPageObject = pageObject[currentPageIdx];
        curPageObject.SetActive(true);

        //라운드 텍스트
        roundText.text = $"{currentPageIdx+1} Round";
    }
    /// <summary>
    /// 이전 라운드 결과 보기
    /// </summary>
    public void ShowPreviousRoundResult()
    {
        //페이지가 음수일 수 없음
        if (currentPageIdx == 0) return;

        pageObject[currentPageIdx].SetActive(false);
        currentPageIdx -= 1;
        pageObject[currentPageIdx].SetActive(true);

        //라운드 텍스트
        roundText.text = $"{currentPageIdx+1} Round";
    }
    /// <summary>
    /// 다음 라운드 결과 보기
    /// </summary>
    public void ShowNextRoundResult()
    {
        //현재 진행중인 라운드부터는 볼 수 없음
        if (currentPageIdx+2 == FruitGameManager.Instance.CurrentRound) return;

        pageObject[currentPageIdx].SetActive(false);
        currentPageIdx += 1;
        pageObject[currentPageIdx].SetActive(true);

        //라운드 텍스트
        roundText.text = $"{currentPageIdx+1} Round";
    }

    /// <summary>
    /// 최종 결과 공개
    /// </summary>
    public void ShowFinalResult()
    {
        finalResultUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// 결과 UI창 닫기
    /// </summary>
    public void CloseResultUI()
    {
        finalResultUI.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 선택 버튼 초기화
    /// </summary>
    public void InitSelectButton()
    {
        //버튼 활성화
        foreach (var user in FruitGameManager.Instance.UserInfoList)
        {
            user.fruit1SellButton.interactable = true;
            user.fruit2SellButton.interactable = true;
        }

        //각 유저들이 선택한 과일 가격 초기화
        allUsersellDic.Clear();
        sellEachFruitCountDic.Clear();
        resultSellDic.Clear();
        isNotPublicDic.Clear();
    }
}
