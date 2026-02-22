using TMPro;
using UnityEngine;

public class SettingPopulation : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputPopular;
    public void InputPopulation()
    {
        FruitGameManager.Instance.PeopleCount = int.Parse(inputPopular.text.ToString());
    }
}
