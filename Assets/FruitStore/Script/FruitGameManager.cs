using UnityEngine;

public class FruitGameManager : MonoBehaviour
{ 
    static FruitGameManager _instance;
    public static FruitGameManager Instance { get { Init(); return _instance; } }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject gm = GameObject.Find("FruitGameManager");
            if (gm == null)
            {
                gm = new GameObject { name = "FruitGameManager" };

                gm.AddComponent<FruitGameManager>();
            }
            DontDestroyOnLoad(gm);
            _instance = gm.GetComponent<FruitGameManager>();
        }
    }

    #region µ•¿Ã≈Õ
    private int peopleCount;

    public int maxPeopleCount = 20;
    public int PeopleCount { get { return peopleCount; } set { peopleCount = value; } }
    #endregion
}
