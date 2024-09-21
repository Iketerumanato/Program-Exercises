using UnityEngine;

public class ObjectPoolButton : MonoBehaviour
{
    public void Create()
    {
        EnemyObjectPool.Instance.GetEnemy();
    }

    public void Clear()
    {
        EnemyObjectPool.Instance.ClearEnemy();
    }
}