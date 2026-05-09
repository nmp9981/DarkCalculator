using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreResult : MonoBehaviour
{
    [SerializeField]
    Button okButton;

    [SerializeField]
    TextMeshProUGUI resultText;

    private void Awake()
    {
        BindingOKButton();
    }
    private void OnEnable()
    {
        CalScore();
        ShowResultRecord();
    }
    /// <summary>
    /// OK 버튼 바인딩
    /// </summary>
    void BindingOKButton()
    {
        okButton.onClick.AddListener(MoveToMainScene);
    }
    /// <summary>
    /// 기능 : 메인씬 이동
    /// </summary>
    void MoveToMainScene()
    {
        GameManager.Instance.InitInGameData();
        SceneManager.LoadScene("MainCalMode", LoadSceneMode.Single);
        SoundManager._sound.PlayBGM(0);
    }
    /// <summary>
    /// 기능 : 결과 기록 조회
    /// </summary>
    void ShowResultRecord()
    {
        if(GameManager.Instance.CurrentPlayMode == PlayMode.General)
        {
            resultText.text = $"경과 시간\n{GameManager.Instance.RecordTime} \n\n정답 개수\n{GameManager.Instance.CurrentSolveCount} \n\n" +
            $"점수\n{GameManager.Instance.Score}";
        }else if(GameManager.Instance.CurrentPlayMode == PlayMode.Challenge)
        {
            resultText.text = $"정답 개수\n{GameManager.Instance.CurrentSolveCount} \n\n" +
            $"등급\n{CalClass(GameManager.Instance.CurrentSolveCount)}";
        }
    }
    /// <summary>
    /// 등급 산출
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    string CalClass(int success)
    {
        if (success <= 5)
        {
            return "F";
        }else if (success>=6 && success<=9)
        {
            return "D";
        }
        else if (success >= 10 && success <= 12)
        {
            return "C";
        }
        else if (success >= 13 && success <= 16)
        {
            return "B";
        }
        else if (success >= 17 && success <= 19)
        {
            return "A";
        }
        else if (success >= 20 && success <= 22)
        {
            return "S";
        }
        return "SS";
    }

    /// <summary>
    /// 기능 :점수 계산
    /// </summary>
    void CalScore()
    {
        float answerCountScore = (float)GameManager.Instance.CurrentSolveCount/GameManager.Instance.TargetSolveCount;
        float perfectTimeLimit = (GameManager.Instance.TargetSolveCount * 5/2) + 3 + GameManager.Instance.Cal3DigitCount*3;

        float finalScore = (GameManager.Instance.RecordTime <= perfectTimeLimit) ? 100f* answerCountScore :
            Mathf.Max(0, 100 - (GameManager.Instance.RecordTime - perfectTimeLimit))* answerCountScore;
  
        GameManager.Instance.Score = (int) finalScore;
    }
}
