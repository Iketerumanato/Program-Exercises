using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] Rigidbody PlayerRig;
    [SerializeField] float moveSpeed = 0.5f;

    [SerializeField] GameObject PlayerPrefab;
    Vector3 SpawnPos;
    GameObject spawnedMyself;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        PlayerRig.velocity = new Vector2(moveHorizontal * moveSpeed, PlayerRig.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space)) DestroyObj(ref spawnedMyself);
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
        spawnedMyself = Instantiate(PlayerPrefab, SpawnPos, Quaternion.identity);
    }
}
