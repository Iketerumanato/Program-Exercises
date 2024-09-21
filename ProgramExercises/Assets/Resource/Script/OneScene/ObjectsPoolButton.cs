using UnityEngine;

public class ObjectsPoolButton : MonoBehaviour
{
    [System.Obsolete]
    public void Create()
    {
        EnemyObjectsPool.Instance.GetEnemy();
    }

    [System.Obsolete]
    public void Clear()
    {
        EnemyObjectsPool.Instance.ClearEnemy();
    }
}