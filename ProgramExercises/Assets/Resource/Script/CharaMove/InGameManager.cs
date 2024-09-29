using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    Vector3 SpawnPos;
    GameObject spawnedPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) DestroyObj(ref spawnedPrefab);
        if (Input.GetKeyDown(KeyCode.R)) SpawnPlayer();
    }

    public void DestroyObj<T>(ref T obj, float time = 0) where T : Object
    {
        if (obj != null)
        {
#if UNITY_EDITOR
            if (Application.isPlaying) Object.Destroy(obj, time);
            else Object.DestroyImmediate(obj);
#else
            Object.Destroy(obj, time);
#endif
            obj = null;
        }
    }

    void SpawnPlayer()
    {
        spawnedPrefab = Instantiate(PlayerPrefab, SpawnPos, Quaternion.identity);
    }
}