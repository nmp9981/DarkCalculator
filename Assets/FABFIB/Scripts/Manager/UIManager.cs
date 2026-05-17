using System.Collections.Generic;
using UnityEngine;

namespace FABFIB
{ 
    public class UIManager : MonoBehaviour
    {
        //UI페이지 모음
        [SerializeField] List<GameObject> uiList = new();

        /// <summary>
        /// UI 오브젝트 비활성화
        /// </summary>
        void Off_UIList()
        {
            foreach (GameObject go in uiList)
            {
                go.SetActive(false);    
            }
        }

        /// <summary>
        /// 인트로 페이지로 이동
        /// </summary>
        public void GotoIntroPage()
        {
            Off_UIList();
            uiList[0].SetActive(true);
        }

        /// <summary>
        /// 인원 등록 페이지로 이동
        /// </summary>
        public void GotoInputPeoplePage()
        {
            Off_UIList();
            uiList[1].SetActive(true);
        }

        /// <summary>
        /// 유저 등록 페이지로 넘어감
        /// </summary>
        public void GotoEnrollPlayer()
        {
            Off_UIList();
            uiList[2].SetActive(true);
        }

        /// <summary>
        /// 인게임 페이지로 이동
        /// </summary>
        public void GotoInGamePage()
        {
            Off_UIList();
            uiList[2].SetActive(true);
        }
    }
}