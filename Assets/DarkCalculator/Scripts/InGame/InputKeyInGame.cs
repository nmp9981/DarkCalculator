using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputKeyInGame : MonoBehaviour
{
    [SerializeField]
    GameObject clearObject;

    [SerializeField]
    GameObject KeyButtonSet;

    [SerializeField]
    TMP_InputField inputAnswerField;

    QuestionProblem questionProblem;
    
    void Awake()
    {
        BindingKeyButton();
        questionProblem = GameObject.Find("Question").GetComponent<QuestionProblem>();
    }

    void BindingKeyButton()
    {
        foreach(Button key in KeyButtonSet.GetComponentsInChildren<Button>(true))
        {
            key.onClick.AddListener(()=>InputNumber(key.gameObject.name));
        }
        
    }
    /// <summary>
    /// ±‚¥… : º˝¿⁄≈∞ ¿‘∑¬πˆ∆∞ µÓ∑œ
    /// </summary>
    /// <param name="name">πˆ∆∞ ø¿∫Í¡ß∆Æ ∏Ì¿∏∑Œ ±∏∫–</param>
    public void InputNumber(string name)
    {
        string keyNumber = name.Substring(3);
        
        switch (keyNumber)
        {
            case "Minus":
                GameManager.Instance.InputAnswerString += "-";
                break;
            case "X":
                GameManager.Instance.InputAnswerString = string.Empty;
                break;
            default:
                GameManager.Instance.InputAnswerString += keyNumber;
                break;
        }
        ShowInputAnswer();
        CompareAnswerAndInput();
    }
    /// <summary>
    /// ±‚¥… : ¿‘∑¬∞™¿Ã ∫∏¿Ã∞‘
    /// </summary>
    void ShowInputAnswer()
    {
        inputAnswerField.text = GameManager.Instance.InputAnswerString;
    }
    /// <summary>
    /// ¿‘∑¬∞™∞˙ ¡§¥‰ ∫Ò±≥
    /// </summary>
    void CompareAnswerAndInput()
    {
        //¡§¥‰
        if (GameManager.Instance.InputAnswerString == GameManager.Instance.RealAnswer.ToString())
        {
            GameManager.Instance.CurrentSolveCount += 1;//¡§¥‰ ∞≥ºˆ ¡ı∞°
            //∏µŒ ∏¬√„
            if (GameManager.Instance.CurrentProblemNum == GameManager.Instance.TargetSolveCount)
            {
                AllSolveProblem();
            }

            questionProblem.SetProblem();
            questionProblem.ShowProblemCount();
            //√ ±‚»≠
            InputInit();
        }
    }
    /// <summary>
    /// ±‚¥… : πÆ¡¶∏¶ ∏µŒ ê¨≠ü¿ª ∂ß ∑Œ¡˜
    /// 1) Ω√∞£ ±‚∑œ
    /// 2) ≈¨∏ÆæÓ UI »∞º∫»≠
    /// </summary>
    public void AllSolveProblem()
    {
        GameManager.Instance.RecordTime = Mathf.Floor(questionProblem.currentTime);
        clearObject.SetActive(true);
    }
    /// <summary>
    /// ±‚¥… : ¿‘∑¬ √ ±‚»≠
    /// </summary>
    public void InputInit()
    {
        inputAnswerField.text = string.Empty;
        GameManager.Instance.InputAnswerString = string.Empty;
    }
}
