using UnityEngine;

public class ObjectPoolButton : MonoBehaviour
{
    [SerializeField] EnemyObjectPool _enemyObjectPool;

    public void Create()
    {
        _enemyObjectPool.GetEnemy();
    }

    public void Clear()
    {
        _enemyObjectPool.ClearEnemy();
    }
}