using UnityEngine;

public class IntroUI : MonoBehaviour
{
    [SerializeField] GameObject howToPlayObject;
    [SerializeField] GameObject settingObject;

    /// <summary>
    /// 게임 종료
    /// </summary>
    public void GameExit()
    {
#if UNITY_EDITOR //에디터에서
        UnityEditor.EditorApplication.isPlaying = false;
#else //나머지
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    /// <summary>
    /// 게임 방법 소개
    /// </summary>
    public void ShowHowToPlay()
    {
        howToPlayObject.SetActive(true);
    }
    /// <summary>
    /// 게임 방법 소개 닫기
    /// </summary>
    public void CloseHowToPlay()
    {
        howToPlayObject.SetActive(false);
    }
    /// <summary>
    /// 모드 세팅
    /// </summary>
    public void ShowGameSetting()
    {
        settingObject.SetActive(true);
    }
    /// <summary>
    /// 모드 세팅 닫기
    /// </summary>
    public void CloseGameSetting()
    {
        settingObject.SetActive(true);
    }
}
