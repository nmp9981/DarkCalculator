using System.Collections.Generic;
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

        /// <summary>
        /// 인트로 창으로 이동
        /// </summary>
        public void GotoIntroPage()
        {
            uiList[0].gameObject.SetActive(true);
            uiList[1].gameObject.SetActive(false);
            uiList[2].gameObject.SetActive(false);
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
                uiList[3].gameObject.SetActive(true);
                uiList[2].gameObject.SetActive(false);
            }
        }
    }
}