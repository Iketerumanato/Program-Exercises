using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectsPool : MonoBehaviour
{  
    [SerializeField] private EnemyObjects _enemyPrefab;  // �I�u�W�F�N�g�v�[���ŊǗ�����I�u�W�F�N�g
    private ObjectPool<EnemyObjects> _enemyPool;  // �I�u�W�F�N�g�v�[���{��

    // �A�N�Z�X���₷���悤�ɃV���O���g����
    private static EnemyObjectsPool _instance;

    [System.Obsolete]
    public static EnemyObjectsPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemyObjectsPool>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        _enemyPool = new ObjectPool<EnemyObjects>(
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
    public EnemyObjects GetEnemy()
    {
        return _enemyPool.Get();
    }

    // �v�[���̒��g����ɂ���
    public void ClearEnemy()
    {
        _enemyPool.Clear();
    }

    // �v�[���ɓ����C���X�^���X��V������������ۂɍs������
    private EnemyObjects OnCreateObject()
    {
        return Instantiate(_enemyPrefab, transform);
    }

    // �v�[������C���X�^���X���擾�����ۂɍs������
    private void OnGetObject(EnemyObjects enemyObject)
    {
        enemyObject.transform.position = Random.insideUnitSphere * 5;
        enemyObject.Initialize(() => _enemyPool.Release(enemyObject));
        enemyObject.gameObject.SetActive(true);
    }

    // �v�[���ɃC���X�^���X��ԋp�����ۂɍs������
    private void OnReleaseObject(EnemyObjects enemyObject)
    {
        Debug.Log("Release");  // EnemyObject���Ŕ�A�N�e�B�u�ɂ���̂Ń��O�o�͂̂݁B�����Ŕ�A�N�e�B�u�ɂ���p�^�[��������B
    }

    // �v�[������폜�����ۂɍs������
    private void OnDestroyObject(EnemyObjects enemyObject)
    {
        Destroy(enemyObject.gameObject);
    }
}