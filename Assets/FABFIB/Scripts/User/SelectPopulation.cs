using TMPro;
using UnityEngine;

namespace FABFIB
{
    public class SelectPopulation : MonoBehaviour
    {
        [SerializeField]
        TMP_InputField inputPopular;
        [SerializeField]
        TextMeshProUGUI popularMessage;

        private void Awake()
        {
            popularMessage.text = string.Empty;
        }

        /// <summary>
        /// 인구 입력
        /// </summary>
        public void InputPopulation()
        {
            GameManager.Instance.UserCount = int.Parse(inputPopular.text.ToString());
        }
        /// <summary>
        /// 인원 수 검사
        /// 5~20명만 받음
        /// </summary>
        /// <returns></returns>
        public bool InspectPopulation()
        {
            if (GameManager.Instance.UserCount < GameManager.Instance.MinUserCount
                || GameManager.Instance.UserCount > GameManager.Instance.MaxUserCount)
            {
                popularMessage.text = $"3~10만 입력 가능합니다.";
                Invoke("OffMessagePopular", 0.5f);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 메세지 off
        /// </summary>
        private void OffMessagePopular()
        {
            popularMessage.text = string.Empty;
        }
    }
}
