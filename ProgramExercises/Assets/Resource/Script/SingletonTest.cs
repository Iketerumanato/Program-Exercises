using UnityEngine;

public class Singleton
{
    private static Singleton instance;

    public static Singleton Instance
    {
        get
        {
            if (instance == null) instance = new Singleton();
            return instance;
        }
    }

    public void WriteTest()
    {
        Debug.Log("Use SingletonPattern");
    }
}

public class SingletonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Singleton.Instance.WriteTest();
    }
}