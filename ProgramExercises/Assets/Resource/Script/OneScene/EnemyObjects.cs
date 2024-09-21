using UnityEngine;
using System;

public class EnemyObjects : MonoBehaviour
{
    private Action _onDisable;  // 非アクティブ化するためのコールバック
    private float _elapsedTime;  // 初期化されてからの経過時間

    public void Initialize(Action onDisable)
    {
        _onDisable = onDisable;
        _elapsedTime = 0;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= 2)
        {
            _onDisable?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
