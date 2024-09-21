using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectPool : MonoBehaviour
{  
    [SerializeField] private EnemyObject _enemyPrefab;  // オブジェクトプールで管理するオブジェクト
    private ObjectPool<EnemyObject> _enemyPool;  // オブジェクトプール本体

    // アクセスしやすいようにシングルトン化
    private static EnemyObjectPool _instance;

    [System.Obsolete]
    public static EnemyObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemyObjectPool>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        _enemyPool = new ObjectPool<EnemyObject>(
            createFunc: () => OnCreateObject(),
            actionOnGet: (obj) => OnGetObject(obj),
            actionOnRelease: (obj) => OnReleaseObject(obj),
            actionOnDestroy: (obj) => OnDestroyObject(obj),
            collectionCheck: true,
            defaultCapacity: 3,
            maxSize: 10
        );
    }

    // プールからオブジェクトを取得する
    public EnemyObject GetEnemy()
    {
        return _enemyPool.Get();
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

    // プールにインスタンスを返却した際に行う処理
    private void OnReleaseObject(EnemyObject enemyObject)
    {
        Debug.Log("Release");  // EnemyObject側で非アクティブにするのでログ出力のみ。ここで非アクティブにするパターンもある。
    }

    // プールから削除される際に行う処理
    private void OnDestroyObject(EnemyObject enemyObject)
    {
        Destroy(enemyObject.gameObject);
    }
}