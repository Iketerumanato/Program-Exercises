using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] EnemyObject _enemyPrefab;  // オブジェクトプールで管理するオブジェクト
    private ObjectPool<EnemyObject> _enemyPool;  // オブジェクトプール本体

    [SerializeField] Button CreateObjButton;
    [SerializeField] Button ClearObjButton;

    private void Start()
    {
        _enemyPool = new ObjectPool<EnemyObject>(
            createFunc: () => OnCreateObject(),
            actionOnGet: (obj) => OnGetObject(obj),
            actionOnDestroy: (obj) => OnDestroyObject(obj),
            collectionCheck: true,
            defaultCapacity: 3,
            maxSize: 10); ;

        CreateObjButton.onClick.AddListener(GetEnemy);
        ClearObjButton.onClick.AddListener(ClearEnemy);
    }

    // プールからオブジェクトを取得する
    public void GetEnemy()
    {
        _enemyPool.Get();
    }

    // プールの中身を空にする
    public void ClearEnemy()
    {
        _enemyPool.Clear();
    }

    // プールに入れるインスタンスを新しく生成する際に行う処理
    private EnemyObject OnCreateObject()
    {
        return Instantiate(_enemyPrefab, transform);
    }

    // プールからインスタンスを取得した際に行う処理
    private void OnGetObject(EnemyObject enemyObject)
    {
        enemyObject.transform.position = Random.insideUnitSphere * 5;
        enemyObject.Initialize(() => _enemyPool.Release(enemyObject));
        enemyObject.gameObject.SetActive(true);
    }

    // プールから削除される際に行う処理
    private void OnDestroyObject(EnemyObject enemyObject)
    {
        DestroyObj(ref enemyObject, 0f);
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
}