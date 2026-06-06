using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FABFIB
{ 
    public class UIManager : MonoBehaviour
    {
        //UI페이지 모음
        [SerializeField] public List<GameObject> uiList = new();

        //UI 상세 페이지
        [SerializeField] SelectPopulation selectPopulation;
        [SerializeField] EnrollPlayer enrollPlayer;

        //유저 정보 스폰 위치
        [SerializeField] Transform spawnUserTransform;
        [SerializeField] GameObject playerInfoPrefab;

        //메세지
        [SerializeField] TextMeshProUGUI notifyText;//알림

        //승리 UI
        [SerializeField] GameObject _victoryObj;

        /// <summary>
        /// 인트로 창으로 이동
        /// </summary>
        public void GotoIntroPage()
        {
            uiList[0].gameObject.SetActive(true);
            uiList[1].gameObject.SetActive(false);
            uiList[2].gameObject.SetActive(false);

            _victoryObj.SetActive(false);
            uiList[3].gameObject.SetActive(false);
        }

        /// <summary>
        /// 인구 수 결정 페이지로 이동
        /// </summary>
        public void GotoSetPopulationPage()
        {
            uiList[1].gameObject.SetActive(true);
            uiList[0].gameObject.SetActive(false);
            uiList[2].gameObject.SetActive(false);
        }

        /// <summary>
        /// 유저 등록 페이지로 이동
        /// </summary>
        public void GotoEnrollPlayerPage()
        {
            if (selectPopulation.InspectPopulation())
            {
                uiList[2].gameObject.SetActive(true);
                uiList[1].gameObject.SetActive(false);
                uiList[3].gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 인게임 페이지로 이동
        /// </summary>
        public void GotoInGamePage()
        {
            if (enrollPlayer.CheckEnroll_AllUserNick())
            {
                EnrollUser();
                uiList[3].gameObject.SetActive(true);
                uiList[2].gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// 유저 등록
        /// </summary>
        void EnrollUser()
        {
            GameManager gm = GameManager.Instance;

            //턴 제한 모드
            if(gm.FABFIBGameMode == FABFIBMode.Turn)
            {
                gm.CurTurnCount = 1;
            }

            int userCount = gm.UserCount;
            gm.StartUserIndex = Random.Range(0, userCount);
            for (int idx = 0; idx < userCount; idx++)
            {
                //오브젝트 생성
                GameObject playerObj = Instantiate(playerInfoPrefab);
                playerObj.transform.parent = spawnUserTransform;

                //플레이어 정보 등록
                string playerName = gm.playerNameList[idx];
                PlayerInfo playerInfo = playerObj.GetComponent<PlayerInfo>();

                bool isStartUser = (idx==gm.StartUserIndex)?true:false;
                playerInfo.EnrollPlayerInfo(playerName,idx, isStartUser);
                gm.playerList.Add(playerInfo);
                gm.CurrentUserIndex = gm.StartUserIndex;
            }
        }

        /// <summary>
        /// 메세지 보이기
        /// </summary>
        public void ShowMessage(string msg)
        {
            notifyText.text = msg;
            Invoke("CloseMessage",1f);
        }
        /// <summary>
        /// 메세지 끄기
        /// </summary>
        void CloseMessage()
        {
            notifyText.text = string.Empty;
        }
    }
}