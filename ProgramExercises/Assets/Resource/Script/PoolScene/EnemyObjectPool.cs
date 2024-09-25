using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] EnemyObject _enemyPrefab;  // �I�u�W�F�N�g�v�[���ŊǗ�����I�u�W�F�N�g
    private ObjectPool<EnemyObject> _enemyPool;  // �I�u�W�F�N�g�v�[���{��

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

    // �v�[������I�u�W�F�N�g���擾����
    public void GetEnemy()
    {
        _enemyPool.Get();
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

    // �v�[������폜�����ۂɍs������
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