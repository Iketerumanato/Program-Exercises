using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectPool : MonoBehaviour
{  
    [SerializeField] private EnemyObject _enemyPrefab;  // �I�u�W�F�N�g�v�[���ŊǗ�����I�u�W�F�N�g
    private ObjectPool<EnemyObject> _enemyPool;  // �I�u�W�F�N�g�v�[���{��

    // �A�N�Z�X���₷���悤�ɃV���O���g����
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

    // �v�[������I�u�W�F�N�g���擾����
    public EnemyObject GetEnemy()
    {
        return _enemyPool.Get();
    }

    // �v�[���̒��g����ɂ���
    public void ClearEnemy()
    {
        _enemyPool.Clear();
    }

    // �v�[���ɓ����C���X�^���X��V������������ۂɍs������
    private EnemyObject OnCreateObject()
    {
        return Instantiate(_enemyPrefab, transform);
    }

    // �v�[������C���X�^���X���擾�����ۂɍs������
    private void OnGetObject(EnemyObject enemyObject)
    {
        enemyObject.transform.position = Random.insideUnitSphere * 5;
        enemyObject.Initialize(() => _enemyPool.Release(enemyObject));
        enemyObject.gameObject.SetActive(true);
    }

    // �v�[���ɃC���X�^���X��ԋp�����ۂɍs������
    private void OnReleaseObject(EnemyObject enemyObject)
    {
        Debug.Log("Release");  // EnemyObject���Ŕ�A�N�e�B�u�ɂ���̂Ń��O�o�͂̂݁B�����Ŕ�A�N�e�B�u�ɂ���p�^�[��������B
    }

    // �v�[������폜�����ۂɍs������
    private void OnDestroyObject(EnemyObject enemyObject)
    {
        Destroy(enemyObject.gameObject);
    }
}