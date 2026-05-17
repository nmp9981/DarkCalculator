using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace FABFIB
{
    public class EnrollPlayer : MonoBehaviour
    {
        [SerializeField]
        TMP_InputField[] enrollUserArray = new TMP_InputField[10];
        [SerializeField]
        TextMeshProUGUI notifyText;//¾Ë¸²

        private void Awake()
        {
            notifyText.text = string.Empty;

            int idx = 0;
            foreach (var tmp in this.gameObject.GetComponentsInChildren<TMP_InputField>(true))
            {
                enrollUserArray[idx] = tmp;
                idx++;
            }
        }

        private void OnEnable()
        {
            notifyText.text = string.Empty;
            notifyText.gameObject.SetActive(false);
            Debug.Log(GameManager.Instance.UserCount);
            for (int i = 0; i < GameManager.Instance.MaxUserCount; i++)
            {
                if (i >= GameManager.Instance.UserCount) enrollUserArray[i].gameObject.SetActive(false);
                else enrollUserArray[i].gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// ¸ðµç À¯ÀúµéÀÇ ÀÌ¸§ÀÌ ±â·ÏµÇ¾ú´ÂÁö °Ë»ç
        /// </summary>
        /// <returns></returns>
        public bool CheckEnroll_AllUserNick()
        {
            int curCount = 0;
            for (int i = 0; i < GameManager.Instance.UserCount; i++)
            {
                string name = enrollUserArray[i].text;
                //ÀÌ¸§ À¯È¿¼º °Ë»ç
                if (CheckEffectiveness_UserName(name)) curCount++;
            }

            //ÀüºÎ À¯È¿
            GameManager.Instance.playerNameList.Clear();
            if (curCount == GameManager.Instance.UserCount)
            {
                for (int i = 0; i < curCount; i++)
                {
                    string name = enrollUserArray[i].text;
                    GameManager.Instance.playerNameList.Add(name);
                }
                //Áßº¹ ÀÌ¸§ °Ë»ç
                if (IsRepetitionUserName(GameManager.Instance.playerNameList))
                {
                    ShowRepetitionMassage();
                    return false;//Áßº¹ÀÌ ¹ß»ýÇßÀ¸¹Ç·Î À¯È¿ÇÏÁö ¾ÊÀ½
                }
                return true;
            }

            return false;//À¯È¿ÇÏÁö ¾ÊÀº ÀÌ¸§µéÀÌ ÀÖÀ½
        }

        /// <summary>
        /// À¯Àú ³×ÀÓ À¯È¿¼º °Ë»ç
        /// </summary>
        /// <returns></returns>
        public bool CheckEffectiveness_UserName(string name)
        {
            //Null °Ë»ç
            if (name == null) return false;

            //±ÛÀÚ ¼ö °Ë»ç
            if (name.Length >= 7 || name.Length <= 1)
            {
                ShowNotLengthMassage();
                return false;
            }

            //Æ¯¼ö ¹®ÀÚ °Ë»ç
            if (Regex.IsMatch(name, @"[^a-zA-Z0-9°¡-ÆR¤¡-¤¾¤¿-¤Ó\s]"))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// À¯ÀúÀÌ¸§ Áßº¹ °Ë»ç
        /// </summary>
        /// <param name="nameList"></param>
        /// <returns></returns>
        private bool IsRepetitionUserName(List<string> nameList)
        {
            List<string> sortingList = nameList;
            sortingList.Sort();//Á¤·Ä
            for (int i = 0; i < sortingList.Count - 1; i++)
            {
                //ÀÌ¸§ Áßº¹ ¹ß»ý
                if (sortingList[i] == sortingList[i + 1]) return true;
            }

            return false;//Áßº¹X
        }

        public void ShowNotFullMassage()
        {
            notifyText.gameObject.SetActive(true);
            notifyText.text = $"¸ðµç À¯ÀúÀÇ ´Ð³×ÀÓÀ»\n ÀÔ·ÂÇØ¾ß ÇÕ´Ï´Ù.\nÀÌ¸§¿¡ Æ¯¼ö¹®ÀÚ°¡ ¼¯ÀÌÁö´Â\n¾Ê¾Ò´ÂÁö È®ÀÎÇØ ÁÖ¼¼¿ä.";
            Invoke("Off_ShowNotFullMassage", 0.5f);
        }
        public void ShowNotLengthMassage()
        {
            notifyText.gameObject.SetActive(true);
            notifyText.text = $"´Ð³×ÀÓÀÇ ±ÛÀÚ ¼ö´Â\n 2~6ÀÚ¸¸ °¡´ÉÇÕ´Ï´Ù.";
            Invoke("Off_ShowNotFullMassage", 0.5f);
        }
        public void ShowRepetitionMassage()
        {
            notifyText.gameObject.SetActive(true);
            notifyText.text = $"Áßº¹µÈ ´Ð³×ÀÓÀÌ ÀÖ½À´Ï´Ù";
            Invoke("Off_ShowNotFullMassage", 0.5f);
        }
        void Off_ShowNotFullMassage()
        {
            notifyText.text = string.Empty;
            notifyText.gameObject.SetActive(false);
        }
    }
    }
