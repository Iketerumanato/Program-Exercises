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
        Debug.Log("SingletonPatternégópíÜ");
    }
    public void WriteNotes()
    {
        Debug.LogWarning("égÇ¢âﬂÇ¨Ç…ÇÕíçà”!");
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