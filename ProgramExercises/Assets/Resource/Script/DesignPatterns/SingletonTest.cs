using UnityEngine;

public class Singleton
{
    private static Singleton instance;

    public static Singleton Instance
    {
        get
        {
            instance ??= new Singleton();
            return instance;
        }
    }

    public void WriteTest()
    {
        Debug.Log("SingletonPattern使用中");
    }
    public void WriteNotes()
    {
        Debug.LogWarning("使い過ぎには注意!");
    }
}

public class SingletonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Singleton.Instance.WriteTest();
        Singleton.Instance.WriteNotes();
    }
}